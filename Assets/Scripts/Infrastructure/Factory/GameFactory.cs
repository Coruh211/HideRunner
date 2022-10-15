using Infrastructure.AssetManagement;
using Unity.VisualScripting;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private ScriptableObject _scriptableObject;
        public GameFactory(IAssetProvider assetProvider)
        {
            _assets = assetProvider;
        }
        public GameObject CreateObject(string path)
        {
            return _assets.Instantiate(path);
        }

        public GameObject CreateObject(GameObject gameObject)
        {
            return _assets.Instantiate(gameObject);
        }
       
    }
}