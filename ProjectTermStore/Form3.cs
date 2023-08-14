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

namespace ProjectTermStore
{
    public partial class StockEmployeeFrom : Form
    {
        List<datamodel> models = new List<datamodel>();
        APD65_63011212031Entities context = new APD65_63011212031Entities();
        public StockEmployeeFrom()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoginForm LoginF = new LoginForm();
            this.Close();
            LoginF.Show();
        }
        private void WebExtractionTab_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoginForm LoginF = new LoginForm();
            this.Close();
            LoginF.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoginForm LoginF = new LoginForm();
            this.Close();
            LoginF.Show();
        }

        private void StockEmployeeFrom_Load(object sender, EventArgs e)
        {
            var products = context.ProductStockComStores.ToList();
            textBox8.Text = products.Count.ToString();
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

            textBox10.Text = products.Count.ToString();
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
                listView2.Items.Add(item);
                if (comboBox1.FindString(product.type.ToString()) < 0)
                {
                    comboBox1.Items.Add(product.type);
                }
            }

            var promotions = context.PromotionComStores.ToList();
            textBox22.Text = promotions.Count.ToString();
            foreach (var promotion in promotions)
            {
                ListViewItem item = new ListViewItem(promotion.promotionId.ToString());
                item.SubItems.Add(promotion.name);
                item.SubItems.Add(promotion.sellpercent.ToString());
                listView5.Items.Add(item);
            }
        }
        private Image LoadImage(string url)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myHttpWebRequest.UserAgent = "Chrome/105.0.0.0";
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream streamResponse = myHttpWebResponse.GetResponseStream();
            Bitmap bmp = new Bitmap(streamResponse);
            streamResponse.Dispose();
            return bmp;
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
            pictureBox2.Image = LoadImage(product.image);
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > 0)
            {
                Console.WriteLine(comboBox2.SelectedItem);
                listView3.Items.Clear();
                var products = context.ProductStockComStores.Where(p => p.type == comboBox2.SelectedItem).ToList();
                textBox8.Text = products.Count.ToString();
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
                textBox8.Text = products.Count.ToString();
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

        private void button9_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox9.Text))
            {
                listView3.Items.Clear();
                var products = context.ProductStockComStores.ToList();
                foreach (var product in products)
                {
                    var productIdStr = product.pId.ToString();
                    if (productIdStr.Contains(textBox9.Text))
                    {
                        ListViewItem item = new ListViewItem(product.pId.ToString());
                        item.SubItems.Add(product.name);
                        item.SubItems.Add(product.type);
                        item.SubItems.Add(product.amount.ToString());
                        listView3.Items.Add(item);
                    }
                }
                int count = listView3.Items.Count;
                textBox8.Text = count.ToString();
            }
            else
            {
                MessageBox.Show("กรุณากรอกข้อมูล");
                listView3.Items.Clear();
                var products = context.ProductStockComStores.ToList();
                foreach (var product in products)
                {
                    ListViewItem item = new ListViewItem(product.pId.ToString());
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.type);
                    item.SubItems.Add(product.amount.ToString());
                    listView3.Items.Add(item);
                }
                int count = listView3.Items.Count;
                textBox8.Text = count.ToString();
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox14.Text);
            var result = context.ProductStockComStores.Where(p => p.pId == id).First();
            context.ProductStockComStores.Remove(result);
            context.SaveChanges();

            listView3.Items.Remove(listView3.SelectedItems[0]);
            this.StockEmployeeFrom_Load(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox14.Text);
            var result = context.ProductStockComStores.Where(p => p.pId == id).First();
            int Newprice = int.Parse(textBox11.Text);
            int Newamount = int.Parse(textBox15.Text);
            result.name = textBox13.Text;
            result.description = textBox12.Text;
            result.price = Newprice;
            result.amount = Newamount;
            var change = context.SaveChanges();
            MessageBox.Show("Success " + change + " row");
            listView3.Items.Clear();
            this.StockEmployeeFrom_Load(sender, e);
        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int sum;
            int id = int.Parse(listView2.SelectedItems[0].SubItems[0].Text);
            var product = context.ProductStockComStores.Where(p => p.pId == id).First();
            if (String.IsNullOrEmpty(textBox21.Text))
            {
                sum = product.price;
            }
            else
            {
                sum = int.Parse(textBox21.Text)+product.price;
            }
            ListViewItem item = new ListViewItem(product.pId.ToString());
            item.SubItems.Add(product.name);
            item.SubItems.Add(product.price.ToString());
            item.SubItems.Add("-");
            item.SubItems.Add("-");
            listView4.Items.Add(item);
            textBox21.Text = sum.ToString();
        }

        private void listView4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int id = int.Parse(listView4.SelectedItems[0].SubItems[0].Text);
            listView4.SelectedItems[0].Remove();
            var product = context.ProductStockComStores.Where(p => p.pId == id).First();
            if (!String.IsNullOrEmpty(textBox21.Text))
            {
                int sum = int.Parse(textBox21.Text);
                textBox21.Text = (sum - product.price).ToString();
                if(int.Parse(textBox21.Text) < 0)
                {
                    textBox21.Text = 0.ToString();
                }
            }
            
            if (product.promotionId != null)
            {
                Console.WriteLine("Promotion Not NULL");
                product.promotionId = null;
                context.SaveChanges();
            }

        }

        private void listView4_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            PromotionComStore promotion = new PromotionComStore();

            promotion.name = textBox18.Text;
            promotion.description = textBox17.Text;
            promotion.sellprice = int.Parse(textBox19.Text);
            promotion.sellpercent = int.Parse(textBox20.Text);

            context.PromotionComStores.Add(promotion);
            context.SaveChanges();

            var promotionList = context.PromotionComStores.ToList();
            var promotionIndx = promotionList[promotionList.Count()-1].promotionId;
            Console.WriteLine("Promotion Index: "+promotionIndx);
            foreach (ListViewItem item in listView4.Items)
            {
                ProductStockComStore product = new ProductStockComStore();
                Console.WriteLine(item.SubItems[0].Text);
                int id = int.Parse(item.SubItems[0].Text);
                var result = context.ProductStockComStores.Where(p => p.pId == id).First();
                result.promotionId = promotionIndx;
                var change =context.SaveChanges();
                MessageBox.Show("row:" + change);
            }
            listView5.Items.Clear();
            this.StockEmployeeFrom_Load(sender, e);
        }

        private void PromotionTab_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox23.Text))
            {
                int id = int.Parse(textBox23.Text);
                var resultProduct = context.ProductStockComStores.Where(p => p.promotionId == id).ToList();
                foreach(var item in resultProduct)
                {
                    item.promotionId = null;
                    context.SaveChanges();
                }
                var resultPromotion = context.PromotionComStores.Where(pmt => pmt.promotionId == id).First();
                context.PromotionComStores.Remove(resultPromotion);
                var change = context.SaveChanges();
                MessageBox.Show("Success " + change + " row");
                listView5.Items.Clear();
                this.StockEmployeeFrom_Load(sender, e);
            }
        }

        private void listView5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox21.Clear();
            listView4.Items.Clear();
            int id = int.Parse(listView5.SelectedItems[0].SubItems[0].Text);
            var promotion = context.PromotionComStores.Where(pmt => pmt.promotionId == id).First();
            //
            textBox23.Text = id.ToString();
            textBox18.Text = listView5.SelectedItems[0].SubItems[1].Text;
            textBox20.Text = promotion.sellpercent.ToString();
            textBox19.Text = promotion.sellprice.ToString();

            var productinlist = context.ProductStockComStores.Where(p => p.promotionId == id).ToList();
            foreach(var product in productinlist)
            {
                int price = product.price;
                int percent = int.Parse(textBox20.Text);
                double pricePro = price - ((double)price / 100 * (double)percent);
                ListViewItem item = new ListViewItem(product.pId.ToString());
                item.SubItems.Add(product.name);
                item.SubItems.Add(product.price.ToString());
                item.SubItems.Add(percent.ToString());
                item.SubItems.Add(pricePro.ToString());
                listView4.Items.Add(item);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int sum = 0;
            int id = int.Parse(textBox23.Text);
            var result = context.PromotionComStores.Where(pmt => pmt.promotionId == id).First();
            result.name = textBox18.Text;
            result.description = textBox17.Text;
            result.sellpercent = int.Parse(textBox20.Text);
            foreach (ListViewItem item in listView4.Items)
            {
                ProductStockComStore product = new ProductStockComStore();
                Console.WriteLine(item.SubItems[0].Text);
                int idProduct = int.Parse(item.SubItems[0].Text);
                var resultProduct = context.ProductStockComStores.Where(p => p.pId == idProduct).First();
                int price = resultProduct.price;
                int percent = int.Parse(textBox20.Text);
                double pricePro = price - ((double)price / 100 * (double)percent);
                sum = sum + (int)pricePro;
                if (resultProduct.promotionId != id)
                {
                    resultProduct.promotionId = id; 
                }
                context.SaveChanges();
            }
            result.sellprice = sum;
            context.SaveChanges();
            listView5.Items.Clear();
            this.StockEmployeeFrom_Load(sender, e);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox20.Text))
            {
                int SumPrice = 0;
                foreach (ListViewItem item in listView4.Items)
                {
                    int percent = int.Parse(textBox20.Text);
                    item.SubItems[3].Text = percent.ToString();
                    ProductStockComStore product = new ProductStockComStore();
                    int id = int.Parse(item.SubItems[0].Text);
                    int price = int.Parse(item.SubItems[2].Text);
                    double ans = price - ((double)price / 100 * (double)percent);
                    item.SubItems[4].Text = ans.ToString();
                    SumPrice = SumPrice + (int)ans;
                } 
                textBox19.Text = SumPrice.ToString(); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            models.Clear();
            string url = textBox1.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);

            //Type
            HtmlNode type = doc.DocumentNode.SelectSingleNode("//a[@href=\"" + url + "\"]");
            textBox3.Text = type.InnerHtml;

            //page
            //HtmlNode pageNode = doc.DocumentNode.SelectSingleNode("//li[@class='page']");
            HtmlNodeCollection pagenumNode = doc.DocumentNode.SelectNodes("//a[@data-ci-pagination-page]");
            var max = 0;
            if (pagenumNode != null)
            {
                foreach (var i in pagenumNode)
                {
                    var result = Int16.Parse(i.Attributes["data-ci-pagination-page"].Value);
                    if (result > max)
                    {
                        max = result;
                    }
                }
            }
            else
            {
                max = 1;
            }
            textBox2.Text = max.ToString();

            //ListView
            HtmlNodeCollection titleNode = doc.DocumentNode.SelectNodes("//div[@data-qty='1']");
            HtmlNodeCollection descriptionNode = doc.DocumentNode.SelectNodes("//div[@class='row description']");
            HtmlNodeCollection priceNode = doc.DocumentNode.SelectNodes("//p[@class='price_total']");
            HtmlNodeCollection imageNode = doc.DocumentNode.SelectNodes("//img[@class='img-responsive imgpspecial']");
            var n = 0;
            foreach (var i in titleNode)
            {
                datamodel model;
                ListViewItem item = new ListViewItem(i.Attributes["data-id"].Value);
                item.SubItems.Add(i.Attributes["data-name"].Value);
                listView1.Items.Add(item);
                var priceStr = priceNode[n].InnerText;
                var price1 = new string(priceStr.Where(c => char.IsDigit(c)).ToArray());
                //Console.WriteLine(priceStr.Remove(priceStr.Length - 3));
                //Console.WriteLine("https://www.jib.co.th/" + imageNode[n].Attributes["src"].Value);
                //Console.WriteLine(type.InnerText);


                model = new datamodel(
                    int.Parse(i.Attributes["data-id"].Value),
                    i.Attributes["data-name"].Value,
                    descriptionNode[n].InnerText,
                    int.Parse(price1),
                    "https://www.jib.co.th" + imageNode[n].Attributes["src"].Value,
                    textBox3.Text
                );
                models.Add(model);

                Console.WriteLine(models[n].id);
                n++;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            HtmlWeb web = new HtmlWeb();
            if (int.Parse(textBox2.Text) > 0)
            {
                for (int i = 1; i < int.Parse(textBox2.Text); i++)
                {
                    HtmlAgilityPack.HtmlDocument doc = web.Load(url + "/" + i + "00");

                    var n = 0;
                    //ListView
                    HtmlNodeCollection titleNode = doc.DocumentNode.SelectNodes("//div[@data-qty='1']");
                    HtmlNodeCollection descriptionNode = doc.DocumentNode.SelectNodes("//div[@class='row description']");
                    HtmlNodeCollection priceNode = doc.DocumentNode.SelectNodes("//p[@class='price_total']");
                    HtmlNodeCollection imageNode = doc.DocumentNode.SelectNodes("//img[@class='img-responsive imgpspecial']");
                    foreach (var j in titleNode)
                    {
                        datamodel model;
                        ListViewItem item = new ListViewItem(j.Attributes["data-id"].Value);
                        item.SubItems.Add(j.Attributes["data-name"].Value);
                        listView1.Items.Add(item);

                        var priceStr = priceNode[n].InnerText;
                        var price1 = new string(priceStr.Where(c => char.IsDigit(c)).ToArray());

                        model = new datamodel(
                            int.Parse(j.Attributes["data-id"].Value),
                            j.Attributes["data-name"].Value,
                            descriptionNode[n].InnerText,
                            int.Parse(price1),
                            "https://www.jib.co.th" + imageNode[n].Attributes["src"].Value,
                            textBox3.Text
                        );
                        models.Add(model);
                        n++;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var count = models.Count();
            Random random = new Random();
            MessageBox.Show("มีทั้งหมด " + models.Count() + " รายการ");
            HtmlWeb web = new HtmlWeb();
            var index = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                try
                {
                    ProductStockComStore product = new ProductStockComStore();
                    product.pId = int.Parse(item.SubItems[0].Text);
                    product.name = item.SubItems[1].Text;
                    product.description = models[index].description;
                    product.price = models[index].price;
                    product.image = models[index].image;
                    product.type = models[index].type;
                    product.amount = random.Next(501);
                    context.ProductStockComStores.Add(product);
                    context.SaveChanges();
                    Console.WriteLine(product.pId + " " + product.name + " " + product.description + " " + product.price + " " + product.image + " " + product.type + " " + product.amount);
                }
                catch (Exception) { count--; }
                index++;

                /*
                var id = item.SubItems[0].Text;
                var name = item.SubItems[1].Text;
                HtmlAgilityPack.HtmlDocument docPage = web.Load("https://www.jib.co.th/web/product/readProduct/" + id);
                ProductComStoreV2 product = new ProductComStoreV2();

                product.pId = int.Parse(id);
                product.name = name;

                //Description
                HtmlNode DescriptpageNode = docPage.DocumentNode.SelectSingleNode("//meta[@name='description']");
                product.description = DescriptpageNode.Attributes["content"].Value;

                //Price
                HtmlNode PricepageNode = docPage.DocumentNode.SelectSingleNode("//div[@class=" +
                    "'col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block']");
                String price = PricepageNode.InnerText;
                price = new string(price.Where(c => char.IsDigit(c)).ToArray());
                product.price = int.Parse(price);

                //Image
                HtmlNode ImagepageNode = docPage.DocumentNode.SelectSingleNode("//meta[@property='og:image']");
                product.image = ImagepageNode.Attributes["content"].Value;

                product.Type = textBox3.Text;
                product.promotionId = null;

                Console.WriteLine(product.pId + " " + product.name + " " + product.description + " " + product.price + " " + product.image + " " + product.Type);
                context.ProductComStoreV2.Add(product);
                //var change = context.SaveChanges();
                //Console.WriteLine(change);
                context.SaveChanges();
                */
            }
            MessageBox.Show("Success" + count + " list");
            //int change = context.SaveChanges();
            //Console.WriteLine(change);
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            /*
            string url = textBox1.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            var id = (listView1.SelectedItems[0].SubItems[0]).Text;
            HtmlNode idNode = doc.DocumentNode.SelectSingleNode("//div[@data-id="+ id+ "and @data-qty='1']");
            var name = "" + idNode.Attributes["data-name"].Value;

            HtmlNode titleNode = doc.DocumentNode.SelectSingleNode("//a[@title='" + name + "']");
            */

            var id = (listView1.SelectedItems[0].SubItems[0]).Text;
            var name = (listView1.SelectedItems[0].SubItems[1]).Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument docPage = web.Load("https://www.jib.co.th/web/product/readProduct/" + id);

            //id and name
            textBox4.Text = id;
            textBox5.Text = name;

            //Description
            HtmlNode DescriptpageNode = docPage.DocumentNode.SelectSingleNode("//meta[@name='description']");
            textBox6.Text = DescriptpageNode.Attributes["content"].Value;

            //Price
            HtmlNode PricepageNode = docPage.DocumentNode.SelectSingleNode("//div[@class=" +
                "'col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block']");
            String price = PricepageNode.InnerText;
            textBox7.Text = new string(price.Where(c => char.IsDigit(c)).ToArray());

            //Image
            HtmlNode ImagepageNode = docPage.DocumentNode.SelectSingleNode("//meta[@property='og:image']");
            pictureBox1.Image = LoadImage(ImagepageNode.Attributes["content"].Value);
            Console.WriteLine(LoadImage(ImagepageNode.Attributes["content"].Value));
        }
    }
}
