import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Climb } from '../model/climb';

@Injectable({
  providedIn: 'root',
})
export class ClimbService {
  baseUrl = 'http://localhost:5098';
  constructor(private http: HttpClient) { }

  getClimbs(): Observable<Climb[]> {
    return this.http.get<any[]>(`${this.baseUrl}/usersession`).pipe(
      map(sessions => sessions.map(session => ({
        userId: session.userID || session.userid,
        routeId: session.routeID || session.routeid,
        grade: session.gradeFbleau || session.gradefbleau,
        status: session.status,
        climbId: session.routeID || session.routeid  // Alias for compatibility
      })))
    );
  }

  getClimb(id: number): Observable<Climb> {
    return this.http.get<Climb>(`${this.baseUrl}/climb/${id}`);
  }

  createClimb(climb: Climb): Observable<any> {
    return this.http.post(`${this.baseUrl}/climb`, climb);
  }

  updateClimbStatus(userID: number, routeID: number, status: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/UserRoute/${userID}/${routeID}/${status}`, {});
  }

  updateClimb(climb: Climb): Observable<any> {
    return this.http.put(`${this.baseUrl}/climb`, climb);
  }

  deleteClimb(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/climb/${id}`);
  }

  addClimb(climb: Climb): Observable<any> {
    return this.http.post(`${this.baseUrl}/climb`, climb);
  }
}
