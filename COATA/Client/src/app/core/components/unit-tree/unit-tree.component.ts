
import {SelectionModel, CollectionViewer, SelectionChange} from '@angular/cdk/collections';
import {FlatTreeControl} from '@angular/cdk/tree';
import {Component, Injectable, OnInit} from '@angular/core';
import {MatTreeFlatDataSource, MatTreeFlattener} from '@angular/material/tree';
import { DynamicDataSource } from './data-source';
import { FlatNode, ItemNode } from './node.model';
import { UnitType } from '../../models/unit-type.model';
import { Observable, merge } from 'rxjs';
import { map } from 'rxjs/operators';
import { UnitApiService } from '../../api/unit/unit-api.service';
import { ClassificationApiService } from '../../api/unit/classification-api.service';
import { ClassificationCreateModel } from '../../models/classification.model';

@Component({
  selector: 'app-unit-tree',
  templateUrl: './unit-tree.component.html',
  styleUrls: ['./unit-tree.component.css'],
  providers: [DynamicDataSource]
})
export class UnitTreeComponent{

  search:string;

  flatNodeMap = new Map<FlatNode, ItemNode>();

  nestedNodeMap = new Map<ItemNode, FlatNode>();

  treeControl: FlatTreeControl<FlatNode>;

  treeFlattener: MatTreeFlattener<ItemNode, FlatNode>;

  dataSource: MatTreeFlatDataSource<ItemNode, FlatNode>;

  constructor(private _database: DynamicDataSource) {
    this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,
      this.isExpandable, this.getChildren);
    this.treeControl = new FlatTreeControl<FlatNode>(this.getLevel, this.isExpandable);
    this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
    this._database.dataChange.subscribe(data => {
      this.dataSource.data = data;
    });
  }

  getLevel = (node: FlatNode) => node.level;

  isExpandable = (node: FlatNode) => node.expandable;

  getChildren = (node: ItemNode): ItemNode[] => node.children;

  hasChild = (_: number, _nodeData: FlatNode) => _nodeData.expandable;

  hasNoContent = (_: number, _nodeData: FlatNode) => _nodeData.name === '';

  isUnitTemplate = (_: number, _nodeData: FlatNode) => _nodeData.isTemplate && !_nodeData.isClassification;

  isClassificationTemplate = (_: number, _nodeData: FlatNode) => _nodeData.isTemplate && _nodeData.isClassification;

  editAvailable = (node: FlatNode): boolean => !node.isClassification;

  transformer = (node: ItemNode, level: number) => {
    const existingNode = this.nestedNodeMap.get(node);
    const flatNode = existingNode && existingNode.name === node.name && existingNode.id === existingNode.id
        ? existingNode
        : new FlatNode();
    Object.assign(flatNode, node);
    flatNode.isEditable = node.isEditable;
    flatNode.isTemplate = node.isTemplate || false;
    flatNode.level = level;
    flatNode.isClassification = this.isClassification(node);
    flatNode.expandable = true;
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node, flatNode);
    return flatNode;
  }

  changeEdit(node: FlatNode)
  {
     node.isEditable = !node.isEditable;
     const mapped = this.flatNodeMap.get(node);
     this._database.updateEditable(mapped, !node.isEditable)
  }
  getTypes()
  {
    return this._database.getTypes();
  }

  getSubjectTypes(node: FlatNode): UnitType[]
  {
    return this._database.getSubjectTypes((node.data as UnitType).name);
  }
  isClassification(node: ItemNode)
  {
    return node.data !== null && typeof node.data === 'object';
  }

  getParentNode(node: FlatNode): FlatNode | null {
    const currentLevel = this.getLevel(node);

    if (currentLevel < 1) {
      return null;
    }

    const startIndex = this.treeControl.dataNodes.indexOf(node) - 1;

    for (let i = startIndex; i >= 0; i--) {
      const currentNode = this.treeControl.dataNodes[i];

      if (this.getLevel(currentNode) < currentLevel) {
        return currentNode;
      }
    }
    return null;
  }

  loadChildren(node: FlatNode) {
    const mapped = this.flatNodeMap.get(node);
    if(!node.wasExpanded)
    {
      if(node.isClassification)
      {
        const parent = this.getParentNode(node);
        this._database.loadUnits(mapped, parent!!.id)
      }
      else
        this._database.loadClassifications(mapped)
      node.wasExpanded = true;
      this.dataSource._data.next(this._database.data);
    }
  }

  saveClassification(node:FlatNode, value: string, unitTypeId: number)
  {
    const mapped = this.flatNodeMap.get(node);
    const parent = this.getParentNode(node);
    this._database.saveClassification(mapped, value, parent.id, unitTypeId);
  }

  saveUnit(node: FlatNode, value: string)
  {
    const mapped = this.flatNodeMap.get(node);
    const parent = this.getParentNode(node);
    this._database.saveUnit(mapped, value, parent.id);
  }

  addTemplate(node: FlatNode) {
    const mapped = this.flatNodeMap.get(node);
    const parent = this.getParentNode(node);
    if(!node.wasExpanded)
    {
      if(node.isClassification)
      {
        this._database.loadUnits(mapped, parent.id)
      }
      else
      {
        this._database.loadClassifications(mapped)
      }
    }
    this._database.insertTemplate(mapped!, node.isClassification 
        ? parent.id 
        : {name: (parent.data as UnitType).name} as UnitType);
    this.treeControl.expand(node);
  }

}
