import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../model/user';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  currentUser: User;
  jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient) { }

  register(user: User) {
    return this.http.post(this.baseUrl + 'auth/register', user);
  }

  login(user: User) {
    return this.http.post(this.baseUrl + 'auth/login', user).pipe(
      map((response: any) => {
        localStorage.setItem('token', response.token);
        const decodedToken = this.jwtHelper.decodeToken(response.token);
        this.currentUser = ({
            userName: decodedToken.unique_name
          } as User);
      })
    );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUser = null;
  }

}
