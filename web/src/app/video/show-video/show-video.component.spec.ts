import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowVideoComponent } from './show-video.component';

describe('ShowVideoComponent', () => {
  let component: ShowVideoComponent;
  let fixture: ComponentFixture<ShowVideoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowVideoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowVideoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
