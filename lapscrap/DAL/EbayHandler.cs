using lapscrap.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace lapscrap.DAL
{
    class ebayHandler
    {
        private List<EbayItem> items = new List<EbayItem>();

        public List<EbayItem> Items
        {
            get { return items; }
        }

        public ebayHandler(string search)
        {
            //AddRange hängt komplette Liste an
            this.items.AddRange(getDetails(search));
        }

        public IList<EbayItem> getDetails(string search)
        {
            List<EbayItem> items = new List<EbayItem>();

            /*
             * eBay API Details
             * 
             **/
            string service_url = "https://svcs.ebay.com/services/search/FindingService/v1?SECURITY-APPNAME=";
            string appId = "SimonGie-lapscrap-PRD-e5d8a3c47-da2fb544";
            string operations = "&OPERATION-NAME=findItemsByKeywords&SERVICE-VERSION=1.0.0&RESPONSE-DATA-FORMAT=JSON&callback=_cb_findItemsByKeywords&REST-PAYLOAD&keywords=";
            string keywords = search;
            string settings = "&itemFilter(0).name=Condition&itemFilter(0).value=2000&itemFilter(0).value=2500&itemFilter(0).value=3000&paginationInput.entriesPerPage=12&GLOBAL-ID=EBAY-DE&siteid=77";

            string url = service_url + appId + operations + keywords + settings;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            // Set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string ebayResponse = readStream.ReadToEnd();
            response.Close();
            readStream.Close();

            // Malformed json String --> replace unfitting parts
            ebayResponse = ebayResponse.Replace("/**/_cb_findItemsByKeywords(", "");
            ebayResponse = ebayResponse.Remove(ebayResponse.Length - 1, 1);

            JsonTextReader reader = new JsonTextReader(new StringReader(ebayResponse));
            JObject ebayParser = JObject.Parse(ebayResponse);
            JToken jEbay = ebayParser.First.First.First["searchResult"][0];
            foreach (JToken item in jEbay["item"])
            {
                EbayItem eItem = new EbayItem(item);
                items.Add(eItem);
            }
            return items;
        }

    }
}