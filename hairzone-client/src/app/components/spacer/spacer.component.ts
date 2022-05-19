import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-spacer',
  templateUrl: './spacer.component.html',
  styleUrls: ['./spacer.component.css']
})
export class SpacerComponent implements OnInit, AfterViewInit {

  @Input() height: string = "1rem";
  @ViewChild('spacer') spacer?: ElementRef;

  constructor() { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    if(this.spacer) {
      this.spacer.nativeElement.style.setProperty('height', this.height);
    }
  }

}
