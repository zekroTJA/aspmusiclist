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
export class AppComponent {
  title = 'ClientApp';

  public range = Array;

  public entries: ListEntry[];
  public inputValue = '';
  public refreshing = false;

  constructor(private api: APIService) {
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
}
