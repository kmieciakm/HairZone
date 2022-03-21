import { NgModule } from "@angular/core";
import { ButtonModule } from 'primeng/button';

const primeng = [
  ButtonModule
];

@NgModule({
  imports: primeng,
  exports: primeng
})
export class PrimengModule { }
