using UnityEngine;

namespace Utils.Drawer.Key
{
    public class KeyValueAttribute : PropertyAttribute
    {
        public readonly string PropertyName;

        public KeyValueAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}