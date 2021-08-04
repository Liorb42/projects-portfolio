import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BtnRequestGameComponent } from './btn-request-game.component';

describe('BtnRequestGameComponent', () => {
  let component: BtnRequestGameComponent;
  let fixture: ComponentFixture<BtnRequestGameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BtnRequestGameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BtnRequestGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
