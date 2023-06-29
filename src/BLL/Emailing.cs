using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BLL
{
    public static class Emailing
    {
        private static Uri siteUrl = new Uri(DAL.DB.HostPath + "dashboard");
        private static Uri siteUrlHome = new Uri(DAL.DB.HostPath);

        public static void AddInternalOrderEmailing(int id)
        {
            var recipient = DAL.InternalOrder.GetApprovedRecipient(id);
            var recipientRequester = DAL.InternalOrder.GetRecipient(id);

            var title = "Order Waiting Approval - ACS - #" + id;
            var subtitle = "The following order requires approval:";
            var body = getEmailBody(id, subtitle);

            DAL.Emailing.sendEmailHtml(recipient, new System.Collections.Generic.List<string>(), title, body);

            if (recipientRequester != recipient)
            {
                DAL.Emailing.sendEmailHtmlAttachment(recipientRequester, new System.Collections.Generic.List<string>(), title, body);
            }
        }        

        public static void EditInternalOrderEmailing(int id)
        {
            var recipient = DAL.InternalOrder.GetApprovedRecipient(id);
            var title = "Updated Order Waiting Approval - ACS - #" + id;
            var subtitle = "The following order requires approval:";
            var body = getEmailBody(id, subtitle);

            DAL.Emailing.sendEmailHtml(recipient, new System.Collections.Generic.List<string>(), title, body);
        }

        public static void DeniedInternalOrderEmailing(int id)
        {
            var recipient = DAL.InternalOrder.GetRecipient(id);
            var title = "Order Denied - ACS - #" + id;
            var subtitle = "The following order has been denied:";
            var body = getEmailBody(id, subtitle);
            DAL.Emailing.sendEmailHtml(recipient, new System.Collections.Generic.List<string>(), title, body);
        }

        public static void ReviewInternalOrderEmailing(int id)
        {
            var recipient = DAL.InternalOrder.GetPlacedByRecipient(id);
            var title = "Order waiting Review - ACS - #" + id;
            var subtitle = "The following order requires review:";
            var body = getEmailBody(id, subtitle);

            DAL.Emailing.sendEmailHtml(recipient, new System.Collections.Generic.List<string>(), title, body);
        }

        public static void ApproveInternalOrderEmailing(int id)
        {
            var data = DAL.PurchaseOrder.getPurchaseOrder(id);
            var name = id + ".PDF";

            PDF.PurchaseOrder.PurchaseOrder PDF = new PDF.PurchaseOrder.PurchaseOrder(data);
            System.IO.Stream stream = new System.IO.MemoryStream();

            PDF.ExportToPdf(stream);

            stream.Position = 0;

            var recipientRequester = DAL.InternalOrder.GetRecipient(id);
            var recipient = DAL.InternalOrder.GetPlacedByRecipient(id);

            var title = "Order Approved - ACS - #" + id;
            var subtitle = "The following order has been approved:";
            var body = getEmailBody(id, subtitle);

            DAL.Emailing.sendEmailHtmlAttachment(recipient, new System.Collections.Generic.List<string>(), title, body, stream, name);

            if (recipientRequester != recipient)
            {
                DAL.Emailing.sendEmailHtmlAttachment(recipientRequester, new System.Collections.Generic.List<string>(), title, body);
            }
        }

      /*  public static void EditQuotationEmailing(int id)
        {
            var recipient = DAL.Quotations.GetApprovedRecipient(id);
            var title = "New Quotation - ACS - #" + id;
            var subtitle = "The following quotation was created:";
            var body = getEmailBody(id, subtitle);

            DAL.Emailing.sendEmailHtml(recipient, new System.Collections.Generic.List<string>(), title, body);
        }*/

        public static string getEmailBody(int id, string subtitle)
        {
            var data = DAL.InternalOrder.GetEmailData(id);
            var stock = "";
            var stockOnceOff = "";
            var stockServices = "";
            int currentStatus = DAL.InternalOrder.GetOrderStatus(id); 

            var buttonText = currentStatus.Equals((int)DAL.Constants.InternalOrderStatus.REVIEW) ? "Click To Review" : 
                currentStatus.Equals((int)DAL.Constants.InternalOrderStatus.APPROVED) || currentStatus.Equals((int)DAL.Constants.InternalOrderStatus.DENIED) ? null: "Click To Approve";
            var showApproveButton = buttonText != null ?
                    "<tr>"+                        
                  "<td align='center'>" +
                 "<a href='" + siteUrl + "?orderId=" + data.Id +"'>" +
                "<button style='background-color:#337ab7;color:white;text-align:center;width:50%;height:50%;border: 1px solid #337ab7;font-size:large;border-radius:4px'>" +
                "<strong>" + buttonText + "</strong>" +
                "</button>"
                 + "</a>" +
                 "</td>" + "</tr>" : "";

            if(currentStatus.Equals((int)DAL.Constants.InternalOrderStatus.DENIED))
            {
                DAL.InternalOrder.removeDenyOrder(id);
            }

            var totalInclVat = data.Vat + data.Total;
            var additionalComment = "<br/><p><strong>Additional Comment: </strong>" + data.InternalComment + "</p>";
            if (data.InternalComment != null)
            {
                subtitle += additionalComment;
            }
            if(data.internalOrderItems.Count > 0)
            {
                stock += "<tr style = 'border: 1px solid #2a3042;padding: 4px;border-radius: 4px;text-align: center;background-color: #2a304226;color: #2a3042;'><td> Listed Items</td></tr><tr><td style ='padding:0;'><table id='items' role = 'presentation' style = 'width:100%;border-collapse:collapse;border-spacing:0;'><tr><th>Stock</th><th>GL Code</th><th>Department</th><th>UoM</th><th>Quantity</th><th>Pack Size</th><th>UoM Price</th><th>Total(UoM)</th><th>List Price</th><th>New Price</th><th>VAT Appl.</th><th>GRN Appl.</th><th>Total(VAT Excl.)</th></tr>";
                for (var item = 0; item < data.internalOrderItems.Count; item++)
                {
                    var vat = "No";
                    var grn = "No";
                    if ((bool)data.internalOrderItems[item].VatAppl)
                    {
                        vat = "Yes";
                    }
                    if ((bool)data.internalOrderItems[item].GrnAppl)
                    {
                        grn = "Yes";
                    }

                    if (data.internalOrderItems[item].Value > data.internalOrderItems[item].OriginalValue)
                    {
                        //green
                        stock += "<tr><td>" + data.internalOrderItems[item].StockName + "</td>" + "<td>" + data.internalOrderItems[item].GlFullName + "</td>" + "<td>" + data.internalOrderItems[item].DepartmentName + "</td>" + "<td>" + data.internalOrderItems[item].UomName + "</td>" + "<td>" + data.internalOrderItems[item].Quantity + "</td>" + "<td>" + data.internalOrderItems[item].PackSize + "</td>" + "<td>" + data.internalOrderItems[item].UomPrice + "</td>" + "<td>" + data.internalOrderItems[item].TotalUnits + "</td>" + "<td>" + data.internalOrderItems[item].OriginalValue + "</td>" + "<td style='padding-bottom: 5px;background-color:#aded92;border-radius:4px;'>" + data.internalOrderItems[item].Value + "</td>" + "<td>" + vat + "</td>" + "<td>" + grn + "</td>" + "<td>" + data.internalOrderItems[item].Total + "</td></tr>";
                    }
                    else
                    {
                        stock += "<tr><td>" + data.internalOrderItems[item].StockName + "</td>" + "<td>" + data.internalOrderItems[item].GlFullName + "</td>" + "<td>" + data.internalOrderItems[item].DepartmentName + "</td>" + "<td>" + data.internalOrderItems[item].UomName + "</td>" + "<td>" + data.internalOrderItems[item].Quantity + "</td>" + "<td>" + data.internalOrderItems[item].PackSize + "</td>" + "<td>" + data.internalOrderItems[item].UomPrice + "</td>" + "<td>" + data.internalOrderItems[item].TotalUnits + "</td>" + "<td>" + data.internalOrderItems[item].OriginalValue + "</td>" + "<td style='padding-bottom: 5px;background-color:#ffff0091;border-radius:4px;'>" + data.internalOrderItems[item].Value + "</td>" + "<td>" + vat + "</td>" + "<td>" + grn + "</td>" + "<td>" + data.internalOrderItems[item].Total + "</td></tr>";
                    }                    
                }
                stock += "</table></td></tr>";
            }
          
            if(data.onceOffItems.Count > 0)
            {
                stockOnceOff += "<tr style='border: 1px solid #2a3042;padding: 4px;border-radius: 4px;text-align: center;background-color: #2a304226;color: #2a3042;'><td>Once-Off Items</td></tr><tr><td style='padding:0;'><table id='items' role='presentation' style='width:100%;border-collapse:collapse;border-spacing:0;'><tr><th>Item Code</th><th>Item Desc</th><th>GL Code</th><th>Department</th><th>Price</th><th>UoM</th><th>Qty</th><th>Pack Size</th><th>Total (UoM)</th><th>VAT Appl.</th><th>GRN Appl.</th><th>Total (VAT Excl.)</th></tr>";
                for (var item = 0; item < data.onceOffItems.Count; item++)
                {
                    var vat = "No";
                    var grn = "No";
                    var totalUom = data.onceOffItems[item].Quantity * data.onceOffItems[item].PackSize;
                    if ((bool)data.onceOffItems[item].VatAppl)
                    {
                        vat = "Yes";
                    }
                    if ((bool)data.onceOffItems[item].GrnAppl)
                    {
                        grn = "Yes";
                    }
                    
                    stockOnceOff += "<tr><td>" + data.onceOffItems[item].Code + "</td>" + "<td>" + data.onceOffItems[item].Description + "</td>" + "<td>" + data.onceOffItems[item].GlFullName + "</td>" + "<td>" + data.onceOffItems[item].DepartmentName + "</td>" + "<td style='padding-bottom: 5px;background-color:#ffff0091;border-radius:4px;padding: 4px;'>" + data.onceOffItems[item].Value + "</td>" + "<td>" + data.onceOffItems[item].UomName + "</td>" + "<td>" + data.onceOffItems[item].Quantity + "</td>" + "<td>" + data.onceOffItems[item].PackSize + "</td>" + "<td>" + totalUom + "</td>" + "<td>" + vat + "</td>" + "<td>" + grn + "</td>" + "<td>" + data.onceOffItems[item].Total + "</td></tr>";
                }
                stockOnceOff += "</table></td></tr>";
            }
            
            if(data.services.Count > 0)
            {
                stockServices += "<tr style='border: 1px solid #2a3042;padding: 4px;border-radius: 4px;text-align: center;background-color: #2a304226;color: #2a3042;'><td>Services</td></tr><tr><td style='padding:0;'><table id='items' role='presentation' style='width:100%;border-collapse:collapse;border-spacing:0;'><tr><th>Code</th><th>Service Desc</th><th>GL Code</th><th>Department</th><th>Qty</th><th>Price</th><th>VAT Appl.</th><th>GRN Appl.</th><th>Total (VAT Excl.)</th></tr>";
                for (var item = 0; item < data.services.Count; item++)
                {
                    var vat = "No";
                    var grn = "No";
                    if ((bool)data.services[item].VatAppl)
                    {
                        vat = "Yes";
                    }
                    if ((bool)data.services[item].GrnAppl)
                    {
                        grn = "Yes";
                    }
                    
                    stockServices += "<tr><td>" + data.services[item].Code + "</td>" + "<td>" + data.services[item].Description + "</td>" + "<td>" + data.services[item].GlFullName + "</td>" + "<td>" + data.services[item].DepartmentName + "</td>" + "<td>" + data.services[item].Quantity + "</td>" + "<td style='padding-bottom: 5px;background-color:#ffff0091;border-radius:4px;'>" + data.services[item].Value + "</td>" + "<td>" + vat + "</td>" + "<td>" + grn + "</td>" + "<td>" + data.services[item].Value + "</td></tr>";
                }
                stockServices += "</table></td></tr>";
            }
            return @"<!DOCTYPE html><html lang='en' xmlns:o='urn:schemas-microsoft-com:office:office'>
                  <head><meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><meta name='x-apple-disable-message-reformatting'><title></title>
                  <style>table, td, div, h1, p {font-family: Arial, sans-serif;}#items tr th, #items tr td{border:1px solid #cccccc;padding: 10px;}#items{margin-top: 10px;}
                  </style>
          
                  </head>
                 <body style='margin:0;padding:0;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;margin:5px'><tr><td>" + subtitle +
                 "</td></tr><tr><td><table role='presentation' style='border-collapse:collapse;border:0;border-spacing:0;text-align:left;'><tr><td style='padding:0px 30px 20px 30px;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding-bottom: 5px;'><strong>Order No:</strong> #"
                 + data.Id + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Date:</strong> "
                 + ((DateTime)data.DateCreated).ToString("dd MMM yyyy") + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Approved By:</strong> "
                 + data.ApproveByFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Request By:</strong> "
                 + data.RequestByFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Placed By:</strong> "
                 + data.PlacedByFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Supplier:</strong> "
                 + data.SupplierFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Quotation Number:</strong> "
                 + data.QuotationNumber + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Plant Location:</strong> "
                 + data.PlantLocationName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Project:</strong> "
                 + data.ProjectName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Company:</strong> "
                 + data.CompanyName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Follow Up Date:</strong> "
                 + ((DateTime)data.FollowUpDate).ToString("dd MMM yyyy") + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Delivery Date:</strong> "
                 + ((DateTime)data.DeliveryDate).ToString("dd MMM yyyy") + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Currency:</strong> "
                 + data.SupplierCurrency + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>VAT:</strong> "
                 + data.Vat + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Total (Excl. VAT):</strong> "
                 + data.Total + "</td></tr><tr><td style='padding-bottom: 5px;background-color:#ffff0091;border-radius:4px;'><strong>Total (Incl. VAT):</strong>"
                 + totalInclVat + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Internal Comment:</strong> "
                 + data.Comment + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Supplier Comment:</strong> "
                 + data.SupplierComment + "</td></tr>" + stock + stockOnceOff + stockServices + "</table></td></tr>"

                 +
                  "<tr><td>" +
                  "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:12px;font-family:Arial,sans-serif;'>" +
                  "<br>" + showApproveButton +
                  "<tr><td align='left'><br><br>This email is automatically generated by the ACS System, please do not reply. Instead, direct any queries to candice@amficomposites.co.za" +
                  "</td></tr>" +
                  "</table>" +
                "</td></tr></table>" +
                "</td></tr></table>" +
                "</body>" +
                "</html>";
        }

        public static void FollowUpEmailing(int id)
        {
            var recipient = DAL.InternalOrder.GetPlacedByRecipient(id);
            var ReqRecipient = DAL.InternalOrder.GetRecipient(id);
            List<string> cc = new List<string>();

            if (recipient != ReqRecipient)
            {
                cc.Add(ReqRecipient);
            }
          
            var title = "Follow Up Reminder - Follow Up - ACS - #" + id;
            var subtitle = "The following order is scheduled to be followed up today:";
            var body = getDeliveryReminderBody(id, subtitle);

            DAL.Emailing.sendEmailHtml(recipient, cc, title, body);

        }
        public static void DeliveryReminderEmailing(int id, string type)
        {
            var recipient = DAL.InternalOrder.GetPlacedByRecipient(id);
            var ReqRecipient = DAL.InternalOrder.GetRecipient(id);
            List<string> cc = new List<string>();

            if (recipient != ReqRecipient)
            {
                cc.Add(ReqRecipient);
            }
                        
            var title = "";
            var subtitle = "";
            switch (type)
            {
                case "today":
                {
                    title = "Delivery Reminder - Scheduled Delivery - ACS - #" + id;
                    subtitle = "The following order is scheduled to be delivered today:";
                    break;
                }
                case "upcomming":
                {
                    title = "Delivery Reminder - Upcomming Delivery - ACS - #" + id;
                    subtitle = "The following order is scheduled to be delivered:";
                    break;
                }
                case "late":
                {
                    title = "Delivery Reminder - Late Delivery - ACS - #" + id;
                    subtitle = "The following order is delayed in delivery:";
                    break;
                }
            }
            
            
            var body = getDeliveryReminderBody(id, subtitle);

            DAL.Emailing.sendEmailHtml(recipient, cc, title, body);
        }
        
        public static string getDeliveryReminderBody(int id, string subtitle)
        {
            var data = DAL.InternalOrder.GetEmailData(id);
            var stock = "";
            var stockOnceOff = "";
            var stockServices = "";

            var totalInclVat = data.Vat + data.Total;
            var additionalComment = "<br/><p><strong>Additional Comment: </strong>" + data.InternalComment + "</p>";
            if (data.InternalComment != null)
            {
                subtitle += additionalComment;
            }
            if (data.internalOrderItems.Count > 0)
            {
                stock += "<tr style = 'border: 1px solid #2a3042;padding: 4px;border-radius: 4px;text-align: center;background-color: #2a304226;color: #2a3042;'><td> Listed Items</td></tr><tr><td style ='padding:0;'><table id='items' role = 'presentation' style = 'width:100%;border-collapse:collapse;border-spacing:0;'><tr><th>Stock</th><th>GL Code</th><th>Department</th><th>UoM</th><th>Quantity</th><th>Pack Size</th><th>UoM Price</th><th>Total(UoM)</th><th>List Price</th><th>New Price</th><th>VAT Appl.</th><th>GRN Appl.</th><th>Total(VAT Excl.)</th></tr>";
                for (var item = 0; item < data.internalOrderItems.Count; item++)
                {
                    var vat = "No";
                    var grn = "No";
                    if ((bool)data.internalOrderItems[item].VatAppl)
                    {
                        vat = "Yes";
                    }
                    if ((bool)data.internalOrderItems[item].GrnAppl)
                    {
                        grn = "Yes";
                    }

                    if (data.internalOrderItems[item].Value > data.internalOrderItems[item].OriginalValue)
                    {
                        //green
                        stock += "<tr><td>" + data.internalOrderItems[item].StockName + "</td>" + "<td>" + data.internalOrderItems[item].GlFullName + "</td>" + "<td>" + data.internalOrderItems[item].DepartmentName + "</td>" + "<td>" + data.internalOrderItems[item].UomName + "</td>" + "<td>" + data.internalOrderItems[item].Quantity + "</td>" + "<td>" + data.internalOrderItems[item].PackSize + "</td>" + "<td>" + data.internalOrderItems[item].UomPrice + "</td>" + "<td>" + data.internalOrderItems[item].TotalUnits + "</td>" + "<td>" + data.internalOrderItems[item].OriginalValue + "</td>" + "<td style='padding-bottom: 5px;background-color:#aded92;border-radius:4px;'>" + data.internalOrderItems[item].Value + "</td>" + "<td>" + vat + "</td>" + "<td>" + grn + "</td>" + "<td>" + data.internalOrderItems[item].Total + "</td></tr>";
                    }
                    else
                    {
                        stock += "<tr><td>" + data.internalOrderItems[item].StockName + "</td>" + "<td>" + data.internalOrderItems[item].GlFullName + "</td>" + "<td>" + data.internalOrderItems[item].DepartmentName + "</td>" + "<td>" + data.internalOrderItems[item].UomName + "</td>" + "<td>" + data.internalOrderItems[item].Quantity + "</td>" + "<td>" + data.internalOrderItems[item].PackSize + "</td>" + "<td>" + data.internalOrderItems[item].UomPrice + "</td>" + "<td>" + data.internalOrderItems[item].TotalUnits + "</td>" + "<td>" + data.internalOrderItems[item].OriginalValue + "</td>" + "<td style='padding-bottom: 5px;background-color:#ffff0091;border-radius:4px;'>" + data.internalOrderItems[item].Value + "</td>" + "<td>" + vat + "</td>" + "<td>" + grn + "</td>" + "<td>" + data.internalOrderItems[item].Total + "</td></tr>";
                    }
                }
                stock += "</table></td></tr>";
            }

            if (data.onceOffItems.Count > 0)
            {
                stockOnceOff += "<tr style='border: 1px solid #2a3042;padding: 4px;border-radius: 4px;text-align: center;background-color: #2a304226;color: #2a3042;'><td>Once-Off Items</td></tr><tr><td style='padding:0;'><table id='items' role='presentation' style='width:100%;border-collapse:collapse;border-spacing:0;'><tr><th>Item Code</th><th>Item Desc</th><th>GL Code</th><th>Department</th><th>Price</th><th>UoM</th><th>Qty</th><th>Pack Size</th><th>Total (UoM)</th><th>VAT Appl.</th><th>GRN Appl.</th><th>Total (VAT Excl.)</th></tr>";
                for (var item = 0; item < data.onceOffItems.Count; item++)
                {
                    var vat = "No";
                    var grn = "No";
                    var totalUom = data.onceOffItems[item].Quantity * data.onceOffItems[item].PackSize;
                    if ((bool)data.onceOffItems[item].VatAppl)
                    {
                        vat = "Yes";
                    }
                    if ((bool)data.onceOffItems[item].GrnAppl)
                    {
                        grn = "Yes";
                    }

                    stockOnceOff += "<tr><td>" + data.onceOffItems[item].Code + "</td>" + "<td>" + data.onceOffItems[item].Description + "</td>" + "<td>" + data.onceOffItems[item].GlFullName + "</td>" + "<td>" + data.onceOffItems[item].DepartmentName + "</td>" + "<td style='padding-bottom: 5px;background-color:#ffff0091;border-radius:4px;padding: 4px;'>" + data.onceOffItems[item].Value + "</td>" + "<td>" + data.onceOffItems[item].UomName + "</td>" + "<td>" + data.onceOffItems[item].Quantity + "</td>" + "<td>" + data.onceOffItems[item].PackSize + "</td>" + "<td>" + totalUom + "</td>" + "<td>" + vat + "</td>" + "<td>" + grn + "</td>" + "<td>" + data.onceOffItems[item].Total + "</td></tr>";
                }
                stockOnceOff += "</table></td></tr>";
            }

            if (data.services.Count > 0)
            {
                stockServices += "<tr style='border: 1px solid #2a3042;padding: 4px;border-radius: 4px;text-align: center;background-color: #2a304226;color: #2a3042;'><td>Services</td></tr><tr><td style='padding:0;'><table id='items' role='presentation' style='width:100%;border-collapse:collapse;border-spacing:0;'><tr><th>Code</th><th>Service Desc</th><th>GL Code</th><th>Department</th><th>Qty</th><th>Price</th><th>VAT Appl.</th><th>GRN Appl.</th><th>Total (VAT Excl.)</th></tr>";
                for (var item = 0; item < data.services.Count; item++)
                {
                    var vat = "No";
                    var grn = "No";
                    if ((bool)data.services[item].VatAppl)
                    {
                        vat = "Yes";
                    }
                    if ((bool)data.services[item].GrnAppl)
                    {
                        grn = "Yes";
                    }

                    stockServices += "<tr><td>" + data.services[item].Code + "</td>" + "<td>" + data.services[item].Description + "</td>" + "<td>" + data.services[item].GlFullName + "</td>" + "<td>" + data.services[item].DepartmentName + "</td>" + "<td>" + data.services[item].Quantity + "</td>" + "<td style='padding-bottom: 5px;background-color:#ffff0091;border-radius:4px;'>" + data.services[item].Value + "</td>" + "<td>" + vat + "</td>" + "<td>" + grn + "</td>" + "<td>" + data.services[item].Value + "</td></tr>";
                }
                stockServices += "</table></td></tr>";
            }

            return @"<!DOCTYPE html><html lang='en' xmlns:o='urn:schemas-microsoft-com:office:office'>
                  <head><meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><meta name='x-apple-disable-message-reformatting'><title></title>
                  <style>table, td, div, h1, p {font-family: Arial, sans-serif;}#items tr th, #items tr td{border:1px solid #cccccc;padding: 10px;}#items{margin-top: 10px;}
                  </style>
          
                  </head>
                 <body style='margin:0;padding:0;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;margin:5px'><tr><td>" + subtitle +
                 "</td></tr><tr><td><table role='presentation' style='border-collapse:collapse;border:0;border-spacing:0;text-align:left;'><tr><td style='padding:0px 30px 20px 30px;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding-bottom: 5px;'><strong>Order No:</strong> #"
                 + data.Id + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Date:</strong> "
                 + ((DateTime)data.DateCreated).ToString("dd MMM yyyy") + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Approved By:</strong> "
                 + data.ApproveByFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Request By:</strong> "
                 + data.RequestByFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Placed By:</strong> "
                 + data.PlacedByFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Supplier:</strong> "
                 + data.SupplierFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Quotation Number:</strong> "
                 + data.QuotationNumber + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Plant Location:</strong> "
                 + data.PlantLocationName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Follow Up Date:</strong> "
                 + ((DateTime)data.FollowUpDate).ToString("dd MMM yyyy") + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Delivery Date:</strong> "
                 + ((DateTime)data.DeliveryDate).ToString("dd MMM yyyy") + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Currency:</strong> "
                 + data.SupplierCurrency + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>VAT:</strong> "
                 + data.Vat + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Total (Excl. VAT):</strong> "
                 + data.Total + "</td></tr><tr><td style='padding-bottom: 5px;background-color:#ffff0091;border-radius:4px;'><strong>Total (Incl. VAT):</strong>"
                 + totalInclVat + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Internal Comment:</strong> "
                 + data.Comment + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Supplier Comment:</strong> "
                 + data.SupplierComment + "</td></tr>" + stock + stockOnceOff + stockServices + "</table></td></tr>"

                 +
                  "<tr><td>" +
                  "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:12px;font-family:Arial,sans-serif;'>" +
                  "<tr><td align='left'><br><br>This email is automatically generated by the ACS System, please do not reply. Instead, direct any queries to candice@amficomposites.co.za" +
                  "</td></tr>" +
                  "</table>" +
                "</td></tr></table>" +
                "</td></tr></table>" +
                "</body>" +
                "</html>";
            //return @"<!DOCTYPE html><html lang='en' xmlns:o='urn:schemas-microsoft-com:office:office'><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><meta name='x-apple-disable-message-reformatting'><title></title><style>table, td, div, h1, p {font-family: Arial, sans-serif;}#items tr th, #items tr td{border:1px solid #cccccc;padding: 10px;}#items{margin-top: 10px;}</style></head><body style='margin:0;padding:0;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;margin:5px'><tr><td>" + subtitle + "</td></tr><tr><td><table role='presentation' style='border-collapse:collapse;border:0;border-spacing:0;text-align:left;'><tr><td style='padding:0px 30px 20px 30px;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding-bottom: 5px;'><strong>Order No:</strong> #" + data.Id + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Date:</strong> " + ((DateTime)data.DateCreated).ToString("dd MMM yyyy") + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Approved By:</strong> " + data.ApproveByFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Request By:</strong> " + data.RequestByFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Placed By:</strong> " + data.PlacedByFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Supplier:</strong> " + data.SupplierFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Quotation Number:</strong> " + data.QuotationNumber + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Plant Location:</strong> " + data.PlantLocationName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Follow Up Date:</strong> " + ((DateTime)data.FollowUpDate).ToString("dd MMM yyyy") + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Delivery Date:</strong> " + ((DateTime)data.DeliveryDate).ToString("dd MMM yyyy") + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Internal Comment:</strong> " + data.Comment + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Supplier Comment:</strong> " + data.SupplierComment + "</td></tr></table></td></tr><tr><td align='left'><br><br><br>This email is automatically generated by the ACS System, please do not reply. Instead, direct any queries to candice@amficomposites.co.za</td></tr></table></td></tr></table></td></tr></table></body></html>";
        }

        public static void MonitoredPriceApprovalEmail(List<DAL.DTO.InternalOrderItem> increasedItems)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var recipients = DAL.InternalOrder.GetPriceIncreaseReminder();
            
            var stock = "";

            if (increasedItems.Count > 0)
            {
                stock += "<tr style = 'border: 1px solid #2a3042;padding: 4px;border-radius: 4px;text-align: center;background-color: #2a304226;color: #2a3042;'><td> Listed Items</td></tr><tr><td style ='padding:0;'><table id='items' role = 'presentation' style = 'width:100%;border-collapse:collapse;border-spacing:0;'><tr><th>Stock</th><th>List Price</th><th>New Price</th></tr>";
                for (var item = 0; item < increasedItems.Count; item++)
                {                    
                    if (increasedItems[item].Value > increasedItems[item].OriginalValue)
                    {                                 
                        stock += "<tr><td>" + increasedItems[item].InternalProductName + "</td>" + "<td>" + increasedItems[item].OriginalValue + "</td>" + "<td style='padding-bottom: 5px;background-color:#aded92;border-radius:4px;'>" + increasedItems[item].Value + "</td></tr>";
                    }
                }
                stock += "</table></td></tr>";
            }

            if (recipients.Count > 1)
            {
                List<string> cc = new List<string>();
                cc = recipients;
                cc.Remove(recipients[0]);

                var title = "Pending Price Increase Reminder - ACS";
                var subtitle = "Pending price increases waiting approval, click the button to view pending items.";
                var page = "/price-lookup";
                var body = @"<!DOCTYPE html><html lang='en' xmlns:o='urn:schemas-microsoft-com:office:office'><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><meta name='x-apple-disable-message-reformatting'><title></title><style>table, td, div, h1, p {font-family: Arial, sans-serif;}#items tr th, #items tr td{border:1px solid #cccccc;padding: 10px;}#items{margin-top: 10px;}</style></head><body style='margin:0;padding:0;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;margin:5px'><tr><td>" + subtitle + "</td></tr><tr><td></td></tr><tr><td><br><br><br>" + stock + "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:12px;font-family:Arial,sans-serif;'><br><tr><td align='center'><a href='" + siteUrlHome + page + "'><button style='background-color:#337ab7;color:white;text-align:center;width:30%;height:30%;border: 1px solid #337ab7;font-size:large;border-radius:4px'><strong>Click To Approve</strong></button></a></td></tr></table></td></tr></table></body></html>";

                DAL.Emailing.sendEmailHtml(recipients[0], cc, title, body);
            }
            else
            {
                if(recipients.Count == 1)
                {
                    List<string> rec = new List<string>();
                    rec = recipients;

                    var title = "Pending Price Increase Reminder - ACS";
                    var subtitle = "Pending price increases waiting approval, click the button to view pending items.";
                    var page = "/price-lookup";
                    var body = @"<!DOCTYPE html><html lang='en' xmlns:o='urn:schemas-microsoft-com:office:office'><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><meta name='x-apple-disable-message-reformatting'><title></title><style>table, td, div, h1, p {font-family: Arial, sans-serif;}#items tr th, #items tr td{border:1px solid #cccccc;padding: 10px;}#items{margin-top: 10px;}</style></head><body style='margin:0;padding:0;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;margin:5px'><tr><td>" + subtitle + "</td></tr><tr><td></td></tr><tr><td><br><br><br><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:12px;font-family:Arial,sans-serif;'><br><tr><td align='center'><a href='" + siteUrlHome + page + "'><button style='background-color:#337ab7;color:white;text-align:center;width:30%;height:30%;border: 1px solid #337ab7;font-size:large;border-radius:4px'><strong>Click To Approve</strong></button></a></td></tr></table></td></tr></table></body></html>";

                    DAL.Emailing.sendEmailHtml(rec[0], new System.Collections.Generic.List<string>(), title, body);
                }
            }
            
        }

        public static void InvoiceNotification(string InvoiceNumber)
        {
            var recipients = DAL.InternalOrder.GetInvoiceIncreaseReminder();

            var title = "Invoice Total - ACS - #" + InvoiceNumber;
            var subtitle = "The following invoice total is higher than the ordered total:";
            var body = "The following invoice total is higher than the ordered total: <br/><p><strong>See Invoice: </strong>" + InvoiceNumber + "</p>";

            if (recipients.Count > 1)
            {
                List<string> cc = new List<string>();
                cc = recipients;
                cc.Remove(recipients[0]);

                DAL.Emailing.sendEmailHtml(recipients[0], cc, title, body);                
            }
            else
            {
                if (recipients.Count == 1)
                {
                    List<string> rec = new List<string>();
                    rec = recipients;

                    DAL.Emailing.sendEmailHtml(rec[0], new System.Collections.Generic.List<string>(), title, body);                    
                }
            }
  
        }

        public static void AddQuotationEmailing(int id)
        {
            var recipient = DAL.Quotations.GetPlacedByRecipient(id);
            var recipientRequester = DAL.Quotations.GetRecipient(id);

            var title = "Quotation - #" + id;
            var subtitle = " ";
            var body = getQuotationEmailBody(id, subtitle);

            DAL.Emailing.sendEmailHtml(recipient, new System.Collections.Generic.List<string>(), title, body);

            if (recipientRequester != recipient)
            {
                DAL.Emailing.sendEmailHtmlAttachment(recipientRequester, new System.Collections.Generic.List<string>(), title, body);
            }
        }

        public static void EditQuotationEmailing(int id)
        {
            var recipient = DAL.Quotations.GetPlacedByRecipient(id);
            var title = "Quotation- #" + id;
            var subtitle = " ";
            var body = getQuotationEmailBody(id, subtitle);

            DAL.Emailing.sendEmailHtml(recipient, new System.Collections.Generic.List<string>(), title, body);
        }

        public static string getQuotationEmailBody(int id, string subtitle)
        {
            var data = DAL.QuotationPDF.getQuotation(id);

            var product = "";
            var transport = "";

            //var totalInclVat = data.Vat + data.Total;


            if (data.QuoteItems.Count > 0)
            {
                product += "<tr style = 'border: 1px solid #2a3042;padding: 4px;border-radius: 4px;text-align: center;background-color: #2a304226;color: #2a3042;'><td> Products</td></tr><tr><td style ='padding:0;'><table id='items' role = 'presentation' style = 'width:100%;border-collapse:collapse;border-spacing:0;'><tr><th>Product Description</th><th>Width</th><th>Length</th><th>m2/Coil</th><th>Price/m2</th><th>Coil Price</th><th>Qty</th><th>Total</th></tr>";
                for (var item = 0; item < data.QuoteItems.Count; item++)
                {
                    
                        product += "<tr><td>" + data.QuoteItems[item].ProductName + "</td>" + "<td>" + data.QuoteItems[item].Width + "</td>" + "<td>" + data.QuoteItems[item].Length + "</td>" + "<td>" + data.QuoteItems[item].m2Coil + "<td>" + data.QuoteItems[item].priceCoil.ToString("c2") + "</td>" + "</td>" + "<td>" + data.QuoteItems[item].FormattedAmount + "</td>" + "<td>" + data.QuoteItems[item].Quantity + "</td>" + "<td>" + data.QuoteItems[item].Total.ToString("c2") + "</td>" + "</tr>";
                  
                }
                product += "</table></td></tr>";
            }

            if (data.QuoteTransports.Count > 0)
            {
                transport += "<tr style='border: 1px solid #2a3042;padding: 4px;border-radius: 4px;text-align: center;background-color: #2a304226;color: #2a3042;'><td>Transport</td></tr><tr><td style='padding:0;'><table id='items' role='presentation' style='width:100%;border-collapse:collapse;border-spacing:0;'><tr><th>Transport</th><th>Unit Price</th><th>Qty</th><th>Total</th></tr>";
                for (var item = 0; item < data.QuoteTransports.Count; item++)
                {
                    transport += "<tr><td>" + data.QuoteTransports[item].Description + "</td>" + "<td>" + data.QuoteTransports[item].FormattedPrice + "</td>" + "<td>" + data.QuoteTransports[item].Quantity + "<td>" + data.QuoteTransports[item].Total.ToString("c2") + "</td></tr>";
                }
                transport += "</table></td></tr>";
            }

            return @"<!DOCTYPE html><html lang='en' xmlns:o='urn:schemas-microsoft-com:office:office'><head><meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><meta name='x-apple-disable-message-reformatting'><title></title><style>table, td, div, h1, p {font-family: Arial, sans-serif;}#items tr th, #items tr td{border:1px solid #cccccc;padding: 10px;}#items{margin-top: 10px;}</style></head><body style='margin:0;padding:0;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;margin:5px'><tr><td>" + subtitle + "</td></tr><tr><td><table role='presentation' style='border-collapse:collapse;border:0;border-spacing:0;text-align:left;'><tr><td style='padding:0px 30px 20px 30px;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding-bottom: 5px;'><strong>Quotation No:</strong> #" + data.Id + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Date:</strong> " + ((DateTime)data.DateCreated).ToString("dd MMM yyyy") + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Request By:</strong> " + data.RequestByFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Placed By:</strong> " + data.PlacedByFullName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Customer:</strong> " + data.CustomerName + "</td></tr><tr><td style='padding-bottom: 5px;'><strong>Total (Excl.VAT):</strong> " + data.Total.ToString("c2") + "</td></tr> " + "<tr><td style = 'padding-bottom: 5px;' ><strong> VAT:</strong> " + data.VAT.ToString("c2")+ "</td></tr> "+ "<tr><td style='padding-bottom: 5px;'><strong>Total (Incl.VAT):</strong> " + data.TotalAndVAT.ToString("c2") + "</td></tr> "  + product + transport + " </table></td></tr><tr><td><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:12px;font-family:Arial,sans-serif;'><br><tr><td align='center'><a href='" + siteUrl + "?orderId=" + data.Id + "'></a></td></tr><tr><td align='left'><br><br>This email is automatically generated by the ACS System, please do not reply. Instead, direct any queries to candice@amficomposites.co.za</td></tr></table></td></tr></table></td></tr></table></body></html>";
        }
        public static void QuotationEmailing(int id)
        {
            var data = DAL.QuotationPDF.getQuotation(id);
            var name = "Quotation " + id + ".pdf";

            PDF.Quotation1.Quotation PDF = new PDF.Quotation1.Quotation(data);
            System.IO.Stream stream = new System.IO.MemoryStream();

            PDF.ExportToPdf(stream);

            stream.Position = 0;

            var recipientRequester = DAL.Quotations.GetRecipient(id);
            var recipient = DAL.Quotations.GetPlacedByRecipient(id);

            var title = "Quotation- #" + id;
            var subtitle = " ";
            var body = getQuotationEmailBody(id, subtitle);

            DAL.Emailing.sendEmailHtmlAttachment(recipient, new System.Collections.Generic.List<string>(), title, body, stream, name);

            if (recipientRequester != recipient)
            {
                DAL.Emailing.sendEmailHtmlAttachment(recipientRequester, new System.Collections.Generic.List<string>(), title, body);
            }
        }
    }
}
