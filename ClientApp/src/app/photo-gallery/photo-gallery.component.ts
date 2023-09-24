import { Component, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Image } from '../_models/image';

import { IAlbum, Lightbox } from 'ngx-lightbox';


@Component({
  selector: 'photo-gallery',
  templateUrl: './photo-gallery.component.html',
  styleUrls: ['./photo-gallery.component.css']
})

//burda npm install ngx-lightbox  kütüphanesi ekledim resimleri büyültmek ve kaydırmak için
//app.module.ts 'e import ettik Ligtbox ı
//"node_modules/ngx-lightbox/lightbox.css" bunu angular.json da style ekledik
export class PhotoGalleryComponent  implements OnInit{
  @Input() images!:Image[];

   constructor(private lightbox: Lightbox) {}
  ngOnInit(): void {

  }
  open(index: number): void {

    const albums: IAlbum[] = this.images.map((image) => ({
      src: image.name,
      caption: image.description,
      thumb: image.name,
    }));

    this.lightbox.open(albums, index);
  }

}
