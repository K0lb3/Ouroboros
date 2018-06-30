namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class VersusSeasonReward : MonoBehaviour
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

        public VersusSeasonReward()
        {
            this.WAIT_TIME = 0.5f;
            this.WAIT_OPEN = 3f;
            this.mMode = 1;
            base..ctor();
            return;
        }

        private void CreateResult()
        {
            GameManager manager;
            VersusTowerParam param;
            int num;
            GameObject obj2;
            param = MonoSingleton<GameManager>.Instance.GetCurrentVersusTowerParam(-1);
            if (param == null)
            {
                goto Label_00BE;
            }
            if ((this.template != null) == null)
            {
                goto Label_00BE;
            }
            num = 0;
            goto Label_0098;
        Label_002C:
            obj2 = Object.Instantiate<GameObject>(this.template);
            if ((obj2 != null) == null)
            {
                goto Label_0094;
            }
            DataSource.Bind<VersusTowerParam>(obj2, param);
            obj2.SetActive(1);
            if (this.SetData(num, 0, obj2) == null)
            {
                goto Label_008D;
            }
            if ((this.parent != null) == null)
            {
                goto Label_0094;
            }
            obj2.get_transform().SetParent(this.parent.get_transform(), 0);
            goto Label_0094;
        Label_008D:
            obj2.SetActive(0);
        Label_0094:
            num += 1;
        Label_0098:
            if (num < ((int) param.SeasonIteminame.Length))
            {
                goto Label_002C;
            }
            this.template.SetActive(0);
            this.item.SetActive(0);
        Label_00BE:
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

        private unsafe void Refresh()
        {
            GameManager manager;
            PlayerData data;
            VersusTowerParam[] paramArray;
            int num;
            int num2;
            int num3;
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            if ((this.floorTxt != null) == null)
            {
                goto Label_0047;
            }
            this.floorTxt.set_text(&data.VersusTowerFloor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
        Label_0047:
            if ((this.floorEffTxt != null) == null)
            {
                goto Label_0081;
            }
            this.floorEffTxt.set_text(&data.VersusTowerFloor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
        Label_0081:
            paramArray = manager.GetVersusTowerParam();
            num = data.VersusTowerFloor - 1;
            if (paramArray != null)
            {
                goto Label_0098;
            }
            return;
        Label_0098:
            if (num < 0)
            {
                goto Label_00A8;
            }
            if (num < ((int) paramArray.Length))
            {
                goto Label_00A9;
            }
        Label_00A8:
            return;
        Label_00A9:
            this.mNow = 0;
            if (paramArray[num].SeasonIteminame == null)
            {
                goto Label_00CD;
            }
            this.mMax = (int) paramArray[num].SeasonIteminame.Length;
        Label_00CD:
            DataSource.Bind<VersusTowerParam>(this.item, paramArray[num]);
            if ((this.mNow + 1) >= this.mMax)
            {
                goto Label_00F5;
            }
            this.SetButtonText(1);
        Label_00F5:
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
            this.rewardTxt.set_text(LocalizedText.Get("sys.MULTI_VERSUS_SEND_GIFT"));
        Label_0062:
            return;
        }

        private unsafe bool SetData(int idx, bool bPlay, GameObject obj)
        {
            GameManager manager;
            GameObject obj2;
            VersusTowerParam param;
            VersusTowerRewardItem item;
            manager = MonoSingleton<GameManager>.Instance;
            obj2 = ((obj != null) == null) ? this.item : obj;
            param = manager.GetCurrentVersusTowerParam(-1);
            if (param == null)
            {
                goto Label_00D6;
            }
            if (param.SeasonIteminame == null)
            {
                goto Label_00D6;
            }
            if (string.IsNullOrEmpty(*(&(param.SeasonIteminame[idx]))) != null)
            {
                goto Label_00D6;
            }
            if (param.SeasonItemType[idx] != 5)
            {
                goto Label_008E;
            }
            if (manager.Player.IsHaveAward(*(&(param.SeasonIteminame[idx]))) == null)
            {
                goto Label_008E;
            }
            return 0;
        Label_008E:
            if ((obj2 != null) == null)
            {
                goto Label_00D6;
            }
            obj2.SetActive(1);
            item = obj2.GetComponent<VersusTowerRewardItem>();
            if ((item != null) == null)
            {
                goto Label_00BC;
            }
            item.SetData(1, idx);
        Label_00BC:
            if (bPlay == null)
            {
                goto Label_00CE;
            }
            this.ReqAnim(this.openAnim);
        Label_00CE:
            this.SetRewardName(idx, param);
        Label_00D6:
            if ((this.arrival != null) == null)
            {
                goto Label_00F3;
            }
            this.arrival.SetActive(0);
        Label_00F3:
            return 1;
        }

        private unsafe void SetRewardName(int idx, VersusTowerParam param)
        {
            GameManager manager;
            int num;
            string str;
            VERSUS_ITEM_TYPE versus_item_type;
            string str2;
            string str3;
            ItemParam param2;
            ArtifactParam param3;
            UnitParam param4;
            AwardParam param5;
            VERSUS_ITEM_TYPE versus_item_type2;
            manager = MonoSingleton<GameManager>.Instance;
            num = *(&(param.SeasonItemnum[idx]));
            str = *(&(param.SeasonIteminame[idx]));
            versus_item_type = param.SeasonItemType[idx];
            str2 = LocalizedText.Get("sys.MULTI_VERSUS_REWARD_GET_MSG");
            if ((this.rewardTxt != null) == null)
            {
                goto Label_019E;
            }
            str3 = string.Empty;
            versus_item_type2 = versus_item_type;
            switch (versus_item_type2)
            {
                case 0:
                    goto Label_0088;

                case 1:
                    goto Label_00DC;

                case 2:
                    goto Label_00C0;

                case 3:
                    goto Label_011C;

                case 4:
                    goto Label_00F9;

                case 5:
                    goto Label_013A;
            }
            goto Label_0164;
        Label_0088:
            param2 = manager.GetItemParam(str);
            if (param2 == null)
            {
                goto Label_0164;
            }
            str3 = param2.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (int) num);
            goto Label_0164;
        Label_00C0:
            str3 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) num);
            goto Label_0164;
        Label_00DC:
            str3 = &num.ToString() + LocalizedText.Get("sys.GOLD");
            goto Label_0164;
        Label_00F9:
            param3 = manager.MasterParam.GetArtifactParam(str);
            if (param3 == null)
            {
                goto Label_0164;
            }
            str3 = param3.name;
            goto Label_0164;
        Label_011C:
            param4 = manager.GetUnitParam(str);
            if (param4 == null)
            {
                goto Label_0164;
            }
            str3 = param4.name;
            goto Label_0164;
        Label_013A:
            param5 = manager.GetAwardParam(str);
            if (param5 == null)
            {
                goto Label_0153;
            }
            str3 = param5.name;
        Label_0153:
            str2 = LocalizedText.Get("sys.MULTI_VERSUS_REWARD_GET_AWARD");
        Label_0164:
            this.rewardTxt.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_REWARD_NAME"), str3));
            if ((this.getTxt != null) == null)
            {
                goto Label_019E;
            }
            this.getTxt.set_text(str2);
        Label_019E:
            return;
        }

        private void Start()
        {
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

