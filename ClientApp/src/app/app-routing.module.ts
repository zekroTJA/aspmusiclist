/** @format */

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainRouteComponent } from './routes/main/main.route';
import { LoginRouteComponent } from './routes/login/login.route';

const routes: Routes = [
  {
    path: 'login',
    component: LoginRouteComponent,
  },
  {
    path: '',
    component: MainRouteComponent,
  },
  {
    path: '**',
    redirectTo: '/',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
