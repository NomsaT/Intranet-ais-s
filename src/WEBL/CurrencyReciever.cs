using DAL;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WEBL.Controllers;


namespace WEBL
{
    class CurrencyReciever : IHostedService, IDisposable
    {
        private Timer timer;
        private DashboardController controller = new DashboardController();

        public void Dispose()
        {
            timer?.Dispose();

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            //timer = new Timer(async x =>
            //{

            //    DAL.Models.AISContext db = new DAL.Models.AISContext();
            //    Dashboard.PreviousEUR = Dashboard.CurrencyEUR;
            //    Dashboard.PreviousUSD = Dashboard.CurrencyUSD;
            //    //Dashboard.CurrencyEUR = getExchangeServiceEUR().ToString();
            //    //Dashboard.CurrencyUSD = getExchangeServiceUSD().ToString();,.
            //    //var nowString = now.ToString().Split(" ")[0];

            //    List<int> internalOrdersListDelivery = db.InternalOrders.Where(d => d.DeliveryDate.HasValue && d.DeliveryDate.Value.Date == DateTime.Now.Date && d.StatusId == (int)Constants.InternalOrderStatus.APPROVED)
            //    .Select(i => i.Id).ToList();

            //    List<int> internalOrdersListFollowup = db.InternalOrders.Where(d => d.FollowUpDate.HasValue && d.FollowUpDate.Value.Date == DateTime.Now.Date && d.StatusId == (int)Constants.InternalOrderStatus.PENDING)
            //   .Select(i => i.Id).ToList();
            //    List<int> ids = new List<int>();


            //    foreach (int Id in internalOrdersListDelivery)
            //    {
            //        int PlaceByRecipientId = db.InternalOrders.Where(s => s.Id == Id).Select(t => t.PlacedById).FirstOrDefault();
            //        int RequestedByRecipientId = db.InternalOrders.Where(s => s.Id == Id).Select(t => t.RequestedById).FirstOrDefault();

            //        if (PlaceByRecipientId != RequestedByRecipientId)
            //        {
            //            ids.Add(RequestedByRecipientId);
            //            ids.Add(PlaceByRecipientId);
            //        }
            //        else
            //        {
            //            ids.Add(PlaceByRecipientId);
            //        }

            //        if (PlaceByRecipientId != RequestedByRecipientId)
            //        {
            //            /*two different*/
            //            var sendEmailtoPlaceBy = db.Users.Where(i => i.Id == PlaceByRecipientId).Select(e => e.SendEmail).FirstOrDefault();
            //            var sendEmailtoReqBy = db.Users.Where(i => i.Id == RequestedByRecipientId).Select(e => e.SendEmail).FirstOrDefault();
            //            if (sendEmailtoPlaceBy)
            //            {
            //                BLL.Emailing.DeliveryReminderEmailing(Id, "today");
            //            }

            //            if (sendEmailtoReqBy)
            //            {
            //                BLL.Emailing.DeliveryReminderEmailing(Id, "today");
            //            }
            //        }
            //        else
            //        {
            //            /*the same*/
            //            var sendEmailtoPlaceBy = db.Users.Where(i => i.Id == PlaceByRecipientId).Select(e => e.SendEmail).FirstOrDefault();
            //            if (sendEmailtoPlaceBy)
            //            {
            //                BLL.Emailing.DeliveryReminderEmailing(Id, "today");
            //            }
            //        }

            //    }

            //    foreach (int Id in internalOrdersListFollowup)
            //    {
            //        int PlaceByRecipientId = db.InternalOrders.Where(s => s.Id == Id).Select(t => t.PlacedById).FirstOrDefault();
            //        int RequestedByRecipientId = db.InternalOrders.Where(s => s.Id == Id).Select(t => t.RequestedById).FirstOrDefault();

            //        if (PlaceByRecipientId != RequestedByRecipientId)
            //        {
            //            ids.Add(RequestedByRecipientId);
            //            ids.Add(PlaceByRecipientId);
            //        }
            //        else
            //        {
            //            ids.Add(PlaceByRecipientId);
            //        }

            //        if (PlaceByRecipientId != RequestedByRecipientId)
            //        {
            //            /*two different*/
            //            var sendEmailtoPlaceBy = db.Users.Where(i => i.Id == PlaceByRecipientId).Select(e => e.SendEmail).FirstOrDefault();
            //            var sendEmailtoReqBy = db.Users.Where(i => i.Id == RequestedByRecipientId).Select(e => e.SendEmail).FirstOrDefault();
            //            if (sendEmailtoPlaceBy)
            //            {
            //                BLL.Emailing.FollowUpEmailing(Id);
            //            }

            //            if (sendEmailtoReqBy)
            //            {
            //                BLL.Emailing.FollowUpEmailing(Id);
            //            }
            //        }
            //        else
            //        {
            //            /*the same*/
            //            var sendEmailtoPlaceBy = db.Users.Where(i => i.Id == PlaceByRecipientId).Select(e => e.SendEmail).FirstOrDefault();
            //            if (sendEmailtoPlaceBy)
            //            {
            //                BLL.Emailing.FollowUpEmailing(Id);
            //            }
            //        }
            //    }

            //    foreach (int id in ids)
            //    {
            //        BLL.Users.dontEmail(id);
            //    }
            //},
            //null,
            //TimeSpan.Zero,
            //TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        private string getExchangeServiceEUR()
        {
            var client = new RestClient("https://api.apilayer.com/currency_data/convert?to=ZAR&from=EUR&amount=1");
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", "lJnsm6pv9MtcvoFOUmnIcRQGzsU2cfEb");

            IRestResponse response = client.Execute(request);
            dynamic result = JsonConvert.DeserializeObject(response.Content);
            if (result.result == null) {
                return "";
            }
            else
            {
                return result.result;
            }


        }
        private string getExchangeServiceUSD()
        {
            var client = new RestClient("https://api.apilayer.com/currency_data/convert?to=ZAR&from=USD&amount=1");
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", "lJnsm6pv9MtcvoFOUmnIcRQGzsU2cfEb");

            IRestResponse response = client.Execute(request);
            dynamic result = JsonConvert.DeserializeObject(response.Content);
            if (result.result == null)
            {
                return "";
            }
            else
            {
                return result.result;
            }
        }
        
    }
}
