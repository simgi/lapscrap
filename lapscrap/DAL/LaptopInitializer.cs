using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using lapscrap.Models;
using lapscrap.DAL;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Net;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lapscrap.DAL
{
    public class LaptopInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<LaptopContext>
    {
        protected override void Seed(LaptopContext context)
        {
            string filepath = @"D:\data\code\lapscrap\run_results.json";
            List<Laptop> Laptops = new List<Laptop>();
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filepath))
                {
                    // Read the stream to a string, and write the string to the console.
                    string json_laptopdetails = sr.ReadToEnd();
                    //Laptop laptop = new Laptop();


                    //pasteBox.Text = json_laptopdetails;
                    JsonTextReader reader = new JsonTextReader(new StringReader(json_laptopdetails));
                    JObject laptopParser = JObject.Parse(json_laptopdetails);

                    // get JSON child objects into a list
                    IList<JToken> results = laptopParser["overview"].Children().ToList();
                    // serialize JSON results into laptop objects
                    foreach (JToken child in results)
                    {
                        Laptop laptop = new Laptop(child["components"].ToString());
                        laptop.Title = child["name"].ToString();
                        //laptop.Review_url = child["reviews"][0]["review_url"].ToString();
                        laptop.Shop_url = child["shop"][2]["shop_url"].ToString();
                        laptop.Price = Util.parseFloat(child["shop"][0]["price"].ToString());
                        Laptops.Add(laptop);
                    }
                }

                //var laptops = new List<Laptop>
                //{
                //new Laptop{Name="Test Name",Title="Test Device", Battery="Test Battery", Brand="Test Brand", Components="Test Components", Cpu="Test CPU", Ebay_url="Test EbayURL", Gpu="Test GPU", Hd="Test Hd", Height=-1, Id=1, Length=-1, Price=-1, Ram=-1, Resolution="Test Resolution", Review_url="Test ReviewURL" }
                //};

                Laptops.ForEach(l => context.Laptops.Add(l));
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}