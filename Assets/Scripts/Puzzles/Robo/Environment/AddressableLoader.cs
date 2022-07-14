using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Puzzles.Robo.Environment
{

    public interface IInitializable
    {
        event Action Complete;
        bool Initialized { get; }
    }
    public class AddressableListLoader<T> :  IInitializable
    {
        private event Action _complete;
        private string label;

        public event Action Complete
        {
            add => _complete += value;
            remove => _complete -= value;
        }
        public bool Initialized { get; private set; } = false;
        private IList<T> Collection;
        AsyncOperationHandle<IList<T>> task;

        public void LoadAssets(string label, IList<T> collection)
        {
            Collection = collection;
            this.label = label;
            task = Addressables.LoadAssetsAsync<T>(label, null);
            task.Completed += Task_Completed; 
        }

        private void Task_Completed(AsyncOperationHandle<IList<T>> obj)
        {
            
            
            Collection.Clear();
            //task.Completed -= Task_Completed;
            var result = obj.Result;

            if (result == null)
            {
                Debug.LogError($"No result for {label}");
            }
            foreach (var item in result)
            {
                Collection.Add(item);
            }

            Initialized = true;
            _complete?.Invoke();
        }
    }
}
