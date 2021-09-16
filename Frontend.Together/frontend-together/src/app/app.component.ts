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

  urlImage: string =
    'http://127.0.0.1:10000/devstoreaccount1/userprofilepictures/ad4c62b0-5abd-4c08-9cf4-530418951493_1800x1200_is_my_cat_normal_slideshow.jpg';
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
