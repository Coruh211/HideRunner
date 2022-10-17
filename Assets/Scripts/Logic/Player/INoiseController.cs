using System;
using Infrastructure.Services;

namespace Logic.Player
{
    public interface INoiseController: IService
    {
        public float GetNoise();
        public void IncreaseNoise();
        public void ReductionNoise();
        public void DisposeIncrease();
        public void DisposeReduction();

        public event Action<float> UpdateNoise;
        public event Action MaxNoise;
    }
}