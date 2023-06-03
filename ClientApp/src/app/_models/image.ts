export class Image{

  id:Number;
  name:string;
  description:string;
  dateAdded:Date;
  isProfile:boolean;

  constructor(id:number,name:string,description:string,dateAdded:Date,isProfile:boolean) {
     this.id=id;
     this.name=name;
     this.description=description;
     this.dateAdded=dateAdded;
     this.isProfile=isProfile;
  }

}
