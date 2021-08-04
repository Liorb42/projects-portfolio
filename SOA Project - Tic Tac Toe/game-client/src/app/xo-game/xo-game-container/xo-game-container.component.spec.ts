import { ComponentFixture, TestBed } from '@angular/core/testing';

import { XoGameContainerComponent } from './xo-game-container.component';

describe('XoGameContainerComponent', () => {
  let component: XoGameContainerComponent;
  let fixture: ComponentFixture<XoGameContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ XoGameContainerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(XoGameContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
