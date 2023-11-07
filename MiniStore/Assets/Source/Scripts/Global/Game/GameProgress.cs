using UnityEngine.Events;

namespace Assets.Source.Scripts.Global.Game
{
    public class GameProgress
    {
        private UnityEvent _onStartGame;
        private UnityEvent<float> _onScoreChanged;
        private float _score;

        public GameProgress()
        {
            _onStartGame = new();
            _onScoreChanged = new();
            _score = 0f;
        }

        public void AddToStart(UnityAction action)
        {
            _onStartGame.AddListener(action);
        }

        public void AddScoreChange(UnityAction<float> action)
        {
            _onScoreChanged.AddListener(action);
        }

        public void StartGame()
        {
            _onStartGame?.Invoke();
        }

        public void AddScore(float score)
        {
            _score += score;
            _onScoreChanged.Invoke(_score);
        }
    }
}
