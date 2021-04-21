import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { CanActivate } from '@angular/router/';
import { AlertifyService } from './alertify.service';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(private authservice: AuthService, 
    private router: Router, 
    private alertify:AlertifyService) {}
  path: ActivatedRouteSnapshot[];
  route: ActivatedRouteSnapshot;

    canActivate(route:ActivatedRouteSnapshot,state:RouterStateSnapshot): boolean {
      let logged = this.authservice.loggedIn();
  
      if (logged) {
        return true;
      }

      this.router.navigate(["login"]);
      this.alertify.error("You need to login!");
      return false;
    }
}
