namespace CodeGolf.MagicSquare.Core
{
    public class MagicSquare
    {
        public int Dimension { get; private set; }
        public Cell[,] Cells;

        public MagicSquare(int dimensions)
        {
            Dimension = dimensions;
        }

        public void Generate()
        {
            // Algorithm help from http://user.chollian.net/~brainstm/MagicSquare.htm
            Cells = new Cell[Dimension,Dimension];

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    Cells[i, j] = new Cell(i, j);
                }
            }

            if (Dimension%2 != 0)
            {
                GenerateOdd(Dimension);
            }
            else
            {
                GenerateEven(Dimension);
            }
        }

        private void GenerateOdd(int n)
        {
            //1. Put the first number in the middle column of the top row.
            //2. Put the next number in the box moved one column to the right and one row up.
            //3. If the number exceeds a column or a row, place it in the opposite side of that column or row.
            //4. Repeat step 2 'n' times just before you reach the original starting position.
            //5. Place the next number in the same column one row below the last number and continue with step 2.
            // Basically, you're placing the numbers in consecutive order diagonally up and to the right until all spaces are filled.
            int num = 1;
            int nn = n*3/2;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Cells[(j - i + nn)%n, (i*2 - j + n)%n].Value = num++;
                }
            }
        }


        private void GenerateEven(int n)
        {
            int i, j, num = 1;
            int nminus = n - 1, nmiddle = n/2, nn = n*n + 1;
            int osl = 0;
            var switchRow = new int[2];
            int firstBlock = (n - 2)/4;
            int secondBlock = nminus - firstBlock;
            int firstInside = n/4;
            int secondInside = nminus - firstInside;

            for (j = 0; j < n; j++)
            {
                for (i = 0; i < n; i++)
                {
                    if (i >= firstInside && i <= secondInside && j >= firstInside && j <= secondInside)
                        Cells[i, j].Value = num;
                    else if ((i > firstBlock && i < secondBlock) || (j > firstBlock && j < secondBlock))
                        Cells[i, j].Value = nn - num;
                    else Cells[i, j].Value = num;
                    num++;
                }
            }

            if (n%4 == 0) return;

            switchRow[0] = nmiddle;
            switchRow[1] = 0;
            const int lastSwitchColumn = 0;


            for (i = 0; i < nmiddle; i++)
            {
                if (i == firstBlock || i == secondBlock)
                {
                    osl = 1 - osl;
                    continue;
                }
                Swap(secondBlock, i, secondBlock, nminus - i);
                Swap(i, firstBlock, nminus - i, firstBlock);
                Swap(i, secondBlock, nminus - i, secondBlock);
                Swap(i, switchRow[osl], nminus - i, switchRow[osl]);
            }

            for (i = firstBlock + 1; i < secondBlock; i++)
            {
                Swap(firstBlock, i, secondBlock, i);
                Swap(i, firstBlock, i, secondBlock);
            }

            Swap(firstBlock, nmiddle, secondBlock, nmiddle);
            Swap(lastSwitchColumn, firstBlock, lastSwitchColumn, secondBlock);
        }

        private void Swap(int j1, int i1, int j2, int i2)
        {
            int k = Cells[i1, j1].Value;
            Cells[i1, j1].Value = Cells[i2, j2].Value;
            Cells[i2, j2].Value = k;
        }
    }
}