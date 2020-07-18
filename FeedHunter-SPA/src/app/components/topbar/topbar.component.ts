import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/service/auth.service';
import { User } from 'src/app/model/user';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css']
})
export class TopbarComponent implements OnInit {
  loginForm: FormGroup;
  user: User;
  currentUser: User;

  constructor(public auth: AuthService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.createLoginForm();
  }

  createLoginForm()
  {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login() {
    this.user = Object.assign({}, this.loginForm.value);
    this.auth.login(this.user).subscribe(() => {
      this.currentUser = this.auth.currentUser;
    }, error => {
      console.log(error);
    });
  }

  loggedIn() {
    return this.auth.loggedIn();
  }

  logout() {
    return this.auth.logout();
  }

}
