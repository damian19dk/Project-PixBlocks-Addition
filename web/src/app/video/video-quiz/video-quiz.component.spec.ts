import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VideoQuizComponent } from './video-quiz.component';

describe('VideoQuizComponent', () => {
  let component: VideoQuizComponent;
  let fixture: ComponentFixture<VideoQuizComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VideoQuizComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VideoQuizComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
