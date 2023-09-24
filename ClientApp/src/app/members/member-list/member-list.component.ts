import { Component, OnInit } from '@angular/core';


import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { User } from 'src/app/_models/user';
import { NgxSpinnerService } from "ngx-spinner";


@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  users!:User[];
  userParams: any={};
  constructor(private userService:UserService,private alertify:AlertifyService,private spinner: NgxSpinnerService) {}

  ngOnInit(): void {
    this.userParams.orderby="lastactive";
    this.getUsers();
  }

  getUsers(){
    this.spinner.show();
    console.log(this.userParams);
    this.userService.getUsers(null,this.userParams).subscribe(users=>{
      this.spinner.hide();
        this.users=users;
    },err=>{
      this.spinner.hide();
      this.alertify.error(err);
    })
  }


}
