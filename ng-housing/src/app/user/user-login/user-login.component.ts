import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/service/alertify.service';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  constructor(private authService: AuthService,
              private alertifyService: AlertifyService,
              private router: Router) { }

  ngOnInit() {
  }

  onLogin(loginForm: NgForm) {
    const token = this.authService.authUser(loginForm.value);
    if(token) {
      localStorage.setItem('token', token.userName);
      this.alertifyService.success('Login Sucessful!');
      this.router.navigate(['/']);
    }
    else {
    this.alertifyService.error('Wrong User Id or Password!');
    }
  }
}
