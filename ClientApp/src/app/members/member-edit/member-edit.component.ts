import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  user!:User;


  constructor(private route:ActivatedRoute,private userService: UserService,private alertify:AlertifyService,private authService:AuthService,private spinner: NgxSpinnerService) {}
  ngOnInit(): void {
    this.spinner.show();
    this.route.data.subscribe(data=>{
      this.spinner.hide();
      this.user=data['user'];
    })
  }

  updateUser(): void{
    this.spinner.show();
    this.userService.updateUser(this.authService.decodedToken.nameid,this.user).subscribe(()=>{
      this.spinner.hide();
      this.alertify.success("Updated");
    },err=>{
      this.spinner.hide();
      this.alertify.error(err);
    })
  }

}
