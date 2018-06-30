namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class EventStandCharaController2 : MonoBehaviour
    {
        private const float FADEIN = 0.5f;
        private const float FADEOUT = 0.3f;
        public static List<EventStandCharaController2> Instances;
        public GameObject[] StandCharaList;
        public string CharaID;
        private float[] AnchorPostionX;
        private int mCurrentIndex;
        private bool IsFading;
        private float mFadeTime;
        private string mEmotion;
        private bool mClose;
        private StateTypes mState;

        static EventStandCharaController2()
        {
            Instances = new List<EventStandCharaController2>();
            return;
        }

        public EventStandCharaController2()
        {
            this.AnchorPostionX = new float[] { -1f, 0.2f, 0.35f, 0.5f, 0.65f, 0.8f, 2f };
            this.mClose = 1;
            base..ctor();
            return;
        }

        private void Awake()
        {
            GameObject obj2;
            GameObject[] objArray;
            int num;
            Instances.Add(this);
            objArray = this.StandCharaList;
            num = 0;
            goto Label_0028;
        Label_0019:
            obj2 = objArray[num];
            obj2.SetActive(0);
            num += 1;
        Label_0028:
            if (num < ((int) objArray.Length))
            {
                goto Label_0019;
            }
            this.mCurrentIndex = 0;
            this.mEmotion = "normal";
            return;
        }

        private void ChangeStandChara(string name)
        {
            int num;
            num = this.FindIndex(name);
            if (num == -1)
            {
                goto Label_003C;
            }
            this.StandCharaList[this.mCurrentIndex].SetActive(0);
            this.mCurrentIndex = num;
            this.StandCharaList[this.mCurrentIndex].SetActive(1);
        Label_003C:
            return;
        }

        public void Close(float fade)
        {
            if (this.mClose == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
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

        private void FadeIn(float time)
        {
            base.GetComponent<CanvasGroup>().set_alpha(Mathf.Lerp(1f, 0f, time));
            return;
        }

        private void FadeOut(float time)
        {
            base.GetComponent<CanvasGroup>().set_alpha(Mathf.Lerp(0f, 1f, time));
            return;
        }

        private GameObject Find(string name)
        {
            int num;
            if (this.StandCharaList == null)
            {
                goto Label_0047;
            }
            num = ((int) this.StandCharaList.Length) - 1;
            goto Label_0040;
        Label_001B:
            if ((this.StandCharaList[num].get_name() == name) == null)
            {
                goto Label_003C;
            }
            return this.StandCharaList[num];
        Label_003C:
            num -= 1;
        Label_0040:
            if (num >= 0)
            {
                goto Label_001B;
            }
        Label_0047:
            return null;
        }

        private int FindIndex(string name)
        {
            int num;
            if (this.StandCharaList == null)
            {
                goto Label_0040;
            }
            num = ((int) this.StandCharaList.Length) - 1;
            goto Label_0039;
        Label_001B:
            if ((this.StandCharaList[num].get_name() == name) == null)
            {
                goto Label_0035;
            }
            return num;
        Label_0035:
            num -= 1;
        Label_0039:
            if (num >= 0)
            {
                goto Label_001B;
            }
        Label_0040:
            return -1;
        }

        public static EventStandCharaController2 FindInstances(string id)
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

        private void OnDestroy()
        {
            GameObject obj2;
            GameObject[] objArray;
            int num;
            Instances.Remove(this);
            objArray = this.StandCharaList;
            num = 0;
            goto Label_0029;
        Label_001A:
            obj2 = objArray[num];
            obj2.SetActive(0);
            num += 1;
        Label_0029:
            if (num < ((int) objArray.Length))
            {
                goto Label_001A;
            }
            this.mState = 3;
            this.mCurrentIndex = 0;
            this.mEmotion = "normal";
            return;
        }

        public void Open(float fade)
        {
            int num;
            if (this.mClose != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mClose = 0;
            this.StandCharaList[this.mCurrentIndex].SetActive(0);
            num = this.FindIndex(this.mEmotion);
            num = (num == -1) ? 0 : num;
            this.mCurrentIndex = num;
            this.StandCharaList[this.mCurrentIndex].SetActive(1);
            this.StartFadeIn(fade);
            return;
        }

        public void Open(string name)
        {
            this.ChangeStandChara(name);
            return;
        }

        private void Start()
        {
        }

        private void StartFadeIn(float fade)
        {
            this.IsFading = 1;
            this.mFadeTime = fade;
            this.mState = 0;
            if (this.mFadeTime > 0f)
            {
                goto Label_0035;
            }
            base.GetComponent<CanvasGroup>().set_alpha(1f);
        Label_0035:
            return;
        }

        private void StartFadeOut(float fade)
        {
            this.IsFading = 1;
            this.mFadeTime = fade;
            this.mState = 2;
            if (this.mFadeTime > 0f)
            {
                goto Label_0035;
            }
            base.GetComponent<CanvasGroup>().set_alpha(0f);
        Label_0035:
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

        public void UpdateEmotion(string name)
        {
            this.ChangeStandChara(name);
            return;
        }

        public bool Fading
        {
            get
            {
                return this.IsFading;
            }
        }

        public string Emotion
        {
            get
            {
                return this.mEmotion;
            }
            set
            {
                this.mEmotion = value;
                return;
            }
        }

        public bool IsClose
        {
            get
            {
                return this.mClose;
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

