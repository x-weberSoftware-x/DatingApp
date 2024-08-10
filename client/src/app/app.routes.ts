import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guards/auth.guard';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {
        //this is a dummy route that lets us set up funcionality for all routes in the children array
        //this way we do not have to type the same attributes on all the routes
        path: '',
        runGuardsAndResolvers: 'always',
        //canactivate sets these routes to use our guards we set up
        canActivate: [authGuard],
        children: [
            {path: 'members', component: MemberListComponent},
            //this route will take a dynamic id
            {path: 'members/:id', component: MemberDetailComponent},
            {path: 'lists', component: ListsComponent},
            {path: 'messages', component: MessagesComponent}
        ]
    }, 
    //wildcard for if none of the above routes match, for instance if they pick a route we do not have
    {path: '**', component: HomeComponent, pathMatch: 'full'}

];
