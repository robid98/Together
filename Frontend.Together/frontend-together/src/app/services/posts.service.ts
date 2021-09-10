import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PostInterface } from '../interfacesApi/post.interface';
import { ResultInterface } from '../interfacesApi/result.api.interface';

@Injectable({
  providedIn: 'root',
})
export class PostsService {
  backendTogetherUrl: string;

  constructor(private httpClient: HttpClient) {
    this.backendTogetherUrl = 'https://localhost:5001';
  }

  getPosts(): Promise<PostInterface[]> {
    return this.httpClient
      .get<PostInterface[]>(`${this.backendTogetherUrl}/api/v1/posts`)
      .toPromise();
  }

  getPostById(postId: string): Promise<PostInterface> {
    return this.httpClient
      .get<PostInterface>(`${this.backendTogetherUrl}/api/v1/posts/${postId}`)
      .toPromise();
  }

  insertPost(newPost: PostInterface): Promise<PostInterface> {
    return this.httpClient
      .post<PostInterface>(`${this.backendTogetherUrl}/api/v1/posts`, newPost)
      .toPromise();
  }

  deletePost(postId: string): Promise<ResultInterface> {
    return this.httpClient
      .delete<ResultInterface>(
        `${this.backendTogetherUrl}/api/v1/posts/${postId}`
      )
      .toPromise();
  }

  updatePost(post: PostInterface): Promise<ResultInterface> {
    return this.httpClient
      .put<ResultInterface>(
        `${this.backendTogetherUrl}/api/v1/posts/${post.postId}`,
        post
      )
      .toPromise();
  }
}
