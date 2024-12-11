using UnityEngine;
using UnityEngine.UI;
using System;

namespace MaxTools
{
    public class ColorWrapper
    {
        GameObject target;

        Graphic graphic;
        SpriteRenderer spriteRenderer;
        MeshRenderer meshRenderer;

        Func<Color> getter;
        Action<Color> setter;

        public ColorWrapper(GameObject target)
        {
            this.target = target;
        }

        public bool isValid
        {
            get
            {
                if (target == null)
                    return false;

                if (graphic != null)
                    return true;

                if (spriteRenderer != null)
                    return true;

                if (meshRenderer != null)
                    return true;

                // Graphic
                graphic = target.GetComponent<Graphic>();

                if (graphic != null)
                {
                    getter = () => graphic.color;
                    setter = (value) => graphic.color = value;
                    return true;
                }

                // SpriteRenderer
                spriteRenderer = target.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    getter = () => spriteRenderer.color;
                    setter = (value) => spriteRenderer.color = value;
                    return true;
                }

                // MeshRenderer
                meshRenderer = target.GetComponent<MeshRenderer>();

                if (meshRenderer != null)
                {
                    getter = () => meshRenderer.material.color;
                    setter = (value) => meshRenderer.material.color = value;
                    return true;
                }

                return false;
            }
        }

        public Color color
        {
            get
            {
                if (isValid)
                {
                    return getter();
                }
                else
                    throw new Exception("Graphic/Renderer component not found!");
            }

            set
            {
                if (isValid)
                {
                    setter(value);
                }
                else
                    throw new Exception("Graphic/Renderer component not found!");
            }
        }
    }
}
