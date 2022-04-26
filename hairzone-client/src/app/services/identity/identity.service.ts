import { HttpClient, JsonpClientBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import jwtDecode, { JwtPayload } from "jwt-decode";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private http: HttpClient) { }

  login(signIn: SignIn): Observable<void> {
    return this.http
      .post<Token>(`${environment.identityUrl}/identity/login`, signIn)
      .pipe(map(token => {
        localStorage.setItem('user_token', token.jwt);
        const userPayload = jwtDecode<UserPayload>(token.jwt);
        const user = UserPayload.getUser(userPayload);
        localStorage.setItem('user', JSON.stringify(user));
      }));
  }

  getAccessToken(): string | null {
    return localStorage.getItem("user_token");
  }

  getLoggedInUser(): User | null {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      return JSON.parse(storedUser);
    }
    return null;
  }

  forgotPassword(email: string): Observable<void> {
    return this.http
      .post<void>(`${environment.identityUrl}/identity/forgotpassword`, new ForgotPasswordRequest(email));
  }

  resetPassword(resetPassword: ResetPasswordRequest): Observable<void> {
    return this.http
      .post<void>(`${environment.identityUrl}/identity/resetpassword`, resetPassword);
  }

  logout(): void {
    localStorage.removeItem("user_token");
    localStorage.removeItem("user");
  }
}

export class SignIn {
  constructor(public email: string, public password: string) { }
}

export class User {
  constructor(public email: string, public roles: string[]) { }
}

class Token {
  constructor(public jwt: string) { }
}

class ForgotPasswordRequest {
  constructor(public email: string) { }
}

export class ResetPasswordRequest {
  constructor(public email: string, public newpassword: string, public token: string) { }
}

class UserPayload implements JwtPayload {
  iss?: string;
  sub?: string;
  aud?: string[] | string;
  exp?: number;
  nbf?: number;
  iat?: number;
  jti?: string;
  email?: string;
  role?: string[] | string;

  static getUser(payload: UserPayload): User | null {
    if (payload.email && payload.role)
    {
      let roles: string[] = []; 
      if (typeof payload.role === "string")
      {
        roles = [payload.role];
      }
      roles = payload.role as string[];
      return new User(payload.email, roles);
    }
    return null;
  }
}