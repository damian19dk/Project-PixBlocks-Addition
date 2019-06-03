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
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PageNotFoundComponent } from './error-pages/page-not-found/page-not-found.component';
import { AuthenticationService } from './services/authentication.service';
import { UnauthorizedComponent } from './error-pages/unauthorized/unauthorized.component';
import { AddVideoComponent } from './video/add-video/add-video.component';
import { VideoBrowseComponent } from './video/video-browse/video-browse.component';
import { ShowVideoComponent } from './video/show-video/show-video.component';
import { VideoThumbnailComponent } from './video/video-thumbnail/video-thumbnail.component';
import { VideoService } from './services/video.service';
import { OverviewComponent } from './video/show-video/overview/overview.component';
import { QuestionsComponent } from './video/show-video/questions/questions.component';
import { CoursesComponent } from './video/show-video/courses/courses.component';
import { CourseManagerComponent } from './courses/course-manager/course-manager.component';
import { NewCourseComponent } from './courses/new-course/new-course.component';
import { LoadingService } from './services/loading.service';
import { EditCourseComponent } from './courses/edit-course/edit-course.component';
import { FunctionalityNotPreparedComponent } from './error-pages/functionality-not-prepared/functionality-not-prepared.component';
import { CourseService } from './services/course.service';
import { LessonManagerComponent } from './lessons/lesson-manager/lesson-manager.component';
import { NewLessonComponent } from './lessons/new-lesson/new-lesson.component';
import { EditLessonComponent } from './lessons/edit-lesson/edit-lesson.component';
import { HeadersInterceptor } from './interceptors/headers-interceptor';
import { JwtInterceptor } from './interceptors/jwt-interceptor';
import { LessonService } from './services/lesson.service';
import { CourseThumbnailComponent } from './courses/course-thumbnail/course-thumbnail.component';
import { SearchBarComponent } from './common/search-bar/search-bar.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'add_video', component: AddVideoComponent },
  { path: 'course_editor', component: CourseManagerComponent },
  { path: 'lesson_editor', component: LessonManagerComponent },
  { path: '', component: HomeComponent },
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: 'show_video/:id', component: ShowVideoComponent },
  { path: 'settings', component: FunctionalityNotPreparedComponent },
  { path: 'profile', component: FunctionalityNotPreparedComponent },
  { path: '**', component: PageNotFoundComponent }
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
    VideoBrowseComponent,
    ShowVideoComponent,
    VideoThumbnailComponent,
    SafePipe,
    MinuteSecondsPipe,
    OverviewComponent,
    QuestionsComponent,
    CoursesComponent,
    CourseManagerComponent,
    NewCourseComponent,
    EditCourseComponent,
    FunctionalityNotPreparedComponent,
    LessonManagerComponent,
    NewLessonComponent,
    EditLessonComponent,
    CourseThumbnailComponent,
    SearchBarComponent
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
    LessonService,
    LoadingService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HeadersInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
