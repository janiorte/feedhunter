import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TimeagoModule, TimeagoIntl, TimeagoFormatter, TimeagoCustomFormatter } from 'ngx-timeago';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { UiSwitchModule } from 'ngx-ui-switch';

import { AppComponent } from './app.component';
import { ArticlesComponent } from './components/articles/articles.component';
import { TopbarComponent } from './components/topbar/topbar.component';
import { RegisterComponent } from './components/register/register.component';
import { AdminComponent } from './components/admin/admin.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { HasRoleDirective } from './directives/HasRole.directive';
import { AuthInterceptor } from './service/AuthInterceptor';


@NgModule({
   declarations: [
      AppComponent,
      ArticlesComponent,
      TopbarComponent,
      RegisterComponent,
      AdminComponent,
      HasRoleDirective
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forRoot(appRoutes),
      TimeagoModule.forRoot({
         intl: { provide: TimeagoIntl },
         formatter: { provide: TimeagoFormatter, useClass: TimeagoCustomFormatter },
       }),
      PaginationModule.forRoot(),
      UiSwitchModule
   ],
   providers: [
      {
         provide: HTTP_INTERCEPTORS,
         useClass: AuthInterceptor,
         multi: true
      }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
