import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { APPROUTES } from 'src/app/helpers/approutes';
import { IdentityService, ResetPasswordRequest } from 'src/app/services/identity/identity.service';

@Component({
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  private token: string | undefined;
  formErrorMessage: string | undefined;
  resetForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    newPassword: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required])
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private identityService: IdentityService
  ) { }

  ngOnInit(): void {
    this.token = this.extractToken();
    console.log(this.token);
  }

  onSubmit() {
    if (this.resetForm.valid) {
      var newPassword = this.resetForm.value.newPassword;
      var confirmPassword = this.resetForm.value.confirmPassword;

      if (newPassword !== confirmPassword) {
        this.formErrorMessage = "Hasła są różne"
        return;
      }

      if (this.token)
      {
        let request = new ResetPasswordRequest(
          this.resetForm.value.email,
          newPassword,
          this.token
        );
        this.performPasswordReset(request);
        return;
      }
      this.formErrorMessage = "Nie można zmienić hasła";
    }
  }

  private extractToken() {
    return this.route.snapshot.queryParams['token'];
  }

  private performPasswordReset(request: ResetPasswordRequest) {
    this.identityService.resetPassword(request)
      .subscribe({
        next: () => {
          this.clearForm();
          this.router.navigate([APPROUTES.LOGIN], { queryParams: { reset: 1}});
        },
        error: err => {
          this.clearForm();
          this.formErrorMessage = "Nie udało się zmienić hasła";
        }
      });
  }

  private clearForm(): void {
    this.resetForm.get('email')?.patchValue('');
    this.resetForm.get('newPassword')?.patchValue('');
    this.resetForm.get('confirmPassword')?.patchValue('');
  }

}
