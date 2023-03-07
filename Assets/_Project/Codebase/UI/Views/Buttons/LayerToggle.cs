using UnityEngine;
using UnityEngine.UI;

namespace _Project.Codebase.UI.Views.Buttons
{
    public class LayerToggle : MonoBehaviour
    {
        public Toggle Toggle => _toggle;
        
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Image _image;
        
        public LayerType LayerType { get; set; }

        public Color Color
        {
            set => _image.color = value;
        }
    }
}
