/** @format */

import { Injectable } from '@angular/core';
import { List, Login } from '../api/api.models';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  public sharedList: List;
  public sharedLogin: Login;

  public purge() {
    delete this.sharedList;
    delete this.sharedLogin;
  }
}
