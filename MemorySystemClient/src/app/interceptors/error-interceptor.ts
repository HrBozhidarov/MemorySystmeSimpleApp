import { HttpInterceptor, HttpRequest, HttpEvent, HttpHandler, HttpErrorResponse } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Router } from '@angular/router';

import { ShareAuthService } from '../share/services/share-auth-service';
import { IdentityService } from '../services/identity/identity.service';

export class ErrorInterceptor implements HttpInterceptor {
    constructor(
        private router: Router, 
        private shareAuthService: ShareAuthService,
        private identityService: IdentityService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
            .pipe(catchError((error: HttpErrorResponse) => {
                if (error.status === 401) {
                    this.shareAuthService.updatedDataSelection(this.identityService.isLoggedIn());

                    this.router.navigate(['/login']);
                }
                return throwError(error);
            }))
    }
}
