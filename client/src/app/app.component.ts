import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  //adding this means we can make http requests inside this component
  http = inject(HttpClient);
  title = 'DatingApp';
  users: any;

  //specify which url for the get requests, this returns a observable of the response body as a json object
  //subscribe to the observable, since this is an http request it will auto unsubscribe when completed
  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/users').subscribe({
      //what to do next, in this case set users equal to the response from api/users
      next: (response) => { this.users = response},
      //what to do if there is an error
      error: (error) => { console.log(error) },
      //what to do when it completes
      complete: () => { console.log('Request has completed')}
    });
  }
}
