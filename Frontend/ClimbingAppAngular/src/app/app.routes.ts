import { Routes } from '@angular/router';
import { ClimbList } from './climb-list/climb-list';
import { EditClimb } from './edit-climb/edit-climb';
import { ClimbDetailComponent } from './climb-detail/climb-detail.component';
export const routes: Routes = [
    {path: 'climbs', component: ClimbList},
    {path: 'edit-climb/:id', component: EditClimb},
    {path: 'climb-detail/:id', component: ClimbDetailComponent}
];
