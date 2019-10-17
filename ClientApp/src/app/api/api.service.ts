/** @format */

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ListEntry, List } from './api.models';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class APIService {
  private readonly root = (sub: string = '') => (sub ? `/api/${sub}` : '/api');

  private readonly listEntries = (sub: string = '') =>
    `${this.root('list')}/entries` + (sub ? `/${sub}` : '');

  private readonly auth = (sub: string = '') =>
    this.root('auth') + (sub ? `/${sub}` : '');

  private readonly defopts = (obj?: object) => {
    const defopts = {
      withCredentials: true,
    };

    if (obj) {
      Object.keys(obj).forEach((k) => {
        defopts[k] = obj[k];
      });
    }

    return defopts;
  };

  private readonly errorCatcher = (err) => {
    console.error(err);

    if (err && err.status === 401) {
      this.router.navigate(['/login']);
    }

    return of(null);
  };

  constructor(private http: HttpClient, private router: Router) {}

  public getList(): Observable<List> {
    return this.http
      .get<List>(this.root('list'), this.defopts())
      .pipe(catchError(this.errorCatcher));
  }

  public createList(listIdentifier: string, keyword: string): Promise<any> {
    return this.http
      .post(this.root('list'), { listIdentifier, keyword }, this.defopts())
      .toPromise<any>();
  }

  public getEntries(): Observable<ListEntry[]> {
    return this.http
      .get<ListEntry[]>(this.listEntries(), this.defopts())
      .pipe(catchError(this.errorCatcher));
  }

  public addEntry(content: string): Observable<any> {
    return this.http
      .post(this.listEntries(), { content }, this.defopts())
      .pipe(catchError(this.errorCatcher));
  }

  public deleteEntry(guid: string): Observable<any> {
    return this.http
      .delete(this.listEntries(guid), this.defopts())
      .pipe(catchError(this.errorCatcher));
  }

  public flushEntries(): Observable<any> {
    return this.http
      .post(this.listEntries('flush'), {}, this.defopts())
      .pipe(catchError(this.errorCatcher));
  }

  public authLogin(listIdentifier: string, keyword: string): Promise<any> {
    return this.http
      .post(this.auth('login'), { listIdentifier, keyword }, this.defopts())
      .toPromise();
  }

  public authLogout(): Promise<any> {
    return this.http.post(this.auth('logout'), {}, this.defopts()).toPromise();
  }
}
