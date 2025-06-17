using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI.View
{
    public class MainScreenView : MonoBehaviour
    {
        [SerializeField] private Button openButton;

        public event Action OnOpenButtonClicked;

        private void Awake()
        {
            openButton.onClick.AddListener(OnOpenButtonClick);
        }

        private void OnDestroy()
        {
            openButton.onClick.RemoveListener(OnOpenButtonClick);
        }

        private void OnOpenButtonClick()
        {
            OnOpenButtonClicked?.Invoke();
        }
    }
}