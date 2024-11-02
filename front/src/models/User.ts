import { Role } from "./Role";

export interface AddressEntity {
  }
  
  export interface OrderEntity {
  }
  
  export enum SubscriptionPeriod {
    Monthly = 0,
    Quarterly = 1,
    Yearly = 2
  }
  
  export interface User {
    id: number;
    created: string;
    updated?: string;
    userName?: string;
    telegramNick: string;
    birthDate?: string;
    lastTrip?: string;
    phoneNumber?: string;
    email?: string;
    addresses: AddressEntity[];
    tripsCount: number;
    discount?: number;
    subscriptionType?: SubscriptionPeriod;
    isSubscriptionActive?: boolean;
    subscriptionTripsCount?: number;
    notes?: string;
    orders: OrderEntity[];
    userRoles: Role[];
  }
  