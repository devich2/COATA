import {Injectable, OnInit} from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {ItemNode, FlatNode} from './node.model';
import { UnitApiService } from '../../api/unit/unit-api.service';
import { ClassificationApiService } from '../../api/unit/classification-api.service';
import { SelectionUnitModel, UnitModel, UnitLightModel, UnitAddResponse } from '../../models/unit.model';
import { Classification, ClassificationCreateModel, ClassificationAddResponse } from '../../models/classification.model';
import { map } from 'rxjs/operators';
import { FlatTreeControl } from '@angular/cdk/tree';
import { UnitType } from '../../models/unit-type.model';

@Injectable()
export class DynamicDataSource{
  dataChange = new BehaviorSubject<ItemNode[]>([]);

  get data(): ItemNode[] { return this.dataChange.value; }

  constructor(private unitService: UnitApiService, private classificationService : ClassificationApiService) {
     this.initialize();

  }

  initialize()
  {
    this.unitService.expandGrouped().subscribe((model : SelectionUnitModel)=> this.handleSeed(model))
  }

  getTypes(): string[]
  {
    return Object.keys(this.classificationService.typeHier.subjectTypes);
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
        if(this.isClassification(value))
        {
          node = new ItemNode(value.id, value.name, value.unitType);
          arr.splice(1,1);
          node.children = this.buildTree(obj[1], level + 1);
        }
        else
        {
          node = new ItemNode(value.id, value.name, value.parentId);
          node.children = this.buildTree(value.children, level + 1);
        }
      }
     
      return accumulator.concat(node);
    }, []);
  }
  
  updateEditable(node: ItemNode, editable: boolean)
  {

      node.isEditable = editable;
      this.dataChange.next(this.data);
  }

  getSubjectTypes(value:string)
  {
    return this.classificationService.typeHier.subjectTypes[value];
  }

  isClassification(obj: {[key: string]: any}) : boolean
  {
      return obj.unitType !== null && typeof obj.unitType === 'object'
  }
  
  saveClassification(node:ItemNode, name: string, parentId: number, unitTypeId: number)
  {
    this.classificationService.createClassification({
        name: name,
        parentId: parentId,
        unitTypeId: unitTypeId
    }as ClassificationCreateModel).subscribe((data: ClassificationAddResponse) => {
        console.log("SOSAT");
        node.id = data.classificationId;
        node.name = name;
        node.isTemplate = false;
        this.dataChange.next(this.data);
      });
  }

  saveUnit(node: ItemNode,name:string, classificationId: number)
  {
    this.unitService.createUnit({parentId: (node.data as number), name : name, unitClassificationId : classificationId} as UnitModel)
    .subscribe((data: UnitAddResponse) => {
      node.id = data.unitId;
      node.name = name;
      node.isTemplate = false;
      this.dataChange.next(this.data);
    });
  }
  loadUnits(node: ItemNode, parentId: number)
  {

        this.unitService.expand(parentId, node.id).pipe(map((data: UnitModel[]) =>
           data.map(d=> 
              new ItemNode(d.id, d.name, d.parentId)))).subscribe((data: ItemNode[])=>
              this.pushItems(node, data));  
  }

  loadClassifications(node: ItemNode)
  {
    this.classificationService.getClassificationsForParent(node.id).pipe(map(data => 
      data.map( el=> 
        new ItemNode(el.id, el.name, el.unitType)))).subscribe((data: ItemNode[]) =>
          this.pushItems(node, data));
  }

  pushItems(node: ItemNode, items: ItemNode[])
  {
    node.children.push(...items);
    this.dataChange.next(this.data);
  }

  insertTemplate(node: ItemNode, data?: number | UnitType)
  {
    node.children.push({
    isTemplate : true,
    name : '',
    children : [],
    data : data
    } as ItemNode);
    this.dataChange.next(this.data);
  }

  updateItem(node: ItemNode, name: string) {
    node.name = name;
    this.dataChange.next(this.data);
  }
}