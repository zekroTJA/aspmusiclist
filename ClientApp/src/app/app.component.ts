/** @format */

import { Component } from '@angular/core';
import { APIService } from './api/api.service';
import { ListEntry } from './api/api.models';

const REFRESH_DELAY = 15000;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
})
export class AppComponent {}
