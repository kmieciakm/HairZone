import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { APPROUTES } from 'src/app/helpers/approutes';
import { IdentityService, SignIn } from 'src/app/services/identity/identity.service';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formErrorMessage: string | undefined;
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(private identityService: IdentityService, private router: Router) { }

  ngOnInit(): void {
    if (this.identityService.getLoggedInUser())
    {
      this.router.navigate([APPROUTES.MANAGEMENT]);
    }
  }

  onSubmit() {
    if (this.loginForm.valid) {
      let signIn = new SignIn(
        this.loginForm.value.email,
        this.loginForm.value.password
      );
      this.identityService.login(signIn).subscribe({
        next: () => this.router.navigateByUrl(APPROUTES.MANAGEMENT),
        error: err => {
          this.loginForm.get('password')?.patchValue('');
          this.formErrorMessage = err.error;
          console.log(err.error); }
      });
    }
  }

}
