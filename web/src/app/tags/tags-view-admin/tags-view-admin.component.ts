import {Component, OnInit} from '@angular/core';
import {TagDto} from '../../models/tagDto.model';
import {TagService} from '../../services/tag.service';
import {LoadingService} from "../../services/loading.service";

@Component({
  selector: 'app-tags-view-admin',
  templateUrl: './tags-view-admin.component.html',
  styleUrls: ['./tags-view-admin.component.css']
})
export class TagsViewAdminComponent implements OnInit {

  tags: Array<TagDto>;

  constructor(private tagService: TagService,
              private loadingService: LoadingService) {
  }

  ngOnInit() {
    this.getAllTags();
  }

  getAllTags() {
    this.loadingService.load();
    this.tagService.getAll().subscribe(
      data => {
        this.tags = data;
        this.loadingService.unload();
      },
      error => {
        this.loadingService.unload();
      }
    );
  }
}
