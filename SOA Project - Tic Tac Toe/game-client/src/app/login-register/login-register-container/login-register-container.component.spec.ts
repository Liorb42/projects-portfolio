import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogInRegisterContainerComponent } from './login-register-container.component';

describe('LogInRegisterContainerComponent', () => {
  let component: LogInRegisterContainerComponent;
  let fixture: ComponentFixture<LogInRegisterContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LogInRegisterContainerComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LogInRegisterContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
