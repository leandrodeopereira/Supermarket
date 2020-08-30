import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { IUser } from '../shared/models/user';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<IUser>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  getCurrentUserValue(): IUser {
    return this.currentUserSource.value;
  }

  loadCurrentUser(token: string): Observable<void> {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + token);

    return this.http.get(this.baseUrl + 'account', { headers }).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  login(values: any): Observable<void> {
    return this.http.post(this.baseUrl + 'account/login', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }
}