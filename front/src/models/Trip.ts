import { Address } from "./Address";
import { Transport } from "./Transport";
import { Carrier } from "./Carrier";

  export interface Trip {
    tripId: number;
    orders?: any[]; // Define Orders if needed
    tripType: string;
    departureAddress: Address;
    destinationAddress: Address;
    created: string;
    updated?: string;
    tripDateTime: string;
    passengersCapacity: number;
    tripPrice: number;
    carrierId: number;
    carrier: Carrier;
    transportId: number;
    transport: Transport;
  }
  