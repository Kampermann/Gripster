import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClimbService } from '../services/climb-service';
import { UserRouteService } from '../services/user-route.service';
import { Climb } from '../model/climb';

@Component({
  selector: 'app-climb-detail',
  templateUrl: './climb-detail.component.html',
  styleUrls: ['./climb-detail.component.css']
})
export class ClimbDetailComponent implements OnInit {
  userRoutes: any[] = [];
  loading = true;
    error: string | null = null;

@Input() climb!: Climb;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private climbService: ClimbService,
    private userRouteService: UserRouteService
  ) {}

ngOnInit(): void {
    this.loadClimb();
  }

    loadClimb(): void {
    this.loading = true;
    console.log('Fetching climbs from:', this.climbService.baseUrl);
    this.climbService.getClimb(this.climb.id).subscribe({
      next: (climbs) => {
        console.log('Received climbs:', climbs);
        this.loading = false;
      },
      error: (error) => {
        console.error('Error fetching climbs:', error);
        this.error = error.message || 'Failed to load climbs';
        this.loading = false;
      }
    });
  }

  loadUserRoutes(): void {
    this.userRouteService.getUserRoutesByRouteId(this.climb.id).subscribe({
      next: (data) => {
        this.userRoutes = data;
      },
      error: (err) => {
        console.error('Error loading user routes:', err);
      }
    });
  }

  updateClimbStatus(userId: number, status: string): void {
    this.userRouteService.insertUserRouteByID(userId, this.climb.id, status).subscribe({
      next: () => {
        alert(`Climb marked as ${status}!`);
        this.loadUserRoutes();
      },
      error: (err) => {
        console.error('Error updating status:', err);
        alert('Failed to update climb status');
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/climbs']);
  }
}
