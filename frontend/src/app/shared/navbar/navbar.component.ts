import { Component, OnInit, Input } from '@angular/core';
import { AlertifyService } from '../../services/alertify.service';
import { AuthService } from '../../services/auth.service';
declare const swal: any;

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  @Input() title: string;
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  logout() {
    this.authService.logOut();
  }

  alert() {
    swal({
      title: 'Are you sure?',
      text: 'You are going out of system?',
      type: 'warning',
      showCancelButton: true,
      confirmButtonClass: 'btn btn-success',
      cancelButtonClass: 'btn btn-danger',
      confirmButtonText: 'YES, LOGOUT!'
    });
    
  }

  menuClick() {
    //document.getElementById('main-panel').style.marginRight = '260px';
  }
}
