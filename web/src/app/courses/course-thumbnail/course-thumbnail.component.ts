import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/models/course.model';
import { ImageService } from 'src/app/services/image.service';
import { environment } from 'src/environments/environment.prod';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-course-thumbnail',
  templateUrl: './course-thumbnail.component.html',
  styleUrls: ['./course-thumbnail.component.css']
})
export class CourseThumbnailComponent implements OnInit {

  @Input() course: Course;
  image: any;
  error: string;

  constructor(public imageService: ImageService,
    private modalService: NgbModal) { }


  ngOnInit() {
    this.getPicture();
  }

  private getPicture() {
    let picture = null;
    if (this.course.picture == null) {
      this.course.picture = "https://mdrao.ca/wp-content/uploads/2018/03/DistanceEdCourse_ResitExam.png";
      return;
    }

    picture = this.course.picture.substring(this.course.picture.indexOf('image/') + 6);
    this.course.picture = environment.baseUrl + "/api/Image/" + picture;
  }

  openVerticallyCentered(content) {
    this.modalService.open(content, { centered: true });
  }

}
