import { Routes } from '@angular/router';
import { ClimbList } from './components/climb-list/climb-list.component';
import { ClimbDetailComponent } from './components/climb-detail/climb-detail.component';
import { MyClimbs } from './components/my-climbs/my-climbs.component';
import { MyProjects } from './components/my-projects/my-projects.component';
//import { AddClimb } from './components/add-climb/add-climb.component';       // placeholder
//import { EditClimb } from './components/edit-climb/edit-climb.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/climbs', pathMatch: 'full' },
  { path: 'climbs', component: ClimbList, canActivate: [AuthGuard] },
  { path: 'climb-detail/:id', component: ClimbDetailComponent, canActivate: [AuthGuard] },
  { path: 'my-climbs', component: MyClimbs, canActivate: [AuthGuard] },
  { path: 'my-projects', component: MyProjects, canActivate: [AuthGuard] },

  // Admin-only routes
  /* { 
     path: 'add-climb', 
     component: AddClimb,       // must match the class name exactly
     canActivate: [AuthGuard], 
     data: { roles: ['admin'] } 
   },
   { 
     path: 'edit-climb/:id', 
     component: EditClimb, 
     canActivate: [AuthGuard], 
     data: { roles: ['admin'] } 
   }, */
];
