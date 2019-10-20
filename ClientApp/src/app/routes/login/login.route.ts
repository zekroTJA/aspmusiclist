/** @format */

import { Component } from '@angular/core';
import { APIService } from '../../api/api.service';
import { ListEntry, List, Login } from '../../api/api.models';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/services/shared.service';

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

  constructor(
    private api: APIService,
    private router: Router,
    private shared: SharedService
  ) {}

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

        switch (err.status) {
          case 401:
            this.error.text = 'Invalid keyword.';
            break;
          case 429:
            this.error.text = 'You have tried to often. Try again later.';
            break;
          default:
            this.error.text = err.error.message;
            break;
        }

        this.error.visibility = true;
      });
  }

  public create() {
    if (!this.keyword || !this.listname) {
      return;
    }

    this.api
      .createList(this.listname, this.keyword)
      .then((list: List) => {
        this.shared.sharedList = list;
        this.shared.sharedLogin = {
          Keyword: this.keyword,
          ListIdentifier: this.listname,
        } as Login;
        this.router.navigate(['/created']);
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
