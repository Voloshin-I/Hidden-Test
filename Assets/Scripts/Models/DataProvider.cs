using System.Collections.Generic;
using HOG.Interfaces.Models;

namespace HOG.Models
{
    public class DataProvider<T> : IDataProvider<T> where T : IModel
    {
        public T[] models { get; protected set; }
        public IReadOnlyDictionary<string, T> modelsByID => modelsByIDInternal;
        protected Dictionary<string, T> modelsByIDInternal;
    }
}