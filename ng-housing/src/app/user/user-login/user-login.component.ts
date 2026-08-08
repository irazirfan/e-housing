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
  authMode = '2';

  constructor(private authService: AuthService,
              private alertifyService: AlertifyService,
              private router: Router) { }

  ngOnInit() {
  }

  onLogin(loginForm: NgForm) {
    this.authService.authUser(loginForm.value, this.authMode).subscribe((result) => {
      if (result && (result.token || result.username || result.userName)) {
        const username = result.username || result.userName || loginForm.value.username || loginForm.value.userName;
        const token = result.token;
        if (token) {
          localStorage.setItem('token', token);
        }
        localStorage.setItem('currentUser', username);
        this.alertifyService.success('Login Sucessful!');
        this.router.navigate(['/']);
      } else {
        this.alertifyService.error('Wrong User Id or Password!');
      }
    }, () => {
      this.alertifyService.error('Wrong User Id or Password!');
    });
  }
}
