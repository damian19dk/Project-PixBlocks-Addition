import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VideoBrowseComponent } from './video-browse.component';

describe('VideoBrowseComponent', () => {
  let component: VideoBrowseComponent;
  let fixture: ComponentFixture<VideoBrowseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VideoBrowseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VideoBrowseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
