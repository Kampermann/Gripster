import { Component, signal } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { ClimbCard } from './climb-card/climb-card';
import { ClimbList } from './climb-list/climb-list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu'

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, ClimbCard, ClimbList, MatToolbarModule, MatButtonModule, MatIconModule, MatMenuModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ClimbingAppAngular');
}
