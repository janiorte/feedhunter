import { Routes } from '@angular/router';
import { ArticlesComponent } from './components/articles/articles.component';
import { RegisterComponent } from './components/register/register.component';
import { AdminComponent } from './components/admin/admin.component';

export const appRoutes: Routes = [
    { path: '', component: ArticlesComponent },
    { path: 'register', component: RegisterComponent},
    { path: 'admin', component: AdminComponent}
];
