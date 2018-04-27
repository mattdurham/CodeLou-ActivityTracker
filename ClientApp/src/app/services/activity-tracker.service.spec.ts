import { TestBed, inject } from '@angular/core/testing';

import { ActivityTrackerService } from './activity-tracker.service';

describe('ActivitityTrackerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ActivityTrackerService]
    });
  });

  it('should be created', inject([ActivityTrackerService], (service: ActivityTrackerService) => {
    expect(service).toBeTruthy();
  }));
});
