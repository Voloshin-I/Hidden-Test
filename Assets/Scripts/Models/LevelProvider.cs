using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HOG.Models
{
    public class LevelProvider : DataProvider<LevelModel>
    {
        public LevelProvider(IEnumerable<LevelModel> models)
        {
            this.models = models.ToArray();
            modelsByIDInternal = new Dictionary<string, LevelModel>();
            foreach (LevelModel model in models)
            {
                if (string.IsNullOrEmpty(model.id))
                {
                    Debug.LogError($"model id is null or empty");
                    continue;
                }

                modelsByIDInternal[model.id] = model;
            }
        }
    }
}