import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ConfigService } from './config.service';
import { LoginUser, RegisterUser, AuthResponse } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'auth_token';
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasToken());
  
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  constructor(
    private http: HttpClient,
    private configService: ConfigService,
    private router: Router
  ) {}

  login(credentials: LoginUser): Observable<string> {
    const url = `${this.configService.getAuthUrl()}/enter`;
    return this.http.post<AuthResponse>(url, credentials).pipe(
      map(response => {
        if (response && response.data) {
          this.setToken(response.data);
          this.isAuthenticatedSubject.next(true);
          return response.data;
        } else {
          throw new Error('Login failed');
        }
      }),
      catchError(error => {
        this.isAuthenticatedSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  register(user: RegisterUser): Observable<string> {
    const url = `${this.configService.getAuthUrl()}/new`;
    return this.http.post<AuthResponse>(url, user).pipe(
      map(response => {
        if (response && response.data) {
          this.setToken(response.data);
          this.isAuthenticatedSubject.next(true);
          return response.data;
        } else {
          throw new Error('Registration failed');
        }
      }),
      catchError(error => {
        this.isAuthenticatedSubject.next(false);
        return throwError(() => error);
      })
    );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.isAuthenticatedSubject.next(false);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  private setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  private hasToken(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }
  
  isTokenExpired(): boolean {
    const token = this.getToken();
    if (!token) return true;
    
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const expirationDate = new Date(payload.exp * 1000);
      return expirationDate < new Date();
    } catch (e) {
      return true;
    }
  }
  
    // Método para verificar se o usuário está autenticado
  checkAuthStatus(): void {
    const isAuthenticated = this.hasToken() && !this.isTokenExpired();
    this.isAuthenticatedSubject.next(isAuthenticated);
    
    if (!isAuthenticated && this.router.url !== '/login' && this.router.url !== '/register') {
      this.logout();
    }
  }
}