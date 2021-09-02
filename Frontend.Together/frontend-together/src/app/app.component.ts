import { Component, OnInit } from '@angular/core';
import { PostsService } from './services/posts.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  
  weatherForecast : any[] = []
  
  constructor(private postService : PostsService){ }

  ngOnInit()
  {
    this.postService.getWeatherForecast().subscribe(res => {
      this.weatherForecast = res;
      console.log(this.weatherForecast);
    })
  }
}
