import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

constructor() { }

  authUser(user: any) {
    let userArray = [];
    if(localStorage.getItem('Users')) {
      userArray = JSON.parse(localStorage.getItem('Users'));
    }
    return userArray.find(u=> u.userName === user.userName && u.password === user.password);
  }

}
