import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeFormat',
})
export class TimeFormatPipe implements PipeTransform {
  transform(value?: string): string {
    return value ? value.substring(0, 5) : '';
  }
}
