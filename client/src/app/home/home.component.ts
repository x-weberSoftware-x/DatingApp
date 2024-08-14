import { Component } from '@angular/core';
import { RegisterComponent } from "../register/register.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{
  registerMode = false;

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean){
    //way of sending data from child to parent, this is the parent
    //we are recieving the false that is sent
    this.registerMode = event;
  }

}
