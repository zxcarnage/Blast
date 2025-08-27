using UnityEngine;

namespace Utils.Layer
{
    public static class LayerMaskExtension
    {
        public static bool IsOnLayer(this Component component, int layerMask)
            => layerMask == (layerMask | (1 << component.gameObject.layer));
    }
}