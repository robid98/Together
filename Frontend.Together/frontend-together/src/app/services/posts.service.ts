import { HttpClient  } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  backendTogetherUrl: string;

  constructor(private httpClient: HttpClient ) { 
    this.backendTogetherUrl = "https://localhost:5001";
  }

  getWeatherForecast(){
    return this.httpClient.get<any>(`${this.backendTogetherUrl}/weatherforecast`)
  }
}
