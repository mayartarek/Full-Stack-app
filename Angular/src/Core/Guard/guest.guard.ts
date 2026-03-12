import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { CoreService } from '../Services/core.service';

export const guestGuard: CanActivateFn = () => {

  const router = inject(Router);
  const _coreService=inject(CoreService)
        const token = _coreService.getCookie('access_token');

  if (token) {
    router.navigate(['/']);
    return false;
  }

  return true;
};