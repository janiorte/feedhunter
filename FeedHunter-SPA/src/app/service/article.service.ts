import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ArticleOptions } from '../model/articleOptions';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getArticles(page?: number, options?: ArticleOptions) {
    let params = new HttpParams();
    if (page) {
      params = params.append('pageNumber', page.toString());
    }
    for (const id of options.sourceIds) {
      params = params.append('articlesOptions.sourceIds', id.toString());
    }

    return this.http.get(this.baseUrl + 'feed/articles', { params });
  }

  getChannels() {
    return this.http.get(this.baseUrl + 'feed/channels');
  }

  addChannel(url: string) {
    const headers = new HttpHeaders({'Content-Type': 'application/json'});
    return this.http.post(this.baseUrl + 'feed', JSON.stringify(url), { headers });
  }
}
