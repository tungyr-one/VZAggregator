export interface AddressEntity {
  }
  
  export interface OrderEntity {
  }
  
  export enum SubscriptionPeriod {
    Monthly = 0,
    Quarterly = 1,
    Yearly = 2
  }
  
  export interface UserEntity {
    userId: number;
    created: string;
    updated?: string;
    name?: string;
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
  }
  