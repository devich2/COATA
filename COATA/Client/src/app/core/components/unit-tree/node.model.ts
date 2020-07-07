import { UnitType } from '../../models/unit-type.model';

export class FlatNode {
    id: number;
    name: string;
    data?: number | UnitType;
    level: number;
    expandable: boolean;
    isClassification: boolean;
  }

  export class ItemNode {
    id: number;
    name: string;
    data?: number | UnitType;
    children: ItemNode[]
  }