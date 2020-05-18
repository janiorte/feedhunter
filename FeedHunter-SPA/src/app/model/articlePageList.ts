import { Article } from './article';

export interface ArticlePageList {
    currentPage: number;
    totalItems: number;
    pageSize: number;
    articles: Article[];

}
