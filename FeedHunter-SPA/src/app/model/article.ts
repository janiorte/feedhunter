import { Channel } from './channel';

export interface Article {
    title: string;
    link: string;
    description: string;

    author?: string;
    categories?: string[];
    publishingDate?: Date;
    channel?: Channel;
}
