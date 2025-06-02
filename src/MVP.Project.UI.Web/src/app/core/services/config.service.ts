// src/app/core/services/config.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private config: any = null;
  private configUrl = 'assets/config/config.json';

  constructor(private http: HttpClient) {}

  /**
   * Carrega o arquivo de configuração
   */
  loadConfig(): Observable<any> {
    return this.http.get<any>(this.configUrl).pipe(
      tap(config => {
        this.config = config;
        console.log('Configuração carregada', config);
      }),
      catchError(error => {
        console.error('Erro ao carregar configuração', error);
        return throwError(() => new Error('Falha ao carregar configuração'));
      })
    );
  }

  /**
   * Obtém um valor de configuração pelo nome
   */
  get(key: string): any {
    return this.config ? this.config[key] : null;
  }

  /**
   * Obtém a URL completa da API
   */
  getApiUrl(): string {
    return this.config ? this.config.apiUrl : '';
  }

  /**
   * Obtém a URL completa para um endpoint específico
   */
  getFullApiUrl(endpoint: string): string {
    return `${this.getApiUrl()}${endpoint}`;
  }

  /**
   * Obtém a URL de autenticação
   */
  getAuthUrl(): string {
    return this.getFullApiUrl(this.config.authEndpoint);
  }
}