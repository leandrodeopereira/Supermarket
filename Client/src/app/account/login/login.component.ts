import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { AccountService } from '../account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private accountService: AccountService, private router: Router) {}

  ngOnInit(): void {
    this.createLoginForm();
  }

  createLoginForm(): void {
    this.loginForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  onSubmit(): void {
    this.accountService.login(this.loginForm.value).subscribe(
      () => {
        console.log('user logged in');
        this.router.navigateByUrl('/shop');
      },
      (error) => {
        console.log(error);
      }
    );
    console.log(this.loginForm.value);
  }
}
