﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace LOR_Windows_Form
{
    public class Deck
    {
        public string DeckCode { get; set; }
        public Dictionary<string,int> CardsInDeck { get; set; }
    }
    public class card
    {
        public string name { get; set; }
        public string cardCode { get; set; }
        //public List<img> assets { get; set; }
    }
    //public class img
    //{
    //    public string gameAbsolutePath { get; set; }
    //    public string fullAbsolutePath { get; set; }
    //}
        public partial class LORGG : Form
    {
        Dictionary<string,string> allCards;
        Thread t;
        private const string URL = "http://127.0.0.1:21337";
        public LORGG()
        {
            InitializeComponent();
        }
        private void LORGG_Load(object sender, EventArgs e)
        {
            //create and start a new thread in the load event.
            //passing it a method to be run on the new thread.
            t = new Thread(DoThisAllTheTime);
            t.Start();
            CardList.Text = "";
            string json = File.ReadAllText("../../../../sets/set1-en_us.json");
            string json2 = File.ReadAllText("../../../../sets/set2-en_us.json");
            List<card> tempList = new List<card>();
            tempList.AddRange(JsonConvert.DeserializeObject<List<card>>(json));
            tempList.AddRange(JsonConvert.DeserializeObject<List<card>>(json2));
            allCards = tempList.ToDictionary( x => x.cardCode, x => x.name);
        }

        public void DoThisAllTheTime()
        {
            //while (true)
            //{
            //    //prints test every second but will check for deck/game in future
            //    Thread.Sleep(1000);
            //}
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            // In the class
            HttpClient client = new HttpClient();

            // Put the following code where you want to initialize the class
            // It can be the static constructor or a one-time initializer
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/static-decklist").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {

                // Parse the response body.
                var data = await response.Content.ReadAsStringAsync();
                //use JavaScriptSerializer from System.Web.Script.Serialization
                JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                //deserialize to your class
                Deck cards = JsonConvert.DeserializeObject<Deck>(data);
                string text = "";
                if (cards.CardsInDeck != null)
                {
                    foreach (var item in cards.CardsInDeck)
                    {
                        text += allCards[item.Key] +  "   |   Copies: " + item.Value.ToString() + "\n";
                        Image image1 = Image.FromFile("../../../../imgs/" + item.Key +".png");
                        DeckList.Images.Add(image1);
                    }
                    CardList.Text = text;
                }
                else
                {
                    CardList.Text = "Not in game\n";
                }

            }
            else
            {
                CardList.Text = "LOR not running\n";
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }

        private void LORGG_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Abort();
        }
    }
}
