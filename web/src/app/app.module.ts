import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {NgMultiSelectDropDownModule} from 'ng-multiselect-dropdown';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent, SafePipe} from './app.component';
import {HeaderComponent} from './header/header.component';
import {HomeComponent} from './home/home.component';
import {FooterComponent} from './footer/footer.component';
import {LoginComponent} from './user/login/login.component';
import {RegistrationComponent} from './user/registration/registration.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {PageNotFoundComponent} from './error-pages/page-not-found/page-not-found.component';
import {AuthService} from './services/auth.service';
import {UnauthorizedComponent} from './error-pages/unauthorized/unauthorized.component';
import {AddVideoComponent} from './video/add-video/add-video.component';
import {VideoBrowseComponent} from './video/video-browse/video-browse.component';
import {ShowVideoComponent} from './video/show-video/show-video.component';
import {VideoThumbnailComponent} from './video/video-thumbnail/video-thumbnail.component';
import {VideoService} from './services/video.service';
import {OverviewComponent} from './video/show-video/overview/overview.component';
import {QuestionsComponent} from './video/show-video/questions/questions.component';
import {CoursesComponent} from './video/show-video/courses/courses.component';
import {CourseManagerComponent} from './courses/course-manager/course-manager.component';
import {NewCourseComponent} from './courses/new-course/new-course.component';
import {LoadingService} from './services/loading.service';
import {EditCourseComponent} from './courses/edit-course/edit-course.component';
import {FunctionalityNotPreparedComponent} from './error-pages/functionality-not-prepared/functionality-not-prepared.component';
import {CourseService} from './services/course.service';
import {LessonManagerComponent} from './lessons/lesson-manager/lesson-manager.component';
import {NewLessonComponent} from './lessons/new-lesson/new-lesson.component';
import {EditLessonComponent} from './lessons/edit-lesson/edit-lesson.component';
import {HeadersInterceptor} from './interceptors/headers-interceptor';
import {LessonService} from './services/lesson.service';
import {CourseThumbnailComponent} from './courses/course-thumbnail/course-thumbnail.component';
import {SearchBarComponent} from './common/search-bar/search-bar.component';
import {UnauthorizedInterceptor} from './interceptors/unauthorized-interceptor';
import {ProfileComponent} from './user/profile/profile.component';
import {ChangePasswordComponent} from './user/profile/change-password/change-password.component';
import {ChangeEmailComponent} from './user/profile/change-email/change-email.component';
import {LessonThumbnailComponent} from './lessons/lesson-thumbnail/lesson-thumbnail.component';
import {MatButtonModule} from '@angular/material/button';
import {HasRoleDirective} from './user/HasRoleDirective';
import {SelectLanguagesComponent} from './common/select-languages/select-languages.component';
import {AuthGuardService} from './services/auth-guard.service';
import {JwtModule} from '@auth0/angular-jwt';
import {MatToolbarModule} from '@angular/material/toolbar';
import {HomeForLoggedComponent} from './home-for-logged/home-for-logged.component';
import {MatChipsModule} from '@angular/material/chips';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatCheckboxModule} from "@angular/material/checkbox";


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
    SearchBarComponent,
    ProfileComponent,
    ChangePasswordComponent,
    ChangeEmailComponent,
    LessonThumbnailComponent,
    HasRoleDirective,
    SelectLanguagesComponent,
    HomeForLoggedComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    NgbModule.forRoot(),
    NgMultiSelectDropDownModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: function tokenGetter() {
          return localStorage.getItem('Token');
        },
        whitelistedDomains: ['localhost:4200'],
        blacklistedRoutes: []
      }
    }),
    MatToolbarModule,
    MatChipsModule,
    MatCheckboxModule
  ],
  exports: [
    HasRoleDirective
  ],
  providers: [
    AuthGuardService,
    AuthService,
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
      useClass: UnauthorizedInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
