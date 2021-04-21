import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserRegister } from '../../models/userRegister';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm:FormGroup;
  constructor(private formBuilder:FormBuilder) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm = this.formBuilder.group(
      {
        firstName:["",Validators.required],
        lastName:["",Validators.required],
        email:["",Validators.required],
        password:["",[Validators.required,
        Validators.minLength(4),
        Validators.maxLength(8)]],
        confirmPassword:["",Validators.required]
      },
      {validator:this.passwordMatchValidator}
    )
  }

  
  passwordMatchValidator(g:FormGroup){
    return g.get('password').value === 
    g.get('confirmPassword').value?null:{mismatch:true}
  }

  registerBtn(){
    if(this.registerForm.valid){
      let user = new UserRegister();

      user.firstName  = this.registerForm.get("firstName").value;
      user.lastName  = this.registerForm.get("lastName").value;
      user.email  = this.registerForm.get("email").value;
      user.password = this.registerForm.get("password").value

      console.log(user.firstName,user.lastName,user.email,user.password)
    }
  }
}
