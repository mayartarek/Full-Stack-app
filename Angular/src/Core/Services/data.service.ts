import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  public headers = new HttpHeaders();
  constructor(private http: HttpClient) { }

  getList(route: string, options?: any): Observable<any> {
    return this.http.get(route, options);
  }

  post(route: string, body: any, options?: any) {
    return this.http.post(route, body, { 'headers': this.headers });
  }

  put(route: string, body: any, options?: any) {
    return this.http.put(route, body, options);
  }

  delete(route: string, body: any) {
    return this.http.delete(route, body);
  }

  getItemById(route: string, id: any, options?: any): Observable<any> {
    return this.http.get(`${route}?id=${id}`);
  }
  getItemByObjectId(route: string, object: string, id: number, options?: any): Observable<any> {
    return this.http.get(`${route}?${object}=${id}`, options);
  }

}
