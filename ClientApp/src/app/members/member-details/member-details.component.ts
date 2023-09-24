import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit{

  user!:User;


  /**
   *
   */
  constructor(private userService: UserService,private alertify:AlertifyService,private route:ActivatedRoute ,private authService: AuthService,private spinner: NgxSpinnerService) { }

ngOnInit(): void {
this.getUser();
}

followUser(userId: number) {
  this.spinner.show();
  this.userService.followUser(this.authService.decodedToken.nameid, userId)
      .subscribe(result => {
        this.spinner.hide();
        this.alertify.success(this.user.name + ' kullanıcısını takip ediyorsunuz.');
        this.user.followTittle="Takibi Bırak";
      }, err => {
        this.spinner.hide();
        this.alertify.error(err);
      })
}

unfollowUser(userId: number) {
   this.spinner.show();
   this.userService.unfollowUser(this.authService.decodedToken.nameid, userId)
       .subscribe(result => {
         this.spinner.hide();
         this.alertify.success(this.user.name + ' kullanıcısını takipten çıktın.');
         this.user.followTittle="Takip Et";
    
       }, err => {
         this.spinner.hide();
         this.alertify.error(err);
       })
  console.log("Takibi Bırak");
}
//members/3
getUser(){
  //+ işaretini koyarsak başına int e çevirir
  this.spinner.show();
  this.userService.getUser(+this.route.snapshot.params['id']).subscribe(user=>{
    this.spinner.hide();
    this.user=user;
    console.log(user);
  },err=>{
    this.spinner.hide();
    this.alertify.error(err);
  });
}


}
