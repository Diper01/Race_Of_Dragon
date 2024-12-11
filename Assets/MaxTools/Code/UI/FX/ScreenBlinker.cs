using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace MaxTools
{
    using MaxTools.Extensions;

    public class ScreenBlinker : Singleton<ScreenBlinker>
    {
        public class Blink
        {
            public Color color;

            public float riseTime;
            public float middleTime;
            public float fadeTime;

            public int sortingOrder;
            public bool raycastTarget;
            public Sprite sprite;

            public Action startAction;
            public Action middleAction;
            public Action finalAction;

            public TimeMode timeMode;

            public Blink() { }
            public Blink(Color color)
            {
                this.color = color;
            }
            public Blink(Color color, float riseTime)
            {
                this.color = color;
                this.riseTime = riseTime;
            }
            public Blink(Color color, float riseTime, float middleTime)
            {
                this.color = color;
                this.riseTime = riseTime;
                this.middleTime = middleTime;
            }
            public Blink(Color color, float riseTime, float middleTime, float fadeTime)
            {
                this.color = color;
                this.riseTime = riseTime;
                this.middleTime = middleTime;
                this.fadeTime = fadeTime;
            }
            public Blink(Color color, float riseTime, float middleTime, float fadeTime, int sortingOrder)
            {
                this.color = color;
                this.riseTime = riseTime;
                this.middleTime = middleTime;
                this.fadeTime = fadeTime;
                this.sortingOrder = sortingOrder;
            }

            public Blink SetColor(Color color)
            {
                this.color = color;

                return this;
            }

            public Blink SetRiseTime(float riseTime)
            {
                this.riseTime = riseTime;

                return this;
            }
            public Blink SetMiddleTime(float middleTime)
            {
                this.middleTime = middleTime;

                return this;
            }
            public Blink SetFadeTime(float fadeTime)
            {
                this.fadeTime = fadeTime;

                return this;
            }

            public Blink SetTotalTime(float totalTime)
            {
                riseTime = totalTime * 0.5f;
                middleTime = 0.0f;
                fadeTime = totalTime * 0.5f;

                return this;
            }
            public Blink SetTotalTimeDelay(float totalTime)
            {
                riseTime = totalTime * (1.0f / 3.0f);
                middleTime = totalTime * (1.0f / 3.0f);
                fadeTime = totalTime * (1.0f / 3.0f);

                return this;
            }

            public Blink SetSortingOrder(int sortingOrder)
            {
                this.sortingOrder = sortingOrder;

                return this;
            }
            public Blink SetRaycastTarget(bool raycastTarget)
            {
                this.raycastTarget = raycastTarget;

                return this;
            }
            public Blink SetSprite(Sprite sprite)
            {
                this.sprite = sprite;

                return this;
            }

            public Blink SetStartAction(Action startAction)
            {
                this.startAction = startAction;

                return this;
            }
            public Blink SetMiddleAction(Action middleAction)
            {
                this.middleAction = middleAction;

                return this;
            }
            public Blink SetFinalAction(Action finalAction)
            {
                this.finalAction = finalAction;

                return this;
            }

            public Blink SetTimeMode(TimeMode timeMode)
            {
                this.timeMode = timeMode;

                return this;
            }

            IEnumerator c_Run()
            {
                var canvasObject = instance.canvasPool.GetFromPool();
                canvasObject.transform.SetParent(instance.transform);
                canvasObject.SetActive(true);

                if (sprite == null)
                {
                    canvasObject.name = $"{color}";
                }
                else
                    canvasObject.name = $"{color}-[{sprite.name}]";

                var canvas = canvasObject.GetComponent<Canvas>();
                canvas.sortingOrder = sortingOrder;

                var image = canvasObject.GetComponentInChildren<Image>();
                image.color = color.GetWithAlpha01(0.0f);
                image.raycastTarget = raycastTarget;
                image.sprite = sprite;

                startAction?.Invoke();

                if (riseTime > 0.0f)
                {
                    yield return TweenCoroutine.Alpha(
                        image, color.a, color.a / riseTime, LerpType.Towards, timeMode);
                }
                else
                    image.SetAlpha01(color.a);

                if (middleTime > 0.0f)
                {
                    yield return c_Delay(middleTime * 0.5f);

                    middleAction?.Invoke();

                    yield return c_Delay(middleTime * 0.5f);
                }
                else
                    middleAction?.Invoke();

                if (fadeTime > 0.0f)
                {
                    yield return TweenCoroutine.Alpha(
                        image, 0.0f, image.color.a / fadeTime, LerpType.Towards, timeMode);
                }
                else
                    image.SetAlpha01(0.0f);

                finalAction?.Invoke();

                instance.canvasPool.AddToPool(canvasObject);
            }
            IEnumerator c_Delay(float delay)
            {
                if (timeMode == TimeMode.ScaledTime)
                {
                    yield return new WaitForSeconds(delay);
                }
                else
                    yield return new WaitForSecondsRealtime(delay);
            }

            public void Run()
            {
                instance.StartCoroutine(c_Run());
            }
        }

        GamePool canvasPool = null;

        new void Awake()
        {
            base.Awake();

            var canvasPrefab = new GameObject("CanvasPrefab");
            canvasPrefab.transform.SetParent(transform);
            canvasPrefab.SetActive(false);

            var canvas = canvasPrefab.AddComponent<Canvas>();
            canvasPrefab.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var image = new GameObject("Image").AddComponent<Image>();
            image.transform.SetParent(canvas.transform);

            image.rectTransform.anchorMin = Vector2.zero;
            image.rectTransform.anchorMax = Vector2.one;
            image.rectTransform.offsetMin = Vector2.zero;
            image.rectTransform.offsetMax = Vector2.zero;

            canvasPool = new GamePool(canvasPrefab);
        }

        public static Blink NewBlink()
        {
            return new Blink();
        }
        public static Blink NewBlink(Color color)
        {
            return new Blink(color);
        }
        public static Blink NewBlink(Color color, float riseTime)
        {
            return new Blink(color, riseTime);
        }
        public static Blink NewBlink(Color color, float riseTime, float middleTime)
        {
            return new Blink(color, riseTime, middleTime);
        }
        public static Blink NewBlink(Color color, float riseTime, float middleTime, float fadeTime)
        {
            return new Blink(color, riseTime, middleTime, fadeTime);
        }
        public static Blink NewBlink(Color color, float riseTime, float middleTime, float fadeTime, int sortingOrder)
        {
            return new Blink(color, riseTime, middleTime, fadeTime, sortingOrder);
        }

        public static void StopAll()
        {
            instance.StopAllCoroutines();

            for (int i = 1; i < instance.transform.childCount; ++i)
            {
                instance.canvasPool.AddToPool(instance.transform.GetChild(i).gameObject);
            }
        }
    }
}
