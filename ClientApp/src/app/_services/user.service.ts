import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "../_models/user"

declare let alertify: any;


const httpOptions={
  headers:new HttpHeaders({
    'Authorization':'Bearer '+localStorage.getItem('token')
  })
}
@Injectable({
  providedIn:"root"
})
export class UserService {

  baseUrl:string="http://localhost:5166/api/users/";
  constructor( private http:HttpClient){

  }

  getUsers(followParams?:any,userParams?:any):Observable<User[]>{

    let params=new HttpParams();
    if(followParams==="followers"){
        params=params.append('followers','true');
    }
    if(followParams==="followings"){
      params=params.append('followings','true');
    }
    if(userParams!=null){
      if(userParams.Gender!=null)
        params=params.append('Gender',userParams.Gender);

      if(userParams.City!=null)
       params=params.append('City',userParams.City);

      if(userParams.Country!=null)
       params=params.append('Country',userParams.Country);

      if(userParams.minAge!=null)
       params=params.append('minAge',userParams.minAge);
       
      if(userParams.maxAge!=null)
       params=params.append('maxAge',userParams.maxAge);

      if(userParams.orderby!=null)
       params=params.append('orderby',userParams.orderby);

    } 
   return this.http.get<User[]>(this.baseUrl+'GetUsers',{...httpOptions,params});
  }
  getUser(id:number):Observable<User>{
    return this.http.get<User>(this.baseUrl+"GetUser/"+id,httpOptions)
  }

  updateUser(id:number,model: User):Observable<User>{
    return this.http.put<User>(this.baseUrl+"UpdateUser/"+id,model,httpOptions)
  }

  followUser(followerId:number,userId:number){
      return this.http.post(this.baseUrl+followerId+"/FollowToUser/"+userId,{},httpOptions);
  }
  unfollowUser(followerId:number,userId:number){
    return this.http.delete(this.baseUrl+followerId+"/UnfollowToUser/"+userId,httpOptions);
  }

}
