
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

@Component({
  selector: 'app-unit-tree',
  templateUrl: './unit-tree.component.html',
  styleUrls: ['./unit-tree.component.css'],
  providers: [DynamicDataSource]
})
export class UnitTreeComponent{

  flatNodeMap = new Map<FlatNode, ItemNode>();

  nestedNodeMap = new Map<ItemNode, FlatNode>();

  treeControl: FlatTreeControl<FlatNode>;

  treeFlattener: MatTreeFlattener<ItemNode, FlatNode>;

  dataSource: MatTreeFlatDataSource<ItemNode, FlatNode>;

  _database: DynamicDataSource
  constructor(private unitService: UnitApiService, private classificationService : ClassificationApiService) {
    this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,
      this.isExpandable, this.getChildren);
    this.treeControl = new FlatTreeControl<FlatNode>(this.getLevel, this.isExpandable);
    this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
    this._database = new DynamicDataSource(this.unitService, this.classificationService, this.treeControl)
    this._database.dataChange.subscribe(data => {
      this.dataSource.data = data;
    });
  }

  getLevel = (node: FlatNode) => node.level;

  isExpandable = (node: FlatNode) => node.expandable;

  getChildren = (node: ItemNode): ItemNode[] => node.children;

  hasChild = (_: number, _nodeData: FlatNode) => _nodeData.expandable;

  hasNoContent = (_: number, _nodeData: FlatNode) => _nodeData.name === '';

  transformer = (node: ItemNode, level: number) => {
    const existingNode = this.nestedNodeMap.get(node);
    const flatNode = existingNode && existingNode.name === node.name && existingNode.id == node.id
        ? existingNode
        : new FlatNode();
    Object.assign(flatNode, node);
    flatNode.level = level;
    flatNode.isClassification = node.data !== null && typeof node.data === 'object'
    flatNode.expandable = true;
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node, flatNode);
    return flatNode;
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
    if(mapped.children!.length < 1)
    {
        const parent = this.getParentNode(node);
        this._database.loadUnits(mapped, parent!!.id, node.isClassification)
    }
  }

  addNewItem(node: FlatNode) {
    const parentNode = this.flatNodeMap.get(node);
    this._database.insertItem(parentNode!, '');
    this.treeControl.expand(node);
    console.log(this.treeControl.dataNodes)
  }

  saveNode(node: FlatNode, itemValue: string) {
    const nestedNode = this.flatNodeMap.get(node);
    this._database.updateItem(nestedNode!, itemValue);
  }
}
