import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  private loading: boolean;

  constructor() {
    this.loading = false;
  }

  load() {
    setTimeout(() => {this.loading = true; }, 0);
    setTimeout(() => {this.loading = false; }, 60000);
  }

  unload() {
    setTimeout(() => {this.loading = false; }, 160);
  }

  isLoading() {
    return this.loading;
  }
}
