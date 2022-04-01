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
        public int stepIndex = 1;
        public FormMain()
        {
            InitializeComponent();
        }
        
        public void GridField()
        {

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (field[i, j] == 1)
                    {
                        dgvField.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                    }
                    else if (field[i, j] == 2)
                    {
                        dgvField.Rows[i].Cells[j].Style.BackColor = Color.Black;
                    }
                    else
                    {
                        dgvField.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }

                    // dgvField.ClearSelection();
                }
            }



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

        private void buttonMove_Click(object sender, EventArgs e)
        {
            if (dgvMove.CurrentCell.RowIndex == stepIndex)
            {
                var button = (Button)sender;
                dgvMove.CurrentCell.Value = button.Text;
                //dgvMove.ClearSelection();
            }

        }
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(dgvMove.Rows[stepIndex].Cells[0].Value.ToString()) 
            //    && !string.IsNullOrEmpty(dgvMove.Rows[stepIndex].Cells[1].Value.ToString()))
            if (dgvMove.Rows[stepIndex].Cells[0].Value != null && dgvMove.Rows[stepIndex].Cells[1].Value != null)
            {
                stepIndex++;
                dgvMove.Rows.Add(1);
            }
        }
        
        public bool TryStep(string stepShip, int y, int x, int[,] tempField)
        {
            switch (stepShip)
            {
                case "^":
                    if (y - 1 < 0)
                        return true;
                    else
                        tempField[y - 1, x] = field[y, x];
                    break;
                case "v":
                    if (y + 1 > height - 1)
                        return true;
                    else
                        tempField[y + 1, x] = field[y, x];
                    break;
                case ">":
                    if (x + 1 > width - 1)
                        return true;
                    else
                        tempField[y, x + 1] = field[y, x];
                    break;
                case "<":
                    if (x - 1 < 0)
                        return true;
                    else
                        tempField[y, x - 1] = field[y, x];
                    break;
            }
            return false;
        }
        public bool CheckWin()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (field[i, j] != end[i, j]) return false;

                }
            }
            return true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            bool lose = false;
            for (int i = 1; i < stepIndex; i++)
            {
                lose = false;
                var stepYellow = dgvMove.Rows[i].Cells[0].Value.ToString();
                var stepBlack = dgvMove.Rows[i].Cells[1].Value.ToString();
                var tempField = new int[height, width];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (field[y, x] == 1)
                            lose = TryStep(stepYellow, y, x, tempField);
                        else if(field[y,x] == 2)
                            lose = TryStep(stepBlack, y, x, tempField);
                                             
                        if (lose)
                            goto finish;

                    }
                }
                field = tempField;
                GridField();
            }

            finish:
            if (!lose && CheckWin()) MessageBox.Show("Поздравляю, вы выиграли!");
            else MessageBox.Show("Вы проиграли");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            end = new int[height, width];
            field = new int[height, width];
            LoadField();
            dgvField.Rows.Add(height);
            dgvMove.Rows.Add(2);
            dgvMove.Rows[0].Cells[0].Style.BackColor = Color.Yellow;
            dgvMove.Rows[0].Cells[1].Style.BackColor = Color.Black;
            dgvMove.ClearSelection();
            dgvField.ClearSelection();
            dgvMove.Rows[0].Cells[0].Selected = false;
            //dgvMove.Rows[0].Cells[0].Frozen = true;
            //dgvMove.Rows[0].Cells[0].Frozen 
            dgvField.Enabled = false;

            GridField();

        }


    }
}
