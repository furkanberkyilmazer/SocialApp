
import { Component, OnInit } from '@angular/core';


import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit{
  users!:User[];
  followParams:string="followings";
  constructor(private userService:UserService,private alertify:AlertifyService,private authService:AuthService) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    this.userService.getUsers(this.followParams).subscribe(users=>{
        this.users=users;
        console.log(this.followParams);
    },err=>{
      this.alertify.error(err);
    })
  }
}
