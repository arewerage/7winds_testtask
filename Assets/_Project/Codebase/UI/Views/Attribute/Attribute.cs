using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Codebase.UI.Views.Attribute
{
    public class Attribute : MonoBehaviour
    {
        public int Id;
        
        [SerializeField] private Image _firstLayer;
        [SerializeField] private Image _secondLayer;
        [SerializeField] private Image _thirdLayer;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;

        public void SetY(float y)
        {
            _rectTransform.DOAnchorPos(Vector2.up * y, 0f);
        }

        public void SetFirstColor(Color color) =>
            _firstLayer.color = color;

        public void SetSecondColor(Color color) =>
            _secondLayer.color = color;

        public void SetThirdColor(Color color) =>
            _thirdLayer.color = color;

        public void Show(AnimateSide side)
        {
            SetAlpha(0f);
            _rectTransform.DOAnchorPosX((int)side * Screen.width * 3f, 0f);
            _rectTransform.DOAnchorPosX(0f, 0.5f);
            _canvasGroup.DOFade(1f, 0.5f);
        }

        public void Hide(AnimateSide side)
        {
            SetAlpha(1f);
            _rectTransform.DOAnchorPosX(0f, 0f);
            _rectTransform.DOAnchorPosX((int)side * Screen.width * 3f, 0.5f);
            _canvasGroup.DOFade(0f, 0.5f);
        }

        public void SetAlpha(float alpha)
        {
            _canvasGroup.alpha = alpha;
        }

        public void FadeIn()
        {
            _canvasGroup.DOFade(0f, 0.5f);
        }

        public void FadeOut()
        {
            _canvasGroup.DOFade(1f, 0.5f);
        }

        public enum AnimateSide
        {
            Left = -1,
            Right = 1
        }
    }
}
