import { Injectable } from '@angular/core';
import { io } from 'socket.io-client';

const config = {
  url: 'https://chat-service-sela-soa-project.azurewebsites.net',
  options: {},
};

@Injectable({
  providedIn: 'any',
})
export class ChatService {
  private socket = io(config.url, { reconnectionAttempts: 3, timeout: 100000 });

  constructor() {
    this.socket.on('connect', () => {
      console.log('connected to chat server with socket');
    });
    this.socket.on('disconnect', (reason) => {
      if (reason === 'io server disconnect') {
        this.socket.connect();
      } else {
        console.log(`socket disconnected from chat server. ${reason}`);
      }
    });
  }

  listenToEvent(eventName: string, handler: (param: any) => any): void {
    this.socket.on(eventName, handler);
  }

  emitEvent(
    eventName: string,
    arg?: any[],
    callback?: (param: any) => any
  ): void {
    this.socket.emit(eventName, arg, callback);
  }
}
