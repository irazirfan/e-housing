import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiBaseUrl = 'http://localhost:5000/api/account/login';

  constructor(private http: HttpClient) { }

  authUser(user: any, authMode: string): Observable<any> {
    if (authMode === '2') {
      return this.http.post<any>(this.apiBaseUrl, {
        username: user.username || user.userName,
        password: user.password
      }).pipe(
        map((response) => {
          const username = response?.Username || response?.username || response?.userName || response?.UserName;
          const token = response?.Token || response?.token;
          return username && token ? { username, token } : null;
        })
      );
    }

    let userArray = [];
    if (localStorage.getItem('Users')) {
      userArray = JSON.parse(localStorage.getItem('Users'));
    }

    const matchedUser = userArray.find((u: any) => u.userName === user.userName && u.password === user.password);
    return of(matchedUser ? { username: matchedUser.userName, token: matchedUser.userName } : null);
  }
}
