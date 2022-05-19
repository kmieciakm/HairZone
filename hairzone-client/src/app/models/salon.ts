import { Address } from "./address";

export class Salon {
    constructor(
        public guid: string,
        public name: string,
        public phone: string,
        public email: string,
        public address: Address
    ) { }
}