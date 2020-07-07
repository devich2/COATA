import {
    UnitAddResponse,
    UnitBaseResponse,
    UnitUpdateResponse,
    UnitModel, 
    SelectionUnitModel
  } from '../../models/unit.model';
  import {HttpClient, HttpParams} from '@angular/common/http';
  import {forkJoin, Observable} from 'rxjs';
import { Injectable } from '@angular/core';
  
  @Injectable()
  export class UnitApiService {
  
    constructor(protected httpClient: HttpClient) {
    }
  
    expand(unitId: number, classificationId: number): Observable<UnitModel[]> {
      const params = new HttpParams({
          fromObject: {unitId: unitId.toString(), classificationId: classificationId.toString()}
      });
      return this.httpClient.get<UnitModel[]>('/api/unit/expand', {params});
    }
  
    expandGrouped(unitId:number = null): Observable<SelectionUnitModel> {
      let params = new HttpParams();
      if(unitId != null)
        params = params.append("unitId", unitId.toString());
      return this.httpClient.get<SelectionUnitModel>('/api/unit/expand_grouped', {params});
    }
  
    search(name:string, unitType: string): Observable<SelectionUnitModel> {
      let params = new HttpParams();
      if(name != null)
      {
          params = params.append("name", name);
      }
      if(unitType != null)
      {
          params = params.append("unitType", unitType);
      }
      return this.httpClient.get<SelectionUnitModel>('/api/unit/search', {params});
    }
  
    searchExpanded(name:string, unitType: string): Observable<SelectionUnitModel> {
      let params = new HttpParams();
      if(name != null)
      {
          params = params.append("name", name);
      }
      if(unitType != null)
      {
          params = params.append("unitType", unitType);
      }
      return this.httpClient.get<SelectionUnitModel>('/api/unit/search_expanded', {params});
    }

    createUnit(unit: UnitModel): Observable<UnitAddResponse> {
      return this.httpClient.post<UnitAddResponse>('/api/unit', unit);
    }
  
    updateUnit(unitId: number, unit: UnitModel): Observable<UnitUpdateResponse> {
      return this.httpClient.put<UnitUpdateResponse>(`/api/unit/${unitId}`, unit);
    }
  
    delete(unitId: number): Observable<UnitBaseResponse> {
      return this.httpClient.delete<UnitBaseResponse>(`/api/content/${unitId}`);
    }
  
  }
  