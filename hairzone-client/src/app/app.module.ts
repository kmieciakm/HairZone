import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { PrimengModule } from './app-primeng.module';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { ErrorComponent } from './pages/error/error.component';
import { LoginComponent } from './pages/login/login.component';
import { PrivacyComponent } from './pages/privacy/privacy.component';
import { ManagementComponent } from './pages/management/management.component';

import { ButtonComponent } from './components/button/button.component';
import { InputComponent } from './components/input/input.component';
import { MenuComponent } from './components/menu/menu.component';
import { FooterComponent } from './components/footer/footer.component';
import { AuthorizeInterceptor } from './helpers/auth.interceptor';
import { ResetPasswordComponent } from './pages/reset-password/reset-password.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { SalonCardComponent } from './components/salon-card/salon-card.component';
import { SpacerComponent } from './components/spacer/spacer.component';
import { FakeSalonService, ISalonService } from './services/salon/salon.service';
import { BookComponent } from './pages/book/book.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ErrorComponent,
    PrivacyComponent,
    ManagementComponent,
    LoginComponent,
    BookComponent,
    FooterComponent,
    ResetPasswordComponent,
    ForgotPasswordComponent,
    ButtonComponent,
    InputComponent,
    MenuComponent,
    SalonCardComponent,
    SpacerComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    PrimengModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: ISalonService, useClass: FakeSalonService }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
