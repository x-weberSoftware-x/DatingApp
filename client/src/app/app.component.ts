import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_services/account.service';
import { HomeComponent } from "./home/home.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  private accountService = inject(AccountService);

  
  ngOnInit(): void {
    this.setCurrentUser();

  }

  setCurrentUser(){
    //get our user from the browser local storage
    const userString = localStorage.getItem('user');
    //if we dont have a user string then return out of this function
    if (!userString) return;
    //parse userstring in to a json object
    const user = JSON.parse(userString);
    //set our signal to the user
    this.accountService.currentUser.set(user);
  }

 
}
