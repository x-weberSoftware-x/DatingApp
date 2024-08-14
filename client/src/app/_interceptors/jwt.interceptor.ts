import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../_services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(AccountService);

  //this intercepts before the req and sets our authorization to our token this way we dont have to set this authorization with every header
  if (accountService.currentUser()){
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accountService.currentUser()?.token}`
      }
    })
  }

  return next(req);
};
