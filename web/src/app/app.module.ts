import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent, SafePipe, MinuteSecondsPipe } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AuthenticationService } from './services/authentication.service';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { AddVideoComponent } from './add-video/add-video.component';
import { UploadVideoComponent } from './upload-video/upload-video.component';
import { VideoBrowseComponent } from './video-browse/video-browse.component';
import { ShowVideoComponent } from './show-video/show-video.component';
import { VideoThumbnailComponent } from './video-thumbnail/video-thumbnail.component';
import { VideoService } from './services/video.service';
import { OverviewComponent } from './show-video/overview/overview.component';
import { QuestionsComponent } from './show-video/questions/questions.component';


const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegistrationComponent},
  {path: 'add_video', component: AddVideoComponent},
  {path: 'browse_video', component: VideoBrowseComponent},
  {path: '', component: HomeComponent},
  {path: 'home', redirectTo: '', pathMatch: 'full'},
  {path: 'secret', component: UnauthorizedComponent},
  {path: 'show_video/:id', component: ShowVideoComponent},
  {path: 'overview', component: OverviewComponent},
  {path: '**', component: PageNotFoundComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    FooterComponent,
    LoginComponent,
    RegistrationComponent,
    PageNotFoundComponent,
    UnauthorizedComponent,
    AddVideoComponent,
    UploadVideoComponent,
    VideoBrowseComponent,
    ShowVideoComponent,
    VideoThumbnailComponent,
    SafePipe,
    MinuteSecondsPipe,
    OverviewComponent,
    QuestionsComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule,
  ],
  providers: [
    AuthenticationService,
    VideoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
