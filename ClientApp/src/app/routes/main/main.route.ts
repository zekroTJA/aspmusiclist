/** @format */

import { Component } from '@angular/core';
import { APIService } from '../../api/api.service';
import { ListEntry } from '../../api/api.models';
import { Router } from '@angular/router';

const REFRESH_DELAY = 15000;

@Component({
  selector: 'app-route-main',
  templateUrl: './main.route.html',
  styleUrls: ['./main.route.sass'],
})
export class MainRouteComponent {
  public range = Array;

  public entries: ListEntry[];
  public inputValue = '';
  public refreshing = false;

  constructor(private api: APIService, private router: Router) {
    this.pullEntries();
    setInterval(this.pullEntries.bind(this), REFRESH_DELAY);
  }

  private pullEntries() {
    this.refreshing = true;
    setTimeout(() => (this.refreshing = false), 1000);
    this.api.getEntries().subscribe((entries) => {
      this.entries = entries;
    });
  }

  public onEntryDelete(e: ListEntry) {
    this.api.deleteEntry(e.guid).subscribe(() => {
      const i = this.entries.indexOf(e);
      this.entries.splice(i, 1);
    });
  }

  public onEntrySpotify(e: ListEntry) {
    const url = `https://open.spotify.com/search/${encodeURI(e.content)}`;
    const wnd = window.open(url, '_blank');
    if (wnd) {
      wnd.focus();
    }
  }

  public onEntryAdd(event: any) {
    if (this.inputValue.length <= 0) {
      return;
    }

    this.api.addEntry(this.inputValue).subscribe((entry) => {
      this.entries.push(entry);
      this.inputValue = '';
    });
  }

  public onInputKeyPress(event: any) {
    if (event.keyCode === 13) {
      event.preventDefault();
      this.onEntryAdd(null);
    }
  }

  public onLogout(event: any) {
    this.api.authLogout().then(() => {
      this.router.navigate(['/login']);
    });
  }
}
