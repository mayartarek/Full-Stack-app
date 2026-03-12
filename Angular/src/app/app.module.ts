import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { appInterceptor } from 'src/Core/interceptor/app.interceptor';
import { HeaderComponent } from 'src/layout/header/header.component';
import { SideBarComponent } from 'src/layout/side-bar/side-bar.component';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HeaderComponent,
    SideBarComponent,
    CommonModule,
     RouterOutlet
  ],
  providers: [      provideHttpClient(withFetch(), withInterceptors([appInterceptor])),

],
  bootstrap: [AppComponent]
})
export class AppModule { }
