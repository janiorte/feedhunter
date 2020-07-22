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
  decodedToken: any;

  constructor(private http: HttpClient) { }

  register(user: User) {
    return this.http.post(this.baseUrl + 'auth/register', user);
  }

  login(user: User) {
    return this.http.post(this.baseUrl + 'auth/login', user).pipe(
      map((response: any) => {
        localStorage.setItem('token', response.token);
        this.decodedToken = this.jwtHelper.decodeToken(response.token);
        this.currentUser = ({
            userName: this.decodedToken.unique_name
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
    this.decodedToken = null;
    this.currentUser = null;
  }

  roleMatch(allowedRoles): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;
    allowedRoles.forEach(element => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }

}
