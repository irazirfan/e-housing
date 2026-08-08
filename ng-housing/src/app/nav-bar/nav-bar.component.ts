import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../service/alertify.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  loggedInUser: string;
  constructor(private alertify: AlertifyService) { }

  ngOnInit() {
  }

  loggedIn() {
    this.loggedInUser = localStorage.getItem('currentUser');
    return this.loggedInUser;
  }

  onLogout() {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    this.alertify.success('You are logged out!');
  }
}
