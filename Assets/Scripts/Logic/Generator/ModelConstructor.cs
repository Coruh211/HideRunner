using System.Collections.Generic;
using Logic.Enemy;
using StaticData;
using UnityEngine;

namespace Logic.Generator
{
    public class ModelConstructor : IModelConstructor
    {
        private readonly ILevelConstructorService _levelConstructorService;
        private readonly PlayerSo _playerSo;
        private readonly EnemySO _enemySo;
        private readonly ExitSo _exitSo;

        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private List<Transform> _floorPoints;
        private List<GameObject> _enemysList;
        private GameObject _player;
        private GameObject _exit;
        private static readonly float _offsetForCenterFloor = 0.5f;


        public ModelConstructor(ILevelConstructorService levelConstructorService)
        {
            _exitSo = Resources.Load<ExitSo>(AssetPath.EXIT_PATH);
            _playerSo = Resources.Load<PlayerSo>(AssetPath.PLAYERSO_PATH);
            _enemySo = Resources.Load<EnemySO>(AssetPath.ENEMYSO_PATH);
            _levelConstructorService = levelConstructorService;
        }
        
        public void Construct()
        {
            _startPosition = _levelConstructorService.GetPlayerPosition();
            _endPosition = _levelConstructorService.GetEndPosition();
            _floorPoints = _levelConstructorService.GetFloorTransform();
            
            SpawnExit();
            SpawnPlayer();
            SpawnEnemy();
        }

        public void ClearLevel()
        {
            Object.Destroy(_player);
            Object.Destroy(_exit);
            
            for (int i = 0; i < _enemysList.Count; i++)
            {
                Object.Destroy(_enemysList[i].gameObject);
            }
            
            _enemysList.Clear();
            _floorPoints.Clear();
        }

        private void SpawnEnemy()
        {
            _enemysList = new List<GameObject>();
            
            for (int i = 0; i < _enemySo.EnemyAtTheLevel; i++)
            {
                var randomPoint = _floorPoints[Random.Range(0, _floorPoints.Count)];
                var position = randomPoint.position;
                var obj = Object.Instantiate(_enemySo.EnemyPrefab, new Vector3(position.x + _offsetForCenterFloor, position.y, position.z + _offsetForCenterFloor), Quaternion.identity);
                obj.GetComponent<EnemyMove>().Init(_levelConstructorService, _enemySo);
                _enemysList.Add(obj);
            }
        }

        private void SpawnPlayer() => 
            _player = Object.Instantiate(_playerSo.PlayerPrefab, new Vector3(_startPosition.x + _offsetForCenterFloor, 1, _startPosition.y+ _offsetForCenterFloor), Quaternion.identity);

        private void SpawnExit() => 
            _exit = Object.Instantiate(_exitSo.ExitPrefab, new Vector3(_endPosition.x, 0, _endPosition.y), Quaternion.identity);
    }
}