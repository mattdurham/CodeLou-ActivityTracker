import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ActivityTrackerService } from '../services/activity-tracker.service';

@Component({
  selector: 'app-nav-menu',
  providers: [AuthService, ActivityTrackerService],
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  public name: string;

  constructor(private authService: AuthService, private activityService: ActivityTrackerService) {
    authService.handleAuthentication();
    this.activityService.getMe().subscribe(
      result => {
        this.name = result.FirstName + ' ' + result.LastName;
      }
    );
  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  login() {
    this.authService.login();
  }

  createUser() {
    this.activityService.createUser().subscribe(
      result => {
        this.name = result.FirstName + ' ' + result.LastName;
      }
    );
  }
}
