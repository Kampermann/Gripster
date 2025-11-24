import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClimbDetailComponent } from './climb-detail/climb-detail.component';

const routes: Routes = [
  // ...existing routes...
  { path: 'climb/:id', component: ClimbDetailComponent },
  // ...existing routes...
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }