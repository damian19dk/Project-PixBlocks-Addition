import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {NgMultiSelectDropDownModule} from 'ng-multiselect-dropdown';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent, SafePipe, ShortTextPipe} from './app.component';
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
import {NewVideoComponent} from './video/new-video/new-video.component';
import {ShowVideoComponent} from './video/show-video/show-video.component';
import {VideoThumbnailComponent} from './video/video-thumbnail/video-thumbnail.component';
import {VideoService} from './services/video.service';
import {OverviewComponent} from './video/show-video/overview/overview.component';
import {QuestionsComponent} from './video/show-video/questions/questions.component';
import {CoursesComponent} from './video/show-video/courses/courses.component';
import {CourseManagerComponent} from './courses/course-manager/course-manager.component';
import {NewCourseComponent} from './courses/new-course/new-course.component';
import {LoadingService} from './services/loading.service';
import {FunctionalityNotPreparedComponent} from './error-pages/functionality-not-prepared/functionality-not-prepared.component';
import {CourseService} from './services/course.service';
import {HeadersInterceptor} from './interceptors/headers-interceptor';
import {CourseThumbnailComponent} from './courses/course-thumbnail/course-thumbnail.component';
import {SearchBarComponent} from './common/search-bar/search-bar.component';
import {UnauthorizedInterceptor} from './interceptors/unauthorized-interceptor';
import {ProfileComponent} from './user/profile/profile.component';
import {ChangePasswordComponent} from './user/profile/change-password/change-password.component';
import {ChangeEmailComponent} from './user/profile/change-email/change-email.component';
import {MatButtonModule} from '@angular/material/button';
import {HasRoleDirective} from './user/HasRoleDirective';
import {SelectLanguagesComponent} from './common/select-languages/select-languages.component';
import {AuthGuardService} from './services/auth-guard.service';
import {JwtModule} from '@auth0/angular-jwt';
import {MatToolbarModule} from '@angular/material/toolbar';
import {HomeForLoggedComponent} from './home-for-logged/home-for-logged.component';
import {MatChipsModule} from '@angular/material/chips';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatRadioModule} from '@angular/material/radio';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {CoursesViewComponent} from './courses/courses-view/courses-view.component';
import {VideosViewComponent} from './video/videos-view/videos-view.component';
import {VideoManagerComponent} from './video/video-manager/video-manager.component';


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
    NewVideoComponent,
    ShowVideoComponent,
    VideoThumbnailComponent,
    ShortTextPipe,
    SafePipe,
    OverviewComponent,
    QuestionsComponent,
    CoursesComponent,
    CourseManagerComponent,
    NewCourseComponent,
    FunctionalityNotPreparedComponent,
    CourseThumbnailComponent,
    SearchBarComponent,
    ProfileComponent,
    ChangePasswordComponent,
    ChangeEmailComponent,
    HasRoleDirective,
    SelectLanguagesComponent,
    HomeForLoggedComponent,
    CoursesViewComponent,
    VideosViewComponent,
    VideoManagerComponent
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
    MatCheckboxModule,
    MatRadioModule,
    MatFormFieldModule,
    MatSelectModule
  ],
  exports: [
    HasRoleDirective
  ],
  providers: [
    AuthGuardService,
    AuthService,
    VideoService,
    CourseService,
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
