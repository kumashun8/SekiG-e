using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Collections;

namespace SekiG_e
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    //データ登録用配列の設定
    class Flg
    {
        //配列本体
        static int[] flg = new int[Constance.Size];
        static int cnt;

        //重複登録でないかを確認しながら、登録可能な座席を探す
        public static int SetFlg(int x)
        {
            int num_ch;
            cnt++;
            /*(1) 登録済みであるか*/
            if (flg[x] == 1)
            {
                /*(2) 修正登録は初めてか*/
                if (cnt == 1)             
                    num_ch = new GetNum(x).Num; //2回目(最後)の乱数取得                
                else
                {
                    /*(3) (2)以降は1ずつ進んで、未登録の座席(仮)を捜索
                    最後尾まで行くと始めから捜索*/
                    if (x == (Constance.Size - 1)) num_ch = 0;
                    else num_ch = x + 1;
                }
                return SetFlg(num_ch);
            }
            cnt = flg[x] = 1;
            return x;
        }
        //全座席が登録完了か確認
        public static　bool CheckComp()
        {
            foreach (int a in flg)
                if (a == 0) return false;
            return true;
        }
        //登録解除
        public static void ResetFlg(int x)
        {
            flg[x - 1] = 0;
        }
    }

    //乱数を取得
    class GetNum
    {
        public GetNum(int x)
        {
            Random r = new Random();
            Num = r.Next(x);
        }
        public int Num { set; get; }
    }

    //座席(ラベル)の配列化
    class Seat
    {
        static Label[] seatlist; //配列本体
        static string[] backuplist = new string[Constance.Size];
        public string this[int x]
        {
            get { return backuplist[x - 1]; }
            set { backuplist[x] = value; }
        }
        //全てのコントール中からラベルを探し、番号順に配列化
        public Seat(Control hParent)
        {
            seatlist =
                 (from Control p in hParent.Controls
                 where p is Label
                 let num = SetSeat(p)
                 orderby num
                 select (Label)p).ToArray();  
        }
        //ラベルの名前の初期化(label(i)から、単なるiに)
        public static int SetSeat(Control c)
        {
            for (int i = 1; i <= Constance.Size; i++)
                if (c.Name == "label" + i.ToString())
                {
                    c.Name = c.Text = i.ToString();
                    return i;
                }
            return 0;
                    
        }
        public static string GetBackUp(int x)
        {
            return backuplist[x];
        }
        //取得した番号の座席に入力された文字を登録
        public static void RegistrateSeat(int num, string name)
        {
            seatlist[num - 1].Text = seatlist[num - 1].Name + " " + name;
            backuplist[num] = name;
        }
        //取得した番号の座席の登録状態をリセット
        public static void ResetSeat(int x)
        {
            seatlist[x - 1].Text = seatlist[x - 1].Name;
        }
        //現在の席順をテキストファイルとして保存
        public static void Saveseat()
        {
            string filename = "Seat" + DateTime.Now.ToString("MMddHHmm") + "txt";
            StreamWriter writetext = new StreamWriter(filename, false, Encoding.GetEncoding("utf-8"));
            foreach (var i in seatlist)
                writetext.WriteLine(i.Text);
            File.SetAttributes(filename, FileAttributes.ReadOnly);
            MessageBox.Show("現在の席順は\nテキストファイル " + filename + "\nとして保存されました", "Saved!");
            writetext.Close();
        }
    }
    //座席数を設定
    class Constance
    {
        public const int Size = 42;
    }
}

