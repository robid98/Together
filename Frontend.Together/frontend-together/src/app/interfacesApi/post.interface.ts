import { CommentInterface } from './comment.interface';

export interface PostInterface {
  postId?: string;
  postDescription: string;
  postDate: Date;
  postLikes: number;
  postShares: number;
  isPostDeleted: string;
  comments?: CommentInterface[];
}
