import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfigService } from './config.service';
import { CustomerViewModel, CustomerHistoryData } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  constructor(
    private http: HttpClient,
    private configService: ConfigService
  ) {}
  
  getAll(): Observable<CustomerViewModel[]> {
    const url = `${this.configService.getApiUrl()}/customer/get`;
    return this.http.get<CustomerViewModel[]>(url);
  }
  
  getById(id: string): Observable<CustomerViewModel> {
    const url = `${this.configService.getApiUrl()}/customer/${id}`;
    return this.http.get<CustomerViewModel>(url);
  }
  
  create(customer: CustomerViewModel): Observable<any> {
    const url = `${this.configService.getApiUrl()}/customer`;
    return this.http.post<any>(url, customer);
  }
  
  update(id: string, customer: CustomerViewModel): Observable<any> {
    const url = `${this.configService.getApiUrl()}/customer/update/${id}`;
    return this.http.patch<any>(url, customer);
  }
  
  delete(id: string): Observable<any> {
    const url = `${this.configService.getApiUrl()}/customer/remove`;
    const params = new HttpParams().set('id', id);
    return this.http.delete<any>(url, { params });
  }
  
  getHistory(id: string): Observable<CustomerHistoryData[]> {
    const url = `${this.configService.getApiUrl()}/customer/history/${id}`;
    return this.http.get<CustomerHistoryData[]>(url);
  }
}