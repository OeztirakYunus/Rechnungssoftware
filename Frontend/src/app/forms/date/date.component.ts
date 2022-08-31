import {Component, Input, OnInit} from '@angular/core';
import {ControlValueAccessor, FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators} from "@angular/forms";
import {intValidator} from "../../validators/intValidator";
import {doubleValidator} from "../../validators/doubleValidator";

@Component({
  selector: 'app-date',
  templateUrl: './date.component.html',
  styleUrls: ['./date.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: DateComponent
    }
  ]
})
export class DateComponent implements ControlValueAccessor {
  constructor(
    private fb: FormBuilder
  ) {
    this.dateForm.valueChanges.subscribe(x => {
      const regex = new RegExp(this.regex, 'g');
      const match = regex.exec(x.date);
      if(match){
        const day = Number(match[1]);
        const month = Number(match[2]);
        const year = Number(match[3]);
        const date = new Date(year, month-1, day);
        this.onChange(date);

      }
    })
  }
  readonly regex = '^\\s*(3[01]|[12][0-9]|0?[1-9])\\.(1[012]|0?[1-9])\\.((?:19|20)\\d{2})\\s*$';

  dateForm: FormGroup = this.fb.group({
      date: [this.format(new Date()), [Validators.required, Validators.pattern(this.regex)]],
    });

  onChange = (x: any) => {};
  onTouched = () => {};
  @Input() requiredDateText: string = "Error";
  @Input() invalidDateText: string = "Error";
  @Input() label: string = "";
  @Input() inputId: string = "";

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  format(date: Date){
    date = new Date(date);
    return String(date.getDate()).padStart(2, '0') +
      "." +
      String(date.getMonth() + 1).padStart(2, '0') +
      "." +
      String(date.getFullYear()).padStart(4, '0');
  }

  writeValue(obj: any): void {
    const typed = obj as Date;
    if(typed == null){
      return;
    }
    this.date?.setValue(this.format(typed));
  }

  get date(){
    return this.dateForm.get('date');
  }
}
