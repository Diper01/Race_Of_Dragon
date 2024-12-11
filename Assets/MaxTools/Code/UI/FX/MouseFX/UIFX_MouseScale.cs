using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MaxTools
{
    using MaxTools.Extensions;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Graphic))]
    public class UIFX_MouseScale : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler
    {
        [SerializeField]
        [Range01] float pressedSize = 0.8f;

        [SerializeField]
        PointerEventData.InputButton mouseButton =
        PointerEventData.InputButton.Left;

        bool isPressed;

        Image image;
        RectTransform rectTransform;

        Sprite defaultSprite;
        Sprite pressedSprite;

        Vector3 defaultScale;
        Vector3 pressedScale;

        Vector2 defaultRectSize;
        Vector2 pressedRectSize;

        void Start()
        {
            image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();

            defaultScale = transform.localScale;
            pressedScale = defaultScale * pressedSize;

            defaultRectSize = rectTransform.rect.size;
            pressedRectSize = rectTransform.rect.size / pressedScale;

            if (image)
            {
                var scaledTexture = Instantiate(image.sprite.texture);
                scaledTexture.name = $"{image.sprite.name}_[{pressedSize:F2}]";

                int delta_w = (int)(scaledTexture.width * (1.0f - pressedSize));
                int delta_h = (int)(scaledTexture.height * (1.0f - pressedSize));

                if (delta_w % 2 != 0) delta_w++;
                if (delta_h % 2 != 0) delta_h++;

                scaledTexture.Expand(delta_w, delta_h);
                scaledTexture.Apply(false, true);

                defaultSprite = image.sprite;
                pressedSprite = scaledTexture.MakeSprite();
            }
        }

        void Update()
        {
            if (isPressed)
            {
                if ((Mouse.button0Up && mouseButton == PointerEventData.InputButton.Left) ||
                    (Mouse.button2Up && mouseButton == PointerEventData.InputButton.Middle) ||
                    (Mouse.button1Up && mouseButton == PointerEventData.InputButton.Right))
                {
                    SetDefaultSize();

                    isPressed = false;
                }
            }
        }

        void SetDefaultSize()
        {
            if (!image)
            {
                rectTransform.localScale = defaultScale;
                rectTransform.SetRectSize(defaultRectSize);
            }
            else
                image.sprite = defaultSprite;
        }
        void SetPressedSize()
        {
            if (!image)
            {
                rectTransform.localScale = pressedScale;
                rectTransform.SetRectSize(pressedRectSize);
            }
            else
                image.sprite = pressedSprite;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (isPressed)
            {
                SetDefaultSize();
            }
        }
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (isPressed)
            {
                SetPressedSize();
            }
        }
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!isPressed)
            {
                if (eventData.button == mouseButton)
                {
                    SetPressedSize();

                    isPressed = true;
                }
            }
        }
    }
}
