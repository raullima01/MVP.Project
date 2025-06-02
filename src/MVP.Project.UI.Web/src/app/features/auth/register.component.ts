// src/app/features/auth/register/register.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AuthService } from '../../core/services/auth.service';
import { RegisterUser } from '../../core/models/register.user.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatProgressSpinnerModule
  ],
  template: `
    <div class="register-container">
      <mat-card>
        <mat-card-header>
          <mat-card-title>Register</mat-card-title>
        </mat-card-header>
        
        <mat-card-content>
          <form [formGroup]="registerForm" (ngSubmit)="onSubmit()">
            <mat-form-field appearance="outline" class="full-width">
              <mat-label>Email</mat-label>
              <input matInput formControlName="email" type="email" required>
              @if (registerForm.get('email')?.invalid && registerForm.get('email')?.touched) {
                <mat-error>
                  @if (registerForm.get('email')?.hasError('required')) {
                    Email is required
                  } @else if (registerForm.get('email')?.hasError('email')) {
                    Please enter a valid email
                  }
                </mat-error>
              }
            </mat-form-field>
            
            <mat-form-field appearance="outline" class="full-width">
              <mat-label>Password</mat-label>
              <input matInput formControlName="password" type="password" required>
              @if (registerForm.get('password')?.invalid && registerForm.get('password')?.touched) {
                <mat-error>
                  @if (registerForm.get('password')?.hasError('required')) {
                    Password is required
                  } @else if (registerForm.get('password')?.hasError('minlength')) {
                    Password must be at least 6 characters
                  }
                </mat-error>
              }
            </mat-form-field>
            
            <mat-form-field appearance="outline" class="full-width">
              <mat-label>Confirm Password</mat-label>
              <input matInput formControlName="confirmPassword" type="password" required>
              @if (registerForm.get('confirmPassword')?.invalid && registerForm.get('confirmPassword')?.touched) {
                <mat-error>
                  @if (registerForm.get('confirmPassword')?.hasError('required')) {
                    Confirm Password is required
                  } @else if (registerForm.get('confirmPassword')?.hasError('mustMatch')) {
                    Passwords must match
                  }
                </mat-error>
              }
            </mat-form-field>
            
            @if (errorMessage) {
              <div class="error-message">{{ errorMessage }}</div>
            }
            
            <div class="button-row">
              <button 
                mat-raised-button 
                color="primary" 
                type="submit" 
                [disabled]="registerForm.invalid || isLoading">
                @if (isLoading) {
                  <mat-spinner diameter="24"></mat-spinner>
                } @else {
                  Register
                }
              </button>
              
              <a mat-button routerLink="/login">Already have an account? Login</a>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: `
    .register-container {
      display: flex;
      justify-content: center;
      align-items: center;
      height: 100vh;
    }
    
    mat-card {
      max-width: 400px;
      width: 100%;
      padding: 20px;
    }
    
    .full-width {
      width: 100%;
      margin-bottom: 15px;
    }
    
    .button-row {
      display: flex;
      flex-direction: column;
      gap: 10px;
      margin-top: 20px;
    }
    
    .error-message {
      color: #f44336;
      margin: 10px 0;
    }
  `
})
export class RegisterComponent {
  registerForm: FormGroup;
  isLoading = false;
  errorMessage = '';
  
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, {
      validators: this.passwordMatchValidator
    });
  }
  
  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;
    
    if (password !== confirmPassword) {
      form.get('confirmPassword')?.setErrors({ mustMatch: true });
      return { mustMatch: true };
    }
    
    return null;
  }
  
  onSubmit(): void {
    if (this.registerForm.invalid) return;
    
    this.isLoading = true;
    this.errorMessage = '';
    
    const registerData: RegisterUser = this.registerForm.value;
    
    this.authService.register(registerData).subscribe({
      next: () => {
        this.isLoading = false;
        this.router.navigate(['/customers']);
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.message || 'Registration failed. Please try again.';
      }
    });
  }
}