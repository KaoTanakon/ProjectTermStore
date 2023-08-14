using AForge.Video.DirectShow;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode;
namespace ProjectTermStore
{

    public partial class CounterEmployeeFrom : Form
    {
        APD65_63011212031Entities context = new APD65_63011212031Entities();
        FilterInfoCollection webcams;
        VideoCaptureDevice videoIn;
        bool customerChack = false;
        public CounterEmployeeFrom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm LoginF = new LoginForm();
            this.Close();
            LoginF.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm LoginF = new LoginForm();
            this.Close();
            LoginF.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginForm LoginF = new LoginForm();
            this.Close();
            LoginF.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoginForm LoginF = new LoginForm();
            this.Close();
            LoginF.Show();
        }

        private void CounterEmployeeFrom_Load(object sender, EventArgs e)
        {
            var customers = context.CustomerComStores.ToList();
            textBox10.Text = customers.Count.ToString();
            foreach (var customer in customers)
            {
                ListViewItem item = new ListViewItem(customer.cId.ToString());
                item.SubItems.Add(customer.FirstName);
                item.SubItems.Add(customer.LastName);
                item.SubItems.Add(customer.phone);
                listView2.Items.Add(item);
            }

            var products = context.ProductStockComStores.ToList();
            textBox6.Text = products.Count.ToString();
            if (comboBox2.SelectedItem == null)
            {
                comboBox2.Items.Add("สินค้าทั้งหมด");
            }
            foreach (var product in products)
            {
                ListViewItem item = new ListViewItem(product.pId.ToString());
                item.SubItems.Add(product.name);
                item.SubItems.Add(product.type);
                item.SubItems.Add(product.amount.ToString());
                listView3.Items.Add(item);
                if (comboBox2.FindString(product.type.ToString()) < 0)
                {
                    comboBox2.Items.Add(product.type);
                }
            }

            textBox1.Text = products.Count.ToString();
            if (comboBox1.SelectedItem == null)
            {
                comboBox1.Items.Add("สินค้าทั้งหมด");
            }
            foreach (var product in products)
            {
                ListViewItem item = new ListViewItem(product.pId.ToString());
                item.SubItems.Add(product.name);
                item.SubItems.Add(product.type);
                item.SubItems.Add(product.amount.ToString());
                item.SubItems.Add(product.price.ToString());
                listView1.Items.Add(item);
                if (comboBox1.FindString(product.type.ToString()) < 0)
                {
                    comboBox1.Items.Add(product.type);
                }
            }
            webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo webcam in webcams)
            {
                comboBox4.Items.Add(webcam.Name);
            }

