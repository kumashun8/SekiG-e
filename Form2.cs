using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SekiG_e
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label2.Text = "解除する座席番号を半角で入力して下さい\n(複数指定する場合は間に半角スペース)";
                textBox3.Visible = true;
                textBox1.Visible = textBox2.Visible = label3.Visible =　false;
            }
            else
            {
                label2.Text = "登録状態を交換したい座席番号を\n半角で2つ入力して下さい";
                textBox3.Visible = false;
                textBox1.Visible = textBox2.Visible =  label3.Visible = true;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != null && textBox2.Text != null) || textBox3.Text != null)
            {
                if (textBox3.Visible)
                {
                    int[] deleteseat = textBox3.Text.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
                    foreach (int i in deleteseat)
                        Seat.ResetSeat(i);
                    textBox3.Text = null;
                }
                else
                {
                    int x = int.Parse(textBox1.Text);
                    int y = int.Parse(textBox2.Text);
                    string X = Seat.GetBackUp(x);
                    string Y = Seat.GetBackUp(y);

                    Seat.RegistrateSeat(x, Y);
                    Seat.RegistrateSeat(y, X);
                    textBox1.Text = textBox2.Text = null;
                }
                MessageBox.Show("完了しました!", "Clear!");
                this.Close();
            }
            else
            {
                MessageBox.Show("座席番号を入力して下さい", "Error");
            }
            
        }
    }
}
