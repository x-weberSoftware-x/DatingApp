import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})


export class RegisterComponent {
  //pre angular 17.3 way of getting data (in this case users) from parent component (in this case home component)
  // @Input() usersFromHomeComponent: any;
  // post 17.3 way, this is an input signal, we can make this required so we get errors if not used
  // usersFromHomeComponent = input.required<any>();

  //pre angular 17.3 way of sending data from child to parent
  //emit an event
  // @Output() cancelRegister = new EventEmitter();
  //post 17.3 way
  cancelRegister = output<boolean>();
  private accountServices = inject(AccountService);
  model: any = {}


  register(){
    this.accountServices.register(this.model).subscribe({
        next: response => {
          console.log(response);
          this.cancel();
        },
        error: error => console.log(error)
      }
    );
  }


  cancel(){
    //emit false
    this.cancelRegister.emit(false);
  }
}
