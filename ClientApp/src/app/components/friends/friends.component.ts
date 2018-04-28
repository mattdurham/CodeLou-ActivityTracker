import { ActivityTrackerService } from './../../services/activity-tracker.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-friends',
  providers: [ActivityTrackerService],
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {
  constructor(private activityTrackerService: ActivityTrackerService) {}
  public users: User[];
  ngOnInit() {
    this.activityTrackerService.getUsers().subscribe(result => {
      this.users = result;
    });
  }
}
