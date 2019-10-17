/** @format */

export interface ListEntry {
  guid: string;
  listGuid: string;
  content: string;
}

export interface List {
  guid: string;
  identifier: string;
}

export interface Login {
  ListIdentifier: string;
  Keyword: string;
}
