using UnityEngine.AI;


namespace Logic.NavMesh
{
    public class NavMeshBaker: Singleton<NavMeshBaker>
    {
        private NavMeshSurface _surface;

        public void Init()
        {
            _surface = GetComponent<NavMeshSurface>();
            BakeMesh();
        }
        public void BakeMesh()
        {
            _surface.BuildNavMesh();
        }
    }
}