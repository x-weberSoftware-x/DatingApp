import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { BusyService } from '../_services/busy.service';
import { delay, finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  //start our busy service when request goes out
  const busyService = inject(BusyService);
  busyService.busy();

  //now that we have our request let stop the busy service after a 1 second delay
  return next(req).pipe(
    delay(1000),
    finalize(() => busyService.idle())
  );
};
