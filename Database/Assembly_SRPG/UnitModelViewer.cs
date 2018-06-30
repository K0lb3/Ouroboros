namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(100, "OnBack", 1, 100)]
    public class UnitModelViewer : MonoBehaviour, IFlowInterface
    {
        private readonly float TOOLTIP_POSITION_OFFSET_Y;
        [SerializeField]
        private RectTransform JobIconParent;
        [SerializeField]
        private GameObject JobNameObject;
        [SerializeField]
        private GameObject TouchArea;
        [SerializeField]
        private SRPG_Button SkinButton;
        [SerializeField]
        private SRPG_Button ReactionButton;
        [SerializeField]
        private Button VoiceButton;
        [SerializeField]
        private GameObject VoiceUnlock;
        [SerializeField]
        private Tooltip Preafab_UnlockConditionsTooltip;
        public ChangeJobSlotEvent OnChangeJobSlot;
        public SkinSelectEvent OnSkinSelect;
        public PlayReaction OnPlayReaction;
        private TouchControlArea mTouchControlArea;
        private Tooltip mUnlockConditionsTooltip;
        private bool mIsCreatedJobIconInstance;
        private int wait_frame;
        private Dictionary<int, int[]> mJobSetDatas;
        [SerializeField]
        private JobIconScrollListController mJobIconScrollListController;
        private List<UnitInventoryJobIcon> mUnitJobIconSetList;
        private ScrollClamped_JobIcons mScrollClampedJobIcons;
        private bool IsInitalized;
        private GameObject mPlayBackVoiceWindow;
        public static readonly string PLAYBACK_UNITVOICE_PREFAB_PATH;

        static UnitModelViewer()
        {
            PLAYBACK_UNITVOICE_PREFAB_PATH = "UI/PlayBackUnitVoice";
            return;
        }

        public UnitModelViewer()
        {
            this.TOOLTIP_POSITION_OFFSET_Y = 80f;
            this.wait_frame = 1;
            this.mJobSetDatas = new Dictionary<int, int[]>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private int ClampJobIconIndex(int index)
        {
            if (index < 0)
            {
                goto Label_001B;
            }
            index = index % this.mJobSetDatas.Count;
            goto Label_0050;
        Label_001B:
            index = Mathf.Abs(index);
            index = index % this.mJobSetDatas.Count;
            index = this.mJobSetDatas.Count - index;
            index = index % this.mJobSetDatas.Count;
        Label_0050:
            return index;
        }

        public void Initalize()
        {
            TouchControlArea area;
            if (this.IsInitalized == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.TouchArea != null) == null)
            {
                goto Label_003C;
            }
            area = this.TouchArea.GetComponent<TouchControlArea>();
            if ((area != null) == null)
            {
                goto Label_003C;
            }
            this.mTouchControlArea = area;
        Label_003C:
            if ((this.SkinButton != null) == null)
            {
                goto Label_0069;
            }
            this.SkinButton.get_onClick().AddListener(new UnityAction(this, this.OnSkinSelectClick));
        Label_0069:
            if ((this.ReactionButton != null) == null)
            {
                goto Label_0096;
            }
            this.ReactionButton.get_onClick().AddListener(new UnityAction(this, this.OnRectionClick));
        Label_0096:
            if ((this.VoiceButton != null) == null)
            {
                goto Label_00C3;
            }
            this.VoiceButton.get_onClick().AddListener(new UnityAction(this, this.OnVoiceClick));
        Label_00C3:
            if ((this.VoiceUnlock != null) == null)
            {
                goto Label_00E0;
            }
            this.VoiceUnlock.SetActive(1);
        Label_00E0:
            this.IsInitalized = 1;
            this.mJobIconScrollListController.Init();
            this.mJobIconScrollListController.CreateInstance();
            return;
        }

        private void OnDestroy()
        {
            if ((this.mPlayBackVoiceWindow != null) == null)
            {
                goto Label_001C;
            }
            Object.Destroy(this.mPlayBackVoiceWindow);
        Label_001C:
            return;
        }

        private void OnDisable()
        {
            this.mIsCreatedJobIconInstance = 0;
            return;
        }

        public void OnJobIconClick(GameObject target)
        {
            UnitEnhanceV3.Instance.OnJobSlotClick(target);
            DataSource.Bind<UnitData>(this.JobNameObject, UnitEnhanceV3.Instance.CurrentUnit);
            GameParameter.UpdateAll(base.get_gameObject());
            this.UpdateJobSlotStates(1);
            this.ScrollClampedJobIcons.Focus(target.get_transform().get_parent().get_gameObject(), 0, 0, 0f);
            return;
        }

        private void OnRectionClick()
        {
            if (this.OnPlayReaction == null)
            {
                goto Label_0016;
            }
            this.OnPlayReaction();
        Label_0016:
            return;
        }

        private void OnSkinSelectClick()
        {
            if (this.OnSkinSelect == null)
            {
                goto Label_001C;
            }
            this.OnSkinSelect(this.SkinButton);
        Label_001C:
            return;
        }

        private void OnVoiceClick()
        {
            UnitData data;
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_001E;
            }
            DebugUtility.LogError("3DViewerにUnitDataがBindされていません.");
            return;
        Label_001E:
            if (data.CheckUnlockPlaybackVoice() != null)
            {
                goto Label_003F;
            }
            this.ShowUnlockConditionsTooltip(this.VoiceButton.get_gameObject());
            goto Label_004C;
        Label_003F:
            base.StartCoroutine(this.OpenPlayBackUnitVoice());
        Label_004C:
            return;
        }

        private void OnVoiceClose()
        {
            this.mTouchControlArea.set_enabled(1);
            return;
        }

        [DebuggerHidden]
        private IEnumerator OpenPlayBackUnitVoice()
        {
            <OpenPlayBackUnitVoice>c__Iterator176 iterator;
            iterator = new <OpenPlayBackUnitVoice>c__Iterator176();
            iterator.<>f__this = this;
            return iterator;
        }

        public unsafe void Refresh(bool is_hide)
        {
            int[] numArray3;
            int[] numArray1;
            int num;
            int num2;
            int num3;
            int num4;
            int[] numArray;
            int num5;
            JobSetParam param;
            int[] numArray2;
            int num6;
            int num7;
            int num8;
            int num9;
            List<GameObject> list;
            int num10;
            int num11;
            GameObject obj2;
            UnitInventoryJobIcon icon;
            bool flag;
            <Refresh>c__AnonStorey3D5 storeyd;
            <Refresh>c__AnonStorey3D6 storeyd2;
            <Refresh>c__AnonStorey3D7 storeyd3;
            <Refresh>c__AnonStorey3D8 storeyd4;
            int num12;
            int num13;
            storeyd = new <Refresh>c__AnonStorey3D5();
            storeyd.unit = UnitEnhanceV3.Instance.CurrentUnit;
            DataSource.Bind<UnitData>(base.get_gameObject(), storeyd.unit);
            num = 0;
            goto Label_0046;
        Label_0031:
            this.UnitJobIconSetList[num].ResetParam();
            num += 1;
        Label_0046:
            if (num < this.UnitJobIconSetList.Count)
            {
                goto Label_0031;
            }
            storeyd.cc_jobset_array = MonoSingleton<GameManager>.Instance.GetClassChangeJobSetParam(storeyd.unit.UnitID);
            if (storeyd.cc_jobset_array != null)
            {
                goto Label_008D;
            }
            storeyd.cc_jobset_array = new JobSetParam[0];
        Label_008D:
            this.mJobSetDatas.Clear();
            num2 = 0;
            storeyd2 = new <Refresh>c__AnonStorey3D6();
            storeyd2.<>f__ref$981 = storeyd;
            storeyd2.i = 0;
            goto Label_0243;
        Label_00B7:
            num3 = Array.FindIndex<JobSetParam>(storeyd.cc_jobset_array, new Predicate<JobSetParam>(storeyd2.<>m__478));
            if (num3 < 0)
            {
                goto Label_0156;
            }
            storeyd3 = new <Refresh>c__AnonStorey3D7();
            storeyd3.base_jobset_param = MonoSingleton<GameManager>.Instance.GetJobSetParam(storeyd.cc_jobset_array[num3].jobchange);
            if (Array.FindIndex<JobData>(storeyd.unit.Jobs, new Predicate<JobData>(storeyd3.<>m__479)) < 0)
            {
                goto Label_0129;
            }
            goto Label_0233;
        Label_0129:
            numArray1 = new int[] { storeyd2.i, -1 };
            numArray = numArray1;
            this.mJobSetDatas.Add(num2, numArray);
            num2 += 1;
            goto Label_0233;
        Label_0156:
            num5 = -1;
            storeyd4 = new <Refresh>c__AnonStorey3D8();
            storeyd4.<>f__ref$981 = storeyd;
            storeyd4.j = 0;
            goto Label_01F5;
        Label_0176:
            if ((MonoSingleton<GameManager>.Instance.GetJobSetParam(storeyd.cc_jobset_array[storeyd4.j].jobchange).job == storeyd.unit.Jobs[storeyd2.i].JobID) == null)
            {
                goto Label_01E5;
            }
            num5 = Array.FindIndex<JobData>(storeyd.unit.Jobs, new Predicate<JobData>(storeyd4.<>m__47A));
            goto Label_020A;
        Label_01E5:
            storeyd4.j += 1;
        Label_01F5:
            if (storeyd4.j < ((int) storeyd.cc_jobset_array.Length))
            {
                goto Label_0176;
            }
        Label_020A:
            numArray3 = new int[] { storeyd2.i, num5 };
            numArray2 = numArray3;
            this.mJobSetDatas.Add(num2, numArray2);
            num2 += 1;
        Label_0233:
            storeyd2.i += 1;
        Label_0243:
            if (storeyd2.i < ((int) storeyd.unit.Jobs.Length))
            {
                goto Label_00B7;
            }
            num6 = 0;
            goto Label_028D;
        Label_0265:
            this.mJobIconScrollListController.Items[num6].job_icon.get_gameObject().SetActive(0);
            num6 += 1;
        Label_028D:
            if (num6 < this.mJobIconScrollListController.Items.Count)
            {
                goto Label_0265;
            }
            if (this.mJobSetDatas.Count > 2)
            {
                goto Label_0320;
            }
            num7 = 0;
            goto Label_0309;
        Label_02BD:
            this.mJobIconScrollListController.Items[num7].job_icon.get_gameObject().SetActive(1);
            this.RefreshJobIcon(this.mJobIconScrollListController.Items[num7].job_icon.get_gameObject(), num7);
            num7 += 1;
        Label_0309:
            if (num7 < this.mJobSetDatas.Count)
            {
                goto Label_02BD;
            }
            goto Label_03AE;
        Label_0320:
            num8 = 0;
            goto Label_0397;
        Label_0328:
            this.mJobIconScrollListController.Items[num8].job_icon.get_gameObject().SetActive(1);
            num9 = int.Parse(this.mJobIconScrollListController.Items[num8].job_icon.get_name());
            this.RefreshJobIcon(this.mJobIconScrollListController.Items[num8].job_icon.get_gameObject(), num9);
            num8 += 1;
        Label_0397:
            if (num8 < this.mJobIconScrollListController.Items.Count)
            {
                goto Label_0328;
            }
        Label_03AE:
            this.mJobIconScrollListController.Repotision();
            list = new List<GameObject>();
            num10 = 0;
            goto Label_0434;
        Label_03C8:
            if ((this.mJobIconScrollListController.Items[num10].job_icon.BaseJobIconButton.get_name() == &UnitEnhanceV3.Instance.CurrentUnit.JobIndex.ToString()) == null)
            {
                goto Label_042E;
            }
            list.Add(this.mJobIconScrollListController.Items[num10].job_icon.get_gameObject());
        Label_042E:
            num10 += 1;
        Label_0434:
            if (num10 < this.mJobIconScrollListController.Items.Count)
            {
                goto Label_03C8;
            }
            if (list.Count > 0)
            {
                goto Label_04E3;
            }
            num11 = 0;
            goto Label_04CC;
        Label_0460:
            if ((this.mJobIconScrollListController.Items[num11].job_icon.CcJobButton.get_name() == &UnitEnhanceV3.Instance.CurrentUnit.JobIndex.ToString()) == null)
            {
                goto Label_04C6;
            }
            list.Add(this.mJobIconScrollListController.Items[num11].job_icon.get_gameObject());
        Label_04C6:
            num11 += 1;
        Label_04CC:
            if (num11 < this.mJobIconScrollListController.Items.Count)
            {
                goto Label_0460;
            }
        Label_04E3:
            obj2 = this.ScrollClampedJobIcons.Focus(list, 1, is_hide, 0f);
            if ((obj2 != null) == null)
            {
                goto Label_0532;
            }
            icon = obj2.GetComponent<UnitInventoryJobIcon>();
            if ((icon != null) == null)
            {
                goto Label_0532;
            }
            UnitEnhanceV3.Instance.OnJobSlotClick(icon.BaseJobIconButton.get_gameObject());
        Label_0532:
            this.mJobIconScrollListController.Step();
            DataSource.Bind<UnitData>(this.JobNameObject, storeyd.unit);
            GameParameter.UpdateAll(base.get_gameObject());
            this.UpdateJobSlotStates(1);
            if ((this.SkinButton != null) == null)
            {
                goto Label_0589;
            }
            this.SkinButton.set_interactable(storeyd.unit.IsSkinUnlocked());
        Label_0589:
            if ((this.VoiceButton != null) == null)
            {
                goto Label_05C9;
            }
            if ((this.VoiceUnlock != null) == null)
            {
                goto Label_05C9;
            }
            flag = storeyd.unit.CheckUnlockPlaybackVoice();
            this.VoiceUnlock.SetActive(flag == 0);
        Label_05C9:
            return;
        }

        public void RefreshJobIcon(GameObject target, int job_index)
        {
            int num;
            int num2;
            bool flag;
            num = this.ClampJobIconIndex(job_index);
            flag = UnitEnhanceV3.Instance.CurrentUnit.JobIndex == num;
            target.GetComponent<UnitInventoryJobIcon>().SetParam(UnitEnhanceV3.Instance.CurrentUnit, this.mJobSetDatas[num][0], this.mJobSetDatas[num][1], flag, 2);
            this.UpdateJobSlotStates(1);
            return;
        }

        public void ResetTouchControlArea()
        {
            if ((this.mTouchControlArea != null) == null)
            {
                goto Label_001C;
            }
            this.mTouchControlArea.Reset();
        Label_001C:
            return;
        }

        private unsafe void ShowUnlockConditionsTooltip(GameObject _target_obj)
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
            transform = _target_obj.GetComponent<RectTransform>();
            vector = new Vector2();
            &vector.x = 0f;
            &vector.y = (&transform.get_sizeDelta().y / 2f) + this.TOOLTIP_POSITION_OFFSET_Y;
            Tooltip.SetTooltipPosition(transform, vector);
            if ((this.mUnlockConditionsTooltip.TooltipText != null) == null)
            {
                goto Label_00B8;
            }
            this.mUnlockConditionsTooltip.TooltipText.set_text(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_FUNCTION"));
        Label_00B8:
            return;
        }

        private void Start()
        {
            this.Initalize();
            return;
        }

        private void Update()
        {
            if (this.mIsCreatedJobIconInstance == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.UnitJobIconSetList.Count > 0)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            if (this.wait_frame <= 0)
            {
                goto Label_0039;
            }
            this.wait_frame -= 1;
            return;
        Label_0039:
            this.Refresh(0);
            this.mIsCreatedJobIconInstance = 1;
            return;
        }

        private unsafe void UpdateJobSlotStates(bool immediate)
        {
            UnitInventoryJobIcon[] iconArray;
            int num;
            bool flag;
            int num2;
            iconArray = this.ScrollClampedJobIcons.GetComponentsInChildren<UnitInventoryJobIcon>();
            num = 0;
            goto Label_004A;
        Label_0013:
            flag = iconArray[num].BaseJobIconButton.get_name() == &UnitEnhanceV3.Instance.CurrentUnit.JobIndex.ToString();
            iconArray[num].SetSelectIconAnim(flag);
            num += 1;
        Label_004A:
            if (num < ((int) iconArray.Length))
            {
                goto Label_0013;
            }
            return;
        }

        private List<UnitInventoryJobIcon> UnitJobIconSetList
        {
            get
            {
                int num;
                if (this.mUnitJobIconSetList != null)
                {
                    goto Label_0058;
                }
                this.mUnitJobIconSetList = new List<UnitInventoryJobIcon>();
                num = 0;
                goto Label_0042;
            Label_001D:
                this.mUnitJobIconSetList.Add(this.mJobIconScrollListController.Items[num].job_icon);
                num += 1;
            Label_0042:
                if (num < this.mJobIconScrollListController.Items.Count)
                {
                    goto Label_001D;
                }
            Label_0058:
                return this.mUnitJobIconSetList;
            }
        }

        private ScrollClamped_JobIcons ScrollClampedJobIcons
        {
            get
            {
                if ((this.mScrollClampedJobIcons == null) == null)
                {
                    goto Label_0039;
                }
                this.mScrollClampedJobIcons = this.mJobIconScrollListController.GetComponent<ScrollClamped_JobIcons>();
                this.mScrollClampedJobIcons.OnFrameOutItem = new ScrollClamped_JobIcons.FrameOutItem(this.RefreshJobIcon);
            Label_0039:
                return this.mScrollClampedJobIcons;
            }
        }

        [CompilerGenerated]
        private sealed class <OpenPlayBackUnitVoice>c__Iterator176 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <request>__0;
            internal PlayBackUnitVoice <window>__1;
            internal UnitData <bindUnit>__2;
            internal WindowController <wc>__3;
            internal int $PC;
            internal object $current;
            internal UnitModelViewer <>f__this;

            public <OpenPlayBackUnitVoice>c__Iterator176()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_00A1;

                    case 2:
                        goto Label_01EB;
                }
                goto Label_01F2;
            Label_0025:
                if ((this.<>f__this.mPlayBackVoiceWindow == null) == null)
                {
                    goto Label_00F1;
                }
                if (string.IsNullOrEmpty(UnitModelViewer.PLAYBACK_UNITVOICE_PREFAB_PATH) == null)
                {
                    goto Label_0059;
                }
                DebugUtility.LogError("ボイス再生リストのPrefabのPATHが指定されていません.");
                goto Label_01F2;
            Label_0059:
                this.<request>__0 = AssetManager.LoadAsync<GameObject>(UnitModelViewer.PLAYBACK_UNITVOICE_PREFAB_PATH);
                if (this.<request>__0 == null)
                {
                    goto Label_00A1;
                }
                if (this.<request>__0.isDone != null)
                {
                    goto Label_00A1;
                }
                this.$current = this.<request>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_01F4;
            Label_00A1:
                if (this.<request>__0 == null)
                {
                    goto Label_00C2;
                }
                if ((this.<request>__0.asset == null) == null)
                {
                    goto Label_00D1;
                }
            Label_00C2:
                DebugUtility.LogError("ボイス再生リストのPrefabが読み込めません.");
                goto Label_01F2;
            Label_00D1:
                this.<>f__this.mPlayBackVoiceWindow = Object.Instantiate(this.<request>__0.asset) as GameObject;
            Label_00F1:
                this.<window>__1 = this.<>f__this.mPlayBackVoiceWindow.GetComponentInChildren<PlayBackUnitVoice>();
                if ((this.<window>__1 != null) == null)
                {
                    goto Label_01D8;
                }
                this.<window>__1.OnCloseEvent = new PlayBackUnitVoice.CloseEvent(this.<>f__this.OnVoiceClose);
                this.<bindUnit>__2 = DataSource.FindDataOfClass<UnitData>(this.<>f__this.get_gameObject(), null);
                if (this.<bindUnit>__2 != null)
                {
                    goto Label_0165;
                }
                DebugUtility.LogError("3DViewerにUnitDataがBindされていません.");
                goto Label_01F2;
            Label_0165:
                DataSource.Bind<UnitData>(this.<>f__this.mPlayBackVoiceWindow, this.<bindUnit>__2);
                this.<wc>__3 = this.<>f__this.mPlayBackVoiceWindow.GetComponentInParent<WindowController>();
                if ((this.<wc>__3 == null) == null)
                {
                    goto Label_01B1;
                }
                DebugUtility.LogError("WindowControllerが存在しません.");
                goto Label_01F2;
            Label_01B1:
                this.<>f__this.mTouchControlArea.set_enabled(0);
                this.<window>__1.OnOpen();
                this.<wc>__3.Open();
            Label_01D8:
                this.$current = null;
                this.$PC = 2;
                goto Label_01F4;
            Label_01EB:
                this.$PC = -1;
            Label_01F2:
                return 0;
            Label_01F4:
                return 1;
                return flag;
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
        private sealed class <Refresh>c__AnonStorey3D5
        {
            internal UnitData unit;
            internal JobSetParam[] cc_jobset_array;

            public <Refresh>c__AnonStorey3D5()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey3D6
        {
            internal int i;
            internal UnitModelViewer.<Refresh>c__AnonStorey3D5 <>f__ref$981;

            public <Refresh>c__AnonStorey3D6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__478(JobSetParam jobset)
            {
                return (jobset.job == this.<>f__ref$981.unit.Jobs[this.i].JobID);
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey3D7
        {
            internal JobSetParam base_jobset_param;

            public <Refresh>c__AnonStorey3D7()
            {
                base..ctor();
                return;
            }

            internal bool <>m__479(JobData job)
            {
                return (job.JobID == this.base_jobset_param.job);
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey3D8
        {
            internal int j;
            internal UnitModelViewer.<Refresh>c__AnonStorey3D5 <>f__ref$981;

            public <Refresh>c__AnonStorey3D8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__47A(JobData job)
            {
                return (job.JobID == this.<>f__ref$981.cc_jobset_array[this.j].job);
            }
        }

        public delegate void ChangeJobSlotEvent(int index, GameObject target);

        public delegate void PlayReaction();

        public delegate void SkinSelectEvent(SRPG_Button button);
    }
}

