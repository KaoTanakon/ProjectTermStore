using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectTermStore
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            APD65_63011212031Entities context = new APD65_63011212031Entities();
            if(textBox1.Text != null || textBox2.Text != null)
            {
                try
                {
                    var result = context.EmployeesComStores.Where(em =>
                                        em.UserName == textBox1.Text &&
                                        em.Password == textBox2.Text &&
                                        em.role == comboBox1.SelectedIndex + 1).First();
                    if (comboBox1.SelectedIndex + 1 == result.role && comboBox1.SelectedIndex == 0)
                    {
                        BossFrom bossF = new BossFrom();
                        bossF.Show();
                        this.Hide();
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    else if (comboBox1.SelectedIndex + 1 == result.role && comboBox1.SelectedIndex == 1)
                    {
                        StockEmployeeFrom stockemployeeF = new StockEmployeeFrom();
                        stockemployeeF.Show();
                        this.Hide();
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    else if (comboBox1.SelectedIndex + 1 == result.role && comboBox1.SelectedIndex == 2)
                    {
                        CounterEmployeeFrom counteremployeeF = new CounterEmployeeFrom();
                        counteremployeeF.Show();
                        this.Hide();
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                }
                catch(Exception ex){ MessageBox.Show("ข้อมูลไม่ถูกต้อง"); }
            }
        }
    }
}
