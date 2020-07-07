import {
    SelectionUnitModel
  } from '../../models/unit.model';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import { UnitTypeAggr } from '../../models/unit-type.model';
import { Injectable } from '@angular/core';
import { Classification } from '../../models/classification.model';
  
  @Injectable({providedIn: 'root'})
  export class ClassificationApiService {
  
    typeHier:UnitTypeAggr;
    constructor(protected httpClient: HttpClient) {
        this.getTypesHier().subscribe((data: UnitTypeAggr) => {this.typeHier = data; console.log(this.typeHier)});
    }
  
    getTypesHier(): Observable<UnitTypeAggr> {
      return this.httpClient.get<UnitTypeAggr>('/api/unit_types');
    }
  
    getClassificationsForParent(unitId:number = null): Observable<Classification[]> {
      return this.httpClient.get<Classification[]>(`/api/classification/${unitId}`);
    }
  
  }
  