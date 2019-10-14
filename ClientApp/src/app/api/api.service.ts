/** @format */

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ListEntry } from './api.models';

/** @format */

@Injectable({
  providedIn: 'root',
})
export class APIService {
  private readonly root = (sub: string = '') => (sub ? `/api/${sub}` : '/api');
  private readonly listEntries = (sub: string = '') =>
    `${this.root('list')}/entries` + (sub ? `/${sub}` : '');

  private readonly errorCatcher = (err) => {
    console.error(err);
    return of(null);
  };

  constructor(private http: HttpClient) {}

  public getEntries(): Observable<ListEntry[]> {
    return this.http
      .get<ListEntry[]>(this.listEntries(), {})
      .pipe(catchError(this.errorCatcher));
  }

  public addEntry(content: string): Observable<any> {
    return this.http
      .post(this.listEntries(), { content }, {})
      .pipe(catchError(this.errorCatcher));
  }

  public deleteEntry(guid: string): Observable<any> {
    return this.http
      .delete(this.listEntries(guid), {})
      .pipe(catchError(this.errorCatcher));
  }

  public flushEntries(): Observable<any> {
    return this.http
      .post(this.listEntries('flush'), {}, {})
      .pipe(catchError(this.errorCatcher));
  }
}
