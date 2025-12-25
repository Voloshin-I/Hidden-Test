using System.Collections.Generic;

namespace HOG.Interfaces.Models
{
    public interface IDataProvider<T> where T : IModel
    {
        T[] models { get; }
        IReadOnlyDictionary<string, T> modelsByID { get; }
    }
}