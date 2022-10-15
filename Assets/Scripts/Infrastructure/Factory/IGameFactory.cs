using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        GameObject CreateObject(string path);
        GameObject CreateObject(GameObject gameObject);
    }
}