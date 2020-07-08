import { UnitType } from './unit-type.model';

export class Classification {
  id: number;
  name: string;
  unitType: UnitType;
}
export class ClassificationAddResponse {
  classificationId: number;
}
export class ClassificationCreateModel {
  id: number;
  name: string;
  unitTypeId: number;
  parentId: number;
  constructor(name: string, unitTypeId: number, parentId: number) {
    this.name = name;
    this.unitTypeId = unitTypeId;
    this.parentId = parentId;
  }
}