import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-select-languages',
  templateUrl: './select-languages.component.html',
  styleUrls: ['./select-languages.component.css']
})
export class SelectLanguagesComponent implements OnInit {
  @Input() languages: Array<any>;

  constructor() {
  }

  ngOnInit() {
  }

  selectLanguage(language: string): void {
    localStorage.setItem('Accept-Language', language);
  }

}
