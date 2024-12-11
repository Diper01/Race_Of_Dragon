using UnityEngine;

namespace MaxTools
{
    using MaxTools.Extensions;

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class AlphaGroup : MonoBehaviour
    {
        [SerializeField] [Range01] float editorAlpha = 1.0f;
        public bool ignoreParentGroups = false;

        Material m_AlphaMaterial;
        Material alphaMaterial
        {
            get
            {
                if (m_AlphaMaterial == null)
                {
                    m_AlphaMaterial = new Material(Shader.Find("Sprites/Default"));
                    m_AlphaMaterial.enableInstancing = true;
                    m_AlphaMaterial.name = $"Alpha Material - {name}";
                }

                return m_AlphaMaterial;
            }
        }

        static Material m_DefaultMaterial;
        static Material defaultMaterial
        {
            get
            {
                if (m_DefaultMaterial == null)
                {
                    var spriteRenderer = new GameObject().AddComponent<SpriteRenderer>();

                    m_DefaultMaterial = spriteRenderer.sharedMaterial;

                    if (!Application.isPlaying)
                    {
                        DestroyImmediate(spriteRenderer.gameObject);
                    }
                    else
                        Destroy(spriteRenderer.gameObject);
                }

                return m_DefaultMaterial;
            }
        }

        public float alpha
        {
            get => editorAlpha;
            set => editorAlpha = Tools.Clamp01(value);
        }

        void Update()
        {
            if (!Application.isPlaying || this.CheckChange((editorAlpha, ignoreParentGroups), true))
            {
                ForcedUpdate();
                ForcedUpdateChildren(transform);
            }
        }

        public void ForcedUpdate()
        {
            SetChildrenMaterial(transform);

            float k = 1.0f;

            if (!ignoreParentGroups)
            {
                foreach (var group in GetComponentsInParent<AlphaGroup>())
                {
                    if (group == null || group == this)
                    {
                        continue;
                    }

                    k *= group.alpha;

                    if (group.ignoreParentGroups)
                    {
                        break;
                    }
                }
            }

            alphaMaterial.SetAlpha01(editorAlpha * k);
        }
        public void ForcedUpdateChildren(Transform next)
        {
            AlphaGroup group = next.GetComponent<AlphaGroup>();

            if (group != null && group != this)
            {
                if (group.ignoreParentGroups)
                {
                    return;
                }
                else
                    group.ForcedUpdate();
            }

            foreach (Transform child in next)
            {
                ForcedUpdateChildren(child);
            }
        }

        void SetChildrenMaterial(Transform next)
        {
            AlphaGroup group = next.GetComponent<AlphaGroup>();

            if (group != null && group != this)
            {
                return;
            }

            var spriteRenderer = next.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.material = alphaMaterial;

                var ignore = next.GetComponent<AlphaGroupIgnore>();

                if (ignore == null)
                {
                    if (group == null)
                    {
                        var parent = next.parent;

                        while (parent != null)
                        {
                            var parent_ignore = parent.GetComponent<AlphaGroupIgnore>();

                            if (parent_ignore != null && parent_ignore.includeChildren)
                            {
                                spriteRenderer.material = defaultMaterial;

                                break;
                            }

                            if (parent.GetComponent<AlphaGroup>() != null)
                            {
                                break;
                            }

                            parent = parent.parent;
                        }
                    }
                }
                else if (ignore.includeSelf)
                {
                    spriteRenderer.material = defaultMaterial;
                }
            }

            foreach (Transform child in next)
            {
                SetChildrenMaterial(child);
            }
        }
    }
}
