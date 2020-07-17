import { Component, OnInit } from '@angular/core';
import { Channel } from 'src/app/model/channel';
import { ArticleService } from 'src/app/service/article.service';
import { AlertifyService } from 'src/app/service/alertify.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  channels: Channel[];
  url: string;

  constructor(private articleService: ArticleService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.getChannels();
  }

  getChannels() {
    this.articleService.getChannels().subscribe((channels: Channel[]) => {
      this.channels = channels;
    }, error => {
      this.alertify.error('No se pudo obtener los canales');
    });
  }

  addSource() {
    this.articleService.addChannel(this.url).subscribe(() => {
      this.getChannels();
    }, error => {
      this.alertify.error('No se pudo obtener el canal de ' + this.url);
    });
  }

  // ToDo
  delete() {
    console.log('Delete clicked');
  }
}
