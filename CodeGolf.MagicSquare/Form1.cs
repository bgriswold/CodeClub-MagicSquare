using System.Drawing;
using System.Windows.Forms;
using CodeGolf.MagicSquare.Core;

namespace CodeGolf.MagicSquare
{
    public partial class Form1 : Form
    {
        private const int MagicSquare = 15; // <- Set the Magic Square Value
        private const int CellSize = 25;
        private const int CellPadding = 5;
        private readonly Core.MagicSquare _square;

        public Form1()
        {
            InitializeComponent();

            SetBounds(Left, Top, (MagicSquare + 2)*CellSize + CellPadding, (MagicSquare + 5)*CellSize + CellPadding);

            Cursor = Cursors.WaitCursor;

            _square = new Core.MagicSquare(MagicSquare);
            _square.Generate();

            Cursor = Cursors.Arrow;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            DrawMazeBackground(g);
            DrawMaze(g);
        }

        private void DrawMazeBackground(Graphics g)
        {
            g.FillRectangle(Brushes.White, ClientRectangle);
        }

        public void DrawMaze(Graphics g)
        {
            for (int i = 0; i < _square.Dimension; i++)
            {
                for (int j = 0; j < _square.Dimension; j++)
                {
                    DrawCell(i, j, g);
                }
            }
        }

        public void DrawCell(int i, int j, Graphics g, bool drawSolution = false)
        {
            Cell cell = _square.Cells[i, j];

            var format = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};
            var font = new Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point);
            var rectangle = new Rectangle(cell.Row*CellSize + CellPadding + 1, cell.Column*CellSize + CellPadding + 1,
                                          CellSize - 1, CellSize - 1);

            g.DrawString(cell.Value.ToString(), font, Brushes.Blue, rectangle, format);

            var fillPen = new Pen(Color.Blue);
            DrawWallNorth(cell, g, fillPen);
            DrawWallEast(cell, g, fillPen);
            DrawWallSouth(cell, g, fillPen);
            DrawWallWest(cell, g, fillPen);
        }


        private static void DrawWallWest(Cell cell, Graphics g, Pen fillPen)
        {
            g.DrawLine(fillPen, (cell.Row + 1)*CellSize + CellPadding, cell.Column*CellSize + CellPadding,
                       (cell.Row + 1)*CellSize + CellPadding, (cell.Column + 1)*CellSize + CellPadding);
        }

        private static void DrawWallSouth(Cell cell, Graphics g, Pen fillPen)
        {
            g.DrawLine(fillPen, cell.Row*CellSize + CellPadding, (cell.Column + 1)*CellSize + CellPadding,
                       (cell.Row + 1)*CellSize + CellPadding, (cell.Column + 1)*CellSize + CellPadding);
        }

        private static void DrawWallEast(Cell cell, Graphics g, Pen fillPen)
        {
            g.DrawLine(fillPen, cell.Row*CellSize + CellPadding, cell.Column*CellSize + CellPadding,
                       cell.Row*CellSize + CellPadding,
                       (cell.Column + 1)*CellSize + CellPadding);
        }

        private static void DrawWallNorth(Cell cell, Graphics g, Pen fillPen)
        {
            g.DrawLine(fillPen, cell.Row*CellSize + CellPadding, cell.Column*CellSize + CellPadding,
                       (cell.Row + 1)*CellSize + CellPadding, cell.Column*CellSize + +CellPadding);
        }
    }
}