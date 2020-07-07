import {Injectable, OnInit} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {ItemNode, FlatNode} from './node.model';
import { UnitApiService } from '../../api/unit/unit-api.service';
import { ClassificationApiService } from '../../api/unit/classification-api.service';
import { SelectionUnitModel, UnitModel, UnitLightModel } from '../../models/unit.model';
import { Classification } from '../../models/classification.model';
import { map } from 'rxjs/operators';
import { FlatTreeControl } from '@angular/cdk/tree';

@Injectable()
export class DynamicDataSource{
  dataChange = new BehaviorSubject<ItemNode[]>([]);

  get data(): ItemNode[] { return this.dataChange.value; }

  constructor(private unitService: UnitApiService, private classificationService : ClassificationApiService,
    private treeControl:FlatTreeControl<FlatNode>) {
     this.initialize();

  }

  initialize()
  {
    this.unitService.expandGrouped().subscribe((model : SelectionUnitModel)=> this.handleSeed(model))
  }

  handleSeed(model: SelectionUnitModel)
  {
    console.log(model);
    const data = this.buildTree(model.children, 0);
    console.log(data);
    // Notify the change.
    this.dataChange.next(data);
  }


  buildTree(obj: {[key: string]: any}, level: number): ItemNode[] {

    return Object.keys(obj).slice(0).reduce<ItemNode[]>((accumulator, key, i, arr) => {
      const value = obj[key];
      let node: ItemNode;
      if(value != null)
      {
        if(Array.isArray(value))
        {
          return accumulator.concat(this.buildTree(value, level));
        }
        if((value as Classification).unitType)
        {
          node = new ItemNode(value.id, value.name, value.unitType);
          arr.splice(1,1);
          node.children = this.buildTree(obj[1], level + 1);
        }
        if((value as UnitLightModel).parentId !== undefined)
        {
          node = new ItemNode(value.id, value.name, value.parentId);
          node.children = this.buildTree(value.children, level + 1);
        }
      }
     
      return accumulator.concat(node);
    }, []);
  }

  loadUnits(node: ItemNode, parentId: number, isClassification: boolean)
  {
      if(isClassification)
      {
        this.unitService.expand(parentId, node.id).pipe(map((data: UnitModel[]) =>
           data.map(d=> 
              new ItemNode(d.id, d.name, d.parentId)))).subscribe((data: ItemNode[])=>
              this.pushItems(node, data))
      }
      else{
        this.classificationService.getClassificationsForParent(node.id).pipe(map(data => 
        data.map( el=> 
          new ItemNode(el.id, el.name, el.unitType)))).subscribe((data: ItemNode[]) =>
            this.pushItems(node, data));}
            
  }

  pushItems(node: ItemNode, items: ItemNode[])
  {
    node.children.push(...items);
    this.dataChange.next(this.data);
  }
  /** Add an item to to-do list */
  insertItem(parent: ItemNode, name: string) {
    if (parent.children) {
      parent.children.push({name: name} as ItemNode);
      this.dataChange.next(this.data);
    }
  }

  updateItem(node: ItemNode, name: string) {
    node.name = name;
    this.dataChange.next(this.data);
  }
}