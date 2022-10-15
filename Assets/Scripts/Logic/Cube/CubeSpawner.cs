using System;
using System.Collections.Generic;
using Logic.Logic.UI;
using StaticData;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Logic.Logic.Cube
{
    public class CubeSpawner: ICubeSpawner
    {
        private const string SpawnPoint = "SpawnPoint";
        
        private readonly Dictionary<Parameters, int> _userValues;
        private readonly GameObject _cubePrefab;
        private int _spawnInterval;
        private int _distance;
        private int _speed;
        
        private GameObject _spawnPoint;
        
        public CubeSpawner(IUISelector uiSelector)
        {
            _cubePrefab = Resources.Load(AssetPath.CubePath).GameObject();
            _userValues = uiSelector.GetDictionary();
            
            uiSelector.SelectAllInputFields += StartSpawnCubs;
        }
        
        private void StartSpawnCubs()
        {
            _spawnPoint = GameObject.FindGameObjectWithTag(SpawnPoint);
            ReadDictionary();
            SpawnCube();

            Observable.Interval(_spawnInterval.sec())
                .Subscribe(x =>
                {
                    SpawnCube();
                });
        }
        
        private void ReadDictionary()
        {
            _userValues.TryGetValue(Parameters.Time, out var value);
            _spawnInterval = value;
            _userValues.TryGetValue(Parameters.Distance, out value);
            _distance = value; 
            _userValues.TryGetValue(Parameters.Speed, out value);
            _speed = value;
        }

        private void SpawnCube()
        {
            var obj = Object.Instantiate(_cubePrefab, _spawnPoint.transform.position, Quaternion.identity,
                _spawnPoint.transform);
            obj.GetComponent<CubeMove>().SetParameters(speed: _speed, distance: _distance);
        }
    }
}