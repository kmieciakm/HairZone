import { NgModule } from "@angular/core";
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';

const primeng = [
  ButtonModule,
  InputTextModule
];

@NgModule({
  imports: primeng,
  exports: primeng
})
export class PrimengModule { }
