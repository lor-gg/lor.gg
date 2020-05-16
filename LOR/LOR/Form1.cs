using System;
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
using System.IO;
using Newtonsoft.Json;

namespace LOR
{
    //public class img
    //{
    //    public string gameAbsolutePath { get; set; }
    //    public string fullAbsolutePath { get; set; }
    //}

    public partial class Form1 : Form
    {
        Dictionary<string, card> allCards;
        Thread t;
        private const string URL = "http://127.0.0.1:21337";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
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
            allCards = tempList.ToDictionary(x => x.cardCode, x => x);
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
            try
            {
                HttpResponseMessage response = client.GetAsync("/static-decklist").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {

                    // Parse the response body.
                    var data = await response.Content.ReadAsStringAsync();
                    //use JavaScriptSerializer from System.Web.Script.Serialization
                    //deserialize to your class
                    Deck cards = JsonConvert.DeserializeObject<Deck>(data);
                    string text = "";
                    if (cards.CardsInDeck != null)
                    {
                        int singleImgHeight = 37;
                        DeckList.Height = singleImgHeight * cards.CardsInDeck.Count;
                        DeckList.Width = 300;

                        Bitmap outputImage = new Bitmap(DeckList.Width, DeckList.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        using (Graphics graphics = Graphics.FromImage(outputImage)) {
                            int i = 0;
                            foreach (var item in cards.CardsInDeck)
                            {
                                string cardName = allCards[item.Key].name;
                                int cardCopies = item.Value;
                                text += allCards[item.Key].name + "   |   Copies: " + item.Value.ToString() + "\n";
                            
                                Image image1 = Image.FromFile("../../../../imgs/" + item.Key + ".png");
                            
                                // TRYING TO DRAW ALL CARDS TOGETHER IN SAME IMAGE
                                Size imgSize = new Size(DeckList.Width, (singleImgHeight));
                                Point imgPos = new Point(0, i * singleImgHeight);
                                Console.WriteLine("=======");
                                Console.WriteLine(i);
                                Console.WriteLine(imgSize);
                                Console.WriteLine(imgPos);
                                
                                if (allCards[item.Key].type == "Unit")
                                {
                                    double scalingFactor = 0.66;
                                    image1 = ResizeImage(image1, (int)(image1.Width * scalingFactor), (int)(image1.Height * scalingFactor));
                                    graphics.DrawImage(image1, new Rectangle(new Point(0, i*singleImgHeight), imgSize),
                                        new Rectangle(new Point(70, 150), imgSize), GraphicsUnit.Pixel);
                                }
                                else if (allCards[item.Key].type == "Spell")
                                {
                                    double scalingFactor = 0.8;
                                    image1 = ResizeImage(image1, (int)(image1.Width * scalingFactor), (int)(image1.Height * scalingFactor));
                                    graphics.DrawImage(image1, new Rectangle(new Point(0, i*singleImgHeight), imgSize),
                                        new Rectangle(new Point(120, 200), imgSize), GraphicsUnit.Pixel);
                                }
                                
                                using (Font cardFont = new Font("Arial", 17))
                                {
                                    graphics.DrawString(cardName, cardFont, Brushes.White, new Point(0, i*singleImgHeight + 5));
                                    graphics.DrawString(cardCopies.ToString(), cardFont, Brushes.White, new Point(280, i*singleImgHeight + 5));
                                }

                                i++;
                            }
                        }
                        DeckList.Image = outputImage;
                        CardList.Text = text;
                    }
                    else
                    {
                        DeckList.Image = null;
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
            }
            catch (System.AggregateException)
            {
                CardList.Text = "LOR not running\n";
            }
            client.Dispose();


        }

        private void LORGG_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Abort();
        }

        public Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.DrawImage(image, destRect, 0, 0, image.Width,image.Height, GraphicsUnit.Pixel);
            }

            return destImage;
        }
    }
    public class Deck
    {
        public string DeckCode { get; set; }
        public Dictionary<string, int> CardsInDeck { get; set; }
    }
    public class card
    {
        public string name { get; set; }
        public string cardCode { get; set; }
        public string supertype { get; set; }
        public string type { get; set; }
        //public List<img> assets { get; set; }
    }
}
