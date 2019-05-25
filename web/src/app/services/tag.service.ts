import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TagService {

  private allTags: any;

  constructor() {
    this.allTags = ["Haskell", "Java", "Ruby"];
   }

  getTags() {
    return this.allTags;
  }
}
