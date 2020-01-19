import {VideoQuizComponent} from './video/video-quiz/video-quiz.component';
import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from './user/login/login.component';
import {RegistrationComponent} from './user/registration/registration.component';
import {CourseManagerAdminComponent} from './courses/course-manager-admin/course-manager-admin.component';
import {HomeComponent} from './home/home.component';
import {UnauthorizedComponent} from './error-pages/unauthorized/unauthorized.component';
import {ShowVideoComponent} from './video/show-video/show-video.component';
import {ProfileComponent} from './user/profile/profile.component';
import {PageNotFoundComponent} from './error-pages/page-not-found/page-not-found.component';
import {HomeForLoggedComponent} from './home-for-logged/home-for-logged.component';
import {VideoManagerComponent} from './video/video-manager/video-manager.component';
import {SettingsComponent} from './settings/settings.component';
import { CoursesViewListUserComponent } from './courses/courses-view-list-user/courses-view-list-user.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent, data: {animation: 'login'}},
  {path: 'register', component: RegistrationComponent, data: {animation: 'register'}},
  {path: 'videos', component: VideoManagerComponent, data: {animation: 'videos'}},
  {path: 'courses', component: CourseManagerAdminComponent, data: {animation: 'courses'}},
  {path: '', component: HomeComponent, data: {animation: ''}},
  {path: 'home', component: HomeForLoggedComponent, data: {animation: 'home'}},
  {path: 'unauthorized', component: UnauthorizedComponent, data: {animation: 'unauthorized'}},
  {path: 'mycourses', component: CoursesViewListUserComponent},
  {
    path: 'courses/:id/videos/:mediaId',
    component: ShowVideoComponent,
    data: {animation: 'courses/:id/videos/:mediaId'}
    // canActivate: [AuthGuard]
  },
  {path: 'quizes', component: VideoQuizComponent},
  {
    path: 'settings',
    component: SettingsComponent,
    data: {animation: 'settings'}
  },
  {path: 'profile', component: ProfileComponent, data: {animation: 'profile'}},
  {path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
