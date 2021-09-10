import { Component, OnInit } from '@angular/core';
import { PostInterface } from './interfacesApi/post.interface';
import { PostsService } from './services/posts.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  posts: PostInterface[] = [];

  constructor(private postService: PostsService) {}

  ngOnInit() {
    this.postService
      .getPosts()
      .then((res) => {
        this.posts = res;
        console.log(this.posts);
      })
      .catch((err) => {
        alert(err);
        console.log(err);
      });
  }
}
