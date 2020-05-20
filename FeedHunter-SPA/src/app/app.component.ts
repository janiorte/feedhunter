import { Component, OnInit } from '@angular/core';
import { TimeagoIntl } from 'ngx-timeago';
import { strings as spanishStrings } from 'ngx-timeago/language-strings/es';
import { User } from './model/user';
import { AuthService } from './service/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'FeedHunter-SPA';

  constructor(private intl: TimeagoIntl, private auth: AuthService) {
    intl.strings = spanishStrings;
    intl.changes.next();
  }

  ngOnInit() {
    const jwtHelper = new JwtHelperService();
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = jwtHelper.decodeToken(token);
      this.auth.currentUser = {
        userName : decodedToken.unique_name
      } as User;
    }
  }
}
