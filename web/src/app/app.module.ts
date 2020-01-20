import {QuizService} from './services/quiz.service';
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
import {VideoThumbnailAdminComponent} from './video/video-thumbnail-admin/video-thumbnail-admin.component';
import {VideoService} from './services/video.service';
import {CourseManagerAdminComponent} from './courses/course-manager-admin/course-manager-admin.component';
import {CourseNewAdminComponent} from './courses/course-new-admin/course-new-admin.component';
import {LoadingService} from './services/loading.service';
import {FunctionalityNotPreparedComponent} from './error-pages/functionality-not-prepared/functionality-not-prepared.component';
import {CourseService} from './services/course.service';
import {HeadersInterceptor} from './interceptors/headers-interceptor';
import {CourseThumbnailAdminComponent} from './courses/course-thumbnail-admin/course-thumbnail-admin.component';
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
import {CoursesViewUnderPlayerUserComponent} from './courses/courses-view-under-player-user/courses-view-under-player-user.component';
import {VideosViewComponent} from './video/videos-view/videos-view.component';
import {VideoManagerComponent} from './video/video-manager/video-manager.component';
import {CourseThumbnailUserComponent} from './courses/course-thumbnail-user/course-thumbnail-user.component';
import {CoursesEditAdminComponent} from './courses/courses-edit-admin/courses-edit-admin.component';
import {DragDropModule} from '@angular/cdk/drag-drop';
import {CoursesViewListUserComponent} from './courses/courses-view-list-user/courses-view-list-user.component';
import {CourseListElementUserComponent} from './courses/course-list-element-user/course-list-element-user.component';
import {CourseListElementAdminComponent} from './courses/course-list-element-admin/course-list-element-admin.component';
import {CoursesHistoryForUserComponent} from './courses/courses-history-for-user/courses-history-for-user.component';
import {VideoQuizComponent} from './video/video-quiz/video-quiz.component';
import {QuizQuestionComponent} from './video/video-quiz/quiz-question/quiz-question.component';
import {QuizAnswerComponent} from './video/video-quiz/quiz-answer/quiz-answer.component';
import {QuizFormComponent} from './video/video-quiz/quiz-form/quiz-form.component';
import {VideoQuizUserComponent} from './video/video-quiz/video-quiz-user/video-quiz-user.component';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner'; 
import {SettingsComponent} from './settings/settings.component';
import {NewTagAdminComponent} from './tags/new-tag-admin/new-tag-admin.component';
import {TagThumbnailComponent} from './tags/tag/tag-thumbnail.component';
import {ColorSketchModule} from 'ngx-color/sketch';
import {TagsViewAdminComponent} from './tags/tags-view-admin/tags-view-admin.component';
import {TagThumbnailEditAdminComponent} from './tags/tag-thumbnail-edit-admin/tag-thumbnail-edit-admin.component';
import {VideoEditModalAdminComponent} from './video/video-edit-modal-admin/video-edit-modal-admin.component';
import {VideoAddModalAdminComponent} from './video/video-add-modal-admin/video-add-modal-admin.component';
import {VideoChooseAdminComponent} from './video/video-choose-admin/video-choose-admin.component';
import {CourseChooseAdminComponent} from './courses/course-choose-admin/course-choose-admin.component';
import {JWPlayerComponent} from './JWPlayer/jwplayer/jwplayer.component';
import {VideoPreviewComponent} from './JWPlayer/video-preview/video-preview.component';
import {SelectPictureComponent} from './common/select-picture/select-picture.component';
import {SelectVideoComponent} from './common/select-video/select-video.component';

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
    VideoThumbnailAdminComponent,
    ShortTextPipe,
    SafePipe,
    CourseManagerAdminComponent,
    CourseNewAdminComponent,
    FunctionalityNotPreparedComponent,
    CourseThumbnailAdminComponent,
    SearchBarComponent,
    ProfileComponent,
    ChangePasswordComponent,
    ChangeEmailComponent,
    HasRoleDirective,
    SelectLanguagesComponent,
    HomeForLoggedComponent,
    CoursesViewUnderPlayerUserComponent,
    VideosViewComponent,
    VideoManagerComponent,
    CourseThumbnailUserComponent,
    CoursesEditAdminComponent,
    CoursesViewListUserComponent,
    CourseListElementUserComponent,
    CourseListElementAdminComponent,
    CoursesHistoryForUserComponent,
    SettingsComponent,
    NewTagAdminComponent,
    TagThumbnailComponent,
    TagsViewAdminComponent,
    TagThumbnailEditAdminComponent,
    VideoEditModalAdminComponent,
    VideoAddModalAdminComponent,
    VideoChooseAdminComponent,
    CourseChooseAdminComponent,
    JWPlayerComponent,
    VideoPreviewComponent,
    TagThumbnailEditAdminComponent,
    VideoQuizComponent,
    QuizQuestionComponent,
    QuizAnswerComponent,
    QuizFormComponent,
    VideoQuizUserComponent,
    SelectPictureComponent,
    SelectVideoComponent
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
        whitelistedDomains: ['localhost:4200'],
        blacklistedRoutes: []
      }
    }),
    MatToolbarModule,
    MatChipsModule,
    MatCheckboxModule,
    MatRadioModule,
    MatFormFieldModule,
    MatSelectModule,
    DragDropModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    ColorSketchModule
  ],
  exports: [HasRoleDirective],
  providers: [
    AuthGuardService,
    AuthService,
    VideoService,
    CourseService,
    LoadingService,
    QuizService,
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
