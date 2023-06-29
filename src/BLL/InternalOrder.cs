using DevExpress.ClipboardSource.SpreadsheetML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BLL
{
    public static class InternalOrder
    {
        public static IQueryable<DAL.DTO.InternalOrder> getInternalOrder()
        {
            return DAL.InternalOrder.getInternalOrder();
        }
        public static IQueryable<DAL.DTO.InternalOrder> getApprovedInternalOrders()
        {
            return DAL.InternalOrder.getApprovedInternalOrders();
        }

        public static IQueryable<DAL.DTO.InternalOrder> getInternalOdersByDateCreated(DateTime startDate, DateTime endDate)
        {
            return DAL.InternalOrder.getInternalOdersByDateCreated(startDate, endDate);
        }

        public static object getAllOrders()
        {
            return DAL.InternalOrder.getAllOrders();
        }

        public static int AddUpdateInternalOrder(DAL.DTO.InternalOrder values)
        {
            if (values.Id > 0)
            {
                int result = DAL.InternalOrder.editInternalOrder(values.Id, values);
                DAL.InternalOrder.validateInternalOrder(values.Id, values.Username);

                Thread validate = new(delegate ()
                {
                    BLL.Emailing.EditInternalOrderEmailing(values.Id);
               

                List<DAL.DTO.InternalOrderItem> increasedItems = DAL.InternalOrder.getPriceIncreaseItemList(values);
                if (increasedItems.Count > 0)
                {
                    BLL.Emailing.MonitoredPriceApprovalEmail(increasedItems);
                   
                } 
                });
                validate.IsBackground = true;
                validate.Start();

                return result;
            }
            else
            {
                int key = DAL.InternalOrder.addInternalOrder(values);
                DAL.InternalOrder.validateInternalOrder(key, values.Username);

                Thread validate = new(delegate ()
                {
                    BLL.Emailing.AddInternalOrderEmailing(key);
               
                List<DAL.DTO.InternalOrderItem> increasedItems = DAL.InternalOrder.getPriceIncreaseItemList(values);
                if (increasedItems.Count > 0)
                {
                    
                        BLL.Emailing.MonitoredPriceApprovalEmail(increasedItems);
                   
                } 
                });
                validate.IsBackground = true;
                validate.Start();
                return key;
            }
        }

        public static int AddUpdateDraftInternalOrder(DAL.DTO.InternalOrder values)
        {
            if (values.StatusId == 6)
            {
                int result = DAL.InternalOrder.editInternalOrderDraft(values.Id, values);
                return result;
            }
            else
            {
                int key = DAL.InternalOrder.addInternalOrderDraft(values);
                return key;
            }
        }

        public static int editApproveOrder(int id)
        {
            int key = DAL.InternalOrder.editApproveOrder(id);
            Thread email = new(delegate ()
            {
                BLL.Emailing.ApproveInternalOrderEmailing(id);

            });
            email.IsBackground = true;
            email.Start();
            return key;
        }

        public static int editDenyOrder(DAL.DTO.InternalOrderAction action)
        {
            int key = DAL.InternalOrder.editDenyOrder(action);

            Thread email = new(delegate ()
            {
                BLL.Emailing.DeniedInternalOrderEmailing(action.Id);
            });

            email.IsBackground = true;
            email.Start();
          
            return key;
        }

        public static int deleteDraftOrder(int Id)
        {
            DAL.InternalOrder.removeDenyOrder(Id);
            return 1;
        }

        public static int editReviewOrder(DAL.DTO.InternalOrderAction action)
        {

            int key = DAL.InternalOrder.editReviewOrder(action);

            Thread email = new(delegate ()
            {
              BLL.Emailing.ReviewInternalOrderEmailing(action.Id);
            });

            email.IsBackground = true;
            email.Start();
            return key;
        }

        public static IQueryable<DAL.DTO.InternalOrder> getApprovedGRNInternalOrder()
        {
            return DAL.InternalOrder.getApprovedGRNInternalOrder();
        }

        public static IQueryable<DAL.DTO.InternalOrder> getApprovedInternalOrder()
        {
            return DAL.InternalOrder.getApprovedInternalOrder();
        }

        public static IQueryable<DAL.DTO.InternalOrder> getApprovedInternalOrderAppClose()
        {
            return DAL.InternalOrder.getApprovedInternalOrderAppClose();
        }
        
        public static IQueryable<DAL.DTO.InternalOrder> getApprovedInternalOrderGrn()
        {
            return DAL.InternalOrder.getApprovedInternalOrderGrn();
        }

        public static IQueryable<DAL.DTO.InternalOrder> getApprovedInternalOrderInvoice()
        {
            return DAL.InternalOrder.getApprovedInternalOrderInvoice();
        }

        public static DAL.DTO.InternalOrder GetInternalOrderData(int id, int action)
        {
            switch (action)
            {
                case (int)DAL.Constants.InternalOrder.ADD:
                    {
                        return null;// DAL.InternalOrder.getInternalOrder();
                    }
                case (int)DAL.Constants.InternalOrder.UPDATE:
                    {
                        return DAL.InternalOrder.getInternalOrderDataEdit(id);
                    }
                default: throw new Exception();
            }

        }
        public static object GetMonitoredItems(int id)
        {
            return DAL.InternalOrder.GetMonitoredItems(id);
        }

        public static object getDefaultCompany()
        {
            return DAL.InternalOrder.getDefaultCompany();
        }
    }
}
