import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/userModels/user';
import { UserForDetails } from 'src/app/_models/userModels/userForDetails';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit{

  user!:UserForDetails;

  /**
   *
   */
  constructor(private userService: UserService,private alertify:AlertifyService,private route:ActivatedRoute) { }

ngOnInit(): void {
this.getUser();
}

//members/3
getUser(){
  //+ işaretini koyarsak başına int e çevirir
  this.userService.getUser(+this.route.snapshot.params['id']).subscribe(user=>{
    this.user=user;
  },err=>{
    this.alertify.error(err);
  });
}

}
