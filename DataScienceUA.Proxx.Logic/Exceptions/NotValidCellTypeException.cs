using System;

namespace DataScienceUA.Proxx.Logic.Exceptions
{
    public class NotValidCellTypeException : Exception
    {
        public NotValidCellTypeException(string message) : base(message) { }
    }
}