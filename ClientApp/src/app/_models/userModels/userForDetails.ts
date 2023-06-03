import { Image } from "../image";


export class UserForDetails{
  id:number;
  username:string;
  name:string;
  age:number;
  gender:string;
  created:Date;
  lastActive:Date;
  city:string;
  country:string;
  introdution:string;
  hobbies:string;
  images:Image[];

  constructor(id:number,username:string,name:string,age:number,gender:string,created:Date,lastActive:Date,city:string,country:string,introdution:string,hobbies:string,images:Image[]) {
    this.id=id;
    this.username=username;
    this.name=name;
    this.age=age;
    this.gender=gender;
    this.created=created;
    this.lastActive=lastActive;
    this.city=city;
    this.country=country;
    this.introdution=introdution;
    this.hobbies=hobbies;
    this.images=images;
 }

}
