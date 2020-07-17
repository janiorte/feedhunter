import { Component, OnInit } from '@angular/core';
import { Channel } from 'src/app/model/channel';
import { ArticleService } from 'src/app/service/article.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  channels: Channel[];
  url: string;

  constructor(private articleService: ArticleService) { }

  ngOnInit() {
    this.getChannels();
  }

  getChannels() {
    this.articleService.getChannels().subscribe((channels: Channel[]) => {
      this.channels = channels;
    }, error => {
      console.log(error);
    });
  }

  addSource() {
    this.articleService.addChannel(this.url).subscribe(() => {
      this.getChannels();
    }, error => {
      console.log(error);
    });
  }

  // ToDo
  delete() {
    console.log('Delete clicked');
  }
}
