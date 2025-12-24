using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace HOG.Installers
{
    internal class RepositoryLoader : MonoBehaviour
    {
        [Serializable]
        public struct LabelTypePair
        {
            public string label;
            public string type;
        }

        public event Action onLoadCompleted;
        
        public string[] addressableLabels = { "default", "level" };
        public GameObject[] objectsToActivateOnLoad;

        private void Start()
        {
            // In case the scene was reloaded
            if (_loaded)
            {
                OnLoadCompleted();
                return;
            }
            
            _loadOperationsRemaining = addressableLabels.Length;
            foreach (string addressableLabel in addressableLabels)
            {
                LoadAllScriptableObjects(addressableLabel);
            }
        }

        private void LoadAllScriptableObjects(string addressableLabel)
        {
            Addressables.LoadResourceLocationsAsync(addressableLabel).Completed += locationsHandle =>
            {
                if (locationsHandle.Status != AsyncOperationStatus.Succeeded ||
                    locationsHandle.Result == null ||
                    locationsHandle.Result.Count == 0)
                {
                    // НИЧЕГО нет по этому лейблу — это не ошибка
                    Debug.Log($"No Addressables found with label '{addressableLabel}'");

                    OnLoad(addressableLabel, Array.Empty<Object>());
                    Addressables.Release(locationsHandle);
                    return;
                }

                Addressables.LoadAssetsAsync<Object>(addressableLabel, null)
                    .Completed += handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        OnLoad(addressableLabel, handle.Result);
                    }
                    else
                    {
                        Debug.LogError($"Failed to load assets of type 'UnityEngine.Object' with label '{addressableLabel}'");
                    }
                    Addressables.Release(handle);
                };
            };
        }

        private void OnLoad(string label, IList<Object> assets)
        {
            _loadedAssets[label] = assets;
            _loadOperationsRemaining--;
            if (_loadOperationsRemaining == 0)
            {
                OnLoadCompleted();
            }
        }

        private void OnLoadCompleted()
        {
            _loaded = true;
            onLoadCompleted?.Invoke();
            foreach (GameObject o in objectsToActivateOnLoad)
            {
                o.gameObject.SetActive(true);
            }
        }

        private Dictionary<string, IList<Object>> _loadedAssets = new();
        private int _loadOperationsRemaining;
        private static bool _loaded = false;
    }
}