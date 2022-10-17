using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;

namespace Logic.Generator
{
    public interface ILevelConstructorService: IService
    {
        public void Construct();

        public Vector2 GetPlayerPosition();
        public Vector2 GetEndPosition();
        public List<Transform> GetFloorTransform();
    }
}