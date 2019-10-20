/** @format */

import { Component, OnDestroy } from '@angular/core';
import { APIService } from '../../api/api.service';
import { SharedService } from 'src/app/services/shared.service';
import { Router } from '@angular/router';
import { List } from 'src/app/api/api.models';
import * as copyToClipboard from 'copy-to-clipboard';

@Component({
  selector: 'app-route-created',
  templateUrl: './created.route.html',
  styleUrls: ['./created.route.sass'],
})
export class CreatedRouteComponent implements OnDestroy {
  public list: List;
  public masterKeyVisibility: boolean;

  constructor(
    private api: APIService,
    private shared: SharedService,
    private router: Router
  ) {
    if (!shared.sharedList) {
      router.navigate(['/']);
      return;
    }

    this.list = shared.sharedList;
  }

  public copyMasterKey() {
    if (copyToClipboard(this.list.masterKey)) {
      window.alert('Master key copied to clipboard. Please save it safely.');
    } else {
      window.alert('Failed saving master key to clipboard.');
    }
  }

  public login() {
    this.api
      .authLogin(
        this.shared.sharedLogin.ListIdentifier,
        this.shared.sharedLogin.Keyword
      )
      .then(() => {
        this.router.navigate(['/']);
      })
      .catch((err) => {
        console.error(err);
        this.router.navigate(['/login']);
      });
  }

  public ngOnDestroy() {
    this.purgeSharedData();
  }

  public purgeSharedData() {
    this.shared.purge();
  }
}
