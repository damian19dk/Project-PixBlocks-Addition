import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpClient } from '@angular/common/http';
import { AuthenticationService } from '../services/authentication.service';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { LoadingService } from '../services/loading.service';

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {

    private returnUrl: string;

    constructor(public route: ActivatedRoute,
        public router: Router,
        public authenticationService: AuthenticationService,
        public loadingService: LoadingService,
        public http: HttpClient) {
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || 'unauthorized';
    }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(request).pipe(
            catchError(err => {
                if (err.status === 401 || err.status === 403) {
                    this.loadingService.unload();


                    if (request.url.includes("cancel")) {
                        this.router.navigate([this.route.snapshot.queryParams['returnUrl'] || '/']);
                    }

                    this.authenticationService.clearUserData();


                    this.router.navigate([this.returnUrl]);
                }

                const error = err.error.message || err.statusText;
                return throwError(error);
            }));
    }
}