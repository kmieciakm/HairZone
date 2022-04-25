import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { EmailValidator } from '@angular/forms';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { APPROUTES } from 'src/app/helpers/approutes';
import { IdentityService, User } from 'src/app/services/identity/identity.service';
import { environment } from 'src/environments/environment';

@Component({
  templateUrl: './management.component.html',
  styleUrls: ['./management.component.css']
})
export class ManagementComponent implements OnInit {

  message: string = '';
  user: User;

  constructor(private httpClient: HttpClient, private router: Router, private identityService: IdentityService) {
    let loggedInUser = identityService.getLoggedInUser();
    if (loggedInUser)
    {
      this.user = loggedInUser;
    }
    else
    {
      router.navigate(["/login"]);
      this.user = loggedInUser ?? new User('', []);
    }
  }

  ngOnInit(): void {
    this.httpClient
        .get<string>(
          `${environment.identityUrl}/test/hello?name=${this.user.email}`,
          { responseType: 'text' } as Record<string, unknown>)
        .subscribe(message => this.message = message);
  }

  logout(): void {
    this.identityService.logout();
    this.router.navigate([APPROUTES.HOME]);
  }

}
