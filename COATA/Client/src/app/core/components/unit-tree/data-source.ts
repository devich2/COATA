import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ItemNode } from './node.model';
import { UnitApiService } from '../../api/unit/unit-api.service';
import { ClassificationApiService } from '../../api/unit/classification-api.service';
import { SelectionUnitModel, UnitModel, UnitAddResponse } from '../../models/unit.model';
import { ClassificationCreateModel, ClassificationAddResponse } from '../../models/classification.model';
import { map } from 'rxjs/operators';
import { UnitType } from '../../models/unit-type.model';

@Injectable()
export class DynamicDataSource {
  dataChange = new BehaviorSubject<ItemNode[]>([]);

  get data(): ItemNode[] { return this.dataChange.value; }

  constructor(private unitService: UnitApiService, private classificationService: ClassificationApiService) {
    this.initialize();

  }

  initialize() {
    this.unitService.expandGrouped().subscribe((model: SelectionUnitModel) => this.handleSeed(model))
  }

  filter(name: string, type: string, callback: () => void) {
    this.unitService.search(name, type)
      .subscribe((data: SelectionUnitModel) => {
        this.handleSeed(data);
        callback()
      })
  }

  getTypes(): string[] {
    return Object.keys(this.classificationService.typeHier.subjectTypes);
  }

  deleteUnit(node: ItemNode, parent: ItemNode) {
    this.unitService.delete(node.id).
      subscribe(() => {
        parent.children.splice(parent.children.indexOf(node), 1);
        this.dataChange.next(this.data);
      });
  }

  delete(node: ItemNode, parent: ItemNode) {
    parent.children.splice(parent.children.indexOf(node), 1);
    this.dataChange.next(this.data);
  }

  updateNode(node: ItemNode, text: string) {
    this.unitService.updateUnit(node.id, { name: text } as UnitModel)
      .subscribe(() => {
        node.name = text;
        node.isEditable = false;
        this.dataChange.next(this.data);
      });
  }

  handleSeed(model: SelectionUnitModel) {
    const data = this.buildTree(model.children, 0);
    // Notify the change.
    this.dataChange.next(data);
  }


  buildTree(obj: { [key: string]: any }, level: number): ItemNode[] {

    return Object.keys(obj).slice(0).reduce<ItemNode[]>((accumulator, key, i, arr) => {
      const value = obj[key];
      let node: ItemNode;
      if (value != null) {
        if (Array.isArray(value)) {
          return accumulator.concat(this.buildTree(value, level));
        }
        if (this.isClassification(value)) {
          node = new ItemNode(value.id, value.name, value.unitType);
          arr.splice(1, 1);
          node.children = this.buildTree(obj[1], level + 1);
        }
        else {
          node = new ItemNode(value.id, value.name, value.parentId);
          node.children = this.buildTree(value.children, level + 1);
        }
      }

      return accumulator.concat(node);
    }, []);
  }

  updateEditable(node: ItemNode, editable: boolean) {

    node.isEditable = editable;
    this.dataChange.next(this.data);
  }

  getSubjectTypes(value: string) {
    return this.classificationService.typeHier.subjectTypes[value];
  }

  isClassification(obj: { [key: string]: any }): boolean {
    return obj.unitType !== null && typeof obj.unitType === 'object'
  }

  saveClassification(node: ItemNode, name: string, parentId: number, unitTypeId: number) {
    this.classificationService.createClassification({
      name: name,
      parentId: parentId,
      unitTypeId: unitTypeId
    } as ClassificationCreateModel).subscribe((data: ClassificationAddResponse) => {
      node.id = data.classificationId;
      node.name = name;
      node.isTemplate = false;
      this.dataChange.next(this.data);
    });
  }

  saveUnit(node: ItemNode, name: string, classificationId: number) {
    this.unitService.createUnit({ parentId: (node.data as number), name: name, unitClassificationId: classificationId } as UnitModel)
      .subscribe((data: UnitAddResponse) => {
        node.id = data.unitId;
        node.name = name;
        node.isTemplate = false;
        this.dataChange.next(this.data);
      });
  }

  loadUnits(node: ItemNode, parentId: number) {

    this.unitService.expand(parentId, node.id).pipe(map((data: UnitModel[]) =>
      data.map(d =>
        new ItemNode(d.id, d.name, d.parentId)))).subscribe((data: ItemNode[]) =>
          this.pushItems(node, data));
  }

  loadClassifications(node: ItemNode) {
    this.classificationService.getClassificationsForParent(node.id).pipe(map(data =>
      data.map(el =>
        new ItemNode(el.id, el.name, el.unitType)))).subscribe((data: ItemNode[]) =>
          this.pushItems(node, data));
  }

  pushItems(node: ItemNode, items: ItemNode[]) {
    node.children.push(...items);
    this.dataChange.next(this.data);
  }

  insertTemplate(node: ItemNode, data?: number | UnitType) {
    node.children.push({
      isTemplate: true,
      name: '',
      children: [],
      data: data
    } as ItemNode);
    this.dataChange.next(this.data);
  }

  updateItem(node: ItemNode, name: string) {
    node.name = name;
    this.dataChange.next(this.data);
  }
}