/* tslint:disable */
import { Injectable } from '@angular/core';

/**
 * Global configuration for UtmApiV1 services
 */
@Injectable({
  providedIn: 'root',
})
export class UtmApiV1Configuration {
  rootUrl: string = '';
}

export interface UtmApiV1ConfigurationInterface {
  rootUrl?: string;
}
