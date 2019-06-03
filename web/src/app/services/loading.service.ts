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
    setTimeout(() => {this.loading = true;}, 0);
  }

  unload() {
    setTimeout(() => {this.loading = false;}, 250);
  }

  isLoading() {
    return this.loading;
  }
}
