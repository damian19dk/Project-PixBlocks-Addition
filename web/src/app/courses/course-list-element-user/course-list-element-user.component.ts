import {Component, Input, OnInit} from '@angular/core';
import {CourseDocument} from '../../models/courseDocument.model';
import {AuthService} from '../../services/auth.service';
import {TagService} from '../../services/tag.service';

@Component({
  selector: 'app-course-list-element-user',
  templateUrl: './course-list-element-user.component.html',
  styleUrls: ['./course-list-element-user.component.css']
})
export class CourseListElementUserComponent implements OnInit {

  @Input() course: CourseDocument;

  constructor(private authService: AuthService,
              private tagService: TagService) {
  }

  ngOnInit() {
  }

  getTag(name: string) {
    return this.tagService.getTagDto(name);
  }

  isPremiumUser(): boolean {
    return this.authService.isPremium();
  }
}
