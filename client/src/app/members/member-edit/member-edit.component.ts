import { Component, HostListener, inject, OnInit, ViewChild, viewChild } from '@angular/core';
import { Member } from '../../_models/member';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule, FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit{
  //view child is an angular feature where we can access a selector in our html for this component since the html is considered a child
  //we use whatever we named it with the # in the html. In this case we named the form #editForm
  @ViewChild('editForm') editForm?: NgForm;
  //listen to the  browser before unload event
  @HostListener('window:beforeunload', ['$event']) notify($event:any){
    $event.retunValue = true;
  }

  member?: Member;
  private toastr = inject(ToastrService);
  private accountService = inject(AccountService);
  private memberService = inject(MembersService);

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    const user = this.accountService.currentUser();
    if (!user) return;

    this.memberService.getMember(user.username).subscribe({
      next: member => this.member = member
    });
  }

  updateMember(){
    this.memberService.updateMember(this.editForm?.value).subscribe({
      //we dont get anything back in a put(update) so just make a function that tell us it was succesful
      next: () => {
        this.toastr.success('Profile Updated Successfully');
        //since we have saved our changes at this point lets reset the form so the button is disabled and the info message dissappears again
        this.editForm?.reset(this.member);
      }
    });  
  }

}
