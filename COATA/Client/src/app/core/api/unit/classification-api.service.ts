import {
    SelectionUnitModel
  } from '../../models/unit.model';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import { UnitTypeAggr } from '../../models/unit-type.model';
import { Injectable } from '@angular/core';
  
  @Injectable({providedIn: 'root'})
  export class ClassificationApiService {
  
    typeHier:UnitTypeAggr;
    protected constructor(protected httpClient: HttpClient) {
        this.getTypesHier().subscribe((data: UnitTypeAggr) => this.typeHier = data);
    }
  
    getTypesHier(): Observable<UnitTypeAggr> {
      return this.httpClient.get<UnitTypeAggr>('/api/unit_types');
    }
  
    getClassificationsForParent(unitId:number = null): Observable<SelectionUnitModel> {
      return this.httpClient.get<SelectionUnitModel>(`/api/classification/{unitId}`);
    }
  
  }
  