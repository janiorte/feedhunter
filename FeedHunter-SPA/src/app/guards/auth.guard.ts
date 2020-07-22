import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthService } from '../service/auth.service';
import { AlertifyService } from '../service/alertify.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate{
    constructor(
        private authService: AuthService,
        private router: Router,
        private alertify: AlertifyService) {}

    canActivate(route: ActivatedRouteSnapshot): boolean {
        const roles = route.data?.roles as Array<string>;
        if (roles) {
            const match = this.authService.roleMatch(roles);
            if (match) {
                return true;
            }
        }

        this.router.navigate(['']);
        this.alertify.error('You are not allowed to access');
    }
}
