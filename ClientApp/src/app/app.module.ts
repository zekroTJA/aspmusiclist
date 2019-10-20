/** @format */

import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { EntryComponent } from './components/entry/entry.component';
import { MainRouteComponent } from './routes/main/main.route';
import { LoginRouteComponent } from './routes/login/login.route';
import { CreatedRouteComponent } from './routes/created/created.route';
import { ManageRouteComponent } from './routes/manage/manage.route';

@NgModule({
  declarations: [
    AppComponent,
    EntryComponent,
    MainRouteComponent,
    LoginRouteComponent,
    CreatedRouteComponent,
    ManageRouteComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
