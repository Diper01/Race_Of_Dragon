using UnityEngine;
using System.Collections;

namespace MaxTools
{
    [DisallowMultipleComponent]
    public class Deactivator : MonoBehaviour
    {
        [SerializeField] [Min(0)] float delay = 1.0f;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(delay);

            gameObject.SetActive(false);

            Destroy(this);
        }
    }
}
