import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Testfe26Component } from './testfe26.component';

describe('Testfe26Component', () => {
  let component: Testfe26Component;
  let fixture: ComponentFixture<Testfe26Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Testfe26Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Testfe26Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
