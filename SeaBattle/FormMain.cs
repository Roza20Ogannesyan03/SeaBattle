using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeaBattle
{
    public partial class FormMain : Form
    {

        public int height = 4;
        public int width = 4;
        public int[,] field;
        public int[,] end;
        public FormMain()
        {
            end = new int[height, width];
            field = new int[height, width];
            InitializeComponent();
            LoadField();
            dataGridView1.Rows.Add(height);
            dataGridView2.Rows.Add(6);
            GridField();

        }
        public void GridField()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (field[i, j] == 1)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                    }
                    else if (field[i, j] == 2)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Black;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                }
            }
            dataGridView2.Rows[0].Cells[0].Style.BackColor = Color.Yellow;
            dataGridView2.Rows[0].Cells[1].Style.BackColor = Color.Black;


        }
        private void LoadField()
        {
            StreamReader file1 = new StreamReader("field.txt");
            StreamReader file2 = new StreamReader("EndGame.txt");
            for (int i = 0; i < height; i++)
            {
                string[] s1 = file1.ReadLine().Split();
                string[] s2 = file2.ReadLine().Split();
                for (int j = 0; j < width; j++)
                {
                    field[i, j] = int.Parse(s1[j]);
                    end[i, j] = int.Parse(s2[j]);
                }
            }
            file1.Close();
            file2.Close();
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            var mas = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (field[i, j] == 2)
                    {
                        if (j + 1 < width) mas[i, j + 1] = 2;
                        else MessageBox.Show("Туда нельзя");
                       // break;
                    }
                    else
                    {
                        mas[i, j] = field[i, j];
                    }

                }
            }
            field = mas;
            GridField();
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            var mas = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (field[i, j] == 2)
                    {
                        if (j - 1 >= 0) mas[i, j - 1] = 2;
                        else MessageBox.Show("Туда нельзя");
                    }
                    else
                    {
                        mas[i, j] = field[i, j];
                    }

                }
            }
            field = mas;
            GridField();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (field == end) MessageBox.Show("Поздравляю, вы выиграли!");
            else MessageBox.Show("Вы проиграли");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
