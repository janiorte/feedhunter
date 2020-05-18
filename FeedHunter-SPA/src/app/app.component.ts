import { Component } from '@angular/core';
import { TimeagoIntl } from 'ngx-timeago';
import { strings as spanishStrings } from 'ngx-timeago/language-strings/es';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FeedHunter-SPA';

  constructor(intl: TimeagoIntl) {
    intl.strings = spanishStrings;
    intl.changes.next();
  }
}
