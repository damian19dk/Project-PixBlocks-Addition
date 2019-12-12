import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  private readonly allTags: Array<string>;
  private readonly tagSettingsForMultiselect: any;

  constructor() {
    this.allTags = ['Haskell', 'Java', 'Ruby', 'Python', 'Kotlet schabowy', 'WebDevelop', 'Systemy operacyjne', 'JavaScript'].sort(); // zamockowane dane
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

  toTagsList(tags: any) {
    if (tags === null || tags.length === 0) {
      return null;
    }
    return tags.join().split(',');
  }

  toTagsString(tags: Array<string>) {
    return tags === null ? null : tags.join();
  }
}
