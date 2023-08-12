/* tslint:disable */
import { NgModule, ModuleWithProviders } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { UtmApiV1Configuration, UtmApiV1ConfigurationInterface } from './utm-api-v1configuration';

import { UserTasksService } from './services/user-tasks.service';

/**
 * Provider for all UtmApiV1 services, plus UtmApiV1Configuration
 */
@NgModule({
  imports: [
    HttpClientModule
  ],
  exports: [
    HttpClientModule
  ],
  declarations: [],
  providers: [
    UtmApiV1Configuration,
    UserTasksService
  ],
})
export class UtmApiV1Module {
  static forRoot(customParams: UtmApiV1ConfigurationInterface): ModuleWithProviders<UtmApiV1Module> {
    return {
      ngModule: UtmApiV1Module,
      providers: [
        {
          provide: UtmApiV1Configuration,
          useValue: {rootUrl: customParams.rootUrl}
        }
      ]
    }
  }
}
