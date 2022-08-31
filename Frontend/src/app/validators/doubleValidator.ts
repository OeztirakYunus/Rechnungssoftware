import {AbstractControl, ValidationErrors, ValidatorFn} from '@angular/forms';

export function doubleValidator(): ValidatorFn {
  return (control:AbstractControl) : ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    const regex = /^[-]?\d*\,?\d+$/gis;
    return !regex.test(value) ? {double:true}: null;
  }
}
