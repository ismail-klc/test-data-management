import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserLogin } from '../../models/userLogin';
import { AuthService } from '../../services/auth.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email:string;
  password:string;
  loginForm:FormGroup;
  constructor(private formBuilder: FormBuilder,
    private authService:AuthService) { }

  ngOnInit() {
    this.createLoginForm();
  }

  createLoginForm(){
    this.loginForm = this.formBuilder.group(
      {
        email:["",Validators.required],
        password:["",[Validators.required,
        Validators.minLength(4),
        Validators.maxLength(8)]],
      }
    )
  }

  loginBtn() {
    
    if(this.loginForm.valid) {
      let user = new UserLogin();
      user.email  = this.loginForm.get("email").value;
      user.password = this.loginForm.get("password").value
      this.authService.login(user);
      console.log(user.email)
    }
  }
}
