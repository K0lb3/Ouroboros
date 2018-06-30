namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class EventStandChara2 : MonoBehaviour
    {
        public const float FADEIN_TIME = 0.3f;
        public const float FADEOUT_TIME = 0.5f;
        public static List<EventStandChara2> Instances;
        public string CharaID;
        [HideInInspector]
        public bool mClose;
        public GameObject FaceObject;
        public GameObject BodyObject;
        private float[] AnchorPostionX;
        private float mFadeTime;
        private bool IsFading;
        private StateTypes mState;

        static EventStandChara2()
        {
            Instances = new List<EventStandChara2>();
            return;
        }

        public EventStandChara2()
        {
            this.AnchorPostionX = new float[] { -1f, 0.2f, 0.35f, 0.5f, 0.65f, 0.8f, 2f };
            base..ctor();
            return;
        }

        private void Awake()
        {
            Instances.Add(this);
            this.mFadeTime = 0f;
            this.IsFading = 0;
            return;
        }

        public void Close()
        {
            if (this.mClose == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mClose = 1;
            this.StartFadeOut();
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
            color = this.FaceObject.GetComponent<RawImage>().get_color();
            this.FaceObject.GetComponent<RawImage>().set_color(new Color(&color.r, &color.g, &color.b, Mathf.Lerp(1f, 0f, time)));
            color = this.BodyObject.GetComponent<RawImage>().get_color();
            this.BodyObject.GetComponent<RawImage>().set_color(new Color(&color.r, &color.g, &color.b, Mathf.Lerp(1f, 0f, time)));
            return;
        }

        private unsafe void FadeOut(float time)
        {
            Color color;
            color = this.FaceObject.GetComponent<RawImage>().get_color();
            this.FaceObject.GetComponent<RawImage>().set_color(new Color(&color.r, &color.g, &color.b, Mathf.Lerp(0f, 1f, time)));
            color = this.BodyObject.GetComponent<RawImage>().get_color();
            this.BodyObject.GetComponent<RawImage>().set_color(new Color(&color.r, &color.g, &color.b, Mathf.Lerp(0f, 1f, time)));
            return;
        }

        public static EventStandChara2 Find(string id)
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

        public float GetAnchorPostionX(int index)
        {
            return (((index < 0) || (index >= ((int) this.AnchorPostionX.Length))) ? 0f : this.AnchorPostionX[index]);
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

        private void OnDestroy()
        {
            Instances.Remove(this);
            this.mState = 3;
            return;
        }

        public void Open()
        {
            if (this.mClose != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mClose = 0;
            this.StartFadeIn();
            return;
        }

        public void StartFadeIn()
        {
            this.IsFading = 1;
            this.mFadeTime = 0.3f;
            this.mState = 0;
            return;
        }

        public void StartFadeOut()
        {
            this.IsFading = 1;
            this.mFadeTime = 0.5f;
            this.mState = 2;
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
            this.mFadeTime -= Time.get_deltaTime();
            if (this.mFadeTime > 0f)
            {
                goto Label_0041;
            }
            this.mFadeTime = 0f;
            this.IsFading = 0;
            return;
        Label_0041:
            if (this.mState != null)
            {
                goto Label_005D;
            }
            this.FadeIn(this.mFadeTime);
            goto Label_0075;
        Label_005D:
            if (this.mState != 2)
            {
                goto Label_0075;
            }
            this.FadeOut(this.mFadeTime);
        Label_0075:
            return;
        }

        public bool IsClose
        {
            get
            {
                return this.mClose;
            }
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
            OverLeft,
            Left,
            HLeft,
            Center,
            HRight,
            Right,
            OverRight,
            None
        }

        public enum StateTypes
        {
            FadeIn,
            Active,
            FadeOut,
            Inactive,
            None
        }
    }
}

