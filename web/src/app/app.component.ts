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

@Pipe({name: 'shortText'})
export class ShortTextPipe implements PipeTransform {
  transform(text: string): string {
    return text.length > 75 ? text.substring(0, 72) + '...' : text;
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
    if (localStorage.getItem('Accept-Language') === null) {
      localStorage.setItem('Accept-Language', window.navigator.language);
    }
  }

  isLoading() {
    return this.loadingService.isLoading();
  }

  prepareRoute(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData.animation;
  }
}
