namespace CodeGolf.MagicSquare.Core
{
    public class Cell
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public int Value { get; set; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            Value = 0;
        }
    }
}