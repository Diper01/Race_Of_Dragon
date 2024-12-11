using UnityEngine;
using UnityEngine.UI;

namespace MaxTools.Extensions
{
    public static class EX_Graphics
    {
        public static void SetAlpha01(this Graphic me, float alpha)
        {
            me.color = me.color.GetWithAlpha01(alpha);
        }
        public static void SetAlpha32(this Graphic me, int alpha)
        {
            me.color = me.color.GetWithAlpha32(alpha);
        }

        public static void SetAlpha01(this SpriteRenderer me, float alpha)
        {
            me.color = me.color.GetWithAlpha01(alpha);
        }
        public static void SetAlpha32(this SpriteRenderer me, int alpha)
        {
            me.color = me.color.GetWithAlpha32(alpha);
        }

        public static void SetAlpha01(this Material me, float alpha)
        {
            me.color = me.color.GetWithAlpha01(alpha);
        }
        public static void SetAlpha32(this Material me, int alpha)
        {
            me.color = me.color.GetWithAlpha32(alpha);
        }

        public static void SetAlpha01(this ColorWrapper me, float alpha)
        {
            me.color = me.color.GetWithAlpha01(alpha);
        }
        public static void SetAlpha32(this ColorWrapper me, int alpha)
        {
            me.color = me.color.GetWithAlpha32(alpha);
        }

        public static void SetTexture(this Image me, Texture2D texture, float pixelsPerUnit = 100.0f)
        {
            me.sprite = texture.MakeSprite(pixelsPerUnit);
        }
        public static void SetTexture(this SpriteRenderer me, Texture2D texture, float pixelsPerUnit = 100.0f)
        {
            me.sprite = texture.MakeSprite(pixelsPerUnit);
        }

        public static void SetDefaultMaterial(this MeshRenderer me)
        {
            var cacheVar = new CacheVar<Material>();

            if (!cacheVar.TryGetValue(out var defaultMaterial))
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                var meshRenderer = cube.GetComponent<MeshRenderer>();

                defaultMaterial = meshRenderer.sharedMaterial;

                if (!Application.isPlaying)
                {
                    Object.DestroyImmediate(cube);
                }
                else
                    Object.Destroy(cube);
            }

            me.material = defaultMaterial;
        }
        public static void SetDefaultMaterial(this SpriteRenderer me)
        {
            var cacheVar = new CacheVar<Material>();

            if (!cacheVar.TryGetValue(out var defaultMaterial))
            {
                var spriteRenderer = new GameObject().AddComponent<SpriteRenderer>();

                defaultMaterial = spriteRenderer.sharedMaterial;

                if (!Application.isPlaying)
                {
                    Object.DestroyImmediate(spriteRenderer.gameObject);
                }
                else
                    Object.Destroy(spriteRenderer.gameObject);
            }

            me.material = defaultMaterial;
        }
    }
}
