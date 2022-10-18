using System;
using StaticData;
using UniRx;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerNoise: INoiseController
    {
        private readonly PlayerMove _playerMove;
        private readonly PlayerSo _playerSo;

        private float _noiseLevel;
        private bool _startIncrease;
        private bool _startReduction;
        private IDisposable _incteaseDisposable;
        private IDisposable _reductionDisposable;

        public event Action<float> UpdateNoise;
        public event Action MaxNoise;
        
        public PlayerNoise()
        {
            _playerSo = Resources.Load<PlayerSo>(AssetPath.PLAYERSO_PATH);
        }

        public void IncreaseNoise()
        {
            if (_startIncrease)
            {
                return;
            }
            
            DisposeReduction();
            _incteaseDisposable?.Dispose();
            
            _startIncrease = true;
            _incteaseDisposable = Observable.Interval(_playerSo.IncreaseInterval.sec()).Subscribe(x =>
            {
                if (_noiseLevel >= _playerSo.MaxNoise)
                {
                    MaxNoise?.Invoke();
                    return;
                }
                
                _noiseLevel += _playerSo.NoiseIncrease;
                UpdateNoise?.Invoke(_noiseLevel);
            });
        }

        public void ReductionNoise()
        {
            if (_startReduction)
            {
                return;
            }

            DisposeIncrease();
            _reductionDisposable?.Dispose();
            _startReduction = true;

            _reductionDisposable = Observable.Interval(_playerSo.ReductionInterval.sec()).Subscribe(x =>
            {
                if (_noiseLevel <= 0)
                {
                    _reductionDisposable?.Dispose();
                    return;
                }

                _noiseLevel -= _playerSo.NoiseReduction;
                UpdateNoise?.Invoke(_noiseLevel);
            });
        }

        public void ResetNoise()
        {
            _incteaseDisposable?.Dispose();
            _startIncrease = false;
            _reductionDisposable?.Dispose();
            _startReduction = false;
            _noiseLevel = 0;
            UpdateNoise?.Invoke(_noiseLevel);
        }
        
        public void DisposeIncrease()
        {
            _incteaseDisposable?.Dispose();
            _startIncrease = false;
        }
        
        public void DisposeReduction()
        {
            _reductionDisposable?.Dispose();
            _startReduction = false;
        }
        
        public float GetNoise() => 
            _noiseLevel;
    }
}