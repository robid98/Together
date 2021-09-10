import { ReplyInterface } from './reply.interface';

export interface CommentInterface {
  commentId: string;
  commentDescription: string;
  commentDate: Date;
  commentLikes: Number;
  isCommentDeleted: string;
  replies: ReplyInterface[];
}
