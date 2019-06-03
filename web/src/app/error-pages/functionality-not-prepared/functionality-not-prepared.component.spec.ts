import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionalityNotPreparedComponent } from './functionality-not-prepared.component';

describe('FunctionalityNotPreparedComponent', () => {
  let component: FunctionalityNotPreparedComponent;
  let fixture: ComponentFixture<FunctionalityNotPreparedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FunctionalityNotPreparedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FunctionalityNotPreparedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
