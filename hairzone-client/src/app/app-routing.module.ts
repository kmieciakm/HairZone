import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './helpers/auth.guard';
import { BookComponent } from './pages/book/book.component';
import { ErrorComponent } from './pages/error/error.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { ManagementComponent } from './pages/management/management.component';
import { PrivacyComponent } from './pages/privacy/privacy.component';
import { ResetPasswordComponent } from './pages/reset-password/reset-password.component';

const routes: Routes = [
  { path: "", redirectTo: "home", pathMatch: "full" },
  { path: "home", component: HomeComponent },
  { path: "book/:id", component: BookComponent },
  { path: "privacy", component: PrivacyComponent },
  { path: "login", component: LoginComponent },
  { path: "reset-password", component: ResetPasswordComponent },
  { path: "forgot-password", component: ForgotPasswordComponent },
  { path: "management", component: ManagementComponent, canActivate: [AuthGuard] },
  { path: "**", component: ErrorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
