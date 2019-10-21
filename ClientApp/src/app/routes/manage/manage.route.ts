/** @format */

import { Component } from '@angular/core';
import { APIService } from 'src/app/api/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-route-manage',
  templateUrl: './manage.route.html',
  styleUrls: ['./manage.route.sass'],
})
export class ManageRouteComponent {
  public masterKey: string;
  public masterKeyState = 0;
  public newPassword: string;

  constructor(private api: APIService, private router: Router) {}

  public onMasterKeyChange() {
    this.api
      .checkMasterKey(this.masterKey)
      .then(() => {
        this.masterKeyState = 1;
      })
      .catch(() => {
        this.masterKeyState = 2;
      });
  }

  public deleteList() {
    const res = window.confirm(
      'Do you really want to delete this list (forever)?'
    );
    if (res === true) {
      this.api
        .deleteList(this.masterKey)
        .then(() => {
          this.router.navigate(['/login']);
        })
        .catch((err) => {
          console.error(err);
          window.alert(`Failed deleting list: ${err.error.message}`);
        });
    }
  }

  public changePassword() {
    this.api
      .updateListPassword(this.masterKey, this.newPassword)
      .then(() => {
        window.alert('Password changed successfully.');
        this.newPassword = '';
      })
      .catch((err) => {
        console.error(err);
        window.alert(`Failed deleting list: ${err.error.message}`);
      });
  }
}
