import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CoursesBrowseComponent } from './courses-browse.component';

describe('CoursesBrowseComponent', () => {
  let component: CoursesBrowseComponent;
  let fixture: ComponentFixture<CoursesBrowseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CoursesBrowseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CoursesBrowseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
