import {AbstractControl, ValidationErrors, ValidatorFn} from '@angular/forms';

export function undefinedValidator(): ValidatorFn {
  return (control:AbstractControl) : ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    return value == "undefined" ? {undefined:true}: null;
  }
}
