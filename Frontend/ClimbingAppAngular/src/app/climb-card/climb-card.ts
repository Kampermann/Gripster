import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Climb } from '../model/climb';
import { Router } from '@angular/router';  
import { ClimbService } from '../services/climb-service';

@Component({
  selector: 'app-climb-card',
  imports: [CommonModule],
  templateUrl: './climb-card.html',
  styleUrl: './climb-card.css',
})
export class ClimbCard {
  constructor(
    private router: Router,
    private climbService: ClimbService,
  ) {
    console.log('Router injected:', this.router); // Debug log
  }

  @Input() climb!: Climb;
  @Output() delete = new EventEmitter<number>();

  deleteClimb() {
    if (this.climb) {
      this.delete.emit(this.climb.id);
    }
  }

  editClimb(id: number) {
    // Navigate to the edit page for the climb
    this.router.navigate(['/edit-climb', id]);
  }

  viewClimbDetails(): void {
    console.log('Navigating to climb detail:', this.climb.id);
    console.log('Router state before navigation:', this.router.url);
    
    this.router.navigate(['/climb', this.climb.id]).then(
      success => console.log('Navigation successful:', success),
      error => console.error('Navigation error:', error)
    );
  }

  updateClimbStatus(userId: number, routeId: number, status: string): void {
    console.log('Update status clicked:', userId, routeId, status);
    this.climbService.updateClimbStatus(userId, this.climb.id, status).subscribe({
      next: () => {
        console.log('Status updated successfully');
        alert(`Climb marked as ${status}!`);
      },
      error: (err: any) => {
        console.error('Error updating status:', err);
        alert('Failed to update climb status. Check console for details. Sent ' + JSON.stringify({ userId, routeId, status }) + err.message);
      }
    });
  }

  debugClick(): void {
    console.log('Link clicked! Navigating to climb:', this.climb.id);
    alert('Navigating to climb ' + this.climb.id);
  }
}
