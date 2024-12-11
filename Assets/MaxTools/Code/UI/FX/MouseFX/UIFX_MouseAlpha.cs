using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MaxTools
{
    using MaxTools.Extensions;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Graphic))]
    public class UIFX_MouseAlpha : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] [Range01] float enterAlpha = 1.0f;
        [SerializeField] float enterIntensity = 7.0f;
        [SerializeField] LerpType enterLerpType = LerpType.Simple;

        [SerializeField] [Range01] float exitAlpha = 0.5f;
        [SerializeField] float exitIntensity = 5.0f;
        [SerializeField] LerpType exitLerpType = LerpType.Simple;

        Graphic graphic;
        PointerType pointerType;

        void Start()
        {
            graphic = GetComponent<Graphic>();
        }

        void Update()
        {
            switch (pointerType)
            {
                case PointerType.Enter:
                    if (Tween.Alpha(graphic, enterAlpha, enterIntensity, enterLerpType))
                    {
                        graphic.SetAlpha01(enterAlpha);
                        pointerType = PointerType.None;
                    }
                    break;

                case PointerType.Exit:
                    if (Tween.Alpha(graphic, exitAlpha, exitIntensity, exitLerpType))
                    {
                        graphic.SetAlpha01(exitAlpha);
                        pointerType = PointerType.None;
                    }
                    break;
            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData) => pointerType = PointerType.Exit;
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) => pointerType = PointerType.Enter;
    }
}
