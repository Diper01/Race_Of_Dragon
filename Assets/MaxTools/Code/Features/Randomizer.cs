using UnityEngine;

namespace MaxTools
{
    using MaxTools.Extensions;

    [NiceInspector]
    [DisallowMultipleComponent]
    public class Randomizer : MonoBehaviour
    {
        #region Fields
        [BeginFoldoutGroup("Color", "useColor")]
        [SerializeField] bool _tab_color, useColor;
        [SerializeField] [InspectorName("Use Array")] bool colorUseArray;
        [SerializeField] [InspectorName("Use Gradient")] bool useGradient;
        [SerializeField] [ShowIf("useColorArray  & !useGradient")] Color[] colorArray;
        [SerializeField] [ShowIf("useColorArray  &  useGradient")] Gradient[] gradientArray;
        [SerializeField] [ShowIf("!useColorArray & !useGradient")] Color minColor;
        [SerializeField] [ShowIf("!useColorArray & !useGradient")] Color maxColor;
        [SerializeField] [ShowIf("!useColorArray &  useGradient")] Gradient gradient;
        [EndFoldoutGroup]

        [BeginFoldoutGroup("Scale", "useScale")]
        [SerializeField] bool _tab_scale, useScale;
        [SerializeField] [InspectorName("Use Array")] bool scaleUseArray;
        [SerializeField] [InspectorName("Use Uniform Size")] bool useUniformSize;
        [SerializeField] [ShowIf("useScaleArray  & !useUniformSize")] Vector3[] scaleArray;
        [SerializeField] [ShowIf("useScaleArray  &  useUniformSize")] float[] uniformSizeArray;
        [SerializeField] [ShowIf("!useScaleArray & !useUniformSize")] Vector3 minScale;
        [SerializeField] [ShowIf("!useScaleArray & !useUniformSize")] Vector3 maxScale;
        [SerializeField] [ShowIf("!useScaleArray &  useUniformSize")] float minSize;
        [SerializeField] [ShowIf("!useScaleArray &  useUniformSize")] float maxSize;
        [EndFoldoutGroup]

        [BeginFoldoutGroup("Position", "usePosition")]
        [SerializeField] bool _tab_position, usePosition;
        [SerializeField] [InspectorName("Use Array")] bool positionUseArray;
        [SerializeField] [ShowIf(" usePositionArray")] Vector3[] positionArray;
        [SerializeField] [ShowIf("!usePositionArray")] Vector3 minPosition;
        [SerializeField] [ShowIf("!usePositionArray")] Vector3 maxPosition;
        [SerializeField] [InspectorName("Space")] Space positionSpace;
        [EndFoldoutGroup]

        [BeginFoldoutGroup("Rotation", "useRotation")]
        [SerializeField] bool _tab_rotation, useRotation;
        [SerializeField] [InspectorName("Use Array")] bool rotationUseArray;
        [SerializeField] [ShowIf(" useRotationArray")] Vector3[] rotationArray;
        [SerializeField] [ShowIf("!useRotationArray")] Vector3 minRotation;
        [SerializeField] [ShowIf("!useRotationArray")] Vector3 maxRotation;
        [SerializeField] [InspectorName("Space")] Space rotationSpace;
        #endregion

        void OnValidate()
        {
            if (colorUseArray && useGradient)
            {
                foreach (var item in gradientArray)
                {
                    if (item.IsClear())
                    {
                        item.SetColors(Color.white, Color.white);
                    }
                }
            }
        }

        void Start()
        {
            if (useColor)
            {
                if (useGradient)
                {
                    if (colorUseArray)
                    {
                        new ColorWrapper(gameObject).color = gradientArray.GetRandomItem().Evaluate(Randomize.value01);
                    }
                    else
                        new ColorWrapper(gameObject).color = gradient.Evaluate(Randomize.value01);
                }
                else
                {
                    if (colorUseArray)
                    {
                        new ColorWrapper(gameObject).color = colorArray.GetRandomItem();
                    }
                    else
                        new ColorWrapper(gameObject).color = Randomize.Range(minColor, maxColor);
                }
            }

            if (useScale)
            {
                if (useUniformSize)
                {
                    if (scaleUseArray)
                    {
                        transform.SetUniformSize3D(uniformSizeArray.GetRandomItem());
                    }
                    else
                        transform.SetUniformSize3D(Randomize.Range(minSize, maxSize));
                }
                else
                {
                    if (scaleUseArray)
                    {
                        transform.localScale = scaleArray.GetRandomItem();
                    }
                    else
                        transform.localScale = Randomize.Range(minScale, maxScale);
                }
            }

            if (usePosition)
            {
                if (positionUseArray)
                {
                    transform.SetPosition3D(positionArray.GetRandomItem(), positionSpace);
                }
                else
                    transform.SetPosition3D(Randomize.Range(minPosition, maxPosition), positionSpace);
            }

            if (useRotation)
            {
                if (rotationUseArray)
                {
                    transform.SetAngles(rotationArray.GetRandomItem(), rotationSpace);
                }
                else
                    transform.SetAngles(Randomize.Range(minRotation, maxRotation), rotationSpace);
            }

            Destroy(this);
        }
    }
}
