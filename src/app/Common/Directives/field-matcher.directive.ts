import { Directive, Input } from '@angular/core';
import { FormGroup, NG_VALIDATORS, ValidationErrors, Validators } from '@angular/forms';

@Directive({
  selector: '[fHFieldMatcher]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: FieldMatcherDirective,
      multi: true
    }
  ]
})
export class FieldMatcherDirective implements Validators {

  @Input('fHFieldMatcher') values: string[] = [];
  constructor() { }

  validate(formGroup: FormGroup): ValidationErrors {
    //console.log("validator executed")
    return MatchFields(this.values[0], this.values[1])(formGroup);
  }

}

function MatchFields(controlName: string, matchingControlName: string) {

 // console.log("matcher executed");
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];
    const matchingControl = formGroup.controls[matchingControlName];

    // console.log(control);
    // console.log(matchingControl);
    if (!control || !matchingControl) return null

    if (matchingControl.errors && !matchingControl.errors.fHFieldMatcher) return null;

    if (control.value !== matchingControl.value) {
      //console.log("ohh")
      matchingControl.setErrors({ fHFieldMatcher: true })
    } else {
      //console.log("matvh");
      matchingControl.setErrors(null);
    }
  }
}
