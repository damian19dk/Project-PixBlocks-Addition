import {Directive, Input, OnDestroy, OnInit, TemplateRef, ViewContainerRef} from '@angular/core';
import {Subject} from 'rxjs';
import {takeUntil} from 'rxjs/operators';
import {AuthService} from '../services/auth.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit, OnDestroy {
  @Input() appHasRole: Array<string>;
  stop$ = new Subject();
  isVisible = false;

  constructor(
    private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private authService: AuthService) {
  }

  ngOnInit() {
    this.authService.roles$.pipe(
      takeUntil(this.stop$)).subscribe(roles => {
      if (roles.some(r => this.appHasRole.includes(r))) {
        if (!this.isVisible) {
          this.isVisible = true;
          this.viewContainerRef.createEmbeddedView(this.templateRef);
        }
      } else {
        this.isVisible = false;
        this.viewContainerRef.clear();
      }
    });
  }

  ngOnDestroy() {
    this.stop$.next();
  }
}
