using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Math;

namespace demineur
{
    public partial class Form1 : Form
    {
        private const int btn_size = 30;
        private const int grille_size = 10;
        private int currentBoard = grille_size;
        private int numBomb = 0;
        private int[,] matriceBoard;
        private int[,] tempMatriceBoard;
        private int nbBtn;
        private int nbBtnVisible;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initNewGame();
        }

        private void initNewGame()
        {
            tlpBoard.Size = new Size(currentBoard * btn_size, currentBoard * btn_size);
            tlpBoard.RowCount = currentBoard;
            tlpBoard.ColumnCount = currentBoard;
            tlpBoard.RowStyles.Clear();
            tlpBoard.ColumnStyles.Clear();
            nbBtn = currentBoard * currentBoard;
            nbBtnVisible = nbBtn;

            Button NewGame = new Button();
            NewGame.Text = "Nouvelle Partie";
            NewGame.Click += new EventHandler(NewGame_click);
            NewGame.Size = new Size(currentBoard / 3, btn_size);
            NewGame.Location = new Point(0, 0);
            

            initMatrice();

            for (int i = 0; i < currentBoard; i++)
            {
                tlpBoard.RowStyles.Add(new RowStyle(SizeType.Absolute, btn_size));
                tlpBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, btn_size));

                for (int j = 0; j < currentBoard; j++)
                {
                    Panel p = new Panel();
                    Button b = new Button();
                    Label l = new Label();

                    l.Size = b.Size = new Size(btn_size, btn_size);

                    if (matriceBoard[i, j] != 0)
                    {
                        l.Text = matriceBoard[i, j].ToString();
                    }
                    else
                    {
                        l.Text = "";
                    }

                    
                    l.TextAlign = ContentAlignment.MiddleCenter;

                    p.Controls.Add(l);
                    p.Controls.Add(b);
                    p.Margin = new Padding(0);
                    p.BorderStyle = BorderStyle.None;
                    l.SendToBack();

                    b.Click += new EventHandler(clickButton);

                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderSize = 0;

                    Couleur(b, l, i, j);

                    

                    tlpBoard.Controls.Add(p, i, j);


                }
            }
            Size = new Size((currentBoard * (btn_size + 2)), (currentBoard * (btn_size + 2)) + 100);
        }
        private void clickButton(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Panel p = (Panel)b.Parent;
            TableLayoutPanelCellPosition pos = tlpBoard.GetCellPosition(p);
            Label tempLabel = (Label)tlpBoard.GetControlFromPosition(pos.Column, pos.Row).Controls[1];

            verifieCellule(pos.Column, pos.Row);
        }

        private void NewGame_click(object sender, EventArgs e)
        {
            initNewGame();
        }

        private void initMatrice()
        {
            Random rand = new Random();
            numBomb = 10;
            matriceBoard = new int[currentBoard, currentBoard];
            tempMatriceBoard = new int[currentBoard, currentBoard];

            for (int i = 0; i < currentBoard; i++)
            {
                for (int j = 0; j < currentBoard; j++)
                {
                    matriceBoard[i, j] = 0;
                }
            }

            for (int l = 0; l < numBomb; l++)
            {
                int y = rand.Next(0, currentBoard - 1);
                int x = rand.Next(0, currentBoard - 1);

                while (matriceBoard[x, y] == -1)
                {
                    y = rand.Next(0, currentBoard - 1);
                    x = rand.Next(0, currentBoard - 1);

                }
                matriceBoard[x, y] = -1;
                tempMatriceBoard[x, y] = -1;

                if (x < 1)
                {
                    if (y < 1)
                    {
                        matriceBoard[x, y + 1] += 1;
                        matriceBoard[x + 1, y] += 1;
                        matriceBoard[x + 1, y + 1] += 1;
                    }
                    else if (y == currentBoard - 1)
                    {
                        matriceBoard[x, y - 1] += 1;
                        matriceBoard[x + 1, y - 1] += 1;
                        matriceBoard[x + 1, y] += 1;
                    }
                    else
                    {
                        matriceBoard[x, y - 1] += 1;
                        matriceBoard[x + 1, y - 1] += 1;
                        matriceBoard[x, y + 1] += 1;
                        matriceBoard[x + 1, y] += 1;
                        matriceBoard[x + 1, y + 1] += 1;
                    }
                }

                else if (x == currentBoard - 1)
                {
                    if (y < 1)
                    {
                        matriceBoard[x - 1, y] += 1;
                        matriceBoard[x - 1, y + 1] += 1;
                        matriceBoard[x, y + 1] += 1;
                    }
                    else if (y == currentBoard - 1)
                    {
                        matriceBoard[x - 1, y] += 1;
                        matriceBoard[x - 1, y - 1] += 1;
                        matriceBoard[x, y - 1] += 1;
                    }
                    else
                    {
                        matriceBoard[x - 1, y] += 1;
                        matriceBoard[x - 1, y + 1] += 1;
                        matriceBoard[x, y + 1] += 1;
                        matriceBoard[x - 1, y - 1] += 1;
                        matriceBoard[x, y - 1] += 1;
                    }
                }
                else if (x != currentBoard - 1)
                {
                    if (y == currentBoard - 1)
                    {
                        matriceBoard[x - 1, y - 1] += 1;
                        matriceBoard[x - 1, y] += 1;
                        matriceBoard[x, y - 1] += 1;
                        matriceBoard[x + 1, y - 1] += 1;
                        matriceBoard[x + 1, y] += 1;
                    }
                    else if (y < 1)
                    {
                        matriceBoard[x - 1, y] += 1;
                        matriceBoard[x - 1, y + 1] += 1;
                        matriceBoard[x, y + 1] += 1;
                        matriceBoard[x + 1, y + 1] += 1;
                        matriceBoard[x + 1, y] += 1;
                    }
                    else
                    {
                        matriceBoard[x - 1, y - 1] += 1;
                        matriceBoard[x - 1, y] += 1;
                        matriceBoard[x - 1, y + 1] += 1;
                        matriceBoard[x, y - 1] += 1;
                        matriceBoard[x, y + 1] += 1;
                        matriceBoard[x + 1, y - 1] += 1;
                        matriceBoard[x + 1, y] += 1;
                        matriceBoard[x + 1, y + 1] += 1;
                    }

                }
                for (int i = 0; i < currentBoard; i++)
                {
                    for (int j = 0; j < currentBoard; j++)
                    {
                        if (tempMatriceBoard[i, j] == -1)
                        {
                            matriceBoard[i, j] = -1;
                        }

                    }
                }
            }
        }

        private void Couleur(Button b, Label l, int i, int j)
        {
            if (i % 2 == 0)
            {
                if (j % 2 != 0)
                {
                    b.BackColor = Color.Pink;
                    l.BackColor = Color.White;
                }
                else
                {
                    b.BackColor = Color.Plum;
                    l.BackColor = Color.Lavender;
                }

            }
            else
            {
                if (j % 2 != 0)
                {
                    b.BackColor = Color.Plum;
                    l.BackColor = Color.Lavender;
                }
                else
                {
                    b.BackColor = Color.Pink;
                    l.BackColor = Color.White;
                }

            }
            if (matriceBoard[i, j] == -1)
            {
                l.BackColor = Color.Red;
            }
        }

        private void ResetMatrice()
        {
            nbBtn = currentBoard * currentBoard;
            nbBtnVisible = nbBtn;
            for (int i = 0; i < currentBoard; i++)
            {
                for (int j = 0; j < currentBoard; j++)
                {
                    Button tempButton = (Button)tlpBoard.GetControlFromPosition(i, j).Controls[0];
                    Label tempLabel = (Label)tlpBoard.GetControlFromPosition(i, j).Controls[1];
                    tempButton.Visible = true;
                    matriceBoard[i, j] = 0;
                    tempMatriceBoard[i, j] = 0;
                    Couleur(tempButton, tempLabel, i, j);
                }
            }
        }
        private void NewGame()
        {
            ResetMatrice();
            initMatrice();

            for (int i = 0; i < currentBoard; i++)
            {
                for (int j = 0; j < currentBoard; j++)
                {
                    Label tempLabel = (Label)tlpBoard.GetControlFromPosition(i, j).Controls[1];
                    if (matriceBoard[i, j] != 0)
                    {
                        tempLabel.Text = matriceBoard[i, j].ToString();
                    }
                    else
                    {
                        tempLabel.Text = "";
                    }
                }
            }
        }

        private void verifieCellule(int column, int row)
        {
            Button b = (Button)tlpBoard.GetControlFromPosition(column, row).Controls[0];

            if (b.Visible)
            {
                b.Visible = false;
                nbBtnVisible -= 1;
                if(matriceBoard[column, row] == -1)
                {
                    for (int i = 0; i < currentBoard; i++)
                    {
                        for (int j = 0; j < currentBoard; j++)
                        {
                            Label tempLabel = (Label)tlpBoard.GetControlFromPosition(i, j).Controls[1];
                            Button tempButton = (Button)tlpBoard.GetControlFromPosition(i, j).Controls[0];
                            tempButton.Visible = false;
                            if (matriceBoard[i, j] == -1)
                            {
                                tempLabel.BackColor = Color.Red;
                            }
                        }
                    }

                    MessageBox.Show("Tu as perdu !");

                    NewGame();
                    
                }
                else if(nbBtnVisible == numBomb)
                {
                    for (int i = 0; i < currentBoard; i++)
                    {
                        for (int j = 0; j < currentBoard; j++)
                        {
                            Label tempLabel = (Label)tlpBoard.GetControlFromPosition(i, j).Controls[1];
                            Button tempButton = (Button)tlpBoard.GetControlFromPosition(i, j).Controls[0];
                            if(tempButton.Visible)
                            {
                                tempButton.Visible = false;
                                tempLabel.BackColor = Color.Green;
                            }
                        }
                    }

                    MessageBox.Show("Tu as gagné !");

                    NewGame();

                }
                else
                {
                    if(matriceBoard[column, row] == 0)
                    {

                        for(int i = Max(0, column - 1); i < Min(currentBoard, column + 2); i++)
                        {
                            for (int j = Max(0, row - 1); j < Min(currentBoard, row + 2); j++)
                            {
                                verifieCellule(i, j);
                            }
                        }
                    }
                }
            }
        }
    }
}
