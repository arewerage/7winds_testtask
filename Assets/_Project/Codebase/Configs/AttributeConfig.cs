using System.Collections.Generic;
using System.Linq;
using _Project.Codebase.UI.Views.Attribute;
using UnityEngine;

namespace _Project.Codebase.Configs
{
    [CreateAssetMenu(menuName = "Configs/Attribute", fileName = "AttributeConfig")]
    public class AttributeConfig : ScriptableObject
    {
        [SerializeField] private AttributeConfigData[] _attributes;

        private Dictionary<int, Attribute> _cached;

        public Dictionary<int, Attribute> Attributes =>
            _cached ??= _attributes.ToDictionary(x => x.Id, x => x.Prefab);
    }
}
