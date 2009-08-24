using System;
using System.Collections.Generic;
using Suricato.Reflection;

namespace Suricato.Collections
{
    public class Sorter<T> : IComparer<T>
    {
        private IComparer<T> _comparer = null;
        private string _expression = string.Empty;
        private IList<T> list;

        #region IComparer<T> Members

        public int Compare(T x, T y) {
            IComparable x_comparable = (IComparable) Reflector.PropertyGet(x, _expression);
            IComparable y_comparable = (IComparable) Reflector.PropertyGet(y, _expression);

            return x_comparable.CompareTo(y_comparable);
        }

        #endregion

        public Sorter<T> By(string expression) {
            _expression = expression;
            return this;
        }

        public IList<T> Sort(IList<T> source) {
            list = source;
            Sort();
            return list;
        }

        public void Sort() {
            List<T> ListToSort = new List<T>(list);

            if (_comparer != null)
                ListToSort.Sort(_comparer);
            else
                ListToSort.Sort(this);

            list = ListToSort;
        }

        public Sorter<T> With(IComparer<T> comparer) {
            if(!string.IsNullOrEmpty(_expression))
                throw new InvalidOperationException(SuricatoStrings.InvalidOperationBy);
            _comparer = comparer;
            return this;
        }
    }
}