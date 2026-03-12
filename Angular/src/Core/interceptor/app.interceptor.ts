import { HttpErrorResponse, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from "@angular/common/http";
import { CoreService } from "../Services/core.service";
import { AuthService } from "../Services/auth.service";
import { SweetAlertServices } from "../Services/sweetAlert.service";
import { Router } from "@angular/router";
import { inject } from "@angular/core";
import { catchError, finalize, of, throwError } from "rxjs";

export const appInterceptor: HttpInterceptorFn = (
  httpRequest,
  next: HttpHandlerFn
) => {
  const coreService = inject(CoreService);
  const authService = inject(AuthService);
  const sweetAlertMessageService = inject(SweetAlertServices);
  const router = inject(Router);

  const accessToken = coreService.getCookie('access_token');

  let clonedRequest = httpRequest;

  // Clone request to add headers
  if (accessToken) {
    clonedRequest = clonedRequest.clone({
      headers: clonedRequest.headers.set('Authorization', `Bearer ${accessToken}`),
    });
  }
 
  


  // Pass the cloned request with the updated header to the next handler
  return next(clonedRequest).pipe(
  catchError((error: HttpErrorResponse) => {

    if (error.status === 401) {
      return handle401Error(httpRequest, next, authService, router, coreService);
    }

    if (error.status === 400) {
      sweetAlertMessageService.customMessage(error.error[0], 'error');
      return throwError(() => error);
    }

    if (error.status === 404) {
      sweetAlertMessageService.customMessage(error.error[0], 'error');
      return throwError(() => error);
    }

    if (error.status === 0) {
      sweetAlertMessageService.customMessage('Connection time out', 'error');
      return throwError(() => error);
    }

    return throwError(() => error);
  })
);

}
// Handle 401 Error and refresh token
function handle401Error(
  request: HttpRequest<any>,
  next: HttpHandlerFn,
  authService: AuthService,
  router: Router,
  coreService: CoreService
) {
  authService.logOut();
  router.navigate(['/signin']);

  return throwError(() => new Error('Unauthorized'));
}
function next(clonedRequest: any) {
    throw new Error("Function not implemented.");
}

