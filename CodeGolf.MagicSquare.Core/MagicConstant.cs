namespace CodeGolf.MagicSquare.Core
{
    public class MagicConstant
    {
        private readonly int _dimension;

        public MagicConstant(int dimension)
        {
            _dimension = dimension;
        }

        public int Constant
        {
            get { return (_dimension * (_dimension * _dimension + 1)) / 2; }
        }
    }
}