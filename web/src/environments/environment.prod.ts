import { HttpHeaders } from '@angular/common/http';

export const environment = {
  production: true,
  baseUrl: 'https://pixblocksaddition.azurewebsites.net',
  headers: new HttpHeaders().set('Access-Control-Allow-Origin', '*')
};
