import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';

  // ReplaySubject is a type of observable (a kind of 'buffer') that will emit
  // the last x amount of values inside it when subscribed to. We've specified 1 value.
  private currentUserSource = new ReplaySubject<User>(1);

  // it is a standard to end observable type variable names with a '$'.
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));

          // sets next value according to what is passed in.
          this.currentUserSource.next(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }
}
