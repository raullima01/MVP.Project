import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { APP_INITIALIZER } from '@angular/core';
import { ConfigService } from './core/services/config.service';
import { authInterceptor } from './core/interceptors/auth.interceptor';
import { firstValueFrom } from 'rxjs';

export function initializeApp(configService: ConfigService) {
  return () => firstValueFrom(configService.loadConfig());
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])),
    provideAnimations(),
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [ConfigService],
      multi: true
    }
  ]
};