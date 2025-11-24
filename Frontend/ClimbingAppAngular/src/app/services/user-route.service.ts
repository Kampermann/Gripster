import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserRouteService {
  private apiUrl = 'http://localhost:5000/UserRoute'; // Adjust to your API URL

  constructor(private http: HttpClient) {}

  insertUserRouteByID(userId: number, routeId: number, status: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/${userId}/${routeId}`, JSON.stringify(status), {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  getUserRoutes(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getUserRoutesByUserId(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/user/${userId}`);
  }

  getUserRoutesByRouteId(routeId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/route/${routeId}`);
  }

  updateUserRoute(userRoute: any): Observable<any> {
    return this.http.put(this.apiUrl, userRoute);
  }

  deleteUserRoute(userId: number, routeId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${userId}/${routeId}`);
  }
}
