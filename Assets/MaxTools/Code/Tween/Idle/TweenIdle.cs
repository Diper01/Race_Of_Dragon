using UnityEngine;

namespace MaxTools
{
    using MaxTools.Extensions;

    [NiceInspector]
    [DisallowMultipleComponent]
    public class TweenIdle : MonoBehaviour
    {
        #region Fields
        [BeginFoldoutGroup("Color", "useColor")]
        [SerializeField] bool _tab_color;
        public bool useColor;
        [InspectorName("Min Color")] public Color colorMinValue;
        [InspectorName("Max Color")] public Color colorMaxValue;
        [InspectorName("Intensity Range")] [MinMaxUnlimited] public Vector2 colorIntensityRange;
        [InspectorName("Lerp Type")] public LerpType colorLerpType;
        [InspectorName("Time Mode")] public TimeMode colorTimeMode;
        [EndFoldoutGroup]

        [BeginFoldoutGroup("Scale", "useScale")]
        [SerializeField] bool _tab_scale;
        public bool useScale;
        public bool useUniformSize;
        [InspectorName("Min Scale")] [ShowIf("!useUniformSize")] public Vector3 scaleMinValue;
        [InspectorName("Max Scale")] [ShowIf("!useUniformSize")] public Vector3 scaleMaxValue;
        [InspectorName("Uniform Size Range")] [ShowIf("useUniformSize")] [MinMaxUnlimited] public Vector2 uniformSizeRange;
        [InspectorName("Intensity Range")] [MinMaxUnlimited] public Vector2 scaleIntensityRange;
        [InspectorName("Lerp Type")] public LerpType scaleLerpType;
        [InspectorName("Time Mode")] public TimeMode scaleTimeMode;
        [EndFoldoutGroup]

        [BeginFoldoutGroup("Position", "usePosition")]
        [SerializeField] bool _tab_position;
        public bool usePosition;
        [InspectorName("Min Spread Position")] public Vector3 positionMinValue;
        [InspectorName("Max Spread Position")] public Vector3 positionMaxValue;
        [InspectorName("Intensity Range")] [MinMaxUnlimited] public Vector2 positionIntensityRange;
        [InspectorName("Lerp Type")] public LerpType positionLerpType;
        [InspectorName("Space")] public Space positionSpace;
        [InspectorName("Time Mode")] public TimeMode positionTimeMode;
        [EndFoldoutGroup]

        [BeginFoldoutGroup("Rotation", "useRotation")]
        [SerializeField] bool _tab_rotation;
        public bool useRotation;
        [InspectorName("Min Spread Rotation")] public Vector3 rotationMinValue;
        [InspectorName("Max Spread Rotation")] public Vector3 rotationMaxValue;
        [InspectorName("Intensity Range")] [MinMaxUnlimited] public Vector2 rotationIntensityRange;
        [InspectorName("Lerp Type")] public LerpType rotationLerpType;
        [InspectorName("Space")] public Space rotationSpace;
        [InspectorName("Time Mode")] public TimeMode rotationTimeMode;
        [EndFoldoutGroup]

        Color color;
        float colorIntensity;
        ColorWrapper colorWrapper;

        Vector3 scale;
        float scaleIntensity;

        Vector3 startPosition;
        Vector3 position;
        float positionIntensity;

        Quaternion startRotation;
        Quaternion rotation;
        float rotationIntensity;
        #endregion

        void Start()
        {
            color = Randomize.Range(colorMinValue, colorMaxValue);
            colorIntensity = Randomize.Range(colorIntensityRange.x, colorIntensityRange.y);
            colorWrapper = new ColorWrapper(gameObject);

            if (useUniformSize)
            {
                scale = Randomize.Range(uniformSizeRange.x, uniformSizeRange.y) * Vector3.one;
            }
            else
                scale = Randomize.Range(scaleMinValue, scaleMaxValue);

            scaleIntensity = Randomize.Range(scaleIntensityRange.x, scaleIntensityRange.y);

            startPosition = transform.GetPosition3D(positionSpace);
            position = startPosition + Randomize.Range(positionMinValue, positionMaxValue);
            positionIntensity = Randomize.Range(positionIntensityRange.x, positionIntensityRange.y);

            startRotation = transform.GetRotation3D(rotationSpace);
            rotation = startRotation * Quaternion.Euler(Randomize.Range(rotationMinValue, rotationMaxValue));
            rotationIntensity = Randomize.Range(rotationIntensityRange.x, rotationIntensityRange.y);
        }

        void Update()
        {
            if (useColor && Tween.Color(colorWrapper, color, colorIntensity, colorLerpType, colorTimeMode))
            {
                color = Randomize.Range(colorMinValue, colorMaxValue);
                colorIntensity = Randomize.Range(colorIntensityRange.x, colorIntensityRange.y);
            }

            if (useScale && Tween.Scale3D(transform, scale, scaleIntensity, scaleLerpType, scaleTimeMode))
            {
                if (useUniformSize)
                {
                    scale = Randomize.Range(uniformSizeRange.x, uniformSizeRange.y) * Vector3.one;
                }
                else
                    scale = Randomize.Range(scaleMinValue, scaleMaxValue);

                scaleIntensity = Randomize.Range(scaleIntensityRange.x, scaleIntensityRange.y);
            }

            if (usePosition && Tween.Position3D(
                transform, position, positionIntensity, positionLerpType, positionSpace, positionTimeMode))
            {
                position = startPosition + Randomize.Range(positionMinValue, positionMaxValue);
                positionIntensity = Randomize.Range(positionIntensityRange.x, positionIntensityRange.y);
            }

            if (useRotation && Tween.Rotation3D(
                transform, rotation, rotationIntensity, rotationLerpType, rotationSpace, rotationTimeMode))
            {
                rotation = startRotation * Quaternion.Euler(Randomize.Range(rotationMinValue, rotationMaxValue));
                rotationIntensity = Randomize.Range(rotationIntensityRange.x, rotationIntensityRange.y);
            }
        }
    }
}
