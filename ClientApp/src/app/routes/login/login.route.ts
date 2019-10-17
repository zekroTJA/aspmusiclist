/** @format */

import { Component } from '@angular/core';
import { APIService } from '../../api/api.service';
import { ListEntry } from '../../api/api.models';
import { Router } from '@angular/router';

@Component({
  selector: 'app-route-login',
  templateUrl: './login.route.html',
  styleUrls: ['./login.route.sass'],
})
export class LoginRouteComponent {
  public keyword: string;
  public listname: string;
  public error = {
    text: '',
    visibility: false,
  };

  constructor(private api: APIService, private router: Router) {}

  public onKeyPress(event: any) {
    if (event.keyCode === 13) {
      event.preventDefault();
      this.login();
    }
  }

  public login() {
    if (!this.keyword || !this.listname) {
      return;
    }

    this.api
      .authLogin(this.listname, this.keyword)
      .then(() => {
        this.router.navigate(['/']);
      })
      .catch((err) => {
        this.keyword = '';
        this.error.text = 'Invalid keyword.';
        this.error.visibility = true;
      });
  }

  public create() {
    if (!this.keyword || !this.listname) {
      return;
    }

    this.api
      .createList(this.listname, this.keyword)
      .then(() => {
        this.login();
      })
      .catch((err) => {
        console.error(err);
        if (err.error) {
          this.error.text = err.error.message;
          this.error.visibility = true;
        }
      });
  }
}
