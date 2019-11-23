import {Component, Inject, LOCALE_ID, Pipe, PipeTransform} from '@angular/core';
import {LoadingService} from './services/loading.service';
import {DomSanitizer} from '@angular/platform-browser';
import {RouterOutlet} from '@angular/router';
import {fadeAnimation} from './animations';
import {Language, LanguageService} from './services/language.service';

@Pipe({name: 'safe'})
export class SafePipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) {
  }

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

  languages: Array<Language>;

  constructor(@Inject(LOCALE_ID) localeId: string,
              private languageService: LanguageService,
              private loadingService: LoadingService) {
    this.languages = this.languageService.getAllAppLanguages();
    localStorage.setItem('LocaleId', localeId);
  }

  isLoading() {
    return this.loadingService.isLoading();
  }

  prepareRoute(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData.animation;
  }
}
