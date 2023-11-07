using Leopotam.Ecs.Ui.Systems;
using TMPro;
using UnityEngine;

namespace Assets.Source.UI
{
    [RequireComponent(typeof(Canvas))]
    public class GUIPanel : MonoBehaviour
    {
        [SerializeField] private EcsUiEmitter _uiEmitter;
        [SerializeField] private TextMeshProUGUI _scoreText;

        public EcsUiEmitter UIEmitter => _uiEmitter;

        public void ShowScore(float score)
        {
            _scoreText.text = $"Score: {score}";
        }
    }
}
