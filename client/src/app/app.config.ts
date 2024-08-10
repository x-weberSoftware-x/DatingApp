import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { provideToastr } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    //for dropdown
    provideAnimations(),
    //gives toast messages
    provideToastr({
      //position for toast to show at
      positionClass: 'toast-bottom-right'
    })
  ]
};
