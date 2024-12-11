using UnityEngine;
using System.Collections;

namespace MaxTools
{
    using MaxTools.Extensions;

    [NiceInspector]
    [DisallowMultipleComponent]
    public class Destroyer : MonoBehaviour
    {
        [BeginToggleGroup]
        [SerializeField] bool useTimer;
        [SerializeField] [Min(0)] float timer;
        [EndToggleGroup]

        [BeginToggleGroup]
        [SerializeField] bool useDistance;
        [SerializeField] [Min(0)] float distance;
        [SerializeField] bool useStartPosition = true;
        [ShowIf("!useStartPosition")]
        [SerializeField] Vector3 position;
        [SerializeField] Space space;

        IEnumerator Start()
        {
            if (useStartPosition)
            {
                position = transform.GetPosition3D(space);
            }

            if (useTimer)
            {
                yield return new WaitForSeconds(timer);

                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (useDistance)
            {
                if ((transform.GetPosition3D(space) - position).sqrMagnitude > distance * distance)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
