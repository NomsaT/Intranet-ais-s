
export class Customer {

  id: number = 0;
  companyName: string = '';
  companyPrefix: string = '';
  paymentMethodId: number;
  accountNumber: string = '';
  description: string = '';
  physicalAddressId: number;
  deliveryAddressId: number;
  physicalStreetAddress1: string = '';
  physicalStreetAddress2: string = '';
  physicalSuburb: string = '';
  physicalCity: string = '';
  physicalPostCode: number = 0;
  physicalCountry: string = '';
  deliveryStreetAddress1: string = '';
  deliveryStreetAddress2: string = '';
  deliverySuburb: string = '';
  deliveryCity: string = '';
  deliveryPostCode: number = 0;
  deliveryCountry: string = '';
  contacts: Array<any> = [];


}
