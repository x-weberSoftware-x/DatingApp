import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { inject } from '@angular/core';

//this will stop people from typing in routes ( ex /members )  in the url 
// to access the pages they shouldnt if they are not logged in
//if this returns false it stops them, true lets it procceed
export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  //check if their is a current user using our signal (this checks if a user is logged in)
  if(accountService.currentUser()) {
    return true;
  }else{
    toastr.error('You shall not pass!');
    return false;
  }
    
};
