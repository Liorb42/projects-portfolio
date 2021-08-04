import { Injectable } from '@angular/core';
import { io } from 'socket.io-client';

const config = {
  // url: 'https://game-service-sela-soa-project.azurewebsites.net',
  url: 'http://localhost:5000',

  options: {},
};

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private token = JSON.parse(sessionStorage.getItem('token'));

  private socket = io(config.url, {
    query: { token: this.token },
    timeout: 100000,
    reconnectionAttempts: 3,
  });

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

  disconnect(): void {
    this.socket.disconnect();
  }
}
