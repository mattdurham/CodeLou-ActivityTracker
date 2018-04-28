import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { ActivityTrackerService } from '../../services/activity-tracker.service';

@Component({
  selector: 'app-nav-menu',
  providers: [AuthService, ActivityTrackerService],
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public name: string;
  public isAlreadyUser = false;

  constructor(public authService: AuthService, public activityService: ActivityTrackerService) {
    authService.handleAuthentication();
    this.activityService.getMe().subscribe(result => {
      this.name = result.firstName + ' ' + result.lastName;
      this.isAlreadyUser = true;
    });
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
    this.activityService.createUser().subscribe(result => {
      this.name = result.firstName + ' ' + result.lastName;
    });
  }
}
