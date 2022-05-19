import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable, of } from 'rxjs';
import { Address } from 'src/app/models/address';
import { Salon } from 'src/app/models/salon';
import { environment } from 'src/environments/environment';

export abstract class ISalonService {

  constructor() { }

  abstract getCities(): Observable<string[]>
  abstract getSalonsByCity(city: string): Observable<Salon[]>

}

@Injectable({
  providedIn: 'root'
})
export class SalonService extends ISalonService {

  constructor(private http: HttpClient) {
    super()
  }

  getCities(): Observable<string[]> {
    return this.http
      .get<string[]>(`${environment.apiUrl}/city`);
  }

  getSalonsByCity(city: string): Observable<Salon[]> {
    return this.http
      .get<Salon[]>(`${environment.apiUrl}/salon?city=${city}`);
  }

}

@Injectable({
  providedIn: 'root'
})
export class FakeSalonService extends ISalonService {

  constructor() {
    super()
  }

  getCities(): Observable<string[]> {
    return of(
      [ 'Łódź', 'Warszawa', 'Gdańsk' ]
    );
  }

  getSalonsByCity(city: string): Observable<Salon[]> {
    return of(
      [
        new Salon(this.guid(), 'Salon Python', '111 111 111', 'python@email.com', new Address(city, 'Ulica', '1', '00-000')),
        new Salon(this.guid(), 'Salon Java', '222 222 222', 'java@email.com', new Address(city, 'Ulica', '2', '00-000')),
        new Salon(this.guid(), 'Salon Docker', '333 333 333', 'docker@email.com', new Address(city, 'Ulica', '3', '00-000'))
      ]
    );
  }

  private guid(): string {
    return Guid.create().toString();
  }

}
