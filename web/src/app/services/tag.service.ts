import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {retry} from 'rxjs/operators';
import {TagDto} from '../models/tagDto.model';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  private allTags: Map<string, TagDto> = new Map();
  private readonly tagSettingsForMultiselect: any;
  private BASE_PATH = 'Tag';

  constructor(private http: HttpClient) {

    this.getAll().subscribe(
      data => {
        data.forEach(tag => this.allTags.set(tag.name, tag));
      }
    );

    this.tagSettingsForMultiselect = {
      singleSelection: false,
      selectAllText: 'Zaznacz wszystkie',
      unSelectAllText: 'Odznacz wszystkie',
      itemsShowLimit: 5,
      allowSearchFilter: true,
      searchPlaceholderText: 'Szukaj...'
    };
  }

  getAll() {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    return this.http.get<any>(environment.baseUrl + `/api/${this.BASE_PATH}`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  getOne(name: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    return this.http.get<any>(environment.baseUrl + `/api/${this.BASE_PATH}/${name}`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  add(tagDto: TagDto) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    return this.http.post<any>(environment.baseUrl + `/api/${this.BASE_PATH}/create`, tagDto, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  update(name: string, tagDto: TagDto) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    return this.http.put<any>(environment.baseUrl + `/api/${this.BASE_PATH}/${name}`, tagDto, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  remove(name: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    return this.http.delete<any>(environment.baseUrl + `/api/${this.BASE_PATH}/${name}`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  getTagDto(name: string) {
    return this.allTags.get(name);
  }

  addTagDto(name: string) {
    this.getOne(name)
      .subscribe(
        data => {
          this.allTags.set(name, data);
        }
      );
  }

  getTagSettingsForMultiselect() {
    return this.tagSettingsForMultiselect;
  }

  toTagsList(tags: any) {
    if (tags === undefined || tags === null || tags.length === 0) {
      return null;
    }
    return tags.join().split(',');
  }

  toTagsString(tags: Array<string>) {
    return tags === null ? null : tags.join();
  }
}
