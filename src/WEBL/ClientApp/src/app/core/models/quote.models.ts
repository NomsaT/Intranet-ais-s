import { quoteItems } from "./quoteItems.models";
import { quoteTransports } from "./quoteTransports.models";
import { quoteRevision } from "./quoteRevision.models";


export class quote {
  id: number;
  requestedById: number;
  placedById: number;
  customerId: number;
  submissionDate: Date;
  validFor: number;
  daysFrom: number;
  onDelivery: number;
  onOrder: number;
  total: number;
  vat: number;
  totalInclVat: number;
  quoteItems: quoteItems[];
  quoteTransports: quoteTransports[];
  quoteRevision: quoteRevision[];
}
