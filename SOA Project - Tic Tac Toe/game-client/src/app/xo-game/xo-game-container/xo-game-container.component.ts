import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-xo-game-container',
  templateUrl: './xo-game-container.component.html',
  styleUrls: ['./xo-game-container.component.css'],
})
export class XoGameContainerComponent implements OnInit {
  roomName: String;
  constructor() {}

  ngOnInit(): void {
    this.roomName = JSON.parse(sessionStorage.getItem('game')).id.toString();
  }
  ngOnDestroy(): void {
    sessionStorage.setItem('game', '');
  }
}
