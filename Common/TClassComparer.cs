﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    ///  泛型Distinct比较
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DistinctComparer<T> : IEqualityComparer<T>
    {
        private PropertyInfo _PropertyInfo;

        /// <summary>
        /// 通过propertyName 获取PropertyInfo对象        
        /// </summary>
        /// <param name="propertyName"></param>
        public DistinctComparer(string propertyName)
        {
            _PropertyInfo = typeof(T).GetProperty(propertyName,
            BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
            if (_PropertyInfo == null)
            {
                throw new ArgumentException(string.Format("{0} is not a property of type {1}.",
                    propertyName, typeof(T)));
            }
        }

        #region IEqualityComparer<T> Members

        public bool Equals(T x, T y)
        {
            object xValue = _PropertyInfo.GetValue(x, null);
            object yValue = _PropertyInfo.GetValue(y, null);
            if (xValue == null)
                return yValue == null;
            return xValue.Equals(yValue);
        }

        public int GetHashCode(T obj)
        {
            object propertyValue = _PropertyInfo.GetValue(obj, null);
            if (propertyValue == null)
                return 0;
            else
                return propertyValue.GetHashCode();
        }

        #endregion
    }

    /// <summary>
    /// 对比
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumerableCompare<T> : IEqualityComparer<T>
    {
        public delegate bool EqualsComparer<A>(T x, T y);
        private EqualsComparer<T> _equalsComparer;
        public EnumerableCompare(EqualsComparer<T> equalsComparer)
        {
            this._equalsComparer = equalsComparer;
        }
        public bool Equals(T x, T y)
        {
            if (null != this._equalsComparer)
                return this._equalsComparer(x, y);
            else
                return false;
        }
        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }


}
