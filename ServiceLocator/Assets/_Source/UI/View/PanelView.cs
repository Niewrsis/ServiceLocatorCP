using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI.View
{
    public class PanelView : MonoBehaviour
    {
        [Header("Main References")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _closeButton;

        [Header("Score Elements")]
        [SerializeField] private Text _scoreText;
        [SerializeField] private Button _collectButton;

        public event Action OnCloseButtonClicked;
        public event Action OnCollectButtonClicked;
        public CanvasGroup CanvasGroup => _canvasGroup;

        private void Awake()
        {
            SetupButtons();
            HideImmediate();
        }


        private void SetupButtons()
        {
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
            _collectButton.onClick.AddListener(() => OnCollectButtonClicked?.Invoke());
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
            _collectButton.onClick.RemoveAllListeners();
        }

        public void UpdateScore(int score)
        {
            _scoreText.text = $"Score: {score}";
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void HideImmediate()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        }
    }
}