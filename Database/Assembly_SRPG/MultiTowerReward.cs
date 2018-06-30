namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class MultiTowerReward : MonoBehaviour
    {
        private readonly float WAIT_TIME;
        private readonly float WAIT_OPEN;
        public GameObject item;
        public GameObject root;
        public GameObject template;
        public GameObject parent;
        public GameObject arrival;
        public Text floorTxt;
        public Text floorEffTxt;
        public Text rewardTxt;
        public Text okTxt;
        public Text getTxt;
        public string openAnim;
        public string nextAnim;
        public string resultAnim;
        private int mNow;
        private int mMax;
        private float mWaitTime;
        private MODE mMode;
        private int mRound;

        public MultiTowerReward()
        {
            this.WAIT_TIME = 0.5f;
            this.mMode = 1;
            this.mRound = 1;
            base..ctor();
            return;
        }

        private void CreateResult()
        {
            GameManager manager;
            int num;
            MultiTowerFloorParam param;
            List<MultiTowerRewardItem> list;
            int num2;
            GameObject obj2;
            manager = MonoSingleton<GameManager>.Instance;
            num = GlobalVars.SelectedMultiTowerFloor;
            param = manager.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, num);
            if (param == null)
            {
                goto Label_00E6;
            }
            if ((this.template != null) == null)
            {
                goto Label_00E6;
            }
            list = manager.GetMTFloorReward(param.reward_id, this.mRound);
            num2 = 0;
            goto Label_00C1;
        Label_004B:
            obj2 = Object.Instantiate<GameObject>(this.template);
            if ((obj2 != null) == null)
            {
                goto Label_00BB;
            }
            DataSource.Bind<MultiTowerFloorParam>(obj2, param);
            obj2.SetActive(1);
            if (this.SetData(num2, 0, obj2) == null)
            {
                goto Label_00B3;
            }
            if ((this.parent != null) == null)
            {
                goto Label_00BB;
            }
            obj2.get_transform().SetParent(this.parent.get_transform(), 0);
            goto Label_00BB;
        Label_00B3:
            obj2.SetActive(0);
        Label_00BB:
            num2 += 1;
        Label_00C1:
            if (num2 < list.Count)
            {
                goto Label_004B;
            }
            this.template.SetActive(0);
            this.item.SetActive(0);
        Label_00E6:
            return;
        }

        public void OnClickNext()
        {
            int num;
            if (this.mMode != 3)
            {
                goto Label_008A;
            }
            this.mWaitTime = this.WAIT_TIME;
            if ((this.mNow += 1) >= this.mMax)
            {
                goto Label_004C;
            }
            this.mMode = 4;
            this.ReqAnim(this.nextAnim);
            goto Label_006C;
        Label_004C:
            this.CreateResult();
            this.ReqAnim(this.resultAnim);
            this.SetButtonText(0);
            this.mMode = 5;
        Label_006C:
            MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0f);
            goto Label_00C1;
        Label_008A:
            if (this.mMode != 6)
            {
                goto Label_00C1;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0f);
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "Finish");
            this.mMode = 0;
        Label_00C1:
            return;
        }

        private void Refresh()
        {
            GameManager manager;
            int num;
            MultiTowerFloorParam param;
            List<MultiTowerRewardItem> list;
            manager = MonoSingleton<GameManager>.Instance;
            num = GlobalVars.SelectedMultiTowerFloor;
            param = manager.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, num);
            if (param != null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            if (num >= 0)
            {
                goto Label_0028;
            }
            return;
        Label_0028:
            this.mNow = 0;
            if (string.IsNullOrEmpty(param.reward_id) != null)
            {
                goto Label_005E;
            }
            list = manager.GetMTFloorReward(param.reward_id, this.mRound);
            this.mMax = list.Count;
        Label_005E:
            DataSource.Bind<MultiTowerFloorParam>(this.item, param);
            if ((this.mNow + 1) >= this.mMax)
            {
                goto Label_0084;
            }
            this.SetButtonText(1);
        Label_0084:
            this.mWaitTime = this.WAIT_OPEN;
            this.mMode = 4;
            return;
        }

        private void ReqAnim(string anim)
        {
            Animator animator;
            if ((this.root != null) == null)
            {
                goto Label_0030;
            }
            animator = this.root.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0030;
            }
            animator.Play(anim);
        Label_0030:
            return;
        }

        private void SetButtonText(bool bNext)
        {
            if ((this.okTxt != null) == null)
            {
                goto Label_0036;
            }
            this.okTxt.set_text(LocalizedText.Get((bNext == null) ? "sys.CMD_OK" : "sys.BTN_NEXT"));
        Label_0036:
            if (bNext != null)
            {
                goto Label_0062;
            }
            if ((this.rewardTxt != null) == null)
            {
                goto Label_0062;
            }
            this.rewardTxt.set_text(LocalizedText.Get("sys.MULTI_TOWER_GIFT"));
        Label_0062:
            return;
        }

        private bool SetData(int idx, bool bPlay, GameObject obj)
        {
            GameManager manager;
            GameObject obj2;
            MultiTowerFloorParam param;
            List<MultiTowerRewardItem> list;
            MultiTowerRewardItem item;
            MultiTowerRewardItemUI mui;
            manager = MonoSingleton<GameManager>.Instance;
            obj2 = ((obj != null) == null) ? this.item : obj;
            param = manager.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, GlobalVars.SelectedMultiTowerFloor);
            if (param == null)
            {
                goto Label_00CF;
            }
            list = manager.GetMTFloorReward(param.reward_id, this.mRound);
            item = list[idx];
            if (list == null)
            {
                goto Label_00CF;
            }
            if (item == null)
            {
                goto Label_00CF;
            }
            if (item.type != 4)
            {
                goto Label_0085;
            }
            if (manager.Player.IsHaveAward(item.itemname) == null)
            {
                goto Label_0085;
            }
            return 0;
        Label_0085:
            if ((obj2 != null) == null)
            {
                goto Label_00CF;
            }
            obj2.SetActive(1);
            mui = obj2.GetComponent<MultiTowerRewardItemUI>();
            if ((mui != null) == null)
            {
                goto Label_00B5;
            }
            mui.SetData(idx);
        Label_00B5:
            if (bPlay == null)
            {
                goto Label_00C7;
            }
            this.ReqAnim(this.openAnim);
        Label_00C7:
            this.SetRewardName(idx, param);
        Label_00CF:
            if ((this.arrival != null) == null)
            {
                goto Label_00EC;
            }
            this.arrival.SetActive(0);
        Label_00EC:
            return 1;
        }

        private unsafe void SetRewardName(int idx, MultiTowerFloorParam param)
        {
            GameManager manager;
            List<MultiTowerRewardItem> list;
            MultiTowerRewardItem item;
            int num;
            string str;
            MultiTowerRewardItem.RewardType type;
            string str2;
            string str3;
            ItemParam param2;
            ArtifactParam param3;
            UnitParam param4;
            AwardParam param5;
            MultiTowerRewardItem.RewardType type2;
            manager = MonoSingleton<GameManager>.Instance;
            item = manager.GetMTFloorReward(param.reward_id, this.mRound)[idx];
            num = item.num;
            str = item.itemname;
            type = item.type;
            str2 = LocalizedText.Get("sys.MULTI_TOWER_REWARD_GET_MSG");
            if ((this.rewardTxt != null) == null)
            {
                goto Label_01A0;
            }
            str3 = string.Empty;
            type2 = type;
            switch ((type2 - 1))
            {
                case 0:
                    goto Label_0086;

                case 1:
                    goto Label_00BF;

                case 2:
                    goto Label_00F8;

                case 3:
                    goto Label_013B;

                case 4:
                    goto Label_011C;

                case 5:
                    goto Label_00DB;
            }
            goto Label_0166;
        Label_0086:
            param2 = manager.GetItemParam(str);
            if (param2 == null)
            {
                goto Label_0166;
            }
            str3 = param2.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (int) num);
            goto Label_0166;
        Label_00BF:
            str3 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) num);
            goto Label_0166;
        Label_00DB:
            str3 = &num.ToString() + LocalizedText.Get("sys.GOLD");
            goto Label_0166;
        Label_00F8:
            param3 = manager.MasterParam.GetArtifactParam(str);
            if (param3 == null)
            {
                goto Label_0166;
            }
            str3 = param3.name;
            goto Label_0166;
        Label_011C:
            param4 = manager.GetUnitParam(str);
            if (param4 == null)
            {
                goto Label_0166;
            }
            str3 = param4.name;
            goto Label_0166;
        Label_013B:
            param5 = manager.GetAwardParam(str);
            if (param5 == null)
            {
                goto Label_0155;
            }
            str3 = param5.name;
        Label_0155:
            str2 = LocalizedText.Get("sys.MULTI_TOWER_REWARD_GET_MSG");
        Label_0166:
            this.rewardTxt.set_text(string.Format(LocalizedText.Get("sys.MULTI_TOWER_REWARD_NAME"), str3));
            if ((this.getTxt != null) == null)
            {
                goto Label_01A0;
            }
            this.getTxt.set_text(str2);
        Label_01A0:
            return;
        }

        private void Start()
        {
            this.mRound = MonoSingleton<GameManager>.Instance.GetMTRound(GlobalVars.SelectedMultiTowerFloor);
            this.Refresh();
            return;
        }

        private void Update()
        {
            MODE mode;
            switch ((this.mMode - 1))
            {
                case 0:
                    goto Label_0028;

                case 1:
                    goto Label_0066;

                case 2:
                    goto Label_00DA;

                case 3:
                    goto Label_0094;

                case 4:
                    goto Label_0094;
            }
            goto Label_00DA;
        Label_0028:
            goto Label_003B;
        Label_002D:
            this.mNow += 1;
        Label_003B:
            if (this.SetData(this.mNow, 1, null) == null)
            {
                goto Label_002D;
            }
            this.mMode = 2;
            this.mWaitTime = this.WAIT_TIME;
            goto Label_00DA;
        Label_0066:
            this.mWaitTime -= Time.get_deltaTime();
            if (this.mWaitTime > 0f)
            {
                goto Label_00DA;
            }
            this.mMode = 3;
            goto Label_00DA;
        Label_0094:
            this.mWaitTime -= Time.get_deltaTime();
            if (this.mWaitTime > 0f)
            {
                goto Label_00DA;
            }
            if (this.mMode != 5)
            {
                goto Label_00CE;
            }
            this.mMode = 6;
            goto Label_00D5;
        Label_00CE:
            this.mMode = 1;
        Label_00D5:;
        Label_00DA:
            return;
        }

        private enum MODE
        {
            NONE,
            REQ,
            COUNTDOWN,
            WAIT,
            NEXT,
            FINISH,
            END
        }
    }
}

