import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LobbyContainerComponent } from './lobby-container.component';

describe('LobbyContainerComponent', () => {
  let component: LobbyContainerComponent;
  let fixture: ComponentFixture<LobbyContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LobbyContainerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LobbyContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
