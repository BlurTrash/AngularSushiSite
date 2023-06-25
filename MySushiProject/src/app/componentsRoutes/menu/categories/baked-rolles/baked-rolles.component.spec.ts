import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BakedRollesComponent } from './baked-rolles.component';

describe('BakedRollesComponent', () => {
  let component: BakedRollesComponent;
  let fixture: ComponentFixture<BakedRollesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BakedRollesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BakedRollesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
