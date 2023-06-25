import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FriedRollsComponent } from './fried-rolls.component';

describe('FriedRollsComponent', () => {
  let component: FriedRollsComponent;
  let fixture: ComponentFixture<FriedRollsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FriedRollsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FriedRollsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
