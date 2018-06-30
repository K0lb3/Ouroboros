namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [RequireComponent(typeof(ScrollListController))]
    public class ScrollClamped_TownMenu : MonoBehaviour, ScrollListSetUp
    {
        private readonly float OFFSET_X_MIN;
        private readonly float OFFSET_X;
        private readonly float OFFSET_Y;
        public float Space;
        public int Max;
        public RectTransform ViewObj;
        public ScrollAutoFit AutoFit;
        public GameObject Mask;
        public Button back;
        private ScrollListController mController;
        private float mOffset;
        private float mStartPos;
        private float mCenter;
        private MENU_ID mSelectIdx;
        private bool mIsSelected;
        private GameObject ordealObj;
        [CompilerGenerated]
        private static Func<UnlockParam, bool> <>f__am$cache10;

        public ScrollClamped_TownMenu()
        {
            this.OFFSET_X_MIN = 30f;
            this.OFFSET_X = 60f;
            this.OFFSET_Y = 15f;
            this.Space = 1f;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <OnAfterStartup>m__3F9(UnlockParam unlock)
        {
            return (unlock.UnlockTarget == 0x20000);
        }

        private void OnAfterStartup(bool success)
        {
            GameManager manager;
            UnlockParam[] paramArray;
            UnlockParam param;
            bool flag;
            manager = MonoSingleton<GameManager>.Instance;
            if (<>f__am$cache10 != null)
            {
                goto Label_002B;
            }
            <>f__am$cache10 = new Func<UnlockParam, bool>(ScrollClamped_TownMenu.<OnAfterStartup>m__3F9);
        Label_002B:
            param = Enumerable.FirstOrDefault<UnlockParam>(manager.MasterParam.Unlocks, <>f__am$cache10);
            if (param != null)
            {
                goto Label_003D;
            }
            return;
        Label_003D:
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ORDEAL_RELEASE_ANIMATION_PLAYED) != null)
            {
                goto Label_0085;
            }
            if (manager.Player.Lv < param.PlayerLevel)
            {
                goto Label_0085;
            }
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.ORDEAL_RELEASE_ANIMATION_PLAYED, 1, 0);
            base.StartCoroutine(this.OrdealReleaseAnimationCoroutine(this.ordealObj, param));
        Label_0085:
            return;
        }

        public unsafe void OnClick(GameObject obj)
        {
            List<RectTransform> list;
            List<Vector2> list2;
            RectTransform transform;
            int num;
            float num2;
            ImageArray array;
            <OnClick>c__AnonStorey39F storeyf;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            if ((this.AutoFit != null) == null)
            {
                goto Label_013C;
            }
            if (this.mSelectIdx != -1)
            {
                goto Label_013C;
            }
            storeyf = new <OnClick>c__AnonStorey39F();
            if (&this.AutoFit.get_velocity().x <= this.AutoFit.ItemScale)
            {
                goto Label_0049;
            }
            return;
        Label_0049:
            list = this.mController.ItemList;
            list2 = this.mController.ItemPosList;
            transform = base.get_gameObject().GetComponent<RectTransform>();
            storeyf.rect = obj.GetComponent<RectTransform>();
            if ((this.AutoFit != null) == null)
            {
                goto Label_013C;
            }
            num = list.FindIndex(new Predicate<RectTransform>(storeyf.<>m__3FB));
            if (num == -1)
            {
                goto Label_013C;
            }
            vector3 = list2[num];
            num2 = &transform.get_anchoredPosition().x - (&vector3.x - this.mCenter);
            this.AutoFit.SetScrollToHorizontal(num2);
            array = obj.GetComponent<ImageArray>();
            if ((array != null) == null)
            {
                goto Label_0102;
            }
            this.mSelectIdx = array.ImageIndex;
        Label_0102:
            if ((this.Mask != null) == null)
            {
                goto Label_011F;
            }
            this.Mask.SetActive(1);
        Label_011F:
            if ((this.back != null) == null)
            {
                goto Label_013C;
            }
            this.back.set_interactable(0);
        Label_013C:
            return;
        }

        public void OnNext()
        {
            RectTransform transform;
            int num;
            if ((base.GetComponent<RectTransform>() != null) == null)
            {
                goto Label_0068;
            }
            if ((this.mController != null) == null)
            {
                goto Label_0068;
            }
            if ((this.AutoFit != null) == null)
            {
                goto Label_0068;
            }
            num = this.AutoFit.GetCurrent();
            this.AutoFit.SetScrollToHorizontal((((float) (num - 1)) * this.AutoFit.ItemScale) + this.AutoFit.Offset);
        Label_0068:
            return;
        }

        public void OnPrev()
        {
            RectTransform transform;
            int num;
            if ((base.GetComponent<RectTransform>() != null) == null)
            {
                goto Label_0068;
            }
            if ((this.mController != null) == null)
            {
                goto Label_0068;
            }
            if ((this.AutoFit != null) == null)
            {
                goto Label_0068;
            }
            num = this.AutoFit.GetCurrent();
            this.AutoFit.SetScrollToHorizontal((((float) (num + 1)) * this.AutoFit.ItemScale) + this.AutoFit.Offset);
        Label_0068:
            return;
        }

        public unsafe void OnSetUpItems()
        {
            float num;
            RectTransform transform;
            Vector2 vector;
            Vector2 vector2;
            float num2;
            Rect rect;
            this.mController = base.GetComponent<ScrollListController>();
            this.mController.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this, this.OnUpdateItems));
            this.mController.OnUpdateItemEvent.AddListener(new UnityAction<List<RectTransform>>(this, this.OnUpdateScale));
            this.mController.OnAfterStartup.AddListener(new UnityAction<bool>(this, this.OnAfterStartup));
            num = 0f;
            if ((this.ViewObj != null) == null)
            {
                goto Label_0093;
            }
            num = &this.ViewObj.get_rect().get_width() * 0.5f;
        Label_0093:
            transform = base.GetComponent<RectTransform>();
            vector = transform.get_sizeDelta();
            vector2 = transform.get_anchoredPosition();
            num2 = this.mController.ItemScale * this.Space;
            &vector2.x = num - (this.mController.ItemScale * 0.5f);
            &vector.x = num2 * ((float) this.Max);
            transform.set_sizeDelta(vector);
            transform.set_anchoredPosition(vector2);
            this.mStartPos = &vector2.x;
            this.mOffset = this.mController.ItemScale * 0.5f;
            if ((this.AutoFit != null) == null)
            {
                goto Label_0148;
            }
            this.AutoFit.ItemScale = num2;
            this.AutoFit.Offset = this.mStartPos;
        Label_0148:
            return;
        }

        public void OnUpdateItems(int idx, GameObject obj)
        {
            int num;
            ImageArray array;
            LevelLock @lock;
            bool flag;
            if (this.Max == null)
            {
                goto Label_001C;
            }
            if ((this.mController == null) == null)
            {
                goto Label_0023;
            }
        Label_001C:
            obj.SetActive(0);
        Label_0023:
            num = idx % this.Max;
            if (num >= 0)
            {
                goto Label_0048;
            }
            num = Mathf.Abs(this.Max + num) % this.Max;
        Label_0048:
            array = obj.GetComponent<ImageArray>();
            if ((array != null) == null)
            {
                goto Label_0062;
            }
            array.ImageIndex = num;
        Label_0062:
            this.SetReleaseStoryPartAction(obj, num);
            @lock = obj.GetComponentInChildren<LevelLock>(1);
            if ((@lock != null) == null)
            {
                goto Label_00E5;
            }
            flag = 0;
            if (num == 5)
            {
                goto Label_0095;
            }
            if (num == 4)
            {
                goto Label_0095;
            }
            if (num != 2)
            {
                goto Label_00DE;
            }
        Label_0095:
            flag = 1;
            if (num != 5)
            {
                goto Label_00B5;
            }
            @lock.Condition = 0x20000;
            this.ordealObj = obj;
            goto Label_00DE;
        Label_00B5:
            if (num != 4)
            {
                goto Label_00CC;
            }
            @lock.Condition = 0x100000;
            goto Label_00DE;
        Label_00CC:
            if (num != 2)
            {
                goto Label_00DE;
            }
            @lock.Condition = 0x400000;
        Label_00DE:
            @lock.set_enabled(flag);
        Label_00E5:
            return;
        }

        public unsafe void OnUpdateScale(List<RectTransform> rects)
        {
            RectTransform transform;
            List<Vector2> list;
            List<Vector2> list2;
            int num;
            RectTransform transform2;
            float num2;
            float num3;
            float num4;
            float num5;
            Vector2 vector;
            float num6;
            float num7;
            float num8;
            float num9;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            if ((this.mController == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            transform = base.GetComponent<RectTransform>();
            if ((transform != null) == null)
            {
                goto Label_0048;
            }
            this.mCenter = (this.mStartPos - &transform.get_anchoredPosition().x) + this.mOffset;
        Label_0048:
            list = this.mController.ItemPosList;
            list2 = new List<Vector2>();
            num = 0;
            goto Label_01FD;
        Label_0061:
            transform2 = rects[num];
            if ((transform2 != null) == null)
            {
                goto Label_01F9;
            }
            list2.Add(transform2.get_anchoredPosition());
            vector3 = list[num];
            num2 = Mathf.Abs(&vector3.x - this.mCenter);
            num3 = (this.mController.ItemScale * 2f) * this.Space;
            num4 = Mathf.Clamp(1f - (num2 / num3), 0f, 1f);
            num5 = 0.7f + (0.3f * num4);
            transform2.get_transform().set_localScale(new Vector3(num5, num5, num5));
            vector = list[num];
            num6 = this.OFFSET_X - (this.OFFSET_X * num4);
            num7 = this.OFFSET_Y - (this.OFFSET_Y * num4);
            if (num4 < 0.5f)
            {
                goto Label_0167;
            }
            num8 = Mathf.Clamp((1f - num4) * 2f, 0f, 1f);
            num6 = this.OFFSET_X_MIN * num8;
            goto Label_0199;
        Label_0167:
            num9 = Mathf.Clamp(num4 * 2f, 0f, 1f);
            num6 = this.OFFSET_X_MIN + (this.OFFSET_X - (this.OFFSET_X * num9));
        Label_0199:
            vector4 = list[num];
            if (this.mCenter >= &vector4.x)
            {
                goto Label_01D9;
            }
            transform2.set_anchoredPosition(new Vector2(&vector.x - num6, &vector.y + num7));
            goto Label_01F9;
        Label_01D9:
            transform2.set_anchoredPosition(new Vector2(&vector.x + num6, &vector.y + num7));
        Label_01F9:
            num += 1;
        Label_01FD:
            if (num < rects.Count)
            {
                goto Label_0061;
            }
            return;
        }

        [DebuggerHidden]
        private IEnumerator OrdealReleaseAnimationCoroutine(GameObject obj, UnlockParam lockState)
        {
            <OrdealReleaseAnimationCoroutine>c__Iterator13B iteratorb;
            iteratorb = new <OrdealReleaseAnimationCoroutine>c__Iterator13B();
            iteratorb.obj = obj;
            iteratorb.lockState = lockState;
            iteratorb.<$>obj = obj;
            iteratorb.<$>lockState = lockState;
            iteratorb.<>f__this = this;
            return iteratorb;
        }

        private unsafe void SetCenter(GameObject obj)
        {
            List<RectTransform> list;
            List<Vector2> list2;
            RectTransform transform;
            int num;
            float num2;
            <SetCenter>c__AnonStorey39E storeye;
            Vector2 vector;
            Vector2 vector2;
            storeye = new <SetCenter>c__AnonStorey39E();
            list = this.mController.ItemList;
            list2 = this.mController.ItemPosList;
            transform = base.get_gameObject().GetComponent<RectTransform>();
            storeye.rect = obj.GetComponent<RectTransform>();
            if ((this.AutoFit != null) == null)
            {
                goto Label_009E;
            }
            num = list.FindIndex(new Predicate<RectTransform>(storeye.<>m__3FA));
            if (num == -1)
            {
                goto Label_009E;
            }
            vector2 = list2[num];
            num2 = &transform.get_anchoredPosition().x - (&vector2.x - this.mCenter);
            this.AutoFit.SetScrollToHorizontal(num2);
        Label_009E:
            return;
        }

        private void SetReleaseStoryPartAction(GameObject obj, int image_idx)
        {
            LevelLock @lock;
            if (image_idx == null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart() == null)
            {
                goto Label_0029;
            }
            obj.GetComponent<LevelLock>().ReleaseStoryPart.SetActive(1);
        Label_0029:
            return;
        }

        public void Start()
        {
            this.mSelectIdx = -1;
            this.mIsSelected = 0;
            return;
        }

        private void Update()
        {
            string str;
            MENU_ID menu_id;
            if (this.mSelectIdx == -1)
            {
                goto Label_00E4;
            }
            if (this.mIsSelected != null)
            {
                goto Label_00E4;
            }
            if ((this.AutoFit != null) == null)
            {
                goto Label_00E4;
            }
            if (this.AutoFit.IsMove != null)
            {
                goto Label_00E4;
            }
            str = string.Empty;
            switch (this.mSelectIdx)
            {
                case 0:
                    goto Label_006C;

                case 1:
                    goto Label_0077;

                case 2:
                    goto Label_009E;

                case 3:
                    goto Label_0093;

                case 4:
                    goto Label_00AF;

                case 5:
                    goto Label_00C0;

                case 6:
                    goto Label_0088;
            }
            goto Label_00CB;
        Label_006C:
            str = "CLICK_STORY";
            goto Label_00CB;
        Label_0077:
            str = "CLICK_EVENT";
            GlobalVars.ReqEventPageListType = 0;
            goto Label_00CB;
        Label_0088:
            str = "CLICK_MULTI";
            goto Label_00CB;
        Label_0093:
            str = "CLICK_CHARA";
            goto Label_00CB;
        Label_009E:
            str = "CLICK_TOWER";
            GlobalVars.ReqEventPageListType = 2;
            goto Label_00CB;
        Label_00AF:
            str = "CLICK_KEY";
            GlobalVars.ReqEventPageListType = 1;
            goto Label_00CB;
        Label_00C0:
            str = "CLICK_ORDEAL";
        Label_00CB:
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00E4;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, str);
            this.mIsSelected = 1;
        Label_00E4:
            return;
        }

        [CompilerGenerated]
        private sealed class <OnClick>c__AnonStorey39F
        {
            internal RectTransform rect;

            public <OnClick>c__AnonStorey39F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3FB(RectTransform data)
            {
                return (data == this.rect);
            }
        }

        [CompilerGenerated]
        private sealed class <OrdealReleaseAnimationCoroutine>c__Iterator13B : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject obj;
            internal LevelLock <llock>__0;
            internal UnlockParam lockState;
            internal Animator <animator>__1;
            internal AnimatorStateInfo <animState>__2;
            internal int $PC;
            internal object $current;
            internal GameObject <$>obj;
            internal UnlockParam <$>lockState;
            internal ScrollClamped_TownMenu <>f__this;

            public <OrdealReleaseAnimationCoroutine>c__Iterator13B()
            {
                base..ctor();
                return;
            }

            private void <>__Finally0()
            {
                SRPG_TouchInputModule.UnlockInput(0);
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                uint num;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0031;

                    case 1:
                        goto Label_0025;

                    case 2:
                        goto Label_0025;
                }
                goto Label_0031;
            Label_0025:
                try
                {
                    goto Label_0031;
                }
                finally
                {
                Label_002A:
                    this.<>__Finally0();
                }
            Label_0031:
                return;
            }

            public unsafe bool MoveNext()
            {
                uint num;
                bool flag;
                bool flag2;
                num = this.$PC;
                this.$PC = -1;
                flag = 0;
                switch (num)
                {
                    case 0:
                        goto Label_0027;

                    case 1:
                        goto Label_002F;

                    case 2:
                        goto Label_002F;
                }
                goto Label_0154;
            Label_0027:
                SRPG_TouchInputModule.LockInput();
                num = -3;
            Label_002F:
                try
                {
                    switch ((num - 1))
                    {
                        case 0:
                            goto Label_0054;

                        case 1:
                            goto Label_0127;
                    }
                    this.$current = null;
                    this.$PC = 1;
                    flag = 1;
                    goto Label_0156;
                Label_0054:
                    this.<>f__this.SetCenter(this.obj);
                    this.<llock>__0 = this.obj.GetComponentInChildren<LevelLock>(1);
                    this.<llock>__0.set_enabled(0);
                    this.<llock>__0.ConditionText.set_text(string.Format(LocalizedText.Get("sys.UNLOCK_LV"), (int) this.lockState.PlayerLevel));
                    this.<animator>__1 = this.obj.GetComponentInChildren<Animator>(1);
                    this.<animator>__1.get_gameObject().SetActive(1);
                    this.<animator>__1.SetBool("open", 1);
                Label_00E6:
                    this.<animState>__2 = this.<animator>__1.GetCurrentAnimatorStateInfo(0);
                    if (&this.<animState>__2.IsName("opened") == null)
                    {
                        goto Label_0112;
                    }
                    goto Label_012C;
                Label_0112:
                    this.$current = null;
                    this.$PC = 2;
                    flag = 1;
                    goto Label_0156;
                Label_0127:
                    goto Label_00E6;
                Label_012C:
                    this.<animator>__1.get_gameObject().SetActive(0);
                    goto Label_014D;
                }
                finally
                {
                Label_0142:
                    if (flag == null)
                    {
                        goto Label_0146;
                    }
                Label_0146:
                    this.<>__Finally0();
                }
            Label_014D:
                this.$PC = -1;
            Label_0154:
                return 0;
            Label_0156:
                return 1;
                return flag2;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <SetCenter>c__AnonStorey39E
        {
            internal RectTransform rect;

            public <SetCenter>c__AnonStorey39E()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3FA(RectTransform data)
            {
                return (data == this.rect);
            }
        }

        private enum MENU_ID
        {
            None = -1,
            Story = 0,
            Event = 1,
            Tower = 2,
            Chara = 3,
            Key = 4,
            Ordeal = 5,
            Multi = 6
        }
    }
}

