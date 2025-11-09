using DG.Tweening;
using UnityEngine;

namespace HouseTrap.Core.UI
{
    public class UICanvasGroupFade : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private bool fadeOnAwake = true;
    
        void OnEnable(){
            canvasGroup = GetComponent<CanvasGroup>();
            if(fadeOnAwake){
                FadeIn();
            }
        }

        public void FadeIn(){
            canvasGroup.DOFade(1f,fadeDuration).SetUpdate(true);
        }
        public void FadeOut(){
        
            canvasGroup.DOFade(0f,fadeDuration).SetUpdate(true);
        }
    }
}