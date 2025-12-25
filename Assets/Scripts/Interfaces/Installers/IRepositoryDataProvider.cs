using System;
using System.Collections.Generic;

namespace HOG.Installers
{
    public interface IRepositoryDataProvider
    {
        IEnumerable<T> GetAllOfType<T>() where T : UnityEngine.Object;
        IEnumerable<T> GetAllOfType<T>(string label) where T : UnityEngine.Object;
    }
}
