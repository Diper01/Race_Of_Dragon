using UnityEngine;

namespace MaxTools
{
    [NiceInspector]
    [DisallowMultipleComponent]
    public class Mover : MonoBehaviour
    {
        [SerializeField] Vector3 forceVector;

        [BeginToggleGroup]
        [SerializeField] bool useRandomForceVector;
        [SerializeField] [InspectorName("Min")] Vector3 minForceVector;
        [SerializeField] [InspectorName("Max")] Vector3 maxForceVector;
        [EndToggleGroup]

        [BeginToggleGroup]
        [SerializeField] bool useRandomSign;
        [SerializeField] [InspectorName("X")] bool useRandomSignX;
        [SerializeField] [InspectorName("Y")] bool useRandomSignY;
        [SerializeField] [InspectorName("Z")] bool useRandomSignZ;
        [EndToggleGroup]

        [SerializeField] Space space;
        [SerializeField] TimeMode timeMode;

        void Start()
        {
            if (useRandomForceVector)
            {
                forceVector = Randomize.Range(minForceVector, maxForceVector);
            }

            if (useRandomSign)
            {
                if (useRandomSignX)
                    forceVector.x *= Randomize.signed;

                if (useRandomSignY)
                    forceVector.y *= Randomize.signed;

                if (useRandomSignZ)
                    forceVector.z *= Randomize.signed;
            }
        }

        void Update()
        {
            switch (timeMode)
            {
                case TimeMode.ScaledTime:
                    transform.Translate(forceVector * Time.deltaTime, space);
                    break;

                case TimeMode.UnscaledTime:
                    transform.Translate(forceVector * Time.unscaledDeltaTime, space);
                    break;
            }
        }
    }
}
