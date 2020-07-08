import { Classification } from './classification.model';
  
  export class UnitLightModel
  {
    id:number;
    parentId?:number;
    name:string;
  }

  export class UnitModel extends UnitLightModel{
    unitClassificationId: number;
  }

  export class SelectionUnitModel extends UnitLightModel{
    children: any;
  }
  export class UnitBaseResponse {
    unitId: number;
  }
  
  export class UnitAddResponse extends UnitBaseResponse {
    parentId?:number;
    unitClassificationId: number;
  }

  export class UnitUpdateResponse extends UnitBaseResponse {
      name:string;
  }

  