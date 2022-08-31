import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'myDecimal'
})
export class MyDecimalPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): any {
    return Number(value).toFixed(2).replace('.', ',');
  }

}
