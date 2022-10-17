using System.Collections.Generic;
using Logic.Enemy;
using StaticData;
using UnityEngine;

namespace Logic.Generator
{
    public class GameConstructor : IGameConstructor
    {
        private readonly ILevelConstructorService _levelConstructorService;
        private readonly PlayerSo _playerSo;
        private readonly EnemySO _enemySo;
        private readonly ExitSo _exitSo;

        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private List<Transform> _floorPoints;


        public GameConstructor(ILevelConstructorService levelConstructorService)
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

        private void SpawnEnemy()
        {
            for (int i = 0; i < _enemySo.EnemyAtTheLevel; i++)
            {
                var randomPoint = _floorPoints[Random.Range(0, _floorPoints.Count)];
                var position = randomPoint.position;
                var obj = Object.Instantiate(_enemySo.EnemyPrefab, new Vector3(position.x + 0.5f, position.y, position.z + 0.5f), Quaternion.identity);
                obj.GetComponent<EnemyMove>().Init(_levelConstructorService, _enemySo);
            }
        }

        private void SpawnPlayer()
        {
            Object.Instantiate(_playerSo.PlayerPrefab, new Vector3(_startPosition.x + 0.5f, 1, _startPosition.y+ 0.5f), Quaternion.identity);
        }

        private void SpawnExit()
        {
            Object.Instantiate(_exitSo.ExitPrefab, new Vector3(_endPosition.x, 0, _endPosition.y), Quaternion.identity);
        }

       
    }
}