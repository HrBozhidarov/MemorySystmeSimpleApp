import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { IdentityService } from 'src/app/services/identity/identity.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;
  public submitted: boolean = false;

  constructor(
    private fb: FormBuilder, 
    private identityService: IdentityService,
    private router: Router) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(3)]]
    });
  }
  
  get f() { return this.loginForm.controls; }

  public onLogin() {
    this.submitted = true;

    if (this.loginForm.invalid) {
        return;
    }

    this.identityService.login(this.loginForm.value).subscribe(data => {
      this.identityService.saveToken(data.data);
      this.router.navigate(['/home'])
    })
  }
}
