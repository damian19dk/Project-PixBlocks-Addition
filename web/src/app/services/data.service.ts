import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

export class DataService {

  constructor(protected BASE_PATH: string, protected http: HttpClient) {}

  findByTitle(title: string) {
    let headers = new HttpHeaders()
    .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + `/api/${this.BASE_PATH}/title?title=` + title, { headers });
  }

  getOne(id: string) {
    let headers = new HttpHeaders()
    .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + `/api/${this.BASE_PATH}/?id=` + id, { headers });
  }

  getAll(page: number, count: number = 6) {
    let headers = new HttpHeaders()
    .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + `/api/${this.BASE_PATH}/all?page=` + page + "&count=" + count, { headers });
  }

  add(dto: any) {
    let headers = new HttpHeaders()
    return this.http.post<any>(environment.baseUrl + `/api/${this.BASE_PATH}/create`, dto, { headers });
  }

  update(dto: any) {
    let headers = new HttpHeaders()
    return this.http.put<any>(environment.baseUrl + `/api/${this.BASE_PATH}/change`, dto, { headers });
  }

  remove(id: string) {
    return this.http.delete<any>(environment.baseUrl + `/api/${this.BASE_PATH}?Id=` + id);
  }
}
