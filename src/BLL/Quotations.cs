using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BLL
{
    public static class Quotations
    {
        public static IQueryable<DAL.DTO.Quote> getQuotations()
        {
            return DAL.Quotations.getQuotations();
        }

        public static DAL.DTO.Quote GetQuotationsData(int id, int action)
        {
            switch (action)
            {
                case (int)DAL.Constants.Quotations.ADD:
                    {
                        return null;
                    }
                case (int)DAL.Constants.Quotations.UPDATE:
                    {
                        return DAL.Quotations.getQuotationsDataEdit(id);
                    }
                default: throw new Exception();
            }

        }

        public static int AddUpdateQuotations(DAL.DTO.Quote values)
        {
            if (values.Id > 0)
            {
                int result = DAL.Quotations.editQuotations(values.Id, values);
                DAL.Quotations.validateQuotation(values.Id);
                Thread validate = new(delegate ()
                {
                    BLL.Emailing.QuotationEmailing(values.Id);
                });
                validate.IsBackground = true;
                validate.Start();

                return result;
            }
            else
            {
                int key = DAL.Quotations.addQuotations(values);
                DAL.Quotations.validateQuotation(key);
                Thread validate = new(delegate ()
                {
                    BLL.Emailing.QuotationEmailing(key);
                });
                validate.IsBackground = true;
                validate.Start();
                return key;
            }
        }

        public static int AddUpdateDraftQuotations(DAL.DTO.Quote values)
        {
            if (values.QuoteStatusId == (int)DAL.Constants.QuotationsStatus.DRAFT)
            {
                int result = DAL.Quotations.editQuotationsDraft(values.Id, values);
                return result;
            }
            else
            {
                int key = DAL.Quotations.addQuotationsDraft(values);
                return key;
            }
        }
    }
}
