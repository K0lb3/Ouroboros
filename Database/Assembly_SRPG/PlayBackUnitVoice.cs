namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class PlayBackUnitVoice : MonoBehaviour
    {
        private readonly float TOOLTIP_POSITION_OFFSET_Y;
        [SerializeField]
        private Button CloseButton;
        [SerializeField]
        private Button Bg;
        [SerializeField]
        private RectTransform ItemParent;
        [SerializeField]
        private GameObject ItemTemplate;
        [SerializeField]
        private Tooltip Preafab_UnlockConditionsTooltip;
        private List<GameObject> mItems;
        private UnitData mCurrentUnit;
        private long mLastUnitUniqueID;
        private PlayBackUnitVoiceItem mLastSelectItem;
        private bool mStartPlayVoice;
        private UnitData.UnitPlaybackVoiceData mUnitVoiceData;
        private Tooltip mUnlockConditionsTooltip;
        private SRPG_ScrollRect mScrollRect;
        public CloseEvent OnCloseEvent;

        public PlayBackUnitVoice()
        {
            this.TOOLTIP_POSITION_OFFSET_Y = 20f;
            this.mItems = new List<GameObject>();
            this.mLastUnitUniqueID = -1L;
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.ItemTemplate.SetActive(0);
        Label_001D:
            return;
        }

        private void OnClose()
        {
            WindowController controller;
            controller = base.get_gameObject().GetComponentInParent<WindowController>();
            if ((controller == null) == null)
            {
                goto Label_0023;
            }
            DebugUtility.LogError("WindowControllerが存在しません");
            return;
        Label_0023:
            if (this.mUnitVoiceData == null)
            {
                goto Label_0039;
            }
            this.mUnitVoiceData.Cleanup();
        Label_0039:
            if (this.OnCloseEvent == null)
            {
                goto Label_004F;
            }
            this.OnCloseEvent();
        Label_004F:
            controller.Close();
            return;
        }

        private void OnDestroy()
        {
            if (this.mUnitVoiceData == null)
            {
                goto Label_0016;
            }
            this.mUnitVoiceData.Cleanup();
        Label_0016:
            return;
        }

        public void OnOpen()
        {
            this.Refresh();
            return;
        }

        private void OnScroll(Vector2 _vec)
        {
            if ((this.mUnlockConditionsTooltip != null) == null)
            {
                goto Label_0023;
            }
            this.mUnlockConditionsTooltip.Close();
            this.mUnlockConditionsTooltip = null;
        Label_0023:
            return;
        }

        private void OnSelect(Button button)
        {
            PlayBackUnitVoiceItem item;
            if ((button == null) == null)
            {
                goto Label_0017;
            }
            DebugUtility.LogError("Buttonが存在しません");
            return;
        Label_0017:
            item = button.get_gameObject().GetComponentInChildren<PlayBackUnitVoiceItem>();
            if ((item == null) == null)
            {
                goto Label_003A;
            }
            DebugUtility.LogError("PlayBackUnitVoiceItemが存在しません");
            return;
        Label_003A:
            if (item.IsLocked == null)
            {
                goto Label_0058;
            }
            this.mScrollRect.StopMovement();
            this.ShowUnlockConditionsTooltip(item);
            return;
        Label_0058:
            if ((this.mLastSelectItem != null) == null)
            {
                goto Label_0090;
            }
            if ((this.mLastSelectItem.CueName != item.CueName) == null)
            {
                goto Label_0090;
            }
            this.mLastSelectItem.SetPlayingBadge(0);
        Label_0090:
            item.SetPlayingBadge(1);
            this.mLastSelectItem = item;
            this.PlayVoice(item.CueName);
            return;
        }

        private void PlayVoice(string name)
        {
            string str;
            string str2;
            if (this.mCurrentUnit != null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("UnitDataが存在しません");
            return;
        Label_0016:
            if (this.mUnitVoiceData.Voice != null)
            {
                goto Label_0031;
            }
            DebugUtility.LogError("UnitVoiceが存在しません");
            return;
        Label_0031:
            str = this.mCurrentUnit.GetUnitJobVoiceSheetName(-1);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0054;
            }
            DebugUtility.LogError("UnitDataにボイス設定が存在しません");
            return;
        Label_0054:
            str2 = name.Replace(str + "_", string.Empty);
            this.mUnitVoiceData.Voice.Play(str2, 0f, 1);
            this.mStartPlayVoice = 1;
            return;
        }

        private unsafe void Refresh()
        {
            UnitData data;
            int num;
            int num2;
            int num3;
            GameObject obj2;
            SRPG_Button button;
            int num4;
            GameObject obj3;
            PlayBackUnitVoiceItem item;
            if ((this.mScrollRect != null) == null)
            {
                goto Label_003C;
            }
            if ((this.mScrollRect.get_verticalScrollbar() != null) == null)
            {
                goto Label_003C;
            }
            this.mScrollRect.get_verticalScrollbar().set_value(1f);
        Label_003C:
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_005A;
            }
            DebugUtility.LogError("UnitDataがBindされていません");
            return;
        Label_005A:
            this.mCurrentUnit = data;
            if (this.mUnitVoiceData == null)
            {
                goto Label_00A1;
            }
            if (this.mLastUnitUniqueID == -1L)
            {
                goto Label_00A1;
            }
            if (this.mLastUnitUniqueID == this.mCurrentUnit.UniqueID)
            {
                goto Label_00A1;
            }
            this.mUnitVoiceData.Cleanup();
            this.mUnitVoiceData = null;
        Label_00A1:
            this.mLastUnitUniqueID = this.mCurrentUnit.UniqueID;
            if (this.mUnitVoiceData == null)
            {
                goto Label_00C8;
            }
            this.mUnitVoiceData.Cleanup();
        Label_00C8:
            this.mUnitVoiceData = this.mCurrentUnit.GetUnitPlaybackVoiceData();
            if (this.mItems == null)
            {
                goto Label_012E;
            }
            num = 0;
            goto Label_011D;
        Label_00EB:
            if ((this.mItems[num] == null) == null)
            {
                goto Label_0107;
            }
            goto Label_0119;
        Label_0107:
            this.mItems[num].SetActive(0);
        Label_0119:
            num += 1;
        Label_011D:
            if (num < this.mItems.Count)
            {
                goto Label_00EB;
            }
        Label_012E:
            if ((this.ItemParent == null) != null)
            {
                goto Label_0150;
            }
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_015B;
            }
        Label_0150:
            DebugUtility.LogError("リストテンプレートが存在しません");
            return;
        Label_015B:
            if (this.mUnitVoiceData.VoiceCueList.Count <= this.mItems.Count)
            {
                goto Label_0221;
            }
            num2 = this.mUnitVoiceData.VoiceCueList.Count - this.mItems.Count;
            num3 = 0;
            goto Label_021A;
        Label_019F:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            if ((obj2 == null) == null)
            {
                goto Label_01BE;
            }
            goto Label_0216;
        Label_01BE:
            obj2.get_transform().SetParent(this.ItemParent, 0);
            this.mItems.Add(obj2);
            button = obj2.GetComponentInChildren<SRPG_Button>();
            if ((button == null) == null)
            {
                goto Label_0203;
            }
            DebugUtility.LogError("Buttonが存在しません");
            goto Label_0216;
        Label_0203:
            button.AddListener(new SRPG_Button.ButtonClickEvent(this.OnSelect));
        Label_0216:
            num3 += 1;
        Label_021A:
            if (num3 < num2)
            {
                goto Label_019F;
            }
        Label_0221:
            num4 = 0;
            goto Label_02D4;
        Label_0229:
            obj3 = this.mItems[num4];
            item = obj3.GetComponentInChildren<PlayBackUnitVoiceItem>();
            if ((item == null) == null)
            {
                goto Label_0259;
            }
            DebugUtility.LogError("PlayBackUnitVoiceItemが取得できません");
            return;
        Label_0259:
            item.SetUp(this.mUnitVoiceData.VoiceCueList[num4]);
            item.Refresh();
            item.Unlock();
            if (this.mUnitVoiceData.VoiceCueList[num4].is_locked == null)
            {
                goto Label_02A3;
            }
            item.Lock();
        Label_02A3:
            obj3.set_name(&this.mUnitVoiceData.VoiceCueList[num4].cueInfo.name);
            obj3.SetActive(1);
            num4 += 1;
        Label_02D4:
            if (num4 < this.mUnitVoiceData.VoiceCueList.Count)
            {
                goto Label_0229;
            }
            return;
        }

        private unsafe void ShowUnlockConditionsTooltip(PlayBackUnitVoiceItem voice_item)
        {
            RectTransform transform;
            Vector2 vector;
            Vector2 vector2;
            if ((this.Preafab_UnlockConditionsTooltip == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.mUnlockConditionsTooltip == null) == null)
            {
                goto Label_0039;
            }
            this.mUnlockConditionsTooltip = Object.Instantiate<Tooltip>(this.Preafab_UnlockConditionsTooltip);
            goto Label_0044;
        Label_0039:
            this.mUnlockConditionsTooltip.ResetPosition();
        Label_0044:
            transform = voice_item.GetComponent<RectTransform>();
            vector = new Vector2();
            &vector.x = 0f;
            &vector.y = (&transform.get_sizeDelta().y / 2f) + this.TOOLTIP_POSITION_OFFSET_Y;
            Tooltip.SetTooltipPosition(transform, vector);
            if ((this.mUnlockConditionsTooltip.TooltipText != null) == null)
            {
                goto Label_00B4;
            }
            this.mUnlockConditionsTooltip.TooltipText.set_text(voice_item.GetUnlockConditionsText());
        Label_00B4:
            return;
        }

        private void Start()
        {
            if ((this.CloseButton != null) == null)
            {
                goto Label_002D;
            }
            this.CloseButton.get_onClick().AddListener(new UnityAction(this, this.OnClose));
        Label_002D:
            if ((this.Bg != null) == null)
            {
                goto Label_005A;
            }
            this.Bg.get_onClick().AddListener(new UnityAction(this, this.OnClose));
        Label_005A:
            this.mScrollRect = base.GetComponentInChildren<SRPG_ScrollRect>();
            if ((this.mScrollRect != null) == null)
            {
                goto Label_0093;
            }
            this.mScrollRect.get_onValueChanged().AddListener(new UnityAction<Vector2>(this, this.OnScroll));
        Label_0093:
            return;
        }

        private void Update()
        {
            if (this.mStartPlayVoice == null)
            {
                goto Label_0071;
            }
            if (this.mUnitVoiceData.Voice != null)
            {
                goto Label_002F;
            }
            this.mStartPlayVoice = 0;
            this.mLastSelectItem.SetPlayingBadge(0);
            return;
        Label_002F:
            if (this.mUnitVoiceData.Voice.IsPlaying != null)
            {
                goto Label_0071;
            }
            if ((this.mLastSelectItem == null) == null)
            {
                goto Label_005D;
            }
            this.mStartPlayVoice = 0;
            return;
        Label_005D:
            this.mLastSelectItem.SetPlayingBadge(0);
            this.mStartPlayVoice = 0;
            return;
        Label_0071:
            return;
        }

        public delegate void CloseEvent();
    }
}

