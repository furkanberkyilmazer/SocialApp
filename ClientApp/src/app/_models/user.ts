import { Image } from "./image";


export class User{
  id!:number;
  username!:string;
  name!:string;
  age!:number;
  gender!:string;
  created!:Date;
  lastActive!:Date;
  city!:string;
  country!:string;
  introduction!:string;
  hobbies!:string;
  image!:Image;
  images!:Image[];
  profileImageUrl!: string;
  followTittle !: string;
  //api den dönen farklı dto lar gibi ayrı ayrı koymak yerine hepsini bir yere koyup içini gelene kadar doldurduk.

  constructor(id:number,username:string,name:string,age:number,gender:string,created:Date,lastActive:Date,city:string,country:string,introduction:string,hobbies:string,image:Image,images:Image[],profileImageUrl:string,followTittle:string) {
    this.id=id;
    this.username=username;
    this.name=name;
    this.age=age;
    this.gender=gender;
    this.created=created;
    this.lastActive=lastActive;
    this.city=city;
    this.country=country;
    this.introduction=introduction;
    this.hobbies=hobbies;
    this.image=image;
    this.images=images;
    this.profileImageUrl=profileImageUrl;
    this.followTittle=followTittle;
 }

}
