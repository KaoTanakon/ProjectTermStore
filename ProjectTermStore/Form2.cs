using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectTermStore
{
    public partial class BossFrom : Form
    {
        APD65_63011212031Entities context = new APD65_63011212031Entities();
        public BossFrom()
        {
            InitializeComponent();
        }

        private void BossFrom_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aPD65_63011212031DataSet.EmployeesComStore' table. You can move, or remove it, as needed.
            var employees = context.EmployeesComStores.ToList();
            textBox1.Text = employees.Count.ToString();
            foreach(var employee in employees)
            {
                String role;
                if(employee.role == 1)
                {
                    role = "เจ้าของร้าน";
                }else if(employee.role == 2)
                {
                    role = "พนักงานคลังสินค้า";
                }else if(employee.role == 3)
                {
                    role = "พนักงานขายหน้าร้าน";
                }else
                {
                    role = null;
                }
                ListViewItem item = new ListViewItem(employee.eId.ToString());
                item.SubItems.Add(employee.FirstName);
                item.SubItems.Add(employee.LastName);
                item.SubItems.Add(employee.phone);
                item.SubItems.Add(role);
                listView1.Items.Add(item);
            }
            //
            var customers = context.CustomerComStores.ToList();
            textBox10.Text = customers.Count.ToString();
            foreach(var customer in customers)
            {
                ListViewItem item = new ListViewItem(customer.cId.ToString());
                item.SubItems.Add(customer.FirstName);
                item.SubItems.Add(customer.LastName);
                item.SubItems.Add(customer.phone);
                listView2.Items.Add(item);
            }
            //
            comboBox2.Items.Add("สินค้าทั้งหมด");
            var products = context.ProductStockComStores.ToList();
            textBox6.Text = products.Count.ToString();
            foreach (var product in products)
            {
                ListViewItem item = new ListViewItem(product.pId.ToString());
                item.SubItems.Add(product.name);
                item.SubItems.Add(product.type);
                item.SubItems.Add(product.amount.ToString());
                listView3.Items.Add(item);
                if(comboBox2.FindString(product.type.ToString()) < 0)
                {
                    comboBox2.Items.Add(product.type);
                }
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void EmployeeDataTab_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm LoginF = new LoginForm();
            this.Close();
            LoginF.Show();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int roleInt;
            if (textBox5.Text == "เจ้าของร้าน")
            {
                roleInt = 1;
            }
            else if (textBox5.Text == "พนักงานคลังสินค้า")
            {
                roleInt = 2;
            }
            else if (textBox5.Text == "พนักงานขายหน้าร้าน")
            {
                roleInt = 3;
            }
            else
            {
                roleInt = 0;
            }
            EmployeesComStore employee = new EmployeesComStore();
            employee.UserName = textBox21.Text;
            employee.Password = textBox22.Text;
            employee.FirstName = textBox3.Text;
            employee.LastName = textBox20.Text;
            employee.phone = textBox4.Text;
            employee.role = roleInt;

            context.EmployeesComStores.Add(employee);
            context.SaveChanges();
            listView1.Items.Clear();
            this.BossFrom_Load(sender, e);
            /*
            var result = context.EmployeesComStores.Where(emp => emp.UserName == textBox21.Text).First();
            String role;
            if (employee.role == 1)
            {
                role = "เจ้าของร้าน";
            }
            else if (employee.role == 2)
            {
                role = "พนักงานคลังสินค้า";
            }
            else if (employee.role == 3)
            {
                role = "พนักงานขายหน้าร้าน";
            }
            else
            {
                role = null;
            }
            ListViewItem item = new ListViewItem(result.eId.ToString());
            item.SubItems.Add(result.FirstName);
            item.SubItems.Add(result.LastName);
            item.SubItems.Add(result.phone);
            item.SubItems.Add(role);
            listView1.Items.Add(item);
            textBox1.Text = (int.Parse(textBox1.Text) + 1).ToString();
            */
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int roleInt;
            if (textBox5.Text == "เจ้าของร้าน")
            {
                roleInt = 1;
            }
            else if (textBox5.Text == "พนักงานคลังสินค้า")
            {
                roleInt = 2;
            }
            else if (textBox5.Text == "พนักงานขายหน้าร้าน")
            {
                roleInt = 3;
            }
            else
            {
                roleInt = 0;
            }
            int id = int.Parse(textBox2.Text);
            var result = context.EmployeesComStores.Where(emp => emp.eId == id).First();
            result.FirstName = textBox3.Text;
            result.LastName = textBox20.Text;
            result.phone = textBox4.Text;
            result.role = roleInt;
            var change = context.SaveChanges();
            MessageBox.Show("Success "+change+" row");
            listView1.Items.Clear();
            this.BossFrom_Load(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void employeesComStoreBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseClick_1(object sender, MouseEventArgs e)
        {
            textBox2.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox20.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[4].Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox2.Text);
            var result = context.EmployeesComStores.Where(emp => emp.eId == id).First();
            context.EmployeesComStores.Remove(result);
            context.SaveChanges();

            listView1.Items.Remove(listView1.SelectedItems[0]);
            listView1.Items.Clear();
            this.BossFrom_Load(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox20.Text = null;
            textBox21.Text = null;
            textBox22.Text = null;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            var employees = context.EmployeesComStores.Where(emp => emp.role == comboBox1.SelectedIndex + 1).ToList();
            textBox1.Text = employees.Count.ToString();
            foreach (var employee in employees)
            {
                String role;
                if (employee.role == 1)
                {
                    role = "เจ้าของร้าน";
                }
                else if (employee.role == 2)
                {
                    role = "พนักงานคลังสินค้า";
                }
                else if (employee.role == 3)
                {
                    role = "พนักงานขายหน้าร้าน";
                }
                else
                {
                    role = null;
                }
                ListViewItem item = new ListViewItem(employee.eId.ToString());
                item.SubItems.Add(employee.FirstName);
                item.SubItems.Add(employee.LastName);
                item.SubItems.Add(employee.phone);
                item.SubItems.Add(role);
                listView1.Items.Add(item);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int id = 0;
            if(!String.IsNullOrEmpty(textBox2.Text))
            {
                Console.WriteLine("0o0");
                id = int.Parse(textBox2.Text);
            }
            List<EmployeesComStore> employees;
            listView1.Items.Clear();
            if (!String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox3.Text) && !String.IsNullOrEmpty(textBox4.Text))
            {
                employees = context.EmployeesComStores.Where(emp => emp.eId == id && emp.FirstName.Contains(textBox3.Text) && emp.phone.Contains(textBox4.Text)).ToList();
                textBox1.Text = employees.Count.ToString();
                foreach (var employee in employees)
                {
                    String role;
                    if (employee.role == 1)
                    {
                        role = "เจ้าของร้าน";
                    }
                    else if (employee.role == 2)
                    {
                        role = "พนักงานคลังสินค้า";
                    }
                    else if (employee.role == 3)
                    {
                        role = "พนักงานขายหน้าร้าน";
                    }
                    else
                    {
                        role = null;
                    }
                    ListViewItem item = new ListViewItem(employee.eId.ToString());
                    item.SubItems.Add(employee.FirstName);
                    item.SubItems.Add(employee.LastName);
                    item.SubItems.Add(employee.phone);
                    item.SubItems.Add(role);
                    listView1.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox3.Text))
            {
                employees = context.EmployeesComStores.Where(emp => emp.eId == id && emp.FirstName.Contains(textBox3.Text)).ToList();
                textBox1.Text = employees.Count.ToString();
                foreach (var employee in employees)
                {
                    String role;
                    if (employee.role == 1)
                    {
                        role = "เจ้าของร้าน";
                    }
                    else if (employee.role == 2)
                    {
                        role = "พนักงานคลังสินค้า";
                    }
                    else if (employee.role == 3)
                    {
                        role = "พนักงานขายหน้าร้าน";
                    }
                    else
                    {
                        role = null;
                    }
                    ListViewItem item = new ListViewItem(employee.eId.ToString());
                    item.SubItems.Add(employee.FirstName);
                    item.SubItems.Add(employee.LastName);
                    item.SubItems.Add(employee.phone);
                    item.SubItems.Add(role);
                    listView1.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox4.Text))
            {
                employees = context.EmployeesComStores.Where(emp => emp.eId == id && emp.phone.Contains(textBox4.Text)).ToList();
                textBox1.Text = employees.Count.ToString();
                foreach (var employee in employees)
                {
                    String role;
                    if (employee.role == 1)
                    {
                        role = "เจ้าของร้าน";
                    }
                    else if (employee.role == 2)
                    {
                        role = "พนักงานคลังสินค้า";
                    }
                    else if (employee.role == 3)
                    {
                        role = "พนักงานขายหน้าร้าน";
                    }
                    else
                    {
                        role = null;
                    }
                    ListViewItem item = new ListViewItem(employee.eId.ToString());
                    item.SubItems.Add(employee.FirstName);
                    item.SubItems.Add(employee.LastName);
                    item.SubItems.Add(employee.phone);
                    item.SubItems.Add(role);
                    listView1.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox3.Text) && !String.IsNullOrEmpty(textBox4.Text))
            {
                employees = context.EmployeesComStores.Where(emp => emp.FirstName.Contains(textBox3.Text) && emp.phone.Contains(textBox4.Text)).ToList();
                textBox1.Text = employees.Count.ToString();
                foreach (var employee in employees)
                {
                    String role;
                    if (employee.role == 1)
                    {
                        role = "เจ้าของร้าน";
                    }
                    else if (employee.role == 2)
                    {
                        role = "พนักงานคลังสินค้า";
                    }
                    else if (employee.role == 3)
                    {
                        role = "พนักงานขายหน้าร้าน";
                    }
                    else
                    {
                        role = null;
                    }
                    ListViewItem item = new ListViewItem(employee.eId.ToString());
                    item.SubItems.Add(employee.FirstName);
                    item.SubItems.Add(employee.LastName);
                    item.SubItems.Add(employee.phone);
                    item.SubItems.Add(role);
                    listView1.Items.Add(item);
                }
            }
            else if(!String.IsNullOrEmpty(textBox2.Text))
            {
                employees = context.EmployeesComStores.Where(emp => emp.eId == id).ToList();
                textBox1.Text = employees.Count.ToString();
                foreach (var employee in employees)
                {
                    String role;
                    if (employee.role == 1)
                    {
                        role = "เจ้าของร้าน";
                    }
                    else if (employee.role == 2)
                    {
                        role = "พนักงานคลังสินค้า";
                    }
                    else if (employee.role == 3)
                    {
                        role = "พนักงานขายหน้าร้าน";
                    }
                    else
                    {
                        role = null;
                    }
                    ListViewItem item = new ListViewItem(employee.eId.ToString());
                    item.SubItems.Add(employee.FirstName);
                    item.SubItems.Add(employee.LastName);
                    item.SubItems.Add(employee.phone);
                    item.SubItems.Add(role);
                    listView1.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox3.Text))
            {
                employees = context.EmployeesComStores.Where(emp => emp.FirstName.Contains(textBox3.Text)).ToList();
                textBox1.Text = employees.Count.ToString();
                foreach (var employee in employees)
                {
                    String role;
                    if (employee.role == 1)
                    {
                        role = "เจ้าของร้าน";
                    }
                    else if (employee.role == 2)
                    {
                        role = "พนักงานคลังสินค้า";
                    }
                    else if (employee.role == 3)
                    {
                        role = "พนักงานขายหน้าร้าน";
                    }
                    else
                    {
                        role = null;
                    }
                    ListViewItem item = new ListViewItem(employee.eId.ToString());
                    item.SubItems.Add(employee.FirstName);
                    item.SubItems.Add(employee.LastName);
                    item.SubItems.Add(employee.phone);
                    item.SubItems.Add(role);
                    listView1.Items.Add(item);
                }
            }
            else if (!String.IsNullOrEmpty(textBox4.Text))
            {
                employees = context.EmployeesComStores.Where(emp => emp.phone.Contains(textBox4.Text)).ToList();
                textBox1.Text = employees.Count.ToString();
                foreach (var employee in employees)
                {
                    String role;
                    if (employee.role == 1)
                    {
                        role = "เจ้าของร้าน";
                    }
                    else if (employee.role == 2)
                    {
                        role = "พนักงานคลังสินค้า";
                    }
                    else if (employee.role == 3)
                    {
                        role = "พนักงานขายหน้าร้าน";
                    }
                    else
                    {
                        role = null;
                    }
                    ListViewItem item = new ListViewItem(employee.eId.ToString());
                    item.SubItems.Add(employee.FirstName);
                    item.SubItems.Add(employee.LastName);
                    item.SubItems.Add(employee.phone);
                    item.SubItems.Add(role);
                    listView1.Items.Add(item);
                }
            }
            else
            {
                listView1.Items.Clear();
                this.BossFrom_Load(sender, e);
                MessageBox.Show("Not Found");
            }
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox9.Text = listView2.SelectedItems[0].SubItems[0].Text;
            textBox8.Text = listView2.SelectedItems[0].SubItems[1].Text;
            textBox23.Text = listView2.SelectedItems[0].SubItems[2].Text;
            textBox7.Text = listView2.SelectedItems[0].SubItems[3].Text;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //////////////////////////
            CustomerComStore customer = new CustomerComStore();
            customer.FirstName = textBox8.Text;
            customer.LastName = textBox23.Text;
            customer.phone = textBox7.Text;

            context.CustomerComStores.Add(customer);
            context.SaveChanges();
            listView2.Items.Clear();
            this.BossFrom_Load(sender, e);
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
            this.BossFrom_Load(sender, e);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox9.Text);
            var result = context.CustomerComStores.Where(c => c.cId == id).First();
            context.CustomerComStores.Remove(result);
            context.SaveChanges();

            listView2.Items.Remove(listView2.SelectedItems[0]);
            this.BossFrom_Load(sender, e);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox9.Text = null;
            textBox8.Text = null;
            textBox23.Text = null;
            textBox7.Text = null;
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
            else if(!String.IsNullOrEmpty(textBox9.Text) && !String.IsNullOrEmpty(textBox7.Text))
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
            else if(!String.IsNullOrEmpty(textBox8.Text) && !String.IsNullOrEmpty(textBox7.Text))
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
                this.BossFrom_Load(sender, e);
                MessageBox.Show("Not Found");
            }
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

        private void button16_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            int id = 0;
            List<CustomerComStore> customers;
            listView2.Items.Clear();
            if (!String.IsNullOrEmpty(textBox14.Text))
            {
                Console.WriteLine("0o0");
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
            }else if(!String.IsNullOrEmpty(textBox14.Text) && !String.IsNullOrEmpty(textBox13.Text))
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
            else if(!String.IsNullOrEmpty(textBox14.Text) && !String.IsNullOrEmpty(textBox12.Text))
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
            else if(!String.IsNullOrEmpty(textBox13.Text) && !String.IsNullOrEmpty(textBox12.Text))
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
                this.BossFrom_Load(sender, e);
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
            if (comboBox2.SelectedIndex > 0)
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
    }
}
