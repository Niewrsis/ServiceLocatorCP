using DG.Tweening;
using UnityEngine;

namespace Services
{
    public class FadeService : IFadeService
    {
        public void FadeIn(CanvasGroup canvasGroup, float duration)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1, duration);
        }

        public void FadeOut(CanvasGroup canvasGroup, float duration)
        {
            canvasGroup.DOFade(0, duration).OnComplete(() => canvasGroup.gameObject.SetActive(false));
        }
    }
}