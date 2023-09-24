import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  username:any;
  model:any={};
  constructor(public authService:AuthService,private router:Router,private alertify: AlertifyService,private spinner: NgxSpinnerService){}
  ngOnInit():void{

  }
  login(){
    this.spinner.show();
     this.authService.login(this.model).subscribe(next=>{
      this.spinner.hide();
      this.alertify.success("Welcome ");
      this.router.navigate(['/members'])

     },
     error=>{
      this.spinner.hide();
      this.alertify.error(error);
    })

  }
  loggedIn(){
    return this.authService.loggedIn();

  }
  logout(){
    this.alertify.warning("GoodBye ");
    this.authService.logout();
    this.router.navigate(['/home']);
  }
}

