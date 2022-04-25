import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { APPROUTES } from 'src/app/helpers/approutes';
import { IdentityService, User } from 'src/app/services/identity/identity.service';

@Component({
  templateUrl: './management.component.html',
  styleUrls: ['./management.component.css']
})
export class ManagementComponent implements OnInit {

  user: User;

  constructor(private router: Router, private identityService: IdentityService) {
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
  }

  logout(): void {
    this.identityService.logout();
    this.router.navigate([APPROUTES.HOME]);
  }

}
