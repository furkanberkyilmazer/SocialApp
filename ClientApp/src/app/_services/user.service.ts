import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "../_models/userModels/user";
import { UserForDetails } from "../_models/userModels/userForDetails";

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

  getUsers():Observable<User[]>{
   return this.http.get<User[]>(this.baseUrl+'GetUsers/',httpOptions);
  }
  getUser(id:number):Observable<UserForDetails>{
    return this.http.get<UserForDetails>(this.baseUrl+"GetUser/"+id,httpOptions)
  }




}
