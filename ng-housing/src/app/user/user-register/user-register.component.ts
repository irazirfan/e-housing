import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from 'src/app/model/user';
import { UserService } from 'src/app/service/user.service';
import * as alertify from 'alertifyjs';
import { AlertifyService } from 'src/app/service/alertify.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  registrationForm: FormGroup;
  user: User;
  userSubmitted: boolean;
  authMode = '2';
  private readonly apiBaseUrl = 'http://localhost:5000/api/account/register';

  constructor(private fb: FormBuilder,
              private userService: UserService,
              private alertify: AlertifyService,
              private http: HttpClient,
              private router: Router) { }

  ngOnInit() {
    this.createRegistrationForm();
  }

  createRegistrationForm() {
    this.registrationForm = this.fb.group({
      userName: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(8)]],
      confirmPassword: [null, Validators.required],
      mobile: [null, [Validators.required, Validators.maxLength(10)]]
    },{validators: this.confirmPasswordValidator});
  }

  confirmPasswordValidator(fg: FormGroup): Validators {
    return fg.get('password').value === fg.get('confirmPassword').value ? null :
    {notmatched: true};
  }

  onSubmit() {
    console.log(this.registrationForm);
    this.userSubmitted = true;
    if (this.registrationForm.valid) {
      if (this.authMode === '2') {
        this.http.post(this.apiBaseUrl, {
          username: this.userName.value,
          password: this.password.value
        }).subscribe(() => {
          this.registrationForm.reset();
          this.userSubmitted = false;
          this.alertify.success('Registration sucessful');
          this.router.navigate(['/user/login']);
        }, (error) => {
          const message = error?.error?.ErrorMessage || error?.error?.message || error?.message || 'Registration failed';
          this.alertify.error(message);
        });
      } else {
        this.userService.addUser(this.userData());
        this.registrationForm.reset();
        this.userSubmitted = false;
        this.alertify.success('Registration sucessful');
        this.router.navigate(['/user/login']);
      }
    } else {
      this.alertify.error('Please provide the required fields properly');
    }
  }

  userData(): User {
    return this.user = {
      userName: this.userName.value,
      email: this.email.value,
      password: this.password.value,
      mobile: this.mobile.value
    };
  }

  // ------------------------------------
  // Getter Methods for all form controls
  // ------------------------------------
  get userName() {
    return this.registrationForm.get('userName') as FormControl;
  }
  get email() {
    return this.registrationForm.get('email') as FormControl;
  }
  get password() {
    return this.registrationForm.get('password') as FormControl;
  }
  get confirmPassword() {
    return this.registrationForm.get('confirmPassword') as FormControl;
  }
  get mobile() {
    return this.registrationForm.get('mobile') as FormControl;
  }
  // ------------------------------------
}
