import { TestBed } from '@angular/core/testing';

import { FitHubHttpInterceptorInterceptor } from './fit-hub-http-interceptor.interceptor';

describe('FitHubHttpInterceptorInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      FitHubHttpInterceptorInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: FitHubHttpInterceptorInterceptor = TestBed.inject(FitHubHttpInterceptorInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
