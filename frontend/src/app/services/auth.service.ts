import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { UserLogin } from '../models/userLogin';
import { UserRegister } from '../models/userRegister';
import { environment } from '../../environments/environment';
import { AlertifyService } from './alertify.service';
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  path = environment.API_URL + "auth/";
  userToken: any;
  decodedToken: any;
  TOKEN_KEY = "token"
  helper = new JwtHelperService();

  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private alertifyService:AlertifyService
  ) { }

  login(loginUser: UserLogin) {
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json");
    this.httpClient
      .post(this.path + "login", loginUser, { headers: headers }).subscribe
      (data => { 
        let token = data["token"]
        this.saveToken(token);
        this.userToken = token;
        this.decodedToken = this.helper.decodeToken(token);
        this.alertifyService.success("Logined successfully");
        this.router.navigateByUrl("/dashboard");
      },
      (error => {
        this.alertifyService.error(error["error"]);
      }
      ));
  }

  saveToken(token) {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  loggedIn() { 
    const token = localStorage.getItem(this.TOKEN_KEY);
    return !!token;
  }

  logOut() {
    localStorage.removeItem(this.TOKEN_KEY)
    this.alertifyService.error("Logged out successfully!");
    this.router.navigateByUrl("/login");
  }

  getCurrentUserId(){
    const token = localStorage.getItem(this.TOKEN_KEY);
    return this.helper.decodeToken(token).nameid
  }

}
