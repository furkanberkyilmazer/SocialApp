import { Component, OnInit } from '@angular/core';


import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { User } from 'src/app/_models/userModels/user';


@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  users!:User[];
  constructor(private userService:UserService,private alertify:AlertifyService) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    this.userService.getUsers().subscribe(users=>{
        this.users=users;

    },err=>{
      this.alertify.error(err);
    })
  }


}
