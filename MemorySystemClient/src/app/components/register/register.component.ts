import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup;
  public submitted: boolean = false;
  
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  get f() { return this.registerForm.controls; }

  public onRegister() {
    this.submitted = true;

    if (this.registerForm.invalid) {
        return;
    }

    alert('SUCCESS!! :-)')
  }
}
