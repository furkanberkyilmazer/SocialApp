import { Image } from "../image";


export class User{
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
  image:Image;

  constructor(id:number,username:string,name:string,age:number,gender:string,created:Date,lastActive:Date,city:string,country:string,introdution:string,hobbies:string,image:Image) {
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
    this.image=image;
 }

}
