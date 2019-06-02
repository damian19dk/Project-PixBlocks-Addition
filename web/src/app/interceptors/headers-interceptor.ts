import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { AuthenticationService } from '../services/authentication.service';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';

@Injectable()
export class HeadersInterceptor implements HttpInterceptor {
    constructor(public authenticationService: AuthenticationService) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        request = request.clone({
            setHeaders: {
                Authorization: `Bearer ${this.authenticationService.getToken()}`,
                "Access-Control-Allow-Origin": environment.baseUrl
            }
        });
        return next.handle(request);
    }
}