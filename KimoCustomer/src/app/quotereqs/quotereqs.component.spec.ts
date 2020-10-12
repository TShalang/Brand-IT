import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuotereqsComponent } from './quotereqs.component';

describe('QuotereqsComponent', () => {
  let component: QuotereqsComponent;
  let fixture: ComponentFixture<QuotereqsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuotereqsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuotereqsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
