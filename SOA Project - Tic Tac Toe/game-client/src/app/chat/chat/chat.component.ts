import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { PlayerModel } from 'src/app/models/player.model';
import { ChatService } from 'src/app/services/chat.service';
import { PlayerInfoService } from 'src/app/services/player-info.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent implements OnInit {
  @Input()
  roomName: String;
  player: PlayerModel;
  subscription: Subscription;

  @ViewChild('msgsContainer') msgsContainer;
  @ViewChild('msgText') msgText;

  newMsgElement: any;
  currentSender: String;
  currentMsg: String;

  isServerOn: Boolean;

  constructor(
    private chat: ChatService,
    private playerInfo: PlayerInfoService
  ) {}

  ngOnInit(): void {
    this.subscription = this.playerInfo.currentPlayer.subscribe(
      (player) => (this.player = player)
    );

    this.chat.listenToEvent('newMsgEvent', (data) => {
      this.createMsg(data);
    });
    this.chat.emitEvent('joinRoom', [this.roomName]);
  }

  private createMsg(data: any) {
    this.currentSender = data[0];
    this.currentMsg = data[1];
    this.newMsgElement = document.createElement('div');
    this.newMsgElement.innerHTML = `<span>${this.currentSender}: ${this.currentMsg}</span>`;
    this.msgsContainer.nativeElement.insertBefore(
      this.newMsgElement,
      this.msgsContainer.nativeElement.firstChild
    );
  }
  sendMsg(msg: String) {
    this.currentMsg = msg;
    this.chat.emitEvent('sendMsgToRoom', [
      this.roomName,
      this.player.name,
      this.currentMsg,
    ]);
    this.msgText.nativeElement.value = '';
  }

  ngOnDestroy(): void {
    this.chat.emitEvent('leaveRoom', [this.roomName]);
  }
}
