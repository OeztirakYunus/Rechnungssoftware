import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliverynotesComponent } from './deliverynotes.component';

describe('DeliverynotesComponent', () => {
  let component: DeliverynotesComponent;
  let fixture: ComponentFixture<DeliverynotesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeliverynotesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliverynotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
