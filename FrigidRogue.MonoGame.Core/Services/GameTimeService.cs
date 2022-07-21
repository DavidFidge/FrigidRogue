using System;

using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;
using FrigidRogue.MonoGame.Core.Messages;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class GameTimeService : BaseComponent, IGameTimeService
    {
        private TimeSpan _lastElapsedRealTime;
        private readonly IStopwatchProvider _realTimeStopwatch;
        private readonly int[] _gameSpeedIncrements = { 25, 50, 100, 200, 400, 800 };
        private int _gameSpeedIncrementIndex;
        private int _startingGameSpeedIncrementIndex = 2;

        public GameTimeService(IStopwatchProvider stopwatchProvider)
        {
            _realTimeStopwatch = stopwatchProvider;
            _lastElapsedRealTime = new TimeSpan(0);

            GameTime = new CustomGameTime();
            InitialiseCommon();
        }

        private void InitialiseCommon()
        {
            GameTime.ElapsedGameTime = new TimeSpan(0);
            GameTime.ElapsedRealTime = new TimeSpan(0);
            GameTime.TotalGameTime = new TimeSpan(0);
            GameTime.TotalRealTime = new TimeSpan(0);
            GameTime.IsRunningSlowly = false;
            _gameSpeedIncrementIndex = _startingGameSpeedIncrementIndex;
            GameSpeedPercent = _gameSpeedIncrements[_gameSpeedIncrementIndex];
            IsPaused = false;
        }

        private void Initialise()
        {
            OriginalGameTime = null;
            InitialiseCommon();
            _lastElapsedRealTime = new TimeSpan(0);
        }

        public CustomGameTime GameTime { get; private set; }
        public GameTime OriginalGameTime { get; private set; }

        public int GameSpeedPercent { get; private set; }
        public bool IsPaused { get; private set; }

        private bool _isRunning;

        public void Reset()
        {
            Stop();
            Initialise();

            _realTimeStopwatch.Restart();
        }

        public void Start()
        {
            if (_isRunning)
                throw new Exception("Game timer has already been started.");

            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!_isRunning)
                return;

            OriginalGameTime = gameTime;

            var elapsedRealTime = _realTimeStopwatch.Elapsed;

            GameTime.ElapsedRealTime = elapsedRealTime - _lastElapsedRealTime;
            _lastElapsedRealTime = elapsedRealTime;
            GameTime.TotalRealTime = elapsedRealTime;

            if (IsPaused)
            {
                GameTime.ElapsedGameTime = new TimeSpan(0);
            }
            else
            {
                GameTime.ElapsedGameTime = TimeSpan.FromTicks(GameTime.ElapsedRealTime.Ticks * GameSpeedPercent / 100);
                GameTime.TotalGameTime = GameTime.TotalGameTime.Add(GameTime.ElapsedGameTime);
            }

            var gameTimeUpdateNotification = new GameTimeUpdateNotification(GameTime);

            Mediator.Publish(gameTimeUpdateNotification);
        }

        public void PauseGame()
        {
            IsPaused = true;
        }

        public void ResumeGame()
        {
            IsPaused = false;
        }

        public void IncreaseGameSpeed()
        {
            if (IsPaused)
            {
                ResumeGame();
                return;
            }

            if (_gameSpeedIncrementIndex < _gameSpeedIncrements.Length - 1)
                _gameSpeedIncrementIndex++;

            GameSpeedPercent = _gameSpeedIncrements[_gameSpeedIncrementIndex] ;
        }

        public void DecreaseGameSpeed()
        {
            if (_gameSpeedIncrementIndex > 0)
            {
                _gameSpeedIncrementIndex--;
                GameSpeedPercent = _gameSpeedIncrements[_gameSpeedIncrementIndex];
            }
            else
                PauseGame();
        }

        public void SaveState(ISaveGameService saveGameService)
        {
            var memento = new Memento<GameTimeServiceSaveData>(
                new GameTimeServiceSaveData
                {
                    TotalGameTime = GameTime.TotalGameTime
                });

            saveGameService.SaveToStore(memento);
        }

        public void LoadState(ISaveGameService saveGameService)
        {
            Initialise();

            var gameTimeServiceData = saveGameService.GetFromStore<GameTimeServiceSaveData>();

            GameTime.TotalGameTime = gameTimeServiceData.State.TotalGameTime;

            _realTimeStopwatch.Restart();
        }
    }

    public class CustomGameTime : GameTime
    {
        public TimeSpan ElapsedRealTime { get; set; }
        public TimeSpan TotalRealTime { get; set; }
    }
}