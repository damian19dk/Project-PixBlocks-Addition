import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseThumbnailComponent } from './course-thumbnail.component';

describe('CourseThumbnailComponent', () => {
  let component: CourseThumbnailComponent;
  let fixture: ComponentFixture<CourseThumbnailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CourseThumbnailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseThumbnailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
