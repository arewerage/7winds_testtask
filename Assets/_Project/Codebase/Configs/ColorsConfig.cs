using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Codebase.Configs
{
    [CreateAssetMenu(menuName = "Configs/Colors", fileName = "ColorsConfig")]
    public class ColorsConfig : ScriptableObject
    {
        public List<Color> Colors;

        [SerializeField] private int _errorColorIndex;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Colors.Count > 28)
                Debug.LogError("Цветов не может быть больше, чем 28!!!");
        }
#endif

        public Color GetErrorColor()
        {
            return Colors[_errorColorIndex];
        }

        public IEnumerable<Color> GetRandomColors(int count)
        {
            List<Color> tempColors = new(Colors);
            tempColors.Shuffle();
            return tempColors.Take(count);
        }
    }
}
