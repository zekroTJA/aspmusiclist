/** @format */

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainRouteComponent } from './routes/main/main.route';
import { LoginRouteComponent } from './routes/login/login.route';
import { CreatedRouteComponent } from './routes/created/created.route';
import { ManageRouteComponent } from './routes/manage/manage.route';

const routes: Routes = [
  {
    path: 'login',
    component: LoginRouteComponent,
  },
  {
    path: 'manage',
    component: ManageRouteComponent,
  },
  {
    path: 'created',
    component: CreatedRouteComponent,
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
