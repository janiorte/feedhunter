import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }
  error(msg: string) {
    alertify.error(msg);
  }

  success(msg: string) {
    alertify.success(msg);
  }
}
