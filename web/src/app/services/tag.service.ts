import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TagService {

  private allTags: string[];
  private tagSettingsForMultiselect: any;

  constructor() {
    this.allTags = ["Haskell", "Java", "Ruby"]; // zamockowane dane
    this.tagSettingsForMultiselect = {
      singleSelection: false,
      selectAllText: 'Zaznacz wszystkie',
      unSelectAllText: 'Odznacz wszystkie',
      itemsShowLimit: 5,
      allowSearchFilter: true,
      searchPlaceholderText: 'Szukaj...'
    };

  }

  getTags() {
    return this.allTags;
  }

  getTagSettingsForMultiselect() {
    return this.tagSettingsForMultiselect;
  }
}
