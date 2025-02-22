import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms'

@Component({
  selector: 'app-contact-page',
  imports: [
    FormsModule,
    NgIf
  ],
  templateUrl: './contact-page.component.html',
  styleUrl: './contact-page.component.css'
})
export class ContactPageComponent { // Replace YourComponent with your actual component name
  formData = {
    fName: '',
    lName: '',
    email: '',
    subject: '',
    message: ''
  };

  onSubmit(form: NgForm) {
    if (form.valid) {
      console.log("Form submitted:", this.formData);
      form.reset();
    } else {
      console.log("Form is invalid. Please correct the errors.");
    }
  }
}