import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { PostComponent } from './components/posts.component/post.component/post.component';
import { PostsListComponent } from './components/posts.component/posts-list.component';
import { NavigationBar } from './components/navigation-bar.component/navigation-bar.component';

@NgModule({
  declarations: [
    AppComponent,
    PostComponent,
    PostsListComponent,
    NavigationBar,
  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
