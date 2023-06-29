import { internalOrderItems } from './internalOrderItems.models';
import { onceOffItems } from './onceOffItems.models';
import { services } from './services.models';

export class internalOrder {
  id: number;
  requestedById: number;
  placedById: number;
  quotationNumber: string;
  supplierId: number;
  plantLocationId: number;
  glcodeId: number;
  statusId: number;
  approveByFullName: string;
  dateApproved: Date;
  approveById: number;
  followUpDate: Date;
  deliveryDate: Date;
  total: number;
  comment: string;
  internalComment: string;
  supplierComment: string;
  vat: number;
  totalinclvat: number;
  internalOrderItems: internalOrderItems[];
  onceOffItems: onceOffItems[];
  services: services[];
}
