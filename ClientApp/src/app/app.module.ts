import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { FriendListComponent } from './friend-list/friend-list.component';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { RouterModule } from '@angular/router';
import { AuthGuard } from './auth-guard';
import { ErrorInterceptor } from './_services/error.intercaptor';
import { MemberDetailsComponent } from './members/member-details/member-details.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { PhotoGalleryComponent } from './photo-gallery/photo-gallery.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { LightboxModule } from 'ngx-lightbox';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgxSpinnerModule } from "ngx-spinner";

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegisterComponent,
    MemberListComponent,
    FriendListComponent,
    HomeComponent,
    MessagesComponent,
    NotfoundComponent,
    MemberDetailsComponent,
    PhotoGalleryComponent,
    MemberEditComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    LightboxModule,
    BrowserAnimationsModule,
    NgxSpinnerModule,

  ],
  providers: [
    AuthGuard,
    {
    provide:HTTP_INTERCEPTORS,
    useClass:ErrorInterceptor,
    multi:true
    },
    MemberEditResolver,
],
bootstrap: [AppComponent]
})
export class AppModule { }
