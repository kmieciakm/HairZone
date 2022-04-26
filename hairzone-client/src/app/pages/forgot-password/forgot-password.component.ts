import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IdentityService } from 'src/app/services/identity/identity.service';

@Component({
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  message: string = '';
  forgotForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
  });

  constructor(private identityService: IdentityService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit() {
    if (this.forgotForm.valid) {
      this.identityService.forgotPassword(this.forgotForm.value.email)
        .subscribe({
          next: () => {
            this.forgotForm.get('email')?.patchValue('');
            this.message = "Wysłano email z linkiem do zmiany hasła";
          },
          error: err => {
            this.forgotForm.get('email')?.patchValue('');
          }
        });
    }
  }

}