            listView4.Items.Clear();
            var Orders = context.OrderComStores.ToList();
            textBox16.Text = Orders.Count().ToString();
            foreach (var order in Orders)
            {
                var customer = context.CustomerComStores.Where(c => c.cId == order.cId).First();
                ListViewItem item = new ListViewItem(order.oId.ToString());
                item.SubItems.Add(order.date.ToString());
                item.SubItems.Add(order.total.ToString());
                item.SubItems.Add(order.cId.ToString());
                item.SubItems.Add(customer.FirstName.ToString());
                item.SubItems.Add(customer.LastName.ToString());
                item.SubItems.Add(customer.phone.ToString());

                listView4.Items.Add(item);
            }
        }
        private Image LoadImage(string url)
        {
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.UserAgent = "Chrome/105.0.0.0";
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream streamResponse = myHttpWebResponse.GetResponseStream();
                Bitmap bmp = new Bitmap(streamResponse);
                streamResponse.Dispose();
                return bmp;
            }catch(Exception ex)
            {
                return null;
            }
            
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox9.Text = listView2.SelectedItems[0].SubItems[0].Text;
            textBox8.Text = listView2.SelectedItems[0].SubItems[1].Text;
            textBox23.Text = listView2.SelectedItems[0].SubItems[2].Text;
            textBox7.Text = listView2.SelectedItems[0].SubItems[3].Text;
        }

        private void listView3_MouseClick(object sender, MouseEventArgs e)
        {
            int id = int.Parse(listView3.SelectedItems[0].SubItems[0].Text);
            var product = context.ProductStockComStores.Where(p => p.pId == id).First();
            //
            textBox14.Text = id.ToString();
            textBox13.Text = listView3.SelectedItems[0].SubItems[1].Text;
            textBox12.Text = product.description.ToString();
            textBox11.Text = product.price.ToString();
            textBox15.Text = listView3.SelectedItems[0].SubItems[3].Text;
            pictureBox1.Image = LoadImage(product.image);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            int id = 0;
            List<CustomerComStore> customers;
            listView2.Items.Clear();
            if (!String.IsNullOrEmpty(textBox14.Text))
            {
                id = int.Parse(textBox14.Text);
            }
            if (!String.IsNullOrEmpty(textBox14.Text) && !String.IsNullOrEmpty(textBox13.Text) && !String.IsNullOrEmpty(textBox12.Text))
            {
                var products = context.ProductStockComStores.Where(p => p.pId == id && p.name.Contains(textBox13.Text) && p.description.Contains(textBox12.Text)).ToList();
                textBox6.Text = products.Count.ToString();
                foreach (var product in products)
                {
                    ListViewItem item = new ListViewItem(product.pId.ToString());
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.type);
                    item.SubItems.Add(product.amount.ToString());
                    listView3.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox14.Text) && !String.IsNullOrEmpty(textBox13.Text))
            {
                var products = context.ProductStockComStores.Where(p => p.pId == id && p.name.Contains(textBox13.Text)).ToList();
                textBox6.Text = products.Count.ToString();
                foreach (var product in products)
                {
                    ListViewItem item = new ListViewItem(product.pId.ToString());
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.type);
                    item.SubItems.Add(product.amount.ToString());
                    listView3.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox14.Text) && !String.IsNullOrEmpty(textBox12.Text))
            {
                var products = context.ProductStockComStores.Where(p => p.pId == id && p.description.Contains(textBox12.Text)).ToList();
                textBox6.Text = products.Count.ToString();
                foreach (var product in products)
                {
                    ListViewItem item = new ListViewItem(product.pId.ToString());
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.type);
                    item.SubItems.Add(product.amount.ToString());
                    listView3.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox13.Text) && !String.IsNullOrEmpty(textBox12.Text))
            {
                var products = context.ProductStockComStores.Where(p => p.name.Contains(textBox13.Text) && p.description.Contains(textBox12.Text)).ToList();
                textBox6.Text = products.Count.ToString();
                foreach (var product in products)
                {
                    ListViewItem item = new ListViewItem(product.pId.ToString());
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.type);
                    item.SubItems.Add(product.amount.ToString());
                    listView3.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox14.Text))
            {
                var products = context.ProductStockComStores.Where(p => p.pId == id).ToList();
                textBox6.Text = products.Count.ToString();
                foreach (var product in products)
                {
                    ListViewItem item = new ListViewItem(product.pId.ToString());
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.type);
                    item.SubItems.Add(product.amount.ToString());
                    listView3.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox13.Text))
            {
                var products = context.ProductStockComStores.Where(p => p.name.Contains(textBox13.Text)).ToList();
                textBox6.Text = products.Count.ToString();
                foreach (var product in products)
                {
                    ListViewItem item = new ListViewItem(product.pId.ToString());
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.type);
                    item.SubItems.Add(product.amount.ToString());
                    listView3.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox12.Text))
            {
                var products = context.ProductStockComStores.Where(p => p.description.Contains(textBox12.Text)).ToList();
                textBox6.Text = products.Count.ToString();
                foreach (var product in products)
                {
                    ListViewItem item = new ListViewItem(product.pId.ToString());
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.type);
                    item.SubItems.Add(product.amount.ToString());
                    listView3.Items.Add(item);
                }
            }
            else
            {
                listView3.Items.Clear();
                this.CounterEmployeeFrom_Load(sender, e);
                MessageBox.Show("Not Found");

                var products = context.ProductStockComStores.ToList();
                textBox6.Text = products.Count.ToString();
                foreach (var product in products)
                {
                    ListViewItem item = new ListViewItem(product.pId.ToString());
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.type);
                    item.SubItems.Add(product.amount.ToString());
                    listView3.Items.Add(item);
                }
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex > 0)
            {
                Console.WriteLine(comboBox2.SelectedItem);
                listView3.Items.Clear();
                var products = context.ProductStockComStores.Where(p => p.type == comboBox2.SelectedItem).ToList();
                textBox6.Text = products.Count.ToString();
                foreach (var product in products)
                {
                    ListViewItem item = new ListViewItem(product.pId.ToString());
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.type);
                    item.SubItems.Add(product.amount.ToString());
                    listView3.Items.Add(item);
                }
            }
            else
            {
                listView3.Items.Clear();
                var products = context.ProductStockComStores.ToList();
                textBox6.Text = products.Count.ToString();
                foreach (var product in products)
                {
                    ListViewItem item = new ListViewItem(product.pId.ToString());
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.type);
                    item.SubItems.Add(product.amount.ToString());
                    listView3.Items.Add(item);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int id = 0;
            List<CustomerComStore> customers;
            listView2.Items.Clear();
            if (!String.IsNullOrEmpty(textBox9.Text))
            {
                Console.WriteLine("0o0");
                id = int.Parse(textBox9.Text);
            }
            if (!String.IsNullOrEmpty(textBox9.Text) && !String.IsNullOrEmpty(textBox8.Text) && !String.IsNullOrEmpty(textBox7.Text))
            {
                customers = context.CustomerComStores.Where(c => c.cId == id && c.FirstName.Contains(textBox8.Text) && c.phone.Contains(textBox7.Text)).ToList();
                textBox10.Text = customers.Count.ToString();
                foreach (var customer in customers)
                {
                    ListViewItem item = new ListViewItem(customer.cId.ToString());
                    item.SubItems.Add(customer.FirstName);
                    item.SubItems.Add(customer.LastName);
                    item.SubItems.Add(customer.phone);
                    listView2.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox9.Text) && !String.IsNullOrEmpty(textBox8.Text))
            {
                customers = context.CustomerComStores.Where(c => c.cId == id && c.FirstName.Contains(textBox8.Text)).ToList();
                textBox10.Text = customers.Count.ToString();
                foreach (var customer in customers)
                {
                    ListViewItem item = new ListViewItem(customer.cId.ToString());
                    item.SubItems.Add(customer.FirstName);
                    item.SubItems.Add(customer.LastName);
                    item.SubItems.Add(customer.phone);
                    listView2.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox9.Text) && !String.IsNullOrEmpty(textBox7.Text))
            {
                customers = context.CustomerComStores.Where(c => c.cId == id && c.phone.Contains(textBox7.Text)).ToList();
                textBox10.Text = customers.Count.ToString();
                foreach (var customer in customers)
                {
                    ListViewItem item = new ListViewItem(customer.cId.ToString());
                    item.SubItems.Add(customer.FirstName);
                    item.SubItems.Add(customer.LastName);
                    item.SubItems.Add(customer.phone);
                    listView2.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox8.Text) && !String.IsNullOrEmpty(textBox7.Text))
            {
                customers = context.CustomerComStores.Where(c => c.FirstName.Contains(textBox8.Text) && c.phone.Contains(textBox7.Text)).ToList();
                textBox10.Text = customers.Count.ToString();
                foreach (var customer in customers)
                {
                    ListViewItem item = new ListViewItem(customer.cId.ToString());
                    item.SubItems.Add(customer.FirstName);
                    item.SubItems.Add(customer.LastName);
                    item.SubItems.Add(customer.phone);
                    listView2.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox9.Text))
            {
                customers = context.CustomerComStores.Where(c => c.cId == id).ToList();
                textBox10.Text = customers.Count.ToString();
                foreach (var customer in customers)
                {
                    ListViewItem item = new ListViewItem(customer.cId.ToString());
                    item.SubItems.Add(customer.FirstName);
                    item.SubItems.Add(customer.LastName);
                    item.SubItems.Add(customer.phone);
                    listView2.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox8.Text))
            {
                customers = context.CustomerComStores.Where(c => c.FirstName.Contains(textBox8.Text)).ToList();
                textBox10.Text = customers.Count.ToString();
                foreach (var customer in customers)
                {
                    ListViewItem item = new ListViewItem(customer.cId.ToString());
                    item.SubItems.Add(customer.FirstName);
                    item.SubItems.Add(customer.LastName);
                    item.SubItems.Add(customer.phone);
                    listView2.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox7.Text))
            {
                customers = context.CustomerComStores.Where(c => c.phone.Contains(textBox7.Text)).ToList();
                textBox10.Text = customers.Count.ToString();
                foreach (var customer in customers)
                {
                    ListViewItem item = new ListViewItem(customer.cId.ToString());
                    item.SubItems.Add(customer.FirstName);
                    item.SubItems.Add(customer.LastName);
                    item.SubItems.Add(customer.phone);
                    listView2.Items.Add(item);
                }
            }
            else
            {
                listView2.Items.Clear();
                this.CounterEmployeeFrom_Load(sender, e);
                MessageBox.Show("Not Found");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            CustomerComStore customer = new CustomerComStore();
            customer.FirstName = textBox8.Text;
            customer.LastName = textBox23.Text;
            customer.phone = textBox7.Text;

            context.CustomerComStores.Add(customer);
            context.SaveChanges();
            listView2.Items.Clear();
            this.CounterEmployeeFrom_Load(sender, e);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox9.Text);
            var result = context.CustomerComStores.Where(c => c.cId == id).First();
            result.FirstName = textBox8.Text;
            result.LastName = textBox23.Text;
            result.phone = textBox7.Text;
            var change = context.SaveChanges();
            MessageBox.Show("Success " + change + " row");
            listView2.Items.Clear();
            this.CounterEmployeeFrom_Load(sender, e);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox9.Text);
            var result = context.CustomerComStores.Where(c => c.cId == id).First();
            context.CustomerComStores.Remove(result);
            context.SaveChanges();

            listView2.Items.Remove(listView2.SelectedItems[0]);
            this.CounterEmployeeFrom_Load(sender, e);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox9.Text = null;
            textBox8.Text = null;
            textBox23.Text = null;
            textBox7.Text = null;
        }

        private void SellTap_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            int sum = 0;
            var id = listView6.FindItemWithText(textBox22.Text);
            int amount = int.Parse(textBox4.Text);
            int price = int.Parse(id.SubItems[4].Text);
            int Itemsprice = (int)price * amount;
            id.SubItems[1].Text = textBox4.Text;
            id.SubItems[5].Text = Itemsprice.ToString();

            foreach (ListViewItem itemsum in listView6.Items)
            {
                int value = int.Parse(itemsum.SubItems[5].Text);
                sum += value;
            }
            label4.Text = sum.ToString();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            selectProductItem(id);
            /*var product = context.ProductStockComStores.Where(p => p.pId == id).First();
            
            textBox22.Text = id.ToString();
            textBox3.Text = id.ToString();
            textBox2.Text = id.ToString();
            textBox21.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox20.Text = product.description.ToString();
            textBox5.Text = product.price.ToString();
            pictureBox3.Image = LoadImage(product.image);*/
        }
        public void selectProductItem(int id)
        {
            var product = context.ProductStockComStores.Where(p => p.pId == id).First();
            //
            textBox22.Text = id.ToString();
            textBox3.Text = id.ToString();
            textBox2.Text = id.ToString();
            textBox21.Text = product.name.ToString();
            textBox20.Text = product.description.ToString();
            textBox5.Text = product.price.ToString();
            pictureBox3.Image = LoadImage(product.image);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int selectedCamIndex = comboBox4.SelectedIndex;
            videoIn = new VideoCaptureDevice(webcams[selectedCamIndex].MonikerString);
            videoSourcePlayer1.VideoSource = videoIn;
            videoSourcePlayer1.Start();
            timer1.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(videoIn != null && videoIn.IsRunning)
            {
                timer1.Stop();
                videoSourcePlayer1.Stop();
            }
        }

        private void CounterEmployeeFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoIn != null && videoIn.IsRunning)
            {
                timer1.Stop();
                videoSourcePlayer1.Stop();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox2.Text);
            selectProductItem(id);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            QrCodeEncodingOptions Options = new QrCodeEncodingOptions();
            Options.CharacterSet = "UTF-8";
            Options.Width = 200;
            Options.Height = 130;

            BarcodeWriter writer = new BarcodeWriter();
            writer.Options = Options;
            writer.Format = BarcodeFormat.QR_CODE;
            var result = writer.Write(textBox3.Text);
            pictureBox2.Image = result;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var capture = videoSourcePlayer1.GetCurrentVideoFrame();
            if(capture != null)
            {
                BarcodeReader reader = new BarcodeReader();
                var result = reader.Decode(capture);
                if(result != null)
                {
                    listView6.FindItemWithText(result.Text);
                    selectProductItem(int.Parse(result.Text));

                    if (listView6.FindItemWithText(textBox3.Text) != null)
                    {
                        MessageBox.Show("have one");
                        foreach(ListViewItem items in listView6.Items)
                        {
                            if (items.SubItems[0].Text == result.Text)
                            {
                                var amount = int.Parse(items.SubItems[1].Text);
                                var pricePromotion = int.Parse(items.SubItems[4].Text);
                                amount++;
                                items.SubItems[1].Text = amount.ToString();
                                items.SubItems[5].Text = (amount * pricePromotion).ToString();
                            }
                        }
                    }
                    else
                    {
                        int id = int.Parse(textBox3.Text);
                        int sum = 0;
                        int SumpricePiece = 0;
                        int proId = 0;

                        var product = context.ProductStockComStores.Where(p => p.pId == id).First();
                        int price = int.Parse(textBox5.Text);
                        int amount = int.Parse(textBox4.Text);
                        ListViewItem item = new ListViewItem(textBox3.Text);
                        item.SubItems.Add(textBox4.Text);
                        item.SubItems.Add(textBox5.Text);
                        item.SubItems.Add("-");
                        item.SubItems.Add(textBox5.Text);
                        SumpricePiece = (int)price * amount;
                        item.SubItems.Add(SumpricePiece.ToString());
                        listView6.Items.Add(item);

                        if (listView6.Items.Count >= 2)
                        {
                            Console.WriteLine("ListView6 : " + listView6.Items.Count);
                            foreach (ListViewItem itemlist6 in listView6.Items)
                            {
                                int Iid = int.Parse(itemlist6.SubItems[0].Text);
                                var productPromotion = context.ProductStockComStores.Where(p => p.pId == Iid).First();
                                if (productPromotion.promotionId != null)
                                {
                                    Console.WriteLine(productPromotion.pId + "main");
                                    var products = context.ProductStockComStores.Where(p => p.promotionId == productPromotion.promotionId && p.pId != productPromotion.pId).First();
                                    foreach (ListViewItem Jitemlist in listView6.Items)
                                    {
                                        Console.WriteLine(products.pId);
                                        if (Jitemlist.SubItems[0].Text == products.pId.ToString())
                                        {
                                            var promotionItem = context.PromotionComStores.Where(pmt => pmt.promotionId == products.promotionId).First();
                                            int Iprice = productPromotion.price;
                                            int Jprice = products.price;
                                            int percent = promotionItem.sellpercent;
                                            int Iamount = int.Parse(itemlist6.SubItems[1].Text);
                                            int Jamount = int.Parse(Jitemlist.SubItems[1].Text);
                                            double Ians = Iprice - ((double)Iprice / 100 * (double)percent);
                                            double Jans = Jprice - ((double)Jprice / 100 * (double)percent);

                                            itemlist6.SubItems[3].Text = percent.ToString();
                                            Jitemlist.SubItems[3].Text = percent.ToString();

                                            itemlist6.SubItems[4].Text = Ians.ToString();
                                            Jitemlist.SubItems[4].Text = Jans.ToString();

                                            itemlist6.SubItems[5].Text = ((int)Ians * Iamount).ToString();
                                            Jitemlist.SubItems[5].Text = ((int)Jans * Jamount).ToString();
                                        }
                                    }
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                        foreach (ListViewItem itemsum in listView6.Items)
                        {
                            int value = int.Parse(itemsum.SubItems[5].Text);
                            sum += value;
                        }
                        label4.Text = sum.ToString();
                    }
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            string url = "https://www.jib.co.th/web/product/readProduct/";
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url + textBox2.Text);
            var html = doc.DocumentNode.Descendants("meta");
            var titleNode = html.Where(m => m.GetAttributeValue("property", "") == "og:title").First();
            var descriptionNode = html.Where(m => m.GetAttributeValue("property", "") == "og:description").First();
            HtmlNode FindP1 = doc.DocumentNode.SelectSingleNode("//div[@class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block\"]");
            var FindP2 = FindP1.ChildNodes[5];
            var Addprice = FindP2.InnerHtml;
            var priceStr = new string(Addprice.Where(c => char.IsDigit(c)).ToArray());
            HtmlNode FindT1 = doc.DocumentNode.SelectSingleNode("//div[@class=\"step_nav\"]");
            HtmlNode type = FindT1.ChildNodes[9];
            var imageNode = html.Where(m => m.GetAttributeValue("property", "") == "og:image").First();

            ProductStockComStore productAdd = new ProductStockComStore();
            productAdd.pId = int.Parse(textBox2.Text);
            productAdd.name = titleNode.GetAttributeValue("content", "");
            productAdd.description = descriptionNode.GetAttributeValue("content", "");
            productAdd.price = int.Parse(priceStr);
            productAdd.image = imageNode.GetAttributeValue("content", "");
            productAdd.type = type.InnerHtml;
            productAdd.amount = random.Next(501);
            try
            {
                context.ProductStockComStores.Add(productAdd);
                context.SaveChanges();
            }catch(Exception)
            {
                MessageBox.Show("In stock");
            }
            

            selectProductItem(int.Parse(textBox2.Text));
            Console.WriteLine("id :"+int.Parse(textBox2.Text));
            Console.WriteLine("name :" + titleNode.GetAttributeValue("content", ""));
            Console.WriteLine("description :" + descriptionNode.GetAttributeValue("content", ""));
            Console.WriteLine("price :" + priceStr);
            Console.WriteLine("image :" + imageNode.GetAttributeValue("content", ""));
            Console.WriteLine("type :" + type.InnerHtml);

            ////////////////////////////////////////////////////////////
            if (listView6.FindItemWithText(textBox3.Text) != null)
            {
                MessageBox.Show("have one");
            }
            else
            { 
                int id = int.Parse(textBox3.Text);
                int sum = 0;
                int SumpricePiece = 0;
                int proId = 0;
            
                var product = context.ProductStockComStores.Where(p => p.pId == id).First();
                int price = int.Parse(textBox5.Text);
                int amount = int.Parse(textBox4.Text);
                ListViewItem item = new ListViewItem(textBox3.Text);
                item.SubItems.Add(textBox4.Text);
                item.SubItems.Add(textBox5.Text);
                item.SubItems.Add("-");
                item.SubItems.Add(textBox5.Text);
                SumpricePiece = (int)price * amount;
                item.SubItems.Add(SumpricePiece.ToString());
                listView6.Items.Add(item);

                if (listView6.Items.Count >= 2)
                {
                    Console.WriteLine("ListView6 : "+ listView6.Items.Count);
                    foreach(ListViewItem itemlist6 in listView6.Items)
                    {
                        int Iid = int.Parse(itemlist6.SubItems[0].Text);
                        var productPromotion = context.ProductStockComStores.Where(p => p.pId == Iid).First();
                        if (productPromotion.promotionId != null)
                        {
                            Console.WriteLine(productPromotion.pId+ "main");
                            var products = context.ProductStockComStores.Where(p => p.promotionId == productPromotion.promotionId && p.pId != productPromotion.pId).First();
                            foreach(ListViewItem Jitemlist in listView6.Items)
                            {
                                Console.WriteLine(products.pId);
                                if (Jitemlist.SubItems[0].Text == products.pId.ToString())
                                {
                                    var promotionItem = context.PromotionComStores.Where(pmt => pmt.promotionId == products.promotionId).First();
                                    int Iprice = productPromotion.price;
                                    int Jprice = products.price;
                                    int percent = promotionItem.sellpercent;
                                    int Iamount = int.Parse(itemlist6.SubItems[1].Text);
                                    int Jamount = int.Parse(Jitemlist.SubItems[1].Text);
                                    double Ians = Iprice - ((double)Iprice / 100 * (double)percent);
                                    double Jans = Jprice - ((double)Jprice / 100 * (double)percent);

                                    itemlist6.SubItems[3].Text = percent.ToString();
                                    Jitemlist.SubItems[3].Text = percent.ToString();

                                    itemlist6.SubItems[4].Text = Ians.ToString();
                                    Jitemlist.SubItems[4].Text = Jans.ToString();

                                    itemlist6.SubItems[5].Text = ((int)Ians * Iamount).ToString();
                                    Jitemlist.SubItems[5].Text = ((int)Jans * Jamount).ToString();
                                }
                            }
                            Console.WriteLine("\n");
                        }
                    }
                }
                foreach(ListViewItem itemsum in listView6.Items)
                {
                    int value = int.Parse(itemsum.SubItems[5].Text);
                    sum += value;
                }
                label4.Text = sum.ToString();
            }
        }

        private void listView6_MouseClick(object sender, MouseEventArgs e)
        {
            int id = int.Parse(listView6.SelectedItems[0].SubItems[0].Text);
            selectProductItem(id);
            textBox4.Text = listView6.SelectedItems[0].SubItems[1].Text;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox24.Text);
            try
            {
                var result = context.CustomerComStores.Where(c => c.cId == id).First();
                if (result != null)
                {
                    customerChack = true;
                    MessageBox.Show("รหัสลูกค้าถูกต้อง\n " + result.FirstName + " " + result.LastName);
                }
                else
                {
                    customerChack = false;
                    MessageBox.Show("รหัสลูกค้าไม่ถูกต้อง");
                }
            }
            catch (Exception ex)
            {
                customerChack = false;
                MessageBox.Show("รหัสลูกค้าไม่ถูกต้อง");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox24.Text))
            {
                OrderComStore order = new OrderComStore();
                order.date = DateTime.Now;
                order.total = int.Parse(label4.Text);
                order.cId = int.Parse(textBox24.Text);
                context.OrderComStores.Add(order);
                context.SaveChanges();
                var OrderItems = context.OrderComStores.ToList();
                var OrderItemIndx = OrderItems[OrderItems.Count() - 1].oId;

                foreach(ListViewItem item in listView6.Items)
                {
                    OrderDetailComStore orderDetail = new OrderDetailComStore();
                    orderDetail.pId = int.Parse(item.SubItems[0].Text);
                    orderDetail.amount = int.Parse(item.SubItems[1].Text);
                    orderDetail.oId = OrderItemIndx;

                    context.OrderDetailComStores.Add(orderDetail);
                    context.SaveChanges();
                }
                listView6.Items.Clear();
                label4.Text = "0";
                textBox24.Clear();
            }
            this.CounterEmployeeFrom_Load(sender, e);
        }

        private void listView4_MouseClick(object sender, MouseEventArgs e)
        {
            listView5.Items.Clear();
            var id = int.Parse(listView4.SelectedItems[0].SubItems[0].Text);
            var orderDetails = context.OrderDetailComStores.Where(od => od.oId == id).ToList();
            textBox17.Text = orderDetails.Count().ToString();
            foreach (var orderdetail in orderDetails)
            {
                var product = context.ProductStockComStores.Where(p => p.pId == orderdetail.pId).First();
                ListViewItem item = new ListViewItem(product.pId.ToString());
                item.SubItems.Add(product.name.ToString());
                item.SubItems.Add(orderdetail.amount.ToString());

                listView5.Items.Add(item);
            }
        }

        private void listView6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listView6.Items.Remove(listView6.SelectedItems[0]);
        }

        private void button20_Click(object sender, EventArgs e)
        {
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox18.Text) && !String.IsNullOrEmpty(textBox19.Text))
            {
                listView4.Items.Clear();
                listView5.Items.Clear();

                var customer = context.CustomerComStores.Where(c => c.FirstName == textBox18.Text && c.phone == textBox19.Text).First();
                var orders = context.OrderComStores.Where(o => o.cId == customer.cId).ToList();
                textBox16.Text = orders.Count.ToString();
                foreach (var order in orders)
                {
                    ListViewItem item = new ListViewItem(order.oId.ToString());
                    item.SubItems.Add(order.date.ToString());
                    item.SubItems.Add(order.total.ToString());
                    item.SubItems.Add(order.cId.ToString());
                    item.SubItems.Add(customer.FirstName.ToString());
                    item.SubItems.Add(customer.LastName.ToString());
                    item.SubItems.Add(customer.phone.ToString());

                    listView4.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox18.Text))
            {
                listView4.Items.Clear();
                listView5.Items.Clear();

                var customer = context.CustomerComStores.Where(c => c.FirstName == textBox18.Text).First();
                var orders = context.OrderComStores.Where(o => o.cId == customer.cId).ToList();
                textBox16.Text = orders.Count.ToString();
                foreach (var order in orders)
                {
                    ListViewItem item = new ListViewItem(order.oId.ToString());
                    item.SubItems.Add(order.date.ToString());
                    item.SubItems.Add(order.total.ToString());
                    item.SubItems.Add(order.cId.ToString());
                    item.SubItems.Add(customer.FirstName.ToString());
                    item.SubItems.Add(customer.LastName.ToString());
                    item.SubItems.Add(customer.phone.ToString());

                    listView4.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox19.Text))
            {
                listView4.Items.Clear();
                listView5.Items.Clear();

                var customer = context.CustomerComStores.Where(c => c.phone == textBox19.Text).First();
                var orders = context.OrderComStores.Where(o => o.cId == customer.cId).ToList();
                textBox16.Text = orders.Count.ToString();
                foreach (var order in orders)
                {
                    ListViewItem item = new ListViewItem(order.oId.ToString());
                    item.SubItems.Add(order.date.ToString());
                    item.SubItems.Add(order.total.ToString());
                    item.SubItems.Add(order.cId.ToString());
                    item.SubItems.Add(customer.FirstName.ToString());
                    item.SubItems.Add(customer.LastName.ToString());
                    item.SubItems.Add(customer.phone.ToString());

                    listView4.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("กรุณากรอกข้อมูล");
                listView4.Items.Clear();
                listView5.Items.Clear();
                var orders = context.OrderComStores.ToList();
                textBox16.Text = orders.Count.ToString();
                foreach (var order in orders)
                {
                    var customer = context.CustomerComStores.Where(c => c.cId == order.cId).First();
                    ListViewItem item = new ListViewItem(order.oId.ToString());
                    item.SubItems.Add(order.date.ToString());
                    item.SubItems.Add(order.total.ToString());
                    item.SubItems.Add(order.cId.ToString());
                    item.SubItems.Add(customer.FirstName.ToString());
                    item.SubItems.Add(customer.LastName.ToString());
                    item.SubItems.Add(customer.phone.ToString());

                    listView4.Items.Add(item);
                }
            }
        }
    }
}
