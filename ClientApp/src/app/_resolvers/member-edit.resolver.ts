import {Injectable} from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from "@angular/router";
import { User } from "../_models/user";
import { UserService } from "../_services/user.service";
import { AlertifyService } from "../_services/alertify.service";
import { AuthService } from "../_services/auth.service";
import { Observable, catchError, of } from "rxjs";


//app.modulde ts de provider a ekle MemberEditResolver
@Injectable()
export class MemberEditResolver implements Resolve<User>{

  constructor(private userService:UserService,private alertify:AlertifyService,private authService:AuthService,private route:Router){}


  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):User | Observable<User|any> | Promise<User|any>{
    return this.userService.getUser(this.authService.decodedToken.nameid).pipe(catchError(error=>{
      this.alertify.error("server error");
      this.route.navigate(['/members']);
      return of(null);
    }));
  }


}
