import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonThumbnailComponent } from './lesson-thumbnail.component';

describe('LessonThumbnailComponent', () => {
  let component: LessonThumbnailComponent;
  let fixture: ComponentFixture<LessonThumbnailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LessonThumbnailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonThumbnailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
