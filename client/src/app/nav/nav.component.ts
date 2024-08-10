import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService);

  //initialize an empty object
  model: any = {};

  login() {
    //returns a observable so we need to subscribe
    this.accountService.login(this.model).subscribe(
      {
        //what to do next
        next: response => {
          console.log(response);
        },
        //what to do if there is an error
        error: error => console.log(error)
      }
    );
  }

  logout(){
    this.accountService.logout();
  }

}
