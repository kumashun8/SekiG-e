using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SekiG_e
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {           
            new Seat(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(Flg.CheckComp()))
            {
                Seat.RegistrateSeat(Flg.SetFlg(new GetNum(Constance.Size).Num), textBox1.Text);
                textBox1.Text = null;
            }
            else MessageBox.Show("全座席登録完了です!!","Attention!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Flg.CheckComp() || MessageBox.Show("未登録の座席がありますが、現在の席順を保存しますか?", "Attention!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                Seat.Saveseat();         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("現在の席順が全て消去されますが、よろしいですか?", "Attention!", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                for (int i = 1; i <= 42; i++)
                {
                    Flg.ResetFlg(i);
                    Seat.ResetSeat(i);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }
    }
}
