import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css']
})
export class InputComponent implements OnInit {

  @Input() label: string = '';
  @Input() type: string = 'text';
  @Input() formInputName: string = '';
  @Input() parentGroup!: FormGroup;

  ngOnInit(): void {
  }

}
