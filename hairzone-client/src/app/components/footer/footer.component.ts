import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  
  email: string = "hairzone@hirzone.com";
  phone: string = "123 456 789";

  constructor() { }

  ngOnInit(): void {
  }

}
