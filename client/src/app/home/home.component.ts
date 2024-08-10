import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  //adding this means we can make http requests inside this component
  http = inject(HttpClient);
  registerMode = false;
  users: any;

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean){
    //way of sending data from child to parent, this is the parent
    //we are recieving the false that is sent
    this.registerMode = event;
  }

   //specify which url for the get requests, this returns a observable of the response body as a json object
  //subscribe to the observable, since this is an http request it will auto unsubscribe when completed
  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe({
      //what to do next, in this case set users equal to the response from api/users
      next: (response) => { this.users = response },
      //what to do if there is an error
      error: (error) => { console.log(error) },
      //what to do when it completes
      complete: () => { console.log('Request has completed') }
    });
  }

}
