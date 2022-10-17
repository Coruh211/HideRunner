using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Utilities;
using Observable = UniRx.Observable;

namespace Logic.NavMesh
{
    public class NavMeshBaker: Singleton<NavMeshBaker>
    {
        [SerializeField] private NavMeshSurface _surface;

        public void BakeMesh()
        {
            _surface.BuildNavMesh();
            Debug.Log("NavmeshBuild");
        }
    }
}