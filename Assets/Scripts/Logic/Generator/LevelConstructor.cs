using System.Collections.Generic;
using StaticData;
using StaticData.StaticData;
using UnityEngine;

namespace Logic.Generator
{
    public class LevelConstructor : ILevelConstructorService
    {
        private readonly GridDataGenerator _dataGenerator;
        private readonly GeneratorSo _generatorSo;
        private readonly WallSO _wallSo;
        private readonly FloorSo _floorSo;
        private int[,] _data;
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private List<Transform> _floorObjects = new List<Transform>();
        
        


        public LevelConstructor()
        {
            _generatorSo = Resources.Load<GeneratorSo>(AssetPath.GENERATOR_SO_PATH);
            _wallSo = Resources.Load<WallSO>(AssetPath.WALLSO_PATH);
            _floorSo = Resources.Load<FloorSo>(AssetPath.FLOORSO_PATH);
            
            _dataGenerator = new GridDataGenerator(_generatorSo);
        }

        public void Construct()
        {
            GenerateNewMaze(_generatorSo.FieldRows, _generatorSo.FieldColums);
            GenerateLevel();
        }

        public Vector2 GetPlayerPosition() => 
            _startPosition;

        public Vector2 GetEndPosition() => 
            _endPosition;

        public List<Transform> GetFloorTransform() => 
            _floorObjects;

        private void GenerateNewMaze(int sizeRows, int sizeCols)
        {
            if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
            {
                Debug.LogError("Odd numbers work better for dungeon size.");
            }

            _data = _dataGenerator.FromDimensions(sizeRows, sizeCols);

            FindStartPosition();
            FindEndPosition();
        }
    
        private void FindStartPosition()
        {
            int[,] maze = _data;
            int rMax = maze.GetUpperBound(0);
            int cMax = maze.GetUpperBound(1);

            for (int i = 0; i <= rMax; i++)
            {
                for (int j = 0; j <= cMax; j++)
                {
                    if (maze[i, j] == 0)
                    {
                        _startPosition.x = i;
                        _startPosition.y = j;
                        return;
                    }
                }
            }
        }

        private void FindEndPosition()
        {
            int[,] maze = _data;
            int rMax = maze.GetUpperBound(0);
            int cMax = maze.GetUpperBound(1);
        
            for (int i = rMax; i >= 0; i--)
            {
                for (int j = cMax; j >= 0; j--)
                {
                    if (maze[i, j] == 0)
                    {
                        _endPosition.x = i;
                        _endPosition.y = j;
                        return;
                    }
                }
            }
        }
        
        private void GenerateLevel()
        {
            var level = GameObject.FindGameObjectWithTag("Level");
            
            int[,] maze = _data;
            
            int rMax = maze.GetUpperBound(0);
            int cMax = maze.GetUpperBound(1);
            
            
            for (int i = rMax; i >= 0; i--)
            {
                for (int j = 0; j <= cMax; j++)
                {
                    if (maze[i, j] == 0)
                    {
                        var obj = Object.Instantiate(_floorSo.FloorPrefab, new Vector3(i, 0, j),
                            Quaternion.identity, level.transform);
                        _floorObjects.Add(obj.transform);
                    }
                    else
                    {
                        Object.Instantiate(_wallSo.WallPrefab, new Vector3(i, 0, j),
                            Quaternion.identity, level.transform);
                    }
                }
            }
        }

       
    }
}