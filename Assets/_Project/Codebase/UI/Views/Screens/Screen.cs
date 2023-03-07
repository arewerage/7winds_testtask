using DG.Tweening;
using UnityEngine;

namespace _Project.Codebase.UI.Views.Screens
{
    [RequireComponent(typeof(RectTransform))]
    public class Screen : MonoBehaviour
    {
        [SerializeField] private RectTransform _screenTransform;

        private float Width => _screenTransform.rect.width;

        public void MoveByOrder(int order, float duration)
        {
            _screenTransform.DOAnchorPos(Width * order * Vector2.right, duration);
        }
    }
}
