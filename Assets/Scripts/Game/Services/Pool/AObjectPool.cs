using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Game.Services.Pool
{
    public abstract class AObjectPool <TType, TObject> : IPool<TType, TObject>
    where TType : unmanaged
    where TObject : MonoBehaviour
    {
        private readonly IObjectResolver _container;

        private Dictionary<TType, ObjectPool<TObject>> _pools;
        
        protected abstract IReadOnlyDictionary<TType, IReadOnlyList<TObject>> PoolObjectVariants { get; }

        protected virtual int MaxPoolSize => 10;

        protected AObjectPool(IObjectResolver container)
        {
            _container = container;
        }

        protected void CreateSubPools()
        {
            _pools = new Dictionary<TType, ObjectPool<TObject>>();
            
            foreach (var objectVariantPair in PoolObjectVariants)
            {
                foreach (var prefab in objectVariantPair.Value)
                {
                    var pool = new ObjectPool<TObject>(
                        () => _container.Instantiate(prefab).GetComponent<TObject>(),
                        view =>
                        {
                            view.gameObject.SetActive(true);
                        },
                        view =>
                        {
                            view.gameObject.SetActive(false);
                        },
                        view =>
                        {
                            Object.Destroy(view.gameObject);
                        }, false, 3, MaxPoolSize);

                    _pools[objectVariantPair.Key] = pool;
                }
            }
        }

        public TObject SpawnObject(TType type)
        {
            return _pools[type].Get();
        }

        public void DespawnObject(TType type, TObject objectView)
        {
            _pools[type].Release(objectView);
        }
    }
}