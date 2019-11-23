import {Injectable} from '@angular/core';

export interface Language {
  code: string;
  label: string;
}

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  languages = [
    {code: 'en', label: 'English'},
    {code: 'pl', label: 'Polski'}
  ];

  appLanguages = [
    {code: 'en', label: 'English'},
    {code: 'pl', label: 'Polski'}
  ];

  constructor() {
  }

  getAllLanguages(): Array<Language> {
    return this.languages;
  }

  getAllAppLanguages() {
    return this.appLanguages;
  }
}
