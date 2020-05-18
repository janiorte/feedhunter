import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ArticleService } from 'src/app/service/article.service';
import { Article } from 'src/app/model/article';
import { ArticlePageList } from 'src/app/model/articlePageList';
import { Channel } from 'src/app/model/channel';
import { ArticleOptions } from 'src/app/model/articleOptions';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ArticlesComponent implements OnInit {
  articles: Article[];
  channels: Channel[];
  channelsShown: number[] = [];
  totalItems: number;
  viewContent = false;

  constructor(private articleService: ArticleService) { }

  ngOnInit() {
    this.getChannels();
  }

  getArticles(page?: number){
    const options: ArticleOptions = { sourceIds : this.channelsShown};

    this.articleService.getArticles(page, options).subscribe((articlePageList: ArticlePageList) => {
      this.articles = articlePageList.articles;
      this.totalItems = articlePageList.totalItems;
      window.scrollTo(0, 0);
    }, error => {
      console.log(error);
    });
  }

  updateArticles(channelId: number, visible: boolean)
  {
    if (!visible && this.channelsShown.indexOf(channelId) !== -1) {
      this.channelsShown.splice(this.channelsShown.indexOf(channelId), 1);
    }

    if (visible && this.channelsShown.indexOf(channelId) === -1) {
      this.channelsShown.push(channelId);
    }
    this.getArticles();
  }

  pageChanged(event: any) {
    this.getArticles(event.page);
  }

  getChannels() {
    this.articleService.getChannels().subscribe((channels: Channel[]) => {
      this.channels = channels;
      for (const channel of channels) {
        this.channelsShown.push(channel.id);
      }
      this.getArticles();
    }, error => {
      console.log(error);
    });
  }
}
