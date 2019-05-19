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
    this.loading = true;
  }

  unload() {
    setTimeout(() => {this.loading = false;}, 1000);
  }

  isLoading() {
    return this.loading;
  }
}
