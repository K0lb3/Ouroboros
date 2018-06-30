namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class TrophyList : SRPG_ListBase, IFlowInterface
    {
        private const int CREATE_ENDED_PLATE_COUNT = 50;
        private const int CREATE_PULL_VIEW_COUNT_MAX = 50;
        public TrophyWindow trophy_window;
        public TrophyTypes TrophyType;
        public TrophyCategorys TrophyCategory;
        public TrophyRecordPullView OriginalPullView;
        public ListItemEvents Item_Normal;
        public ListItemEvents Item_Completed;
        public ListItemEvents Item_Ended;
        public GameObject DetailWindow;
        public ListItemEvents Item_Review;
        public ListItemEvents Item_FollowTwitter;
        public RectTransform parent_panel_rect;
        [SerializeField]
        private LayoutElement view_port_handle;
        private RectTransform view_port_handle_rect;
        private RectTransform scroll_trans_rect;
        public bool RefreshOnStart;
        private bool mStarted;
        private CanvasGroup mCanvasGroup;
        private List<TrophyRecordPullView> child_pull_viewes;
        private List<TrophyParam> child_plates;
        private bool is_busy;
        private float FOCUS_EFFECT_SECOND;
        private Vector2 start_pos;
        private Vector2 target_pos;
        private List<TrophyRecordPullView> click_target;
        private ScrollRect scroll_rect;
        private float total_close_item_size;
        private float focus_delay_time;
        private float elapsed_time;
        private eState state;

        public TrophyList()
        {
            this.child_pull_viewes = new List<TrophyRecordPullView>();
            this.child_plates = new List<TrophyParam>();
            this.FOCUS_EFFECT_SECOND = 0.2f;
            this.click_target = new List<TrophyRecordPullView>();
            base..ctor();
            return;
        }

        private void AchievementBtnSetting(SRPG_Button btn)
        {
            Text text;
            text = btn.get_transform().GetComponentInChildren<Text>();
            if ((text != null) == null)
            {
                goto Label_0028;
            }
            text.set_text(LocalizedText.Get("sys.TROPHY_BTN_CLEAR"));
        Label_0028:
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void Awake()
        {
            if ((this.Item_Normal != null) == null)
            {
                goto Label_0037;
            }
            if (this.Item_Normal.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0037;
            }
            this.Item_Normal.get_gameObject().SetActive(0);
        Label_0037:
            if ((this.Item_Review != null) == null)
            {
                goto Label_006E;
            }
            if (this.Item_Review.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_006E;
            }
            this.Item_Review.get_gameObject().SetActive(0);
        Label_006E:
            if ((this.Item_FollowTwitter != null) == null)
            {
                goto Label_00A5;
            }
            if (this.Item_FollowTwitter.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_00A5;
            }
            this.Item_FollowTwitter.get_gameObject().SetActive(0);
        Label_00A5:
            if ((this.Item_Completed != null) == null)
            {
                goto Label_00DC;
            }
            if (this.Item_Completed.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_00DC;
            }
            this.Item_Completed.get_gameObject().SetActive(0);
        Label_00DC:
            if ((this.Item_Ended != null) == null)
            {
                goto Label_0113;
            }
            if (this.Item_Ended.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0113;
            }
            this.Item_Ended.get_gameObject().SetActive(0);
        Label_0113:
            if ((this.DetailWindow != null) == null)
            {
                goto Label_0140;
            }
            if (this.DetailWindow.get_activeInHierarchy() == null)
            {
                goto Label_0140;
            }
            this.DetailWindow.SetActive(0);
        Label_0140:
            this.mCanvasGroup = base.GetComponent<CanvasGroup>();
            if ((this.mCanvasGroup == null) == null)
            {
                goto Label_016E;
            }
            this.mCanvasGroup = base.get_gameObject().AddComponent<CanvasGroup>();
        Label_016E:
            if ((this.scroll_rect == null) == null)
            {
                goto Label_018B;
            }
            this.scroll_rect = base.GetComponentInParent<ScrollRect>();
        Label_018B:
            if ((this.view_port_handle != null) == null)
            {
                goto Label_01AD;
            }
            this.view_port_handle_rect = this.view_port_handle.GetComponent<RectTransform>();
        Label_01AD:
            return;
        }

        private void ChallengeBtnSetting(SRPG_Button btn, TrophyParam trophy)
        {
            bool flag;
            VerticalLayoutGroup group;
            Text text;
            TrophyConditionTypes types;
            flag = 1;
            types = trophy.Objectives[0].type;
            switch ((types - 12))
            {
                case 0:
                    goto Label_0050;

                case 1:
                    goto Label_0050;

                case 2:
                    goto Label_0050;
            }
            if (types == 2)
            {
                goto Label_0050;
            }
            if (types == 3)
            {
                goto Label_0050;
            }
            if (types == 0x17)
            {
                goto Label_0050;
            }
            if (types == 0x2d)
            {
                goto Label_0050;
            }
            if (types == 0x34)
            {
                goto Label_0050;
            }
            goto Label_0057;
        Label_0050:
            flag = 0;
        Label_0057:
            if (trophy.DispType != 3)
            {
                goto Label_0065;
            }
            flag = 0;
        Label_0065:
            btn.get_gameObject().SetActive(flag);
            if (flag != null)
            {
                goto Label_00A0;
            }
            group = btn.get_transform().get_parent().GetComponent<VerticalLayoutGroup>();
            if ((group != null) == null)
            {
                goto Label_00C8;
            }
            group.set_childAlignment(7);
            goto Label_00C8;
        Label_00A0:
            text = btn.get_transform().GetComponentInChildren<Text>();
            if ((text != null) == null)
            {
                goto Label_00C8;
            }
            text.set_text(LocalizedText.Get("sys.TROPHY_BTN_GO"));
        Label_00C8:
            return;
        }

        private void ChangeState(eState _new_state)
        {
            eState state;
            if (this.state != _new_state)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            switch (this.state)
            {
                case 0:
                    goto Label_003B;

                case 1:
                    goto Label_0046;

                case 2:
                    goto Label_0067;

                case 3:
                    goto Label_0072;

                case 4:
                    goto Label_0072;

                case 5:
                    goto Label_0051;

                case 6:
                    goto Label_005C;
            }
            goto Label_0072;
        Label_003B:
            this.EndIdle();
            goto Label_0072;
        Label_0046:
            this.EndCloseOtherWait();
            goto Label_0072;
        Label_0051:
            this.EndOpen();
            goto Label_0072;
        Label_005C:
            this.EndClose();
            goto Label_0072;
        Label_0067:
            this.EndFocus();
        Label_0072:
            this.state = _new_state;
            switch (this.state)
            {
                case 0:
                    goto Label_00A7;

                case 1:
                    goto Label_00B2;

                case 2:
                    goto Label_00BD;

                case 3:
                    goto Label_00E9;

                case 4:
                    goto Label_00C8;

                case 5:
                    goto Label_00D3;

                case 6:
                    goto Label_00DE;
            }
            goto Label_00E9;
        Label_00A7:
            this.StartIdle();
            goto Label_00E9;
        Label_00B2:
            this.StartCloseOtherWait();
            goto Label_00E9;
        Label_00BD:
            this.StartFocus();
            goto Label_00E9;
        Label_00C8:
            this.StartCreatePullItems();
            goto Label_00E9;
        Label_00D3:
            this.StartOpen();
            goto Label_00E9;
        Label_00DE:
            this.StartClose();
        Label_00E9:
            return;
        }

        private void CheckTarget()
        {
            if (this.click_target.Count > 0)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.click_target[0].IsStateClosed == null)
            {
                goto Label_0043;
            }
            if (this.OpenPullView() == null)
            {
                goto Label_006F;
            }
            this.scroll_rect.StopMovement();
            goto Label_006F;
        Label_0043:
            if (this.click_target[0].IsStateOpened == null)
            {
                goto Label_006F;
            }
            if (this.ClosePullView() == null)
            {
                goto Label_006F;
            }
            this.scroll_rect.StopMovement();
        Label_006F:
            return;
        }

        private void ClearAllItems()
        {
            int num;
            num = 0;
            goto Label_001C;
        Label_0007:
            this.child_pull_viewes[num].ClearItems();
            num += 1;
        Label_001C:
            if (num < this.child_pull_viewes.Count)
            {
                goto Label_0007;
            }
            base.ClearItems();
            return;
        }

        private bool ClosePullView()
        {
            this.ChangeState(6);
            return 1;
        }

        protected void EndClose()
        {
            this.scroll_rect.set_verticalNormalizedPosition(Mathf.Clamp(this.scroll_rect.get_verticalNormalizedPosition(), 0f, 1f));
            return;
        }

        private unsafe void EndCloseOtherWait()
        {
            float num;
            Vector2 vector;
            Vector2 vector2;
            num = Mathf.Max(0f, &this.ScrollTransRect.get_anchoredPosition().y - this.total_close_item_size);
            this.ScrollTransRect.set_anchoredPosition(new Vector2(&this.ScrollTransRect.get_anchoredPosition().x, num));
            return;
        }

        private unsafe void EndFocus()
        {
            this.ScrollTransRect.set_anchoredPosition(new Vector2(0f, &this.target_pos.y));
            return;
        }

        private void EndIdle()
        {
            SRPG_TouchInputModule.LockInput();
            return;
        }

        private void EndOpen()
        {
        }

        private void FollowBtnSetting(SRPG_Button btn)
        {
            Text text;
            text = btn.get_transform().GetComponentInChildren<Text>();
            if ((text != null) == null)
            {
                goto Label_0028;
            }
            text.set_text(LocalizedText.Get("sys.TROPHY_BTN_FOLLOWTWITTER"));
        Label_0028:
            return;
        }

        private unsafe int[] GetSortedIndex(List<TrophyState> _trophies)
        {
            int[] numArray;
            int num;
            ulong[] numArray2;
            int num2;
            DateTime time;
            Dictionary<int, ulong> dictionary;
            numArray = new int[_trophies.Count];
            num = 0;
            goto Label_001B;
        Label_0013:
            numArray[num] = num;
            num += 1;
        Label_001B:
            if (num < _trophies.Count)
            {
                goto Label_0013;
            }
            numArray2 = new ulong[_trophies.Count];
            num2 = 0;
            goto Label_00D5;
        Label_003A:
            time = (_trophies[num2] == null) ? DateTime.MinValue : _trophies[num2].RewardedAt;
            numArray2[num2] = ((((((((long) &time.Year) % 100L) * 0x2540be400L) + ((((long) &time.Month) % 100L) * 0x5f5e100L)) + ((((long) &time.Day) % 100L) * 0xf4240L)) + ((((long) &time.Hour) % 100L) * 0x2710L)) + ((((long) &time.Minute) % 100L) * 100L)) + (((long) &time.Second) % 100L);
            num2 += 1;
        Label_00D5:
            if (num2 < _trophies.Count)
            {
                goto Label_003A;
            }
            dictionary = new Dictionary<int, ulong>();
            dictionary.Keys.CopyTo(numArray, 0);
            dictionary.Values.CopyTo(numArray2, 0);
            Array.Sort<ulong, int>(numArray2, numArray);
            Array.Reverse(numArray);
            return numArray;
        }

        private void GotoArena()
        {
            PlayerData data;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data.CheckUnlock(0x10) == null)
            {
                goto Label_002D;
            }
            this.trophy_window.ActivateOutputLinks(0x7d7);
            goto Label_0041;
        Label_002D:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x10);
        Label_0041:
            return;
        }

        private void GotoBeginnerTop()
        {
            this.trophy_window.ActivateOutputLinks(0x7f2);
            return;
        }

        private void GotoEquip()
        {
            PlayerData data;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data.CheckUnlock(0x800) == null)
            {
                goto Label_0030;
            }
            this.trophy_window.ActivateOutputLinks(0x7d4);
            goto Label_0047;
        Label_0030:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x800);
        Label_0047:
            return;
        }

        private void GotoMultiTower()
        {
            GlobalVars.ReqEventPageListType = 2;
            this.trophy_window.ActivateOutputLinks(0x7ef);
            return;
        }

        private void GotoShop(TrophyParam param)
        {
            char[] chArray1;
            PlayerData data;
            EShopType type;
            char[] chArray;
            string[] strArray;
            UnlockTargets targets;
            EShopType type2;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (string.IsNullOrEmpty(param.Objectives[0].sval_base) == null)
            {
                goto Label_0029;
            }
            type = 0;
            goto Label_0070;
        Label_0029:
            chArray1 = new char[] { 0x2c };
            chArray = chArray1;
            strArray = param.Objectives[0].sval_base.Split(chArray);
            if (string.IsNullOrEmpty(strArray[0]) == null)
            {
                goto Label_005D;
            }
            type = 0;
            goto Label_0070;
        Label_005D:
            type = MonoSingleton<GameManager>.Instance.MasterParam.GetShopType(strArray[0]);
        Label_0070:
            if (type < 0)
            {
                goto Label_014C;
            }
            if (data.CheckUnlockShopType(type) == null)
            {
                goto Label_014C;
            }
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_00BE;

                case 1:
                    goto Label_00BE;

                case 2:
                    goto Label_00BE;

                case 3:
                    goto Label_0147;

                case 4:
                    goto Label_0147;

                case 5:
                    goto Label_00D3;

                case 6:
                    goto Label_00DE;

                case 7:
                    goto Label_00F3;

                case 8:
                    goto Label_0108;

                case 9:
                    goto Label_0132;

                case 10:
                    goto Label_011D;
            }
            goto Label_0147;
        Label_00BE:
            this.trophy_window.ActivateOutputLinks(0x7dd);
            goto Label_014C;
        Label_00D3:
            this.GotoArena();
            goto Label_014C;
        Label_00DE:
            this.trophy_window.ActivateOutputLinks(0x7d5);
            goto Label_014C;
        Label_00F3:
            this.trophy_window.ActivateOutputLinks(0x7e4);
            goto Label_014C;
        Label_0108:
            this.trophy_window.ActivateOutputLinks(0x7e5);
            goto Label_014C;
        Label_011D:
            this.trophy_window.ActivateOutputLinks(0x7eb);
            goto Label_014C;
        Label_0132:
            this.trophy_window.ActivateOutputLinks(0x7ec);
            goto Label_014C;
        Label_0147:;
        Label_014C:
            if (type < 0)
            {
                goto Label_018F;
            }
            if (MonoSingleton<GameManager>.Instance.Player.CheckUnlockShopType(type) != null)
            {
                goto Label_018F;
            }
        Label_0168:
            try
            {
                targets = SRPG_Extensions.ToUnlockTargets(type);
                LevelLock.ShowLockMessage(data.Lv, data.VipRank, targets);
                goto Label_018F;
            }
            catch (Exception)
            {
            Label_0189:
                goto Label_018F;
            }
        Label_018F:
            return;
        }

        private void GotoVersus()
        {
            PlayerData data;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data.CheckUnlock(0x10000) == null)
            {
                goto Label_0030;
            }
            this.trophy_window.ActivateOutputLinks(0x7ee);
            goto Label_0047;
        Label_0030:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x10000);
        Label_0047:
            return;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            this.Run();
            return;
        }

        private TrophyRecordPullView MakeTrophyCategory(string _title_str)
        {
            TrophyRecordPullView view;
            view = Object.Instantiate<TrophyRecordPullView>(this.OriginalPullView);
            view.Init(_title_str);
            view.get_transform().SetParent(base.get_transform(), 0);
            return view;
        }

        public ListItemEvents MakeTrophyPlate(TrophyState st, bool is_achievement)
        {
            ListItemEvents events;
            ListItemEvents events2;
            SRPG_Button button;
            RewardData data;
            if (st.IsEnded == null)
            {
                goto Label_0017;
            }
            events = this.Item_Ended;
            goto Label_0063;
        Label_0017:
            if (((this.Item_FollowTwitter != null) == null) || (st.Param.ContainsCondition(0x12) == null))
            {
                goto Label_0046;
            }
            events = this.Item_FollowTwitter;
            goto Label_0063;
        Label_0046:
            events = (st.IsCompleted == null) ? this.Item_Normal : this.Item_Completed;
        Label_0063:
            if ((events == null) == null)
            {
                goto Label_0071;
            }
            return null;
        Label_0071:
            events2 = Object.Instantiate<ListItemEvents>(events);
            DataSource.Bind<TrophyParam>(events2.get_gameObject(), st.Param);
            events2.get_transform().SetParent(base.get_transform(), 0);
            events2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            button = events2.GetComponentInChildren<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0130;
            }
            if (st.IsEnded != null)
            {
                goto Label_0130;
            }
            if ((this.Item_FollowTwitter != null) == null)
            {
                goto Label_010C;
            }
            if (st.Param.ContainsCondition(0x12) == null)
            {
                goto Label_010C;
            }
            this.FollowBtnSetting(button);
            goto Label_0130;
        Label_010C:
            if (st.IsCompleted == null)
            {
                goto Label_0123;
            }
            this.AchievementBtnSetting(button);
            goto Label_0130;
        Label_0123:
            this.ChallengeBtnSetting(button, st.Param);
        Label_0130:
            data = new RewardData(st.Param);
            DataSource.Bind<RewardData>(events2.get_gameObject(), data);
            events2.get_gameObject().SetActive(1);
            return events2;
        }

        private void MsgBoxJumpToQuest(GameObject go)
        {
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if ((HomeWindow.Current != null) == null)
            {
                goto Label_0020;
            }
            HomeWindow.Current.UnlockContents();
        Label_0020:
            return;
        }

        private void OnDisable()
        {
            GameManager local1;
            this.ClearAllItems();
            if ((MonoSingleton<GameManager>.GetInstanceDirect() != null) == null)
            {
                goto Label_003C;
            }
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnDayChange = (GameManager.DayChangeEvent) Delegate.Remove(local1.OnDayChange, new GameManager.DayChangeEvent(this.OnTrophyReset));
        Label_003C:
            return;
        }

        private void OnEnable()
        {
            GameManager local1;
            this.ScrollTransRect.set_anchoredPosition(Vector2.get_zero());
            this.Refresh();
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnDayChange = (GameManager.DayChangeEvent) Delegate.Combine(local1.OnDayChange, new GameManager.DayChangeEvent(this.OnTrophyReset));
            return;
        }

        private void OnItemDetail(GameObject go)
        {
            TrophyParam param;
            GameObject obj2;
            RewardData data;
            param = DataSource.FindDataOfClass<TrophyParam>(go, null);
            if ((this.DetailWindow != null) == null)
            {
                goto Label_0047;
            }
            if (param == null)
            {
                goto Label_0047;
            }
            obj2 = Object.Instantiate<GameObject>(this.DetailWindow);
            DataSource.Bind<TrophyParam>(obj2, param);
            data = new RewardData(param);
            DataSource.Bind<RewardData>(obj2, data);
            obj2.SetActive(1);
        Label_0047:
            return;
        }

        private unsafe void OnItemSelect(GameObject go)
        {
            TrophyParam param;
            PlayerData data;
            TrophyState state;
            RewardData data2;
            QuestParam param2;
            QuestTypes types;
            QuestTypes types2;
            TrophyConditionTypes types3;
            QuestTypes types4;
            param = DataSource.FindDataOfClass<TrophyParam>(go, null);
            if (param == null)
            {
                goto Label_0606;
            }
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(param, 1);
            if (state.IsEnded != null)
            {
                goto Label_00BC;
            }
            if (state.IsCompleted == null)
            {
                goto Label_00BC;
            }
            if (param.IsInvisibleStamina() != null)
            {
                goto Label_0054;
            }
            if (param.IsAvailablePeriod(TimeManager.ServerTime, 1) != null)
            {
                goto Label_0070;
            }
        Label_0054:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.TROPHY_OUTDATED"), null, null, 0, -1);
            this.Refresh();
            return;
        Label_0070:
            GlobalVars.SelectedTrophy.Set(param.iname);
            data2 = new RewardData(param);
            GlobalVars.LastReward.Set(data2);
            GlobalVars.UnitGetReward = new UnitGetParam(data2.Items.ToArray());
            this.trophy_window.ActivateOutputLinks(0x7d0);
            goto Label_0606;
        Label_00BC:
            param2 = new QuestParam();
            switch ((param.Objectives[0].type - 1))
            {
                case 0:
                    goto Label_029D;

                case 1:
                    goto Label_0606;

                case 2:
                    goto Label_0606;

                case 3:
                    goto Label_029D;

                case 4:
                    goto Label_0395;

                case 5:
                    goto Label_0426;

                case 6:
                    goto Label_0288;

                case 7:
                    goto Label_0411;

                case 8:
                    goto Label_048E;

                case 9:
                    goto Label_03F1;

                case 10:
                    goto Label_03FC;

                case 11:
                    goto Label_0606;

                case 12:
                    goto Label_0606;

                case 13:
                    goto Label_0606;

                case 14:
                    goto Label_0444;

                case 15:
                    goto Label_0444;

                case 0x10:
                    goto Label_0464;

                case 0x11:
                    goto Label_0606;

                case 0x12:
                    goto Label_044F;

                case 0x13:
                    goto Label_0479;

                case 20:
                    goto Label_0479;

                case 0x15:
                    goto Label_0479;

                case 0x16:
                    goto Label_0606;

                case 0x17:
                    goto Label_0479;

                case 0x18:
                    goto Label_0479;

                case 0x19:
                    goto Label_04A3;

                case 0x1a:
                    goto Label_04A3;

                case 0x1b:
                    goto Label_04A3;

                case 0x1c:
                    goto Label_04B8;

                case 0x1d:
                    goto Label_04B8;

                case 30:
                    goto Label_04B8;

                case 0x1f:
                    goto Label_048E;

                case 0x20:
                    goto Label_048E;

                case 0x21:
                    goto Label_029D;

                case 0x22:
                    goto Label_0411;

                case 0x23:
                    goto Label_04CD;

                case 0x24:
                    goto Label_04D9;

                case 0x25:
                    goto Label_04D9;

                case 0x26:
                    goto Label_04D9;

                case 0x27:
                    goto Label_0411;

                case 40:
                    goto Label_0411;

                case 0x29:
                    goto Label_0288;

                case 0x2a:
                    goto Label_04A3;

                case 0x2b:
                    goto Label_04B8;

                case 0x2c:
                    goto Label_0606;

                case 0x2d:
                    goto Label_04EE;

                case 0x2e:
                    goto Label_029D;

                case 0x2f:
                    goto Label_0395;

                case 0x30:
                    goto Label_0426;

                case 0x31:
                    goto Label_04EE;

                case 50:
                    goto Label_0444;

                case 0x33:
                    goto Label_0606;

                case 0x34:
                    goto Label_057C;

                case 0x35:
                    goto Label_057C;

                case 0x36:
                    goto Label_0606;

                case 0x37:
                    goto Label_04D9;

                case 0x38:
                    goto Label_04D9;

                case 0x39:
                    goto Label_0444;

                case 0x3a:
                    goto Label_0444;

                case 0x3b:
                    goto Label_04EE;

                case 60:
                    goto Label_04EE;

                case 0x3d:
                    goto Label_057C;

                case 0x3e:
                    goto Label_0426;

                case 0x3f:
                    goto Label_0426;

                case 0x40:
                    goto Label_0426;

                case 0x41:
                    goto Label_0426;

                case 0x42:
                    goto Label_0426;

                case 0x43:
                    goto Label_0426;

                case 0x44:
                    goto Label_0426;

                case 0x45:
                    goto Label_0426;

                case 70:
                    goto Label_0426;

                case 0x47:
                    goto Label_0426;

                case 0x48:
                    goto Label_0426;

                case 0x49:
                    goto Label_0426;

                case 0x4a:
                    goto Label_03C3;

                case 0x4b:
                    goto Label_0587;

                case 0x4c:
                    goto Label_0587;

                case 0x4d:
                    goto Label_029D;

                case 0x4e:
                    goto Label_029D;

                case 0x4f:
                    goto Label_029D;

                case 80:
                    goto Label_0592;

                case 0x51:
                    goto Label_0592;

                case 0x52:
                    goto Label_059D;

                case 0x53:
                    goto Label_059D;

                case 0x54:
                    goto Label_059D;

                case 0x55:
                    goto Label_059D;

                case 0x56:
                    goto Label_059D;

                case 0x57:
                    goto Label_059D;

                case 0x58:
                    goto Label_059D;

                case 0x59:
                    goto Label_05B2;

                case 90:
                    goto Label_05B2;

                case 0x5b:
                    goto Label_05B2;

                case 0x5c:
                    goto Label_05B2;

                case 0x5d:
                    goto Label_05B2;

                case 0x5e:
                    goto Label_05B2;

                case 0x5f:
                    goto Label_05B2;

                case 0x60:
                    goto Label_05B2;

                case 0x61:
                    goto Label_05B2;

                case 0x62:
                    goto Label_05F1;

                case 0x63:
                    goto Label_029D;

                case 100:
                    goto Label_029D;

                case 0x65:
                    goto Label_029D;

                case 0x66:
                    goto Label_0426;

                case 0x67:
                    goto Label_05C7;

                case 0x68:
                    goto Label_05C7;

                case 0x69:
                    goto Label_05DC;
            }
            goto Label_0606;
        Label_0288:
            this.trophy_window.ActivateOutputLinks(0x7d1);
            goto Label_0606;
        Label_029D:
            types = 0;
            if (param2.TransSectionGotoQuest(param.Objectives[0].sval_base, &types, new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)) != null)
            {
                goto Label_02C8;
            }
            return;
        Label_02C8:
            types4 = types;
            switch ((types4 - 1))
            {
                case 0:
                    goto Label_033C;

                case 1:
                    goto Label_037B;

                case 2:
                    goto Label_037B;

                case 3:
                    goto Label_037B;

                case 4:
                    goto Label_0312;

                case 5:
                    goto Label_0351;

                case 6:
                    goto Label_0327;

                case 7:
                    goto Label_037B;

                case 8:
                    goto Label_037B;

                case 9:
                    goto Label_0312;

                case 10:
                    goto Label_037B;

                case 11:
                    goto Label_037B;

                case 12:
                    goto Label_0366;

                case 13:
                    goto Label_033C;
            }
            goto Label_037B;
        Label_0312:
            this.trophy_window.ActivateOutputLinks(0x7d6);
            goto Label_0390;
        Label_0327:
            this.trophy_window.ActivateOutputLinks(0x7ea);
            goto Label_0390;
        Label_033C:
            this.trophy_window.ActivateOutputLinks(0x7d5);
            goto Label_0390;
        Label_0351:
            this.trophy_window.ActivateOutputLinks(0x7f0);
            goto Label_0390;
        Label_0366:
            this.trophy_window.ActivateOutputLinks(0x7f1);
            goto Label_0390;
        Label_037B:
            this.trophy_window.ActivateOutputLinks(0x7d2);
        Label_0390:
            goto Label_0606;
        Label_0395:
            if (param2.TransSectionGotoElite(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)) != null)
            {
                goto Label_03AE;
            }
            return;
        Label_03AE:
            this.trophy_window.ActivateOutputLinks(0x7d2);
            goto Label_0606;
        Label_03C3:
            if (param2.TransSectionGotoStoryExtra(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)) != null)
            {
                goto Label_03DC;
            }
            return;
        Label_03DC:
            this.trophy_window.ActivateOutputLinks(0x7d2);
            goto Label_0606;
        Label_03F1:
            this.GotoEquip();
            goto Label_0606;
        Label_03FC:
            this.trophy_window.ActivateOutputLinks(0x7d3);
            goto Label_0606;
        Label_0411:
            this.trophy_window.ActivateOutputLinks(0x7d5);
            goto Label_0606;
        Label_0426:
            param2.GotoEventListQuest(null, null);
            this.trophy_window.ActivateOutputLinks(0x7d6);
            goto Label_0606;
        Label_0444:
            this.GotoArena();
            goto Label_0606;
        Label_044F:
            this.trophy_window.ActivateOutputLinks(0x7d8);
            goto Label_0606;
        Label_0464:
            this.trophy_window.ActivateOutputLinks(0x7ed);
            goto Label_0606;
        Label_0479:
            this.trophy_window.ActivateOutputLinks(0x7e9);
            goto Label_0606;
        Label_048E:
            this.trophy_window.ActivateOutputLinks(0x7e9);
            goto Label_0606;
        Label_04A3:
            this.trophy_window.ActivateOutputLinks(0x7e9);
            goto Label_0606;
        Label_04B8:
            this.trophy_window.ActivateOutputLinks(0x7e9);
            goto Label_0606;
        Label_04CD:
            this.GotoShop(param);
            goto Label_0606;
        Label_04D9:
            this.trophy_window.ActivateOutputLinks(0x7e5);
            goto Label_0606;
        Label_04EE:
            types2 = 7;
            if (param2.TransSectionGotoTower(param.Objectives[0].sval_base, &types2) != null)
            {
                goto Label_050D;
            }
            return;
        Label_050D:
            types4 = types2;
            switch ((types4 - 10))
            {
                case 0:
                    goto Label_0538;

                case 1:
                    goto Label_052B;

                case 2:
                    goto Label_052B;

                case 3:
                    goto Label_054D;
            }
        Label_052B:
            if (types4 == 5)
            {
                goto Label_0538;
            }
            goto Label_0562;
        Label_0538:
            this.trophy_window.ActivateOutputLinks(0x7d6);
            goto Label_0577;
        Label_054D:
            this.trophy_window.ActivateOutputLinks(0x7f1);
            goto Label_0577;
        Label_0562:
            this.trophy_window.ActivateOutputLinks(0x7ea);
        Label_0577:
            goto Label_0606;
        Label_057C:
            this.GotoVersus();
            goto Label_0606;
        Label_0587:
            this.GotoMultiTower();
            goto Label_0606;
        Label_0592:
            this.GotoBeginnerTop();
            goto Label_0606;
        Label_059D:
            this.trophy_window.ActivateOutputLinks(0x7f3);
            goto Label_0606;
        Label_05B2:
            this.trophy_window.ActivateOutputLinks(0x7e9);
            goto Label_0606;
        Label_05C7:
            this.trophy_window.ActivateOutputLinks(0x7f4);
            goto Label_0606;
        Label_05DC:
            this.trophy_window.ActivateOutputLinks(0x7f6);
            goto Label_0606;
        Label_05F1:
            this.trophy_window.ActivateOutputLinks(0x7f7);
        Label_0606:
            return;
        }

        private void OnTrophyReset()
        {
            this.Refresh();
            return;
        }

        private bool OpenPullView()
        {
            if (this.child_pull_viewes == null)
            {
                goto Label_001C;
            }
            if (this.child_pull_viewes.Count > 0)
            {
                goto Label_0025;
            }
        Label_001C:
            this.ChangeState(2);
            return 1;
        Label_0025:
            this.ChangeState(1);
            return 1;
        }

        private void Refresh()
        {
            TrophyTypes types;
            if (this.mStarted != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.mCanvasGroup != null) == null)
            {
                goto Label_002D;
            }
            this.mCanvasGroup.set_alpha(0f);
        Label_002D:
            this.ClearAllItems();
            switch (this.TrophyType)
            {
                case 0:
                    goto Label_0051;

                case 1:
                    goto Label_005C;

                case 2:
                    goto Label_0075;
            }
            goto Label_008F;
        Label_0051:
            this.RefreshTrophyRecord();
            goto Label_008F;
        Label_005C:
            this.RefreshTrophySimple(this.Trophy_Window.TrophyDailyDatas, -1, 0, 1);
            goto Label_008F;
        Label_0075:
            this.RefreshTrophySimple(this.Trophy_Window.TrophyEndedDatas, 50, 1, 0);
        Label_008F:
            this.ScrollTransRect.set_anchoredPosition(Vector2.get_zero());
            return;
        }

        public void RefreshLight()
        {
            if (this.TrophyType != null)
            {
                goto Label_0016;
            }
            this.RefreshTrophyRecordLight();
            goto Label_001C;
        Label_0016:
            this.Refresh();
        Label_001C:
            return;
        }

        private unsafe void RefreshTrophyRecord()
        {
            List<TrophyCategoryData> list;
            int num;
            Dictionary<int, TrophyRecordPullView> dictionary;
            TrophyRecordPullView view;
            int num2;
            ListItemEvents events;
            int num3;
            int num4;
            int num5;
            Dictionary<int, TrophyRecordPullView>.KeyCollection.Enumerator enumerator;
            if (this.Trophy_Window.TrophyRecordDatas.ContainsKey(this.TrophyCategory) != null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            list = this.Trophy_Window.TrophyRecordDatas[this.TrophyCategory];
            if (list == null)
            {
                goto Label_0045;
            }
            if (list.Count > 0)
            {
                goto Label_0046;
            }
        Label_0045:
            return;
        Label_0046:
            num = 50;
            dictionary = new Dictionary<int, TrophyRecordPullView>();
            view = null;
            this.child_pull_viewes.Clear();
            num2 = 0;
            goto Label_014C;
        Label_0064:
            if (num != null)
            {
                goto Label_006F;
            }
            goto Label_0159;
        Label_006F:
            if (dictionary.ContainsKey(list[num2].Param.hash_code) != null)
            {
                goto Label_010F;
            }
            if (list[num2].Param.IsNotPull == null)
            {
                goto Label_00A8;
            }
            goto Label_0146;
        Label_00A8:
            view = this.MakeTrophyCategory(list[num2].Param.iname);
            if ((view != null) == null)
            {
                goto Label_010F;
            }
            num -= 1;
            view.Setup(dictionary.Count, this);
            dictionary.Add(list[num2].Param.hash_code, view);
            this.child_pull_viewes.Add(view);
            base.AddItem(view.GetComponent<ListItemEvents>());
        Label_010F:
            dictionary[list[num2].Param.hash_code].SetCategoryData(list[num2]);
            if (dictionary.Count < 50)
            {
                goto Label_0146;
            }
            goto Label_0159;
        Label_0146:
            num2 += 1;
        Label_014C:
            if (num2 < list.Count)
            {
                goto Label_0064;
            }
        Label_0159:
            events = null;
            this.child_plates.Clear();
            num3 = 0;
            goto Label_0229;
        Label_016F:
            if (num != null)
            {
                goto Label_017A;
            }
            goto Label_0236;
        Label_017A:
            if (list[num3].Param.IsNotPull != null)
            {
                goto Label_0196;
            }
            goto Label_0223;
        Label_0196:
            num4 = 0;
            goto Label_020A;
        Label_019E:
            events = this.MakeTrophyPlate(list[num3].Trophies[num4], list[num3].Trophies[num4].IsCompleted);
            if ((events != null) == null)
            {
                goto Label_0204;
            }
            this.child_plates.Add(DataSource.FindDataOfClass<TrophyParam>(events.get_gameObject(), null));
            base.AddItem(events);
            num -= 1;
        Label_0204:
            num4 += 1;
        Label_020A:
            if (num4 < list[num3].Trophies.Count)
            {
                goto Label_019E;
            }
        Label_0223:
            num3 += 1;
        Label_0229:
            if (num3 < list.Count)
            {
                goto Label_016F;
            }
        Label_0236:
            enumerator = dictionary.Keys.GetEnumerator();
        Label_0243:
            try
            {
                goto Label_025E;
            Label_0248:
                num5 = &enumerator.Current;
                dictionary[num5].RefreshDisplayParam();
            Label_025E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0248;
                }
                goto Label_027C;
            }
            finally
            {
            Label_026F:
                ((Dictionary<int, TrophyRecordPullView>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_027C:
            this.view_port_handle.get_transform().SetAsLastSibling();
            return;
        }

        private void RefreshTrophyRecordLight()
        {
            List<TrophyCategoryData> list;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            if ((this.mCanvasGroup != null) == null)
            {
                goto Label_0021;
            }
            this.mCanvasGroup.set_alpha(0f);
        Label_0021:
            if (this.Trophy_Window.TrophyRecordDatas.ContainsKey(this.TrophyCategory) != null)
            {
                goto Label_0043;
            }
            this.ClearAllItems();
            return;
        Label_0043:
            list = this.Trophy_Window.TrophyRecordDatas[this.TrophyCategory];
            if (list == null)
            {
                goto Label_006C;
            }
            if (list.Count > 0)
            {
                goto Label_0073;
            }
        Label_006C:
            this.ClearAllItems();
            return;
        Label_0073:
            num = this.child_pull_viewes.Count;
            num2 = 0;
            goto Label_0109;
        Label_0086:
            if (list[num2].Param.IsNotPull == null)
            {
                goto Label_00A1;
            }
            goto Label_0105;
        Label_00A1:
            num3 = 0;
            goto Label_00F4;
        Label_00A8:
            if (list[num2].Param.hash_code == this.child_pull_viewes[num3].HashCode)
            {
                goto Label_00D4;
            }
            goto Label_00F0;
        Label_00D4:
            this.child_pull_viewes[num3].SetCategoryData(list[num2]);
            num -= 1;
        Label_00F0:
            num3 += 1;
        Label_00F4:
            if (num3 < this.child_pull_viewes.Count)
            {
                goto Label_00A8;
            }
        Label_0105:
            num2 += 1;
        Label_0109:
            if (num2 < list.Count)
            {
                goto Label_0086;
            }
            if (num == null)
            {
                goto Label_0122;
            }
            this.Refresh();
            return;
        Label_0122:
            num = this.child_plates.Count;
            num4 = 0;
            goto Label_01E2;
        Label_0136:
            if (list[num4].Param.IsNotPull != null)
            {
                goto Label_0152;
            }
            goto Label_01DC;
        Label_0152:
            num5 = 0;
            goto Label_01C3;
        Label_015A:
            num6 = 0;
            goto Label_01AB;
        Label_0162:
            if ((list[num4].Trophies[num5].Param.iname != this.child_plates[num6].iname) == null)
            {
                goto Label_01A1;
            }
            goto Label_01A5;
        Label_01A1:
            num -= 1;
        Label_01A5:
            num6 += 1;
        Label_01AB:
            if (num6 < this.child_plates.Count)
            {
                goto Label_0162;
            }
            num5 += 1;
        Label_01C3:
            if (num5 < list[num4].Trophies.Count)
            {
                goto Label_015A;
            }
        Label_01DC:
            num4 += 1;
        Label_01E2:
            if (num4 < list.Count)
            {
                goto Label_0136;
            }
            if (num == null)
            {
                goto Label_01FC;
            }
            this.Refresh();
            return;
        Label_01FC:
            num7 = 0;
            goto Label_0222;
        Label_0204:
            this.child_pull_viewes[num7].Refresh(this.ScrollTransRect);
            num7 += 1;
        Label_0222:
            if (num7 < this.child_pull_viewes.Count)
            {
                goto Label_0204;
            }
            return;
        }

        public void RefreshTrophySimple(List<TrophyState> _trophies, int _create_count, bool _need_sort, bool _daily_comp_check)
        {
            int num;
            int[] numArray;
            ListItemEvents events;
            int num2;
            int num3;
            if ((_trophies != null) && (_trophies.Count > 0))
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            num = _create_count;
            numArray = null;
            if (_need_sort == null)
            {
                goto Label_0025;
            }
            numArray = this.GetSortedIndex(_trophies);
        Label_0025:
            if (_daily_comp_check == null)
            {
                goto Label_003B;
            }
            MonoSingleton<GameManager>.Instance.Player.DailyAllCompleteCheck();
        Label_003B:
            events = null;
            num2 = 0;
            this.child_plates.Clear();
            num3 = 0;
            goto Label_00BD;
        Label_0052:
            if (num != null)
            {
                goto Label_005D;
            }
            goto Label_00CA;
        Label_005D:
            num2 = (_need_sort == null) ? num3 : numArray[num3];
            events = this.MakeTrophyPlate(_trophies[num2], _trophies[num2].IsCompleted);
            if ((events != null) == null)
            {
                goto Label_00B7;
            }
            this.child_plates.Add(DataSource.FindDataOfClass<TrophyParam>(events.get_gameObject(), null));
            base.AddItem(events);
            num -= 1;
        Label_00B7:
            num3 += 1;
        Label_00BD:
            if (num3 < _trophies.Count)
            {
                goto Label_0052;
            }
        Label_00CA:
            return;
        }

        private void Run()
        {
            eState state;
            switch (this.state)
            {
                case 0:
                    goto Label_002E;

                case 1:
                    goto Label_0039;

                case 2:
                    goto Label_0044;

                case 3:
                    goto Label_004F;

                case 4:
                    goto Label_005A;

                case 5:
                    goto Label_0065;

                case 6:
                    goto Label_0070;
            }
            goto Label_007B;
        Label_002E:
            this.UpdateIdle();
            goto Label_007B;
        Label_0039:
            this.UpdateCloseOtherWait();
            goto Label_007B;
        Label_0044:
            this.UpdateFocus();
            goto Label_007B;
        Label_004F:
            this.UpdateFocusInterval();
            goto Label_007B;
        Label_005A:
            this.UpdateCreatePullItems();
            goto Label_007B;
        Label_0065:
            this.UpdateOpen();
            goto Label_007B;
        Label_0070:
            this.UpdateClose();
        Label_007B:
            return;
        }

        public void SetClickTarget(TrophyRecordPullView _pull_view)
        {
            if (this.is_busy == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.is_busy = 1;
            this.click_target.Add(_pull_view);
            return;
        }

        protected override void Start()
        {
            base.Start();
            this.mStarted = 1;
            if (this.RefreshOnStart == null)
            {
                goto Label_001E;
            }
            this.Refresh();
        Label_001E:
            return;
        }

        private void StartClose()
        {
            this.view_port_handle.set_minHeight(this.view_port_handle.get_minHeight() + this.click_target[0].TargetViewPortSize);
            this.click_target[0].StartClose();
            return;
        }

        private void StartCloseOtherWait()
        {
            int num;
            this.total_close_item_size = 0f;
            num = 0;
            goto Label_00AF;
        Label_0012:
            if ((this.child_pull_viewes[num] == this.click_target[0]) == null)
            {
                goto Label_0039;
            }
            goto Label_00AB;
        Label_0039:
            if (this.child_pull_viewes[num].IsStateOpen != null)
            {
                goto Label_0054;
            }
            goto Label_00AB;
        Label_0054:
            this.child_pull_viewes[num].ChangeState(3);
            if (this.child_pull_viewes[num].Index >= this.click_target[0].Index)
            {
                goto Label_00AB;
            }
            this.total_close_item_size += this.child_pull_viewes[num].RootLayoutElementMinHeightDef;
        Label_00AB:
            num += 1;
        Label_00AF:
            if (num < this.child_pull_viewes.Count)
            {
                goto Label_0012;
            }
            return;
        }

        private unsafe void StartCreatePullItems()
        {
            float num;
            float num2;
            float num3;
            Vector2 vector;
            Vector2 vector2;
            Rect rect;
            this.click_target[0].CreateContents();
            num = (&this.ScrollTransRect.get_anchoredPosition().y + &this.view_port_handle_rect.get_anchoredPosition().y) - this.click_target[0].TargetViewPortSize;
            num2 = &this.parent_panel_rect.get_rect().get_height() + num;
            num3 = num2 - this.click_target[0].VerticalLayoutSpacing;
            num3 = Mathf.Max(0f, num3);
            this.view_port_handle.set_minHeight((this.view_port_handle.get_minHeight() < num3) ? num3 : this.view_port_handle.get_minHeight());
            return;
        }

        private unsafe void StartFocus()
        {
            Vector2 vector;
            this.focus_delay_time = 0.2f;
            this.elapsed_time = 0f;
            this.start_pos = this.ScrollTransRect.get_anchoredPosition();
            this.target_pos = new Vector2(&this.ScrollTransRect.get_anchoredPosition().x, ((float) this.click_target[0].Index) * this.click_target[0].ItemDistance);
            return;
        }

        private void StartIdle()
        {
            this.is_busy = 0;
            SRPG_TouchInputModule.UnlockInput(0);
            return;
        }

        private void StartOpen()
        {
            this.click_target[0].StartOpen();
            return;
        }

        private void Update()
        {
            if ((this.mCanvasGroup != null) == null)
            {
                goto Label_004D;
            }
            if (this.mCanvasGroup.get_alpha() >= 1f)
            {
                goto Label_004D;
            }
            this.mCanvasGroup.set_alpha(Mathf.Clamp01(this.mCanvasGroup.get_alpha() + (Time.get_unscaledDeltaTime() * 3.333333f)));
        Label_004D:
            return;
        }

        protected void UpdateClose()
        {
            this.scroll_rect.set_verticalNormalizedPosition(Mathf.Clamp(this.scroll_rect.get_verticalNormalizedPosition(), 0f, 1f));
            if (this.click_target[0].IsStateClosed != null)
            {
                goto Label_003C;
            }
            return;
        Label_003C:
            this.ChangeState(0);
            this.click_target.Clear();
            return;
        }

        private void UpdateCloseOtherWait()
        {
            int num;
            num = 0;
            goto Label_0049;
        Label_0007:
            if ((this.child_pull_viewes[num] == this.click_target[0]) == null)
            {
                goto Label_002E;
            }
            goto Label_0045;
        Label_002E:
            if (this.child_pull_viewes[num].IsStateClosed != null)
            {
                goto Label_0045;
            }
            return;
        Label_0045:
            num += 1;
        Label_0049:
            if (num < this.child_pull_viewes.Count)
            {
                goto Label_0007;
            }
            this.ChangeState(2);
            return;
        }

        private void UpdateCreatePullItems()
        {
            this.ChangeState(5);
            return;
        }

        private unsafe void UpdateFocus()
        {
            Vector2 vector;
            this.focus_delay_time -= Time.get_deltaTime();
            if (this.focus_delay_time <= 0f)
            {
                goto Label_0034;
            }
            this.ScrollTransRect.set_anchoredPosition(this.start_pos);
            return;
        Label_0034:
            this.elapsed_time += Time.get_deltaTime();
            this.elapsed_time = Mathf.Min(this.elapsed_time, this.FOCUS_EFFECT_SECOND);
            vector = Vector2.Lerp(this.start_pos, this.target_pos, this.elapsed_time / this.FOCUS_EFFECT_SECOND);
            this.ScrollTransRect.set_anchoredPosition(new Vector2(0f, &vector.y));
            if (this.elapsed_time < this.FOCUS_EFFECT_SECOND)
            {
                goto Label_00B0;
            }
            this.ChangeState(3);
        Label_00B0:
            return;
        }

        private void UpdateFocusInterval()
        {
            this.ChangeState(4);
            return;
        }

        private unsafe void UpdateHandleHeight()
        {
            float num;
            float num2;
            float num3;
            Vector2 vector;
            Vector2 vector2;
            Rect rect;
            if ((this.view_port_handle == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.view_port_handle.get_minHeight() > 0f)
            {
                goto Label_0028;
            }
            return;
        Label_0028:
            num = &this.ScrollTransRect.get_anchoredPosition().y + &this.view_port_handle_rect.get_anchoredPosition().y;
            num2 = &this.parent_panel_rect.get_rect().get_height() + num;
            num3 = Mathf.Clamp(num2, 0f, this.view_port_handle.get_minHeight());
            this.view_port_handle.set_minHeight(num3);
            return;
        }

        private void UpdateIdle()
        {
            this.UpdateHandleHeight();
            this.CheckTarget();
            return;
        }

        private void UpdateOpen()
        {
            if (this.click_target[0].IsStateOpened != null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            this.ChangeState(0);
            this.click_target.Clear();
            return;
        }

        public TrophyWindow Trophy_Window
        {
            get
            {
                if ((this.trophy_window == null) == null)
                {
                    goto Label_001D;
                }
                this.trophy_window = base.GetComponentInParent<TrophyWindow>();
            Label_001D:
                return this.trophy_window;
            }
        }

        private RectTransform ScrollTransRect
        {
            get
            {
                if ((this.scroll_trans_rect == null) == null)
                {
                    goto Label_001D;
                }
                this.scroll_trans_rect = this.GetRectTransform();
            Label_001D:
                return this.scroll_trans_rect;
            }
        }

        private enum eState
        {
            IDLE,
            CLOSE_OTHER_WAIT,
            FOCUS,
            FOCUS_INTERVAL,
            CREATE_PULL_ITEM,
            OPEN,
            CLOSE
        }

        public enum TrophyTypes
        {
            Normal,
            Daily,
            All
        }
    }
}

