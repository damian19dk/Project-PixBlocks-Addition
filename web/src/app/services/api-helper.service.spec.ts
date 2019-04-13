import { TestBed } from '@angular/core/testing';

import { ApiHelperService } from './api-helper.service';

describe('ApiHelperService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApiHelperService = TestBed.get(ApiHelperService);
    expect(service).toBeTruthy();
  });
});
