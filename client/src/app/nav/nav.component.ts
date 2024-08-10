import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService);
  private router = inject(Router);
  private toastr = inject(ToastrService);

  //initialize an empty object
  model: any = {};

  login() {
    //returns a observable so we need to subscribe
    this.accountService.login(this.model).subscribe(
      {
        //what to do next, in this case go to /members after login
        next: () => this.router.navigateByUrl('/members'),
        //what to do if there is an error, in this case send a toast (popup message to user), the error message located in the error object
        error: error => this.toastr.error(error.error)
      }
    );
  }

  logout(){
    this.accountService.logout();
    //navigate to home page on logout
    this.router.navigateByUrl('/');
  }

}
