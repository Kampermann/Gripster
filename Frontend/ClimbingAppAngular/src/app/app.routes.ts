import { Routes } from '@angular/router';
import { ClimbList } from './climb-list/climb-list';
import { ClimbDetailComponent } from './climb-detail/climb-detail.component';

export const routes: Routes = [
  { path: 'climbs', component: ClimbList },
  { path: 'climb-detail/:id', component: ClimbDetailComponent },
];
