using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class LoadingCurtain : MonoBehaviour,
        IActivable
    {
        [SerializeField]
        private CanvasGroup _curtain;

        [SerializeField]
        private float _hidingTime;

        public void Activate()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Deactivate() =>
            _curtain.DOFade(0, _hidingTime)
                .OnComplete(() => gameObject.SetActive(false));
    }
}