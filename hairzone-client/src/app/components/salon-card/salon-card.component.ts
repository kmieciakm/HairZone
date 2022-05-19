import { Component, Input, OnInit } from '@angular/core';
import { Salon } from 'src/app/models/salon';

@Component({
  selector: 'app-salon-card',
  templateUrl: './salon-card.component.html',
  styleUrls: ['./salon-card.component.css']
})
export class SalonCardComponent implements OnInit {

  @Input() salon: Salon | undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
