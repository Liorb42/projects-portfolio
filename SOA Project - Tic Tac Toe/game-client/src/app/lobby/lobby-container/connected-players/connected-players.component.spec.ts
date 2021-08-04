import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConnectedPlayersComponent } from './connected-players.component';

describe('ConnectedPlayersComponent', () => {
  let component: ConnectedPlayersComponent;
  let fixture: ComponentFixture<ConnectedPlayersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConnectedPlayersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConnectedPlayersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
