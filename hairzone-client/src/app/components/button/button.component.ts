import { Component, OnInit, Input, Output, ViewChild, ElementRef, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.css']
})
export class ButtonComponent implements OnInit {

  @Input() text: string = '';
  @Input() width: string = 'auto';
  @Input() padding: string | undefined = undefined;
  @Input() bgColor: string | undefined = undefined;
  @Input() submit: boolean = false;
  
  @Output() onClick: EventEmitter<MouseEvent> = new EventEmitter();

  @ViewChild('button') button?: ElementRef;

  constructor() { }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    this.setStyleProperty("width", this.width);
    this.setStyleProperty("padding", this.padding);
    this.setStyleProperty("background-color", this.bgColor);
  }

  private setStyleProperty(name: string, value: unknown) {
    if (this.button) {
      this.button.nativeElement.style.setProperty(name, value);
    }
  }

}
