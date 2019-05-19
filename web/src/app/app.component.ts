import { Component, LOCALE_ID, Inject } from '@angular/core';
import { LoadingService } from './services/loading.service';

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
