using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Invoice
    {
        public static IQueryable<DAL.DTO.Invoice> getInvoice()
        {
            return DAL.Invoice.getInvoice();
        }
        
        public static DAL.DTO.InvoiceableItems GetInvoiceableItems(int InternalOrderId)
        {
          return DAL.Invoice.GetInvoiceableItems(InternalOrderId);
        }

        public static DAL.DTO.InvoiceOrderData GetInvoiceInternalOrderData(DAL.DTO.ToBeInvoicedItems ToBeInvoicedItems)
        {
            switch (ToBeInvoicedItems.action)
            {
                case (int)DAL.Constants.InvoiceAction.ADD:
                    {
                        return DAL.Invoice.GetNewInvoiceInternalOrderData(ToBeInvoicedItems);
                    }
                case (int)DAL.Constants.InvoiceAction.UPDATE:
                    {
                        return DAL.Invoice.GetInvoiceData(ToBeInvoicedItems.InternalOrderId);
                    }
                case (int)DAL.Constants.InvoiceAction.REMOVE:
                    {
                        return DAL.Invoice.GetInvoiceData(ToBeInvoicedItems.InternalOrderId);
                    }
                default: throw new Exception();
            }

        }

        public static int AddUpdateInvoice(DAL.DTO.Invoice invoice)
        {
            if (invoice.Id > 0)
            {
                if (invoice.ExpectedTotal > invoice.Total)
                {
                    BLL.Emailing.InvoiceNotification(invoice.InvoiceNumber);
                }
                return DAL.Invoice.UpdateInvoice(invoice);
            }
            else
            {
                if (invoice.ExpectedTotal > invoice.Total)
                {
                    BLL.Emailing.InvoiceNotification(invoice.InvoiceNumber);
                }
                return DAL.Invoice.AddInvoice(invoice);                
            }


        }

        public static int RemoveInvoice(int id)
        {
            return DAL.Invoice.RemoveInvoice(id);
        }

        public static List<DAL.DTO.Grn> GetCapturedGrnItems(int id)
        {          
            return DAL.Invoice.GetCapturedGrnItems(id);
        }
    }
}
