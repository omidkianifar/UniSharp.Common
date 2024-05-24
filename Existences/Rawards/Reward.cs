using System;

namespace UniSharp.Common.Existences.Rawards
{
    [Serializable]
    public class Reward
    {
        #region Fields

        private readonly string _name;
        private int _count;

        #endregion

        #region Properties

        public string Name => _name;
        public int Count => _count;

        #endregion

        public Reward(string name, int count)
        {
            _name = name;
            _count = count;
        }

        public void UpdateCount(int count)
        {
            _count = count;
        }
    }
}