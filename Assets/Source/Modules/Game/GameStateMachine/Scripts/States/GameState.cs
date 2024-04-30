using Modules.Services.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Game.StateMachine.Scripts.States
{
    public class GameState : IGameState
    {
        private EnemySpawnService _enemySpawnService;
        private ITimerService _timerService;
        private IGameStateMachine _gameStateMachine;
        private int _maxWaveNumber = 4;
        private int _currentWaveNumber;

        public GameState(EnemySpawnService enemySpawnService, ITimerService timerService, IGameStateMachine gameStateMachine)
        {
            _enemySpawnService = enemySpawnService;
            _timerService = timerService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _enemySpawnService.SpawnGroup(GetEnemyWave(_currentWaveNumber));
            _currentWaveNumber++;

            for (int i = 1; i < _maxWaveNumber; i++) 
            {
                float delayBeforeNextWave = 5f;
                _timerService.StartTimer(delayBeforeNextWave, SpawnNextWave);
            }

            _timerService.WaitUntil(() => _enemySpawnService.HasEnemies == false, _gameStateMachine.EnterIn<EndGameState>);
        }

        public void Exit()
        {

        }

        private void SpawnNextWave() 
        {
            _enemySpawnService.SpawnGroup(GetEnemyWave(_currentWaveNumber));
            _currentWaveNumber++;
        }

        private List<EnemySpawnService.EnemyType> GetEnemyWave(int waveNumber) 
        {
            if (waveNumber < 0)
                throw new ArgumentException("Wave number must be less than 0");

            switch (waveNumber) 
            {
                case 0:
                    return new List<EnemySpawnService.EnemyType>()
                    {
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Zombie,
                    };

                case 1:
                    return new List<EnemySpawnService.EnemyType>()
                    {
                        EnemySpawnService.EnemyType.Mummy,
                        EnemySpawnService.EnemyType.Mummy,
                        EnemySpawnService.EnemyType.Mummy,
                        EnemySpawnService.EnemyType.Mummy,
                        EnemySpawnService.EnemyType.Mummy,
                    };

                case 2:
                    return new List<EnemySpawnService.EnemyType>()
                    {
                        EnemySpawnService.EnemyType.Skeleton,
                        EnemySpawnService.EnemyType.Skeleton,
                        EnemySpawnService.EnemyType.Skeleton,
                        EnemySpawnService.EnemyType.Skeleton,
                        EnemySpawnService.EnemyType.Skeleton,
                    };

                case 3:
                    return new List<EnemySpawnService.EnemyType>()
                    {
                        EnemySpawnService.EnemyType.Skeleton,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Mummy,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Skeleton,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Mummy,
                        EnemySpawnService.EnemyType.Zombie,
                    };

                case 4:
                    return new List<EnemySpawnService.EnemyType>()
                    {
                        EnemySpawnService.EnemyType.Skeleton,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Mummy,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Skeleton,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Mummy,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Skeleton,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Mummy,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Skeleton,
                        EnemySpawnService.EnemyType.Zombie,
                        EnemySpawnService.EnemyType.Mummy,
                        EnemySpawnService.EnemyType.Zombie,
                    };
            }

            throw new MissingReferenceException($"Wave number {waveNumber} does not excist");
        }
    }
}
