import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from './user/login/login.component';
import {RegistrationComponent} from './user/registration/registration.component';
import {CourseManagerComponent} from './courses/course-manager/course-manager.component';
import {HomeComponent} from './home/home.component';
import {UnauthorizedComponent} from './error-pages/unauthorized/unauthorized.component';
import {ShowVideoComponent} from './video/show-video/show-video.component';
import {FunctionalityNotPreparedComponent} from './error-pages/functionality-not-prepared/functionality-not-prepared.component';
import {ProfileComponent} from './user/profile/profile.component';
import {PageNotFoundComponent} from './error-pages/page-not-found/page-not-found.component';
import {AuthGuardService as AuthGuard} from './services/auth-guard.service';
import {HomeForLoggedComponent} from './home-for-logged/home-for-logged.component';
import {VideoManagerComponent} from './video/video-manager/video-manager.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent, data: {animation: 'login'}},
  {path: 'register', component: RegistrationComponent, data: {animation: 'register'}},
  {path: 'videos', component: VideoManagerComponent, data: {animation: 'videos'}, canActivate: [AuthGuard]},
  {path: 'courses', component: CourseManagerComponent, data: {animation: 'courses'}, canActivate: [AuthGuard]},
  {path: '', component: HomeComponent, data: {animation: ''}},
  {path: 'home', component: HomeForLoggedComponent, data: {animation: 'home'}, canActivate: [AuthGuard]},
  {path: 'unauthorized', component: UnauthorizedComponent, data: {animation: 'unauthorized'}},
  {path: 'videos/:id', component: ShowVideoComponent, data: {animation: 'videos/:id'}, canActivate: [AuthGuard]},
  {
    path: 'settings',
    component: FunctionalityNotPreparedComponent,
    data: {animation: 'settings'},
    canActivate: [AuthGuard]
  },
  {path: 'profile', component: ProfileComponent, data: {animation: 'profile'}, canActivate: [AuthGuard]},
  {path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
