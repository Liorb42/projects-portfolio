import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoggedInPlayerComponent } from './logged-in-player.component';

describe('LoggedInPlayerComponent', () => {
  let component: LoggedInPlayerComponent;
  let fixture: ComponentFixture<LoggedInPlayerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoggedInPlayerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoggedInPlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
