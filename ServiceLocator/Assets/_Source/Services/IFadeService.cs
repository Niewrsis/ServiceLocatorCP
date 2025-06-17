using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public interface IFadeService
    {
        void FadeIn(CanvasGroup canvasGroup, float duration);
        void FadeOut(CanvasGroup canvasGroup, float duration);
    }
}