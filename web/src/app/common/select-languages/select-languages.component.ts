import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-select-languages',
  templateUrl: './select-languages.component.html',
  styleUrls: ['./select-languages.component.css']
})
export class SelectLanguagesComponent implements OnInit {
  @Input() languages: Array<any>;
  selectedLanguage: any;

  constructor() {
  }

  ngOnInit() {
    if (this.selectedLanguage === undefined || this.selectedLanguage === null) {
      this.selectedLanguage = localStorage.getItem('Accept-Language');
    }
  }

  selectLanguage(language: string): void {
    localStorage.setItem('Accept-Language', language);
    this.selectedLanguage = language;
    window.location.href = window.location.origin + '/' + language;
  }

}
