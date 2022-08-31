import {AbstractControl, ValidationErrors, ValidatorFn} from '@angular/forms';

export function intValidator(): ValidatorFn {
  return (control:AbstractControl) : ValidationErrors | null => {
    const value = control.value;
    if (value == null) {
      return null;
    }
    const regex = /^[+-]?[0-9]+$/g;
    return !regex.test(value) ? {int:true}: null;
  }
}
