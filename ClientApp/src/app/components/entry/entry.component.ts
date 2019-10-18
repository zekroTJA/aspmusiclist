/** @format */

import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-entry',
  templateUrl: './entry.component.html',
  styleUrls: ['./entry.component.sass'],
})
export class EntryComponent {
  @Input() public isFirst;
  @Output() public delete: EventEmitter<any> = new EventEmitter();
  @Output() public spotify: EventEmitter<any> = new EventEmitter();

  public onDeleteClick(ev: any) {
    this.delete.emit(ev);
  }

  public onSpotifyClick(ev: any) {
    this.spotify.emit(ev);
  }
}
