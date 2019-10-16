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
  public error = {
    text: '',
    visibility: false,
  };

  constructor(private api: APIService, private router: Router) {}

  public onKeyPress(event: any) {
    if (event.keyCode === 13) {
      event.preventDefault();
      this.login(this.keyword);
    }
  }

  private login(pw: string) {
    if (!pw) {
      return;
    }

    this.api
      .authLogin(pw)
      .then(() => {
        this.router.navigate(['/']);
      })
      .catch((err) => {
        this.keyword = '';
        this.error.text = 'Invalid keyword.';
        this.error.visibility = true;
      });
  }
}
