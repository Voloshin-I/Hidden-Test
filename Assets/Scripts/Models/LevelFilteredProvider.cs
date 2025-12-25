using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HOG.Models
{
    public class LevelFilteredProvider : DataProvider<LevelFilteredDataModel>
    {
        public LevelFilteredProvider(IEnumerable<LevelModel> models)
        {
            this.models = models
                .Where(m => !string.IsNullOrEmpty(m.id))
                .Select(m => new LevelFilteredDataModel(m))
                .ToArray();
            modelsByIDInternal = new Dictionary<string, LevelFilteredDataModel>();
            foreach (LevelFilteredDataModel model in this.models)
            {
                modelsByIDInternal[model.id] = model;
            }
        }
    }
}