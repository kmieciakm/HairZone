import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Salon } from 'src/app/models/salon';
import { ISalonService } from 'src/app/services/salon/salon.service';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  currentCity: string | undefined;
  cities$: Observable<string[]> | undefined;
  salons$: Observable<Salon[]> | undefined;

  constructor(
    private salonService: ISalonService
  ) { }

  ngOnInit(): void {
    this.cities$ = this.salonService.getCities();
    this.cities$.subscribe(cities => {
      if(cities.length > 0) {
        let firstCity = cities[0];
        this.showSalons(firstCity);
      }
    });
  }

  showSalons(city: string): void {
    this.currentCity = city;
    this.salons$ = this.salonService.getSalonsByCity(city);
  }

  scroll(el: HTMLElement): void {
    el.scrollIntoView({behavior: "smooth"});
  }
  
}
