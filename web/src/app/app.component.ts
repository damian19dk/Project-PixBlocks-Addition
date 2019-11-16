import { Component, LOCALE_ID, Inject, Pipe, PipeTransform } from '@angular/core';
import { LoadingService } from './services/loading.service';
import { DomSanitizer } from '@angular/platform-browser';
import {RouterOutlet} from '@angular/router';
import {fadeAnimation} from './animations';

@Pipe({ name: 'safe' })
export class SafePipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) { }
  transform(url) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  animations: [
    fadeAnimation
  ]
})
export class AppComponent {
  languages = [
    { code: 'en', label: 'English' },
    { code: 'pl', label: 'Polski' }
  ];

  constructor(@Inject(LOCALE_ID) localeId: string,
              private loadingService: LoadingService) {
      localStorage.setItem('LocaleId', localeId);
  }

  isLoading() {
    return this.loadingService.isLoading();
  }

  prepareRoute(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData.animation;
  }
}
