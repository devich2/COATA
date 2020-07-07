export class UnitType{
    id:number;
    name:string;
}

export class UnitTypeAggr{
    subjectTypes: {[key:string]: UnitType[]}
}