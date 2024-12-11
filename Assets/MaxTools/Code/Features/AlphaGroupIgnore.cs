using UnityEngine;

namespace MaxTools
{
    [DisallowMultipleComponent]
    public class AlphaGroupIgnore : MonoBehaviour
    {
        public bool includeSelf = true;
        public bool includeChildren = false;
    }
}
