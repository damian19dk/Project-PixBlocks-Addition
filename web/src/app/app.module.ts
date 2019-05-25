import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
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
import { CourseManagerComponent } from './courses/course-manager/course-manager.component';
import { NewCourseComponent } from './courses/new-course/new-course.component';
import { LoadingService } from './services/loading.service';
import { EditCourseComponent } from './courses/edit-course/edit-course.component';
import { FunctionalityNotPreparedComponent } from './functionality-not-prepared/functionality-not-prepared.component';
import { CourseService } from './services/course.service';
import { LessonManagerComponent } from './lessons/lesson-manager/lesson-manager.component';
import { NewLessonComponent } from './lessons/new-lesson/new-lesson.component';
import { EditLessonComponent } from './lessons/edit-lesson/edit-lesson.component';


const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegistrationComponent},
  {path: 'add_video', component: AddVideoComponent},
  {path: 'course_editor', component: CourseManagerComponent},
  {path: 'lesson_editor', component: LessonManagerComponent},
  {path: '', component: HomeComponent},
  {path: 'home', redirectTo: '', pathMatch: 'full'},
  {path: 'secret', component: UnauthorizedComponent},
  {path: 'show_video/:id', component: ShowVideoComponent},
  {path: 'settings', component: FunctionalityNotPreparedComponent},
  {path: 'profile', component: FunctionalityNotPreparedComponent},
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
    CourseManagerComponent,
    NewCourseComponent,
    EditCourseComponent,
    FunctionalityNotPreparedComponent,
    LessonManagerComponent,
    NewLessonComponent,
    EditLessonComponent
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    NgMultiSelectDropDownModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule,
  ],
  providers: [
    AuthenticationService,
    VideoService,
    CourseService,
    LoadingService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
