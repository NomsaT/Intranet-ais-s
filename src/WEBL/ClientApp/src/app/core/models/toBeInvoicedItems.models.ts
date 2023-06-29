import { internalOrderItems } from './internalOrderItems.models';
import { onceOffItems } from './onceOffItems.models';
import { services } from './services.models';

export class toBeInvoicedItems {
  InternalOrderId: number;
  action: number;
  ListedItem: number[];
  OnceOffItem: number[];
  Service: number[];
}
