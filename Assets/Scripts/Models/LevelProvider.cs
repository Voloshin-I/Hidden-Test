using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HOG.Models
{
    public class LevelProvider : DataProvider<LevelModel>
    {
        public LevelProvider(IEnumerable<LevelModel> models)
        {
            this.models = models.Where(m => !string.IsNullOrEmpty(m.id)).ToArray();
            modelsByIDInternal = new Dictionary<string, LevelModel>();
            foreach (LevelModel model in models)
            {
                modelsByIDInternal[model.id] = model;
            }
        }
    }
}