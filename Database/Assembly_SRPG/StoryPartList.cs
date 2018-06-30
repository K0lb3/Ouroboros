namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0x68, "ストーリーパートのアイコン選択後の移動が終わった", 1, 0x68), Pin(1, "アイコン選択後の挙動", 0, 1), Pin(100, "解放されているアイコンが選択された", 1, 100), Pin(0x65, "ロックされているアイコンが選択された", 1, 0x65), Pin(0x66, "ストーリーパート解放演出再生", 1, 0x66), Pin(0x67, "ストーリーパートのアイコンを押した", 1, 0x67)]
    public class StoryPartList : ScrollContentsInfo, IFlowInterface
    {
        private const int PIN_SELECT_ICON_ACTION = 1;
        private const int PIN_SELECT_RELEASE_ICON = 100;
        private const int PIN_SELECT_LOCK_ICON = 0x65;
        private const int PIN_PLAY_RELEASE_ANIMATION = 0x66;
        private const int PIN_PUTON_ICON = 0x67;
        private const int PIN_MOVE_END = 0x68;
        public string WorldMapControllerID;
        [SerializeField]
        private GameObject TemplateGo;
        [SerializeField]
        private GameObject ScrollArea;
        [SerializeField]
        private GameObject PageNext;
        [SerializeField]
        private GameObject PagePrev;
        [SerializeField]
        private GameObject TogglePagesGroup;
        [SerializeField]
        private GameObject TemplatePageIcon;
        private bool SetRangeFlag;
        private QuestSectionList mQuestSectionList;
        private RectTransform mMoveRect;
        private bool AnimationReleaseFlag;
        private StoryPartIcon ReleaseStoryPartIcon;
        private bool mReleaseAction;
        private int mSelectIconNum;
        private Button mNextButton;
        private Button mPrevButton;
        private StoryPartIcon mSelectIcon;
        private bool mCheckSelectIconMoveFlag;
        private List<StoryPartIcon> mStoryPartIconList;
        private List<Toggle> mPageIconList;
        private StoryPartIcon mSelectBeforeIcon;

        public StoryPartList()
        {
            this.mStoryPartIconList = new List<StoryPartIcon>();
            this.mPageIconList = new List<Toggle>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            string str;
            string str2;
            string str3;
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_000E;
            }
            goto Label_009D;
        Label_000E:
            if ((this.mSelectIcon != null) == null)
            {
                goto Label_009D;
            }
            if (this.mSelectIcon.LockFlag != null)
            {
                goto Label_005C;
            }
            GlobalVars.SelectedStoryPart.Set(this.mSelectIcon.StoryNum);
            this.mQuestSectionList.Refresh();
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_0098;
        Label_005C:
            str = MonoSingleton<GameManager>.Instance.GetReleaseStoryPartWorldName(this.mSelectIcon.StoryNum);
            if (str != null)
            {
                goto Label_0079;
            }
            return;
        Label_0079:
            str3 = string.Format(LocalizedText.Get("sys.STORYPART_RELEASE_TIMING"), str);
            UIUtility.SystemMessage(null, str3, null, null, 0, -1);
        Label_0098:;
        Label_009D:
            return;
        }

        public override bool CheckRangePos(float pos)
        {
            bool flag;
            flag = 0;
            if (pos <= base.mStartPosX)
            {
                goto Label_0015;
            }
            flag = 1;
            goto Label_0023;
        Label_0015:
            if (pos >= base.mEndPosX)
            {
                goto Label_0023;
            }
            flag = 1;
        Label_0023:
            return flag;
        }

        public override unsafe float GetNearIconPos(float pos)
        {
            float num;
            float num2;
            int num3;
            StoryPartIcon icon;
            Transform transform;
            IEnumerator enumerator;
            RectTransform transform2;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            IDisposable disposable;
            num = pos;
            num2 = 0f;
            num3 = 0;
            icon = null;
            enumerator = base.get_transform().GetEnumerator();
        Label_0019:
            try
            {
                goto Label_00B8;
            Label_001E:
                transform = (Transform) enumerator.Current;
                if (transform.get_gameObject().get_activeSelf() != null)
                {
                    goto Label_0042;
                }
                goto Label_00B8;
            Label_0042:
                transform2 = transform.GetComponent<RectTransform>();
                if ((transform2 == null) == null)
                {
                    goto Label_005D;
                }
                goto Label_00B8;
            Label_005D:
                if (num3 == null)
                {
                    goto Label_0081;
                }
                if (num2 <= Mathf.Abs(pos - -&transform2.get_anchoredPosition().x))
                {
                    goto Label_00B8;
                }
            Label_0081:
                num2 = Mathf.Abs(pos - -&transform2.get_anchoredPosition().x);
                num = -&transform2.get_anchoredPosition().x;
                num3 += 1;
                icon = transform.GetComponent<StoryPartIcon>();
            Label_00B8:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_001E;
                }
                goto Label_00DF;
            }
            finally
            {
            Label_00C9:
                disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    goto Label_00D7;
                }
            Label_00D7:
                disposable.Dispose();
            }
        Label_00DF:
            this.SetButtonInteractable(icon);
            return num;
        }

        public unsafe void OnIcon(GameObject go)
        {
            WorldMapController controller;
            StoryPartIcon icon;
            ScrollAutoFit fit;
            Transform transform;
            IEnumerator enumerator;
            RectTransform transform2;
            Vector2 vector;
            IDisposable disposable;
            if ((go == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            this.mSelectIcon = null;
            if ((this.mQuestSectionList == null) == null)
            {
                goto Label_004F;
            }
            controller = WorldMapController.FindInstance(this.WorldMapControllerID);
            if ((controller != null) == null)
            {
                goto Label_004E;
            }
            this.mQuestSectionList = controller.SectionList;
            goto Label_004F;
        Label_004E:
            return;
        Label_004F:
            icon = go.GetComponent<StoryPartIcon>();
            if ((icon == null) == null)
            {
                goto Label_0063;
            }
            return;
        Label_0063:
            if ((this.ScrollArea == null) == null)
            {
                goto Label_0075;
            }
            return;
        Label_0075:
            fit = this.ScrollArea.GetComponent<ScrollAutoFit>();
            if ((fit == null) == null)
            {
                goto Label_008E;
            }
            return;
        Label_008E:
            enumerator = base.get_transform().GetEnumerator();
        Label_009B:
            try
            {
                goto Label_0124;
            Label_00A0:
                transform = (Transform) enumerator.Current;
                if (transform.get_gameObject().get_activeSelf() != null)
                {
                    goto Label_00C2;
                }
                goto Label_0124;
            Label_00C2:
                if ((icon.get_gameObject() == transform.get_gameObject()) == null)
                {
                    goto Label_0124;
                }
                transform2 = transform.GetComponent<RectTransform>();
                if ((transform2 == null) == null)
                {
                    goto Label_00F2;
                }
                goto Label_0124;
            Label_00F2:
                fit.SetScrollToHorizontal(-&transform2.get_anchoredPosition().x);
                this.mSelectIcon = icon;
                this.mCheckSelectIconMoveFlag = 1;
                FlowNode_GameObject.ActivateOutputLinks(this, 0x67);
                goto Label_0130;
            Label_0124:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_00A0;
                }
            Label_0130:
                goto Label_014B;
            }
            finally
            {
            Label_0135:
                disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    goto Label_0143;
                }
            Label_0143:
                disposable.Dispose();
            }
        Label_014B:
            return;
        }

        public unsafe void OnNext()
        {
            ScrollAutoFit fit;
            RectTransform transform;
            float num;
            float num2;
            int num3;
            float num4;
            Transform transform2;
            IEnumerator enumerator;
            RectTransform transform3;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            Vector2 vector5;
            IDisposable disposable;
            if ((this.ScrollArea == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            fit = this.ScrollArea.GetComponent<ScrollAutoFit>();
            if ((fit == null) == null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            num = &base.get_transform().GetComponent<RectTransform>().get_anchoredPosition().x;
            num2 = 0f;
            num3 = 0;
            num4 = num;
            enumerator = base.get_transform().GetEnumerator();
        Label_0060:
            try
            {
                goto Label_0112;
            Label_0065:
                transform2 = (Transform) enumerator.Current;
                if (transform2.get_gameObject().get_activeSelf() != null)
                {
                    goto Label_0089;
                }
                goto Label_0112;
            Label_0089:
                transform3 = transform2.GetComponent<RectTransform>();
                if ((transform3 == null) == null)
                {
                    goto Label_00A4;
                }
                goto Label_0112;
            Label_00A4:
                if (num <= -&transform3.get_anchoredPosition().x)
                {
                    goto Label_0112;
                }
                if (num3 == null)
                {
                    goto Label_00E0;
                }
                if (num2 <= Mathf.Abs(num - -&transform3.get_anchoredPosition().x))
                {
                    goto Label_0112;
                }
            Label_00E0:
                num2 = Mathf.Abs(num - -&transform3.get_anchoredPosition().x);
                num4 = -&transform3.get_anchoredPosition().x;
                num3 += 1;
            Label_0112:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0065;
                }
                goto Label_0139;
            }
            finally
            {
            Label_0123:
                disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    goto Label_0131;
                }
            Label_0131:
                disposable.Dispose();
            }
        Label_0139:
            fit.SetScrollToHorizontal(num4);
            if ((this.mNextButton != null) == null)
            {
                goto Label_019E;
            }
            if ((this.mPrevButton != null) == null)
            {
                goto Label_019E;
            }
            this.mSelectIconNum += 1;
            this.mNextButton.set_interactable(1);
            if (this.mSelectIconNum != MonoSingleton<GameManager>.Instance.GetStoryPartNum())
            {
                goto Label_019E;
            }
            this.mPrevButton.set_interactable(0);
        Label_019E:
            return;
        }

        public unsafe void OnPrev()
        {
            ScrollAutoFit fit;
            RectTransform transform;
            float num;
            float num2;
            int num3;
            float num4;
            Transform transform2;
            IEnumerator enumerator;
            RectTransform transform3;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            Vector2 vector5;
            IDisposable disposable;
            if ((this.ScrollArea == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            fit = this.ScrollArea.GetComponent<ScrollAutoFit>();
            if ((fit == null) == null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            num = &base.get_transform().GetComponent<RectTransform>().get_anchoredPosition().x;
            num2 = 0f;
            num3 = 0;
            num4 = num;
            enumerator = base.get_transform().GetEnumerator();
        Label_0060:
            try
            {
                goto Label_0112;
            Label_0065:
                transform2 = (Transform) enumerator.Current;
                if (transform2.get_gameObject().get_activeSelf() != null)
                {
                    goto Label_0089;
                }
                goto Label_0112;
            Label_0089:
                transform3 = transform2.GetComponent<RectTransform>();
                if ((transform3 == null) == null)
                {
                    goto Label_00A4;
                }
                goto Label_0112;
            Label_00A4:
                if (num >= -&transform3.get_anchoredPosition().x)
                {
                    goto Label_0112;
                }
                if (num3 == null)
                {
                    goto Label_00E0;
                }
                if (num2 <= Mathf.Abs(num - -&transform3.get_anchoredPosition().x))
                {
                    goto Label_0112;
                }
            Label_00E0:
                num2 = Mathf.Abs(num - -&transform3.get_anchoredPosition().x);
                num4 = -&transform3.get_anchoredPosition().x;
                num3 += 1;
            Label_0112:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0065;
                }
                goto Label_0139;
            }
            finally
            {
            Label_0123:
                disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    goto Label_0131;
                }
            Label_0131:
                disposable.Dispose();
            }
        Label_0139:
            fit.SetScrollToHorizontal(num4);
            if ((this.mNextButton != null) == null)
            {
                goto Label_0195;
            }
            if ((this.mPrevButton != null) == null)
            {
                goto Label_0195;
            }
            this.mSelectIconNum -= 1;
            if (this.mSelectIconNum != 1)
            {
                goto Label_0189;
            }
            this.mPrevButton.set_interactable(0);
        Label_0189:
            this.mNextButton.set_interactable(1);
        Label_0195:
            return;
        }

        private void SaveReleaseActionKey(int story_num)
        {
            string str;
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.RELEASE_STORY_PART_KEY + ((int) story_num), "1", 1);
            return;
        }

        private void SetButtonInteractable(StoryPartIcon icon)
        {
            int num;
            int num2;
            if ((this.mSelectBeforeIcon == icon) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mSelectBeforeIcon = icon;
            if ((icon != null) == null)
            {
                goto Label_00E7;
            }
            if ((this.mNextButton != null) == null)
            {
                goto Label_00E7;
            }
            if ((this.mPrevButton != null) == null)
            {
                goto Label_00E7;
            }
            if (MonoSingleton<GameManager>.Instance.GetStoryPartNum() <= 1)
            {
                goto Label_00E7;
            }
            this.mSelectIconNum = icon.StoryNum;
            if ((this.mNextButton != null) == null)
            {
                goto Label_009D;
            }
            if (this.mSelectIconNum != 1)
            {
                goto Label_009D;
            }
            this.mNextButton.set_interactable(1);
            this.mPrevButton.set_interactable(0);
            goto Label_00E7;
        Label_009D:
            if (this.mSelectIconNum != MonoSingleton<GameManager>.Instance.GetStoryPartNum())
            {
                goto Label_00CF;
            }
            this.mPrevButton.set_interactable(1);
            this.mNextButton.set_interactable(0);
            goto Label_00E7;
        Label_00CF:
            this.mNextButton.set_interactable(1);
            this.mPrevButton.set_interactable(1);
        Label_00E7:
            num = 0;
            goto Label_0132;
        Label_00EE:
            if ((icon == this.mStoryPartIconList[num]) == null)
            {
                goto Label_011C;
            }
            this.mStoryPartIconList[num].SetMask(0);
            goto Label_012E;
        Label_011C:
            this.mStoryPartIconList[num].SetMask(1);
        Label_012E:
            num += 1;
        Label_0132:
            if (num < this.mStoryPartIconList.Count)
            {
                goto Label_00EE;
            }
            num2 = 0;
            goto Label_0185;
        Label_014A:
            if ((num2 + 1) != icon.StoryNum)
            {
                goto Label_016F;
            }
            this.mPageIconList[num2].set_isOn(1);
            goto Label_0181;
        Label_016F:
            this.mPageIconList[num2].set_isOn(0);
        Label_0181:
            num2 += 1;
        Label_0185:
            if (num2 < this.mPageIconList.Count)
            {
                goto Label_014A;
            }
            return;
        }

        public override unsafe Vector2 SetRangePos(Vector2 position)
        {
            Vector2 vector;
            int num;
            Transform transform;
            IEnumerator enumerator;
            RectTransform transform2;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            Vector2 vector5;
            IDisposable disposable;
            vector = position;
            if (this.SetRangeFlag != null)
            {
                goto Label_0113;
            }
            num = 0;
            enumerator = base.get_transform().GetEnumerator();
        Label_001B:
            try
            {
                goto Label_00CD;
            Label_0020:
                transform = (Transform) enumerator.Current;
                if (transform.get_gameObject().get_activeSelf() != null)
                {
                    goto Label_0041;
                }
                goto Label_00CD;
            Label_0041:
                transform2 = transform.GetComponent<RectTransform>();
                if ((transform2 == null) == null)
                {
                    goto Label_005B;
                }
                goto Label_00CD;
            Label_005B:
                if (num == null)
                {
                    goto Label_007C;
                }
                if (base.mStartPosX <= &transform2.get_anchoredPosition().x)
                {
                    goto Label_0092;
                }
            Label_007C:
                base.mStartPosX = &transform2.get_anchoredPosition().x;
            Label_0092:
                if (num == null)
                {
                    goto Label_00B3;
                }
                if (base.mEndPosX >= &transform2.get_anchoredPosition().x)
                {
                    goto Label_00C9;
                }
            Label_00B3:
                base.mEndPosX = &transform2.get_anchoredPosition().x;
            Label_00C9:
                num += 1;
            Label_00CD:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0020;
                }
                goto Label_00F2;
            }
            finally
            {
            Label_00DD:
                disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    goto Label_00EA;
                }
            Label_00EA:
                disposable.Dispose();
            }
        Label_00F2:
            base.mStartPosX = -base.mStartPosX;
            base.mEndPosX = -base.mEndPosX;
            this.SetRangeFlag = 1;
        Label_0113:
            if (&vector.x <= base.mStartPosX)
            {
                goto Label_0137;
            }
            &vector.x = base.mStartPosX;
            goto Label_0156;
        Label_0137:
            if (&vector.x >= base.mEndPosX)
            {
                goto Label_0156;
            }
            &vector.x = base.mEndPosX;
        Label_0156:
            return vector;
        }

        private unsafe void Start()
        {
            SerializeValueList list;
            int num;
            int num2;
            GameObject obj2;
            Vector2 vector;
            StoryPartIcon icon;
            Toggle toggle;
            int num3;
            GameObject obj3;
            int num4;
            int num5;
            this.mMoveRect = null;
            this.mQuestSectionList = null;
            this.ReleaseStoryPartIcon = null;
            this.AnimationReleaseFlag = 0;
            this.mSelectIconNum = 1;
            this.mSelectIcon = null;
            this.mCheckSelectIconMoveFlag = 0;
            this.mSelectBeforeIcon = null;
            this.mNextButton = null;
            if ((this.PageNext != null) == null)
            {
                goto Label_0061;
            }
            this.mNextButton = this.PageNext.GetComponent<Button>();
        Label_0061:
            this.mPrevButton = null;
            if ((this.PagePrev != null) == null)
            {
                goto Label_008A;
            }
            this.mPrevButton = this.PagePrev.GetComponent<Button>();
        Label_008A:
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_00AC;
            }
            this.mQuestSectionList = list.GetComponent<QuestSectionList>("_self");
        Label_00AC:
            this.mReleaseAction = MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart();
            this.SetRangeFlag = 0;
            this.TemplateGo.SetActive(0);
            this.TemplatePageIcon.SetActive(0);
            num = MonoSingleton<GameManager>.Instance.GetStoryPartNum();
            num2 = MonoSingleton<GameManager>.Instance.GetStoryPartNumPresentTime();
            obj2 = null;
            vector = Vector2.get_zero();
            icon = null;
            toggle = null;
            num3 = 0;
            goto Label_02FC;
        Label_0108:
            obj2 = Object.Instantiate<GameObject>(this.TemplateGo);
            vector = obj2.get_transform().get_localScale();
            obj2.get_transform().SetParent(base.get_transform());
            obj2.get_transform().set_localScale(vector);
            obj2.get_gameObject().SetActive(1);
            num4 = num3 + 1;
            obj2.set_name(this.TemplateGo.get_name() + &num4.ToString());
            icon = obj2.GetComponent<StoryPartIcon>();
            if (this.mReleaseAction == null)
            {
                goto Label_01AF;
            }
            if ((num3 + 1) != num2)
            {
                goto Label_01AF;
            }
            icon.Setup(1, num3 + 1);
            this.ReleaseStoryPartIcon = icon;
            goto Label_01D1;
        Label_01AF:
            if (icon.Setup((num3 + 1) > num2, num3 + 1) != null)
            {
                goto Label_01D1;
            }
            Object.Destroy(obj2);
            goto Label_02F6;
        Label_01D1:
            this.mStoryPartIconList.Add(icon);
            toggle = null;
            if (num <= 1)
            {
                goto Label_0279;
            }
            obj3 = Object.Instantiate<GameObject>(this.TemplatePageIcon);
            vector = obj3.get_transform().get_localScale();
            obj3.get_transform().SetParent(this.TogglePagesGroup.get_transform());
            obj3.get_transform().set_localScale(vector);
            obj3.get_gameObject().SetActive(1);
            num5 = num3 + 1;
            obj3.set_name(this.TemplatePageIcon.get_name() + &num5.ToString());
            toggle = obj3.GetComponent<Toggle>();
            this.mPageIconList.Add(toggle);
        Label_0279:
            if (this.mReleaseAction == null)
            {
                goto Label_02BB;
            }
            if ((num3 + 1) != num2)
            {
                goto Label_02F6;
            }
            this.mMoveRect = obj2.GetComponent<RectTransform>();
            this.mSelectIconNum = num2;
            if ((toggle != null) == null)
            {
                goto Label_02F6;
            }
            toggle.set_isOn(1);
            goto Label_02F6;
        Label_02BB:
            if ((num3 + 1) != GlobalVars.SelectedStoryPart.Get())
            {
                goto Label_02F6;
            }
            this.mMoveRect = obj2.GetComponent<RectTransform>();
            this.mSelectIconNum = num2;
            if ((toggle != null) == null)
            {
                goto Label_02F6;
            }
            toggle.set_isOn(1);
        Label_02F6:
            num3 += 1;
        Label_02FC:
            if (num3 < num)
            {
                goto Label_0108;
            }
            if (num != 1)
            {
                goto Label_0345;
            }
            if ((this.PageNext != null) == null)
            {
                goto Label_0328;
            }
            this.PageNext.SetActive(0);
        Label_0328:
            if ((this.PagePrev != null) == null)
            {
                goto Label_0345;
            }
            this.PagePrev.SetActive(0);
        Label_0345:
            return;
        }

        private unsafe void Update()
        {
            RectTransform transform;
            Vector2 vector;
            ScrollAutoFit fit;
            Vector2 vector2;
            if ((this.mMoveRect != null) == null)
            {
                goto Label_00B5;
            }
            transform = base.GetComponent<RectTransform>();
            vector = transform.get_anchoredPosition();
            &vector.x = -&this.mMoveRect.get_anchoredPosition().x;
            transform.set_anchoredPosition(vector);
            this.mMoveRect = null;
            if ((this.mNextButton != null) == null)
            {
                goto Label_00B5;
            }
            if ((this.mPrevButton != null) == null)
            {
                goto Label_00B5;
            }
            if (MonoSingleton<GameManager>.Instance.GetStoryPartNum() != 1)
            {
                goto Label_00B5;
            }
            this.mNextButton.set_interactable(0);
            this.mPrevButton.set_interactable(0);
            if (this.mStoryPartIconList.Count != 1)
            {
                goto Label_00B5;
            }
            this.mStoryPartIconList[0].SetMask(0);
        Label_00B5:
            if (this.mReleaseAction == null)
            {
                goto Label_00EB;
            }
            if ((this.ReleaseStoryPartIcon != null) == null)
            {
                goto Label_00DD;
            }
            this.ReleaseStoryPartIcon.PlayReleaseAnim();
        Label_00DD:
            this.mReleaseAction = 0;
            this.AnimationReleaseFlag = 1;
        Label_00EB:
            if (this.AnimationReleaseFlag == null)
            {
                goto Label_0138;
            }
            if (this.ReleaseStoryPartIcon.IsPlayingReleaseAnim() != null)
            {
                goto Label_0138;
            }
            this.ReleaseStoryPartIcon.ReleaseIcon();
            this.SaveReleaseActionKey(this.ReleaseStoryPartIcon.StoryNum);
            this.ReleaseStoryPartIcon = null;
            this.AnimationReleaseFlag = 0;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
        Label_0138:
            if (this.mCheckSelectIconMoveFlag == null)
            {
                goto Label_0169;
            }
            if (this.ScrollArea.GetComponent<ScrollAutoFit>().IsMove != null)
            {
                goto Label_0169;
            }
            this.mCheckSelectIconMoveFlag = 0;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
        Label_0169:
            this.UpdateButtonInteractable();
            return;
        }

        private unsafe void UpdateButtonInteractable()
        {
            RectTransform transform;
            Vector3 vector;
            if (MonoSingleton<GameManager>.Instance.GetStoryPartNum() != 1)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            transform = base.GetComponent<RectTransform>();
            this.GetNearIconPos(&transform.get_localPosition().x);
            return;
        }
    }
}

