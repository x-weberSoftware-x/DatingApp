import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  //get the url we set up in our environment file
  baseUrl = environment.apiUrl;
  //save our members to an signal array
  members = signal<Member[]>([]);


  getMembers(){
    return this.http.get<Member[]>(this.baseUrl + 'users').subscribe({
      next: members => this.members.set(members)
    });
  }

  getMember(username: string){
    //get our member based on username from our signal of members
    const member = this.members().find(x => x.userName === username);
    //of() returns the member as an observable
    if(member !== undefined) return of(member);

    //if we didnt have the member in our member signal array then run the get
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  updateMember(member : Member){
    
    return this.http.put(this.baseUrl + 'users', member).pipe(
      tap(() => {
        //tap can get the member so we can update it, if we already have a member with this username the replace that member with
        //this new one (so update) otherwise its just going to be the original member so we dont update any other members
        this.members.update(members => members.map(m => m.userName === member.userName ? member : m));
      })
    );
  }

}
