import { Component, LOCALE_ID, Inject, Pipe, PipeTransform } from '@angular/core';
import { LoadingService } from './services/loading.service';
import { DomSanitizer } from '@angular/platform-browser';

@Pipe({ name: 'safe' })
export class SafePipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) { }
  transform(url) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }
}

@Pipe({
  name: 'minuteSeconds'
})
export class MinuteSecondsPipe implements PipeTransform {

    transform(value: number): string {
       const minutes: number = Math.floor(value / 60);
       const hours: number = Math.floor(value / 3600);

       const secondsString = (value - minutes * 60) + 's';
       let minutesString = '';
       let hoursString = '';
       if (hours > 0) {
         hoursString = hours + 'h ';
       }
       if (minutes > 0) {
         minutesString = minutes + 'm ';
       }
       return hoursString + minutesString + secondsString;
    }

}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  languageList = [
    { code: 'en', label: 'English' },
    { code: 'pl', label: 'Polski' }
  ];

  constructor(@Inject(LOCALE_ID) protected localeId: string,
              private loadingService: LoadingService) {
  }

  isLoading() {
    return this.loadingService.isLoading();
  }
}
