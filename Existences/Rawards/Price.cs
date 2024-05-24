using System;

namespace UniSharp.Common.Existences.Rawards
{
    [Serializable]
    public class Price
    {
        #region Fields

        private string _currencyName;
        private double _value;

        #endregion

        #region Properties

        public string CurrencyName => _currencyName;
        public double Value => _value;

        #endregion

        public Price(string currencyName, double value)
        {
            _currencyName = currencyName;
            _value = value;
        }

        public void UpdateValue(double value)
        {
            _value = value;
        }
    }
}