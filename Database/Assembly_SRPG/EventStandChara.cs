namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class EventStandChara : MonoBehaviour
    {
        public const float FADEIN_TIME = 0.3f;
        public const float FADEOUT_TIME = 0.5f;
        public static List<EventStandChara> Instances;
        public string CharaID;
        public bool mClose;
        private float[] PositionX;
        private float FadeTime;
        private bool IsFading;
        private StateTypes mState;

        static EventStandChara()
        {
            Instances = new List<EventStandChara>();
            return;
        }

        public EventStandChara()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            Instances.Add(this);
            this.FadeTime = 0f;
            this.IsFading = 0;
            return;
        }

        public void Close(float fade)
        {
            this.mClose = 1;
            this.StartFadeOut(fade);
            return;
        }

        public static void DiscardAll()
        {
            int num;
            num = Instances.Count - 1;
            goto Label_0045;
        Label_0012:
            if (Instances[num].get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0041;
            }
            Object.Destroy(Instances[num].get_gameObject());
        Label_0041:
            num -= 1;
        Label_0045:
            if (num >= 0)
            {
                goto Label_0012;
            }
            Instances.Clear();
            return;
        }

        private unsafe void FadeIn(float time)
        {
            Color color;
            color = base.get_gameObject().GetComponent<RawImage>().get_color();
            base.get_gameObject().GetComponent<RawImage>().set_color(new Color(&color.r, &color.g, &color.b, Mathf.Lerp(1f, 0f, time)));
            return;
        }

        private unsafe void FadeOut(float time)
        {
            Color color;
            color = base.get_gameObject().GetComponent<RawImage>().get_color();
            base.get_gameObject().GetComponent<RawImage>().set_color(new Color(&color.r, &color.g, &color.b, Mathf.Lerp(0f, 1f, time)));
            return;
        }

        public static EventStandChara Find(string id)
        {
            int num;
            num = Instances.Count - 1;
            goto Label_003D;
        Label_0012:
            if ((Instances[num].CharaID == id) == null)
            {
                goto Label_0039;
            }
            return Instances[num];
        Label_0039:
            num -= 1;
        Label_003D:
            if (num >= 0)
            {
                goto Label_0012;
            }
            return null;
        }

        public static string[] GetCharaIDs()
        {
            List<string> list;
            int num;
            list = new List<string>();
            num = Instances.Count - 1;
            goto Label_0032;
        Label_0018:
            list.Add(Instances[num].CharaID);
            num -= 1;
        Label_0032:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return list.ToArray();
        }

        public float GetPositionX(int index)
        {
            return (((index < 0) || (index >= ((int) this.PositionX.Length))) ? 0f : this.PositionX[index]);
        }

        public unsafe void InitPositionX(RectTransform canvasRect)
        {
            float[] singleArray1;
            RectTransform transform;
            Rect rect;
            Rect rect2;
            Rect rect3;
            Rect rect4;
            Rect rect5;
            Rect rect6;
            Rect rect7;
            transform = base.GetComponent<RectTransform>();
            singleArray1 = new float[] { &transform.get_rect().get_width() / 2f, &canvasRect.get_rect().get_width() / 2f, &canvasRect.get_rect().get_width() - (&transform.get_rect().get_width() / 2f), -&transform.get_rect().get_width() / 2f, &canvasRect.get_rect().get_width() + (&transform.get_rect().get_width() / 2f) };
            this.PositionX = singleArray1;
            return;
        }

        private void OnDestroy()
        {
            Instances.Remove(this);
            this.mState = 3;
            return;
        }

        public void Open(float fade)
        {
            this.mClose = 0;
            this.StartFadeIn(fade);
            return;
        }

        public unsafe void StartFadeIn(float fade)
        {
            Color color;
            this.IsFading = 1;
            this.FadeTime = fade;
            this.mState = 0;
            if (this.FadeTime > 0f)
            {
                goto Label_0065;
            }
            color = base.get_gameObject().GetComponent<RawImage>().get_color();
            base.get_gameObject().GetComponent<RawImage>().set_color(new Color(&color.r, &color.g, &color.b, 1f));
        Label_0065:
            return;
        }

        public unsafe void StartFadeOut(float fade)
        {
            Color color;
            this.IsFading = 1;
            this.FadeTime = fade;
            this.mState = 2;
            if (this.FadeTime > 0f)
            {
                goto Label_0065;
            }
            color = base.get_gameObject().GetComponent<RawImage>().get_color();
            base.get_gameObject().GetComponent<RawImage>().set_color(new Color(&color.r, &color.g, &color.b, 0f));
        Label_0065:
            return;
        }

        private void Update()
        {
            if (this.IsFading != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.FadeTime -= Time.get_deltaTime();
            if (this.FadeTime > 0f)
            {
                goto Label_0041;
            }
            this.FadeTime = 0f;
            this.IsFading = 0;
            return;
        Label_0041:
            if (this.mState != null)
            {
                goto Label_0058;
            }
            this.FadeIn(this.FadeTime);
        Label_0058:
            if (this.mState != 2)
            {
                goto Label_0070;
            }
            this.FadeOut(this.FadeTime);
        Label_0070:
            return;
        }

        public bool Fading
        {
            get
            {
                return this.IsFading;
            }
        }

        public StateTypes State
        {
            get
            {
                return this.mState;
            }
            set
            {
                this.mState = value;
                return;
            }
        }

        public enum PositionTypes
        {
            Left,
            Center,
            Right,
            OverLeft,
            OverRight
        }

        public enum StateTypes
        {
            FadeIn,
            Active,
            FadeOut,
            Inactive
        }
    }
}

