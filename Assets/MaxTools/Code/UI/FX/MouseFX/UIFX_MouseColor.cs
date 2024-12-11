using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MaxTools
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Graphic))]
    public class UIFX_MouseColor : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] Color enterColor = Color.white;
        [SerializeField] float enterIntensity = 7.0f;
        [SerializeField] LerpType enterLerpType = LerpType.Simple;

        [SerializeField] Color exitColor = Color.white;
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
                    if (Tween.Color(graphic, enterColor, enterIntensity, enterLerpType))
                    {
                        graphic.color = enterColor;
                        pointerType = PointerType.None;
                    }
                    break;

                case PointerType.Exit:
                    if (Tween.Color(graphic, exitColor, exitIntensity, exitLerpType))
                    {
                        graphic.color = exitColor;
                        pointerType = PointerType.None;
                    }
                    break;
            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData) => pointerType = PointerType.Exit;
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) => pointerType = PointerType.Enter;
    }
}
