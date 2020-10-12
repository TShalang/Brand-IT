import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmdesignComponent } from './confirmdesign.component';

describe('ConfirmdesignComponent', () => {
  let component: ConfirmdesignComponent;
  let fixture: ComponentFixture<ConfirmdesignComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmdesignComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmdesignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
