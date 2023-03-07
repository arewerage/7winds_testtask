using UnityEngine;
using UnityEngine.UI;

namespace _Project.Codebase.UI.Views.Buttons
{
    public class ColorButton : MonoBehaviour
    {
        public Button Button => _button;

        public Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
    }
}
