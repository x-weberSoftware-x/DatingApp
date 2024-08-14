import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

//service is created when browser loads or refreshes
export class AccountService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  //create a signal that can be a user or null, give it a initial value of null
  currentUser = signal<User | null>(null)

  login(model: any){
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        if (user){
          //save user to the browser local storage
          localStorage.setItem('user', JSON.stringify(user));
          //set our signal to the user
          this.currentUser.set(user);
        }
      })
    );
  }


  register(model: any){
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        if (user){
          //save user to the browser local storage
          localStorage.setItem('user', JSON.stringify(user));
          //set our signal to the user
          this.currentUser.set(user);
        }
        return user;
      })
    );
  }


  logout(){
    //remove the user
    localStorage.removeItem('user');
    //set our signal value back to null since we logged out
    this.currentUser.set(null);
  }
}
