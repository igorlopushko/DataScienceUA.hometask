using DataScienceUA.Proxx.Logic.Exceptions;

namespace DataScienceUA.Proxx.Logic
{
    public class Cell
    {
        private CellType _type;
        private CellVisibilityType _visibility;
        private int _value;

        public CellType Type => _type;
        public CellVisibilityType Visibility => _visibility;
        public int Value => _value;

        public Cell(CellType type)
        {
            if (type == CellType.Number)
            {
                throw new NotValidCellTypeException("Number cell type could not be set.");
            }
            
            _type = type;
            _visibility = CellVisibilityType.Closed;
        }

        public void Open()
        {
            _visibility = CellVisibilityType.Opened;

            if (this.Type == CellType.Hole)
            {
                throw new BlackHoleCollisionException();
            }
        }

        public void IncrementValue()
        {
            _value++;
            if (_type == CellType.Empty)
            {
                _type = CellType.Number;
            }
        }
    }
}