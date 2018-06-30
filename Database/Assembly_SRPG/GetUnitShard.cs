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
    using UnityEngine.UI;

    public class GetUnitShard : MonoBehaviour
    {
        private const string TRIGGER_SHARDGAUGE_CLOSE = "close";
        private const string TRIGGER_JOBOPEN_START = "jobopen_start";
        private const string TRIGGER_JOBOPEN_NEXT = "job_next";
        private const string TRIGGER_JOBOPEN_END = "jobopen_end";
        private const string TRIGGER_JOBOPEN_CLOSE = "jobopen_close";
        private const string GachaShardAwakeText = "sys.GACHA_TEXT_SHARD_AWAKE";
        private const float ShardAnimSpan = 1f;
        private StateMachine<GetUnitShard> mState;
        public float WaitGaugeActionStarted;
        public float WaitGaugeActioned;
        public float WaitRebirthStarActioned;
        public float WaitRebirthTextActioned;
        public float GaugeUpAnimSpan;
        public int GaugeUpAnimSoundSpan;
        public string[] GaugeUpAnimSoundList;
        private string[] DefaultGaugeUpAnimSoundList;
        public GameObject gauge_bar;
        public GameObject rebirthstar_root;
        public GameObject rebirthstar_template;
        public RawImage unit_img;
        public RawImage unit_img_blur01;
        public RawImage unit_img_blur02;
        public RawPolyImage unit_icon;
        public Image_Transparent element_icon;
        public Text ShardName;
        public Text JobName;
        public Text JobComment;
        public Text ShardNext;
        public Text ShardCurrent;
        public SliderAnimator ShardGauge;
        public GameObject Rebirthtext_root;
        private string[] mJobNameList;
        private bool mUnlockUnit;
        private bool mNotUnlockUnit;
        private string ShardGaugeStartAnim;
        private bool isRunningAnimator;
        private int StartAwakeLv;
        private int NowAwakeLv;
        private int NextAwakeLv;
        private int AwakeLvCap;
        private int mCurrentAwakeJobIndex;
        private bool mClicked;
        private List<GameObject> mRebirthStars;
        private List<string> mJobID;
        private List<int> mJobAwakeLv;
        private float mShardStart;
        private float mShardEnd;
        private float mShardAnimTime;
        private float mShardGet;
        private string mCurrentUnitIname;
        private int mget_shard;
        private int msub_shard;
        private int muse_shard;
        private int mremain_shard;
        private int mnext_shard;
        private int mstart_gauge;

        public GetUnitShard()
        {
            string[] textArray1;
            this.WaitGaugeActionStarted = 1f;
            this.WaitGaugeActioned = 1f;
            this.WaitRebirthStarActioned = 1f;
            this.WaitRebirthTextActioned = 1f;
            this.GaugeUpAnimSoundSpan = 5;
            textArray1 = new string[] { "SE_1027", "SE_1028", "SE_1029", "SE_1030", "SE_1031" };
            this.DefaultGaugeUpAnimSoundList = textArray1;
            this.mNotUnlockUnit = 1;
            this.ShardGaugeStartAnim = "UnitShard_gauge";
            this.isRunningAnimator = 1;
            this.mRebirthStars = new List<GameObject>();
            base..ctor();
            return;
        }

        private unsafe void AnimationShard()
        {
            float num;
            float num2;
            int num3;
            int num4;
            float num5;
            float num6;
            int num7;
            num = Time.get_unscaledDeltaTime();
            this.mShardAnimTime += num;
            num2 = Mathf.Lerp(this.mShardStart, this.mShardEnd, Mathf.Clamp01(this.mShardAnimTime / 1f));
            num3 = Mathf.FloorToInt(num2);
            num4 = this.mnext_shard;
            if ((this.ShardCurrent != null) == null)
            {
                goto Label_0078;
            }
            this.ShardCurrent.set_text(&Mathf.FloorToInt((float) (this.mstart_gauge + num3)).ToString());
        Label_0078:
            if ((this.ShardGauge != null) == null)
            {
                goto Label_00C4;
            }
            num5 = ((float) this.mstart_gauge) + num2;
            num6 = num5 / ((float) num4);
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.GaugeUpAnimSoundList[0], 0f);
            this.ShardGauge.AnimateValue(num6, 0f);
        Label_00C4:
            if (this.mShardAnimTime < 1f)
            {
                goto Label_00EB;
            }
            this.mShardStart = this.mShardEnd;
            this.mShardAnimTime = 0f;
        Label_00EB:
            return;
        }

        private int GetAmountShardEx(int index, string iname)
        {
            GameManager manager;
            Dictionary<string, int> dictionary;
            int num;
            GachaDropData data;
            UnitParam param;
            int num2;
            manager = MonoSingleton<GameManager>.Instance;
            dictionary = new Dictionary<string, int>();
            num = ((int) GachaResultData.drops.Length) - 1;
            goto Label_00C9;
        Label_001B:
            data = GachaResultData.drops[num];
            if (data.unit == null)
            {
                goto Label_0033;
            }
            goto Label_00C5;
        Label_0033:
            if (data.artifact == null)
            {
                goto Label_0043;
            }
            goto Label_00C5;
        Label_0043:
            if ((iname != data.item.iname) == null)
            {
                goto Label_005E;
            }
            goto Label_00C5;
        Label_005E:
            if (manager.MasterParam.GetUnitParamForPiece(data.item.iname, 1) != null)
            {
                goto Label_0083;
            }
            goto Label_00C5;
        Label_0083:
            num2 = (dictionary.ContainsKey(iname) == null) ? manager.Player.GetItemAmount(iname) : dictionary[iname];
            if (num2 <= 0)
            {
                goto Label_00BC;
            }
            num2 -= data.num;
        Label_00BC:
            dictionary[iname] = num2;
        Label_00C5:
            num -= 1;
        Label_00C9:
            if (num > index)
            {
                goto Label_001B;
            }
            return ((dictionary.ContainsKey(iname) == null) ? 0 : dictionary[iname]);
        }

        private int GetPastShard(int index, string iname)
        {
            int num;
            int num2;
            GachaDropData data;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return -1;
        Label_000D:
            num = 0;
            num2 = 0;
            goto Label_007C;
        Label_0016:
            data = GachaResultData.drops[num2];
            if (data.unit == null)
            {
                goto Label_002E;
            }
            goto Label_0078;
        Label_002E:
            if (data.artifact == null)
            {
                goto Label_003E;
            }
            goto Label_0078;
        Label_003E:
            if (data.item.type == 1)
            {
                goto Label_0054;
            }
            goto Label_0078;
        Label_0054:
            if ((iname != data.item.iname) == null)
            {
                goto Label_006F;
            }
            goto Label_0078;
        Label_006F:
            num += data.num;
        Label_0078:
            num2 += 1;
        Label_007C:
            if (num2 < index)
            {
                goto Label_0016;
            }
            return num;
        }

        private unsafe void InitRebirthStar()
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            enumerator = this.mRebirthStars.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_001F;
            Label_0011:
                obj2 = &enumerator.Current;
                GameUtility.DestroyGameObject(obj2);
            Label_001F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_003C;
            }
            finally
            {
            Label_0030:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_003C:
            this.mRebirthStars.Clear();
            return;
        }

        private bool IsOpenJobAnimation()
        {
            return (((this.mJobAwakeLv == null) || (this.mJobAwakeLv.Count <= 0)) ? 0 : ((this.NowAwakeLv < this.mJobAwakeLv[this.mCurrentAwakeJobIndex]) == 0));
        }

        public bool IsReachingAwakelv()
        {
            return ((this.StartAwakeLv < this.AwakeLvCap) == 0);
        }

        public void OnClicked()
        {
            this.mClicked = 1;
            return;
        }

        public unsafe void Refresh(UnitParam param, string shard_name, int awake_lv, int get_shard, int current_index)
        {
            object[] objArray1;
            GameManager manager;
            UnitData data;
            string str;
            string str2;
            GameSettings settings;
            int num;
            RarityParam param2;
            int num2;
            int num3;
            int num4;
            GameObject obj2;
            int num5;
            int num6;
            Transform transform;
            int num7;
            Transform transform2;
            int num8;
            string str3;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            int num14;
            int num15;
            int num16;
            Transform transform3;
            Transform transform4;
            float num17;
            int num18;
            int num19;
            JobSetParam param3;
            JobParam param4;
            string str4;
            int num20;
            if (param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            manager = MonoSingleton<GameManager>.Instance;
            this.InitRebirthStar();
            this.rebirthstar_template.SetActive(0);
            this.mNotUnlockUnit = 1;
            this.mget_shard = get_shard;
            this.msub_shard = this.GetPastShard(current_index, param.piece);
            this.muse_shard = 0;
            this.mstart_gauge = 0;
            this.mremain_shard = this.mget_shard;
            data = manager.Player.FindUnitDataByUnitID(param.iname);
            this.mCurrentUnitIname = param.iname;
            str = AssetPath.UnitImage(param, data.CurrentJob.JobID);
            manager.ApplyTextureAsync(this.unit_img, str);
            manager.ApplyTextureAsync(this.unit_img_blur01, str);
            manager.ApplyTextureAsync(this.unit_img_blur02, str);
            str2 = AssetPath.UnitIconSmall(param, param.GetJobId(0));
            manager.ApplyTextureAsync(this.unit_icon, str2);
            settings = GameSettings.Instance;
            if (((data == null) || (0 > data.Element)) || (data.Element >= ((int) settings.Elements_IconSmall.Length)))
            {
                goto Label_0119;
            }
            this.element_icon.set_sprite(settings.Elements_IconSmall[data.Element]);
            goto Label_0125;
        Label_0119:
            this.element_icon.set_sprite(null);
        Label_0125:
            this.ShardName.set_text(shard_name);
            this.StartAwakeLv = this.NowAwakeLv = awake_lv;
            if (data == null)
            {
                goto Label_04C2;
            }
            this.mNotUnlockUnit = 0;
            num = data.GetAwakeLevelCap();
            this.AwakeLvCap = num;
            num3 = manager.MasterParam.GetRarityParam(data.Rarity).UnitAwakeLvCap / 5;
            num4 = 0;
            goto Label_01CF;
        Label_018F:
            obj2 = Object.Instantiate<GameObject>(this.rebirthstar_template);
            obj2.get_transform().SetParent(this.rebirthstar_root.get_transform(), 0);
            obj2.SetActive(1);
            this.mRebirthStars.Add(obj2);
            num4 += 1;
        Label_01CF:
            if (num4 < num3)
            {
                goto Label_018F;
            }
            num5 = this.StartAwakeLv / 5;
            num6 = 0;
            goto Label_0298;
        Label_01EA:
            if (this.mRebirthStars.Count >= num6)
            {
                goto Label_0201;
            }
            goto Label_02A1;
        Label_0201:
            transform = this.mRebirthStars[num6].get_transform().FindChild("Rebirth_star_anim");
            transform.FindChild("Rebirthstar_01").get_gameObject().SetActive(1);
            transform.FindChild("Rebirthstar_02").get_gameObject().SetActive(1);
            transform.FindChild("Rebirthstar_03").get_gameObject().SetActive(1);
            transform.FindChild("Rebirthstar_04").get_gameObject().SetActive(1);
            transform.FindChild("Rebirthstar_05").get_gameObject().SetActive(1);
            num6 += 1;
        Label_0298:
            if (num6 < num5)
            {
                goto Label_01EA;
            }
        Label_02A1:
            num7 = this.StartAwakeLv % 5;
            if (num7 <= 0)
            {
                goto Label_0311;
            }
            transform2 = this.mRebirthStars[num5].get_transform().FindChild("Rebirth_star_anim");
            num8 = 0;
            goto Label_0308;
        Label_02D9:
            str3 = "Rebirthstar_0" + ((int) (num8 + 1));
            transform2.FindChild(str3).get_gameObject().SetActive(1);
            num8 += 1;
        Label_0308:
            if (num8 < num7)
            {
                goto Label_02D9;
            }
        Label_0311:
            if (this.msub_shard <= 0)
            {
                goto Label_04C2;
            }
            num9 = this.StartAwakeLv;
            num10 = this.msub_shard;
            num11 = manager.MasterParam.GetAwakeNeedPieces(num9);
            num12 = this.AwakeLvCap / 5;
            goto Label_0409;
        Label_034B:
            num13 = num9 / 5;
            num14 = num9 % 5;
            num15 = Math.Min(5, num14) + 1;
            num16 = ((num13 + 1) < num12) ? num13 : (num12 - 1);
            transform4 = this.mRebirthStars[num16].get_transform().FindChild("Rebirth_star_anim").FindChild("Rebirthstar_0" + &num15.ToString());
            if ((transform4 != null) == null)
            {
                goto Label_03CD;
            }
            transform4.get_gameObject().SetActive(1);
        Label_03CD:
            num9 += 1;
            if (num9 < this.AwakeLvCap)
            {
                goto Label_03E5;
            }
            goto Label_0412;
        Label_03E5:
            num10 -= num11;
            num11 = manager.MasterParam.GetAwakeNeedPieces(num9);
            if (num10 >= num11)
            {
                goto Label_0409;
            }
            goto Label_0412;
        Label_0409:
            if (num10 >= num11)
            {
                goto Label_034B;
            }
        Label_0412:
            if ((this.ShardNext != null) == null)
            {
                goto Label_0435;
            }
            this.ShardNext.set_text(&num11.ToString());
        Label_0435:
            if ((this.ShardCurrent != null) == null)
            {
                goto Label_0458;
            }
            this.ShardCurrent.set_text(&num10.ToString());
        Label_0458:
            num17 = 0f;
            this.mstart_gauge = num10;
            num17 = ((float) num10) / ((float) num11);
            this.ShardGauge.AnimateValue(num17, 0f);
            this.StartAwakeLv = num9;
            this.NowAwakeLv = this.StartAwakeLv;
            this.NextAwakeLv = ((this.StartAwakeLv + 1) <= this.AwakeLvCap) ? (this.StartAwakeLv + 1) : this.AwakeLvCap;
        Label_04C2:
            num18 = (data == null) ? param.GetUnlockNeedPieces() : manager.MasterParam.GetAwakeNeedPieces(this.StartAwakeLv);
            this.mnext_shard = num18;
            this.muse_shard = (this.mget_shard >= num18) ? num18 : this.mget_shard;
            if ((this.mstart_gauge <= 0) || ((this.mstart_gauge + this.muse_shard) <= this.mnext_shard))
            {
                goto Label_0545;
            }
            this.muse_shard -= this.mstart_gauge;
        Label_0545:
            this.mremain_shard -= this.muse_shard;
            if ((data != null) || (this.muse_shard < this.mnext_shard))
            {
                goto Label_0576;
            }
            this.mUnlockUnit = 1;
        Label_0576:
            if ((this.muse_shard < this.mnext_shard) || (this.msub_shard > 0))
            {
                goto Label_05BF;
            }
            this.NextAwakeLv = ((this.NowAwakeLv + 1) <= this.AwakeLvCap) ? (this.NowAwakeLv + 1) : this.AwakeLvCap;
        Label_05BF:
            if (param.jobsets == null)
            {
                goto Label_0701;
            }
            if (((int) param.jobsets.Length) <= 0)
            {
                goto Label_0701;
            }
            this.mJobID = new List<string>();
            this.mJobAwakeLv = new List<int>();
            num19 = 0;
            goto Label_0683;
        Label_05F6:
            param3 = MonoSingleton<GameManager>.Instance.GetJobSetParam(param.jobsets[num19]);
            if (param3 == null)
            {
                goto Label_067D;
            }
            if (param3.lock_awakelv > 0)
            {
                goto Label_0624;
            }
            goto Label_067D;
        Label_0624:
            if (data == null)
            {
                goto Label_0642;
            }
            if (data.Jobs[num19].IsActivated == null)
            {
                goto Label_0642;
            }
            goto Label_067D;
        Label_0642:
            if (this.StartAwakeLv < param3.lock_awakelv)
            {
                goto Label_0659;
            }
            goto Label_067D;
        Label_0659:
            this.mJobID.Add(param3.job);
            this.mJobAwakeLv.Add(param3.lock_awakelv);
        Label_067D:
            num19 += 1;
        Label_0683:
            if (num19 < ((int) param.jobsets.Length))
            {
                goto Label_05F6;
            }
            if (this.mJobID == null)
            {
                goto Label_0701;
            }
            if (this.mJobID.Count <= 1)
            {
                goto Label_0701;
            }
            param4 = MonoSingleton<GameManager>.Instance.GetJobParam(this.mJobID[0]);
            this.JobName.set_text(param4.name);
            objArray1 = new object[] { param4.name };
            str4 = LocalizedText.Get("sys.GACHA_TEXT_SHARD_AWAKE", objArray1);
            this.JobComment.set_text(str4);
        Label_0701:
            this.isRunningAnimator = 1;
            return;
        }

        private unsafe void RefreshAddGauge()
        {
            float num;
            int num2;
            num = 0f;
            num = ((float) this.muse_shard) / ((float) this.mnext_shard);
            this.ShardGauge.AnimateValue(num, 0f);
            this.ShardCurrent.set_text(&Mathf.FloorToInt((float) this.muse_shard).ToString());
            return;
        }

        private unsafe void RefreshAddStar()
        {
            GameManager manager;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            Transform transform;
            Transform transform2;
            float num10;
            manager = MonoSingleton<GameManager>.Instance;
            num = manager.MasterParam.GetAwakeNeedPieces(this.StartAwakeLv);
            num2 = this.AwakeLvCap / 5;
            num3 = this.mget_shard;
            num4 = this.muse_shard;
            num5 = this.StartAwakeLv;
            goto Label_0100;
        Label_003D:
            num6 = num5 / 5;
            num7 = num5 % 5;
            num8 = Math.Min(5, num7) + 1;
            num9 = ((num6 + 1) < num2) ? num6 : (num2 - 1);
            transform2 = this.mRebirthStars[num9].get_transform().FindChild("Rebirth_star_anim").FindChild("Rebirthstar_0" + &num8.ToString());
            if ((transform2 != null) == null)
            {
                goto Label_00BD;
            }
            transform2.get_gameObject().SetActive(1);
        Label_00BD:
            num5 += 1;
            if (num5 < this.AwakeLvCap)
            {
                goto Label_00D7;
            }
            num3 = num;
            goto Label_010E;
        Label_00D7:
            num3 -= num4;
            num = manager.MasterParam.GetAwakeNeedPieces(num5);
            if (num3 >= num)
            {
                goto Label_00F6;
            }
            goto Label_010E;
        Label_00F6:
            num4 = num;
            this.mstart_gauge = 0;
        Label_0100:
            if ((num3 + this.mstart_gauge) >= num)
            {
                goto Label_003D;
            }
        Label_010E:
            this.NowAwakeLv = num5;
            this.NextAwakeLv = ((this.NowAwakeLv + 1) <= this.AwakeLvCap) ? (this.NowAwakeLv + 1) : this.AwakeLvCap;
            if ((this.ShardNext != null) == null)
            {
                goto Label_0165;
            }
            this.ShardNext.set_text(&num.ToString());
        Label_0165:
            if ((this.ShardCurrent != null) == null)
            {
                goto Label_0188;
            }
            this.ShardCurrent.set_text(&num3.ToString());
        Label_0188:
            num10 = 0f;
            num10 = ((float) num3) / ((float) num);
            this.ShardGauge.AnimateValue(num10, 0f);
            return;
        }

        private void RefreshJobWindow()
        {
            object[] objArray1;
            JobParam param;
            string str;
            if (this.mJobID == null)
            {
                goto Label_0021;
            }
            if (this.mJobID.Count >= this.mCurrentAwakeJobIndex)
            {
                goto Label_0022;
            }
        Label_0021:
            return;
        Label_0022:
            param = MonoSingleton<GameManager>.Instance.GetJobParam(this.mJobID[this.mCurrentAwakeJobIndex]);
            this.JobName.set_text(param.name);
            objArray1 = new object[] { param.name };
            str = LocalizedText.Get("sys.GACHA_TEXT_SHARD_AWAKE", objArray1);
            this.JobComment.set_text(str);
            return;
        }

        private unsafe void RefreshShardGaugeImmediate()
        {
            int num;
            int num2;
            float num3;
            if ((this.ShardGauge == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.msub_shard <= 0)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            if ((this.ShardNext != null) == null)
            {
                goto Label_0046;
            }
            this.ShardNext.set_text(&this.mnext_shard.ToString());
        Label_0046:
            if ((this.ShardCurrent != null) == null)
            {
                goto Label_0067;
            }
            this.ShardCurrent.set_text("0");
        Label_0067:
            num = 0;
            num2 = this.mnext_shard;
            num3 = ((float) num) / ((float) num2);
            this.ShardGauge.AnimateValue(Mathf.Clamp01(num3), 0f);
            return;
        }

        private unsafe void RefreshShardValue()
        {
            GameManager manager;
            UnitData data;
            int num;
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player.FindUnitDataByUnitID(this.mCurrentUnitIname);
            this.NowAwakeLv = this.NextAwakeLv;
            this.muse_shard = 0;
            this.ShardGauge.AnimateValue(0f, 0f);
            this.ShardCurrent.set_text(&this.muse_shard.ToString());
            this.mstart_gauge = 0;
            if (data == null)
            {
                goto Label_00F9;
            }
            num = manager.MasterParam.GetAwakeNeedPieces(this.NowAwakeLv);
            this.mnext_shard = num;
            this.muse_shard = (this.mremain_shard >= this.mnext_shard) ? this.mnext_shard : this.mremain_shard;
            this.mremain_shard -= this.muse_shard;
            this.NextAwakeLv = ((this.NextAwakeLv + 1) < this.AwakeLvCap) ? (this.NextAwakeLv + 1) : this.AwakeLvCap;
            this.ShardNext.set_text(&this.mnext_shard.ToString());
        Label_00F9:
            return;
        }

        public void Reset()
        {
            this.isRunningAnimator = 1;
            return;
        }

        public void Restart()
        {
            if (this.mState != null)
            {
                goto Label_0017;
            }
            this.mState = new StateMachine<GetUnitShard>(this);
        Label_0017:
            this.mCurrentAwakeJobIndex = 0;
            this.isRunningAnimator = 1;
            if (this.StartAwakeLv >= this.AwakeLvCap)
            {
                goto Label_0046;
            }
            this.mState.GotoState<State_Init>();
            goto Label_0051;
        Label_0046:
            this.mState.GotoState<State_WaitEndUnitShard>();
        Label_0051:
            return;
        }

        private void SetUsedShard(int end_gauge)
        {
            float num;
            this.mShardStart = this.mShardEnd = 0f;
            this.mShardAnimTime = 0f;
            this.mShardEnd = (float) end_gauge;
            return;
        }

        private void Start()
        {
            int num;
            if (((int) this.GaugeUpAnimSoundList.Length) > 0)
            {
                goto Label_004A;
            }
            this.GaugeUpAnimSoundList = new string[(int) this.DefaultGaugeUpAnimSoundList.Length];
            num = 0;
            goto Label_003C;
        Label_0028:
            this.GaugeUpAnimSoundList[num] = this.DefaultGaugeUpAnimSoundList[num];
            num += 1;
        Label_003C:
            if (num < ((int) this.DefaultGaugeUpAnimSoundList.Length))
            {
                goto Label_0028;
            }
        Label_004A:
            this.mState = new StateMachine<GetUnitShard>(this);
            this.isRunningAnimator = 1;
            this.Restart();
            return;
        }

        private void Update()
        {
            if (this.mState == null)
            {
                goto Label_0016;
            }
            this.mState.Update();
        Label_0016:
            return;
        }

        public bool UnlockUnit
        {
            get
            {
                return this.mUnlockUnit;
            }
        }

        public bool IsRunningAnimator
        {
            get
            {
                return this.isRunningAnimator;
            }
        }

        private class State_AddingGauge : State<GetUnitShard>
        {
            public State_AddingGauge()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                self.RefreshShardGaugeImmediate();
                self.SetUsedShard(self.muse_shard);
                return;
            }

            public override void Update(GetUnitShard self)
            {
                if (self.mClicked == null)
                {
                    goto Label_0017;
                }
                self.mState.GotoState<GetUnitShard.State_GaugeSkip>();
                return;
            Label_0017:
                if (self.mShardStart >= self.mShardEnd)
                {
                    goto Label_0033;
                }
                self.AnimationShard();
                goto Label_003E;
            Label_0033:
                self.mState.GotoState<GetUnitShard.State_WaitGaugeActioned>();
            Label_003E:
                return;
            }
        }

        private class State_AddingGaugeSkip : State<GetUnitShard>
        {
            public State_AddingGaugeSkip()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                Animator animator;
                animator = self.GetComponent<Animator>();
                if ((animator != null) == null)
                {
                    goto Label_001F;
                }
                animator.SetBool("is_skip", 1);
            Label_001F:
                self.StartCoroutine(this.WaitGaugeObject());
                return;
            }

            [DebuggerHidden]
            private IEnumerator WaitGaugeObject()
            {
                <WaitGaugeObject>c__Iterator11B iteratorb;
                iteratorb = new <WaitGaugeObject>c__Iterator11B();
                iteratorb.<>f__this = this;
                return iteratorb;
            }

            [CompilerGenerated]
            private sealed class <WaitGaugeObject>c__Iterator11B : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal int $PC;
                internal object $current;
                internal GetUnitShard.State_AddingGaugeSkip <>f__this;

                public <WaitGaugeObject>c__Iterator11B()
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
                            goto Label_0021;

                        case 1:
                            goto Label_0038;
                    }
                    goto Label_0054;
                Label_0021:
                    this.$current = new WaitForEndOfFrame();
                    this.$PC = 1;
                    goto Label_0056;
                Label_0038:
                    this.<>f__this.self.mState.GotoState<GetUnitShard.State_AddingGauge>();
                    this.$PC = -1;
                Label_0054:
                    return 0;
                Label_0056:
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
        }

        private class State_AddingRebirthStar : State<GetUnitShard>
        {
            private Transform rebirth_star;
            private int new_star;

            public State_AddingRebirthStar()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                int num;
                int num2;
                int num3;
                int num4;
                int num5;
                Animator animator;
                self.mClicked = 0;
                if ((self.muse_shard + self.mstart_gauge) == self.mnext_shard)
                {
                    goto Label_002B;
                }
                self.mState.GotoState<GetUnitShard.State_CheckRemainPiece>();
                return;
            Label_002B:
                num = self.AwakeLvCap / 5;
                num2 = self.NowAwakeLv / 5;
                num3 = ((num2 + 1) < num) ? num2 : (num - 1);
                num4 = self.NowAwakeLv % 5;
                num5 = Math.Min(5, num4) + 1;
                this.new_star = num5;
                this.rebirth_star = self.mRebirthStars[num3].get_transform().FindChild("Rebirth_star_anim");
                this.rebirth_star.GetComponent<Animator>().SetInteger("new", num5);
                return;
            }

            public override unsafe void Update(GetUnitShard self)
            {
                Animator animator;
                Transform transform;
                Animator animator2;
                if (self.mClicked == null)
                {
                    goto Label_003A;
                }
                animator = this.rebirth_star.GetComponent<Animator>();
                animator.SetInteger("new", 0);
                animator.SetTrigger("close");
                self.mState.GotoState<GetUnitShard.State_AddingGaugeSkip>();
                return;
            Label_003A:
                if (GameUtility.IsAnimatorRunning(this.rebirth_star) != null)
                {
                    goto Label_00B1;
                }
                transform = this.rebirth_star.FindChild("Rebirthstar_0" + &this.new_star.ToString());
                if ((transform != null) == null)
                {
                    goto Label_0083;
                }
                transform.get_gameObject().SetActive(1);
            Label_0083:
                animator2 = this.rebirth_star.GetComponent<Animator>();
                animator2.SetInteger("new", 0);
                animator2.SetTrigger("close");
                self.mState.GotoState<GetUnitShard.State_WaitAddingRebirthStarActioned>();
            Label_00B1:
                return;
            }
        }

        private class State_CheckJobOpen : State<GetUnitShard>
        {
            private Animator at;

            public State_CheckJobOpen()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                this.at = self.GetComponent<Animator>();
                return;
            }

            public override void Update(GetUnitShard self)
            {
                if (GameUtility.IsAnimatorRunning(self) != null)
                {
                    goto Label_009D;
                }
                if (GameUtility.CompareAnimatorStateName(self, "UnitShard_gauge_jobopen_end") == null)
                {
                    goto Label_009D;
                }
                self.mCurrentAwakeJobIndex += 1;
                if (self.mJobAwakeLv == null)
                {
                    goto Label_0082;
                }
                if (self.mJobAwakeLv.Count <= self.mCurrentAwakeJobIndex)
                {
                    goto Label_0082;
                }
                if (self.NowAwakeLv < self.mJobAwakeLv[self.mCurrentAwakeJobIndex])
                {
                    goto Label_0082;
                }
                this.at.SetTrigger("job_next");
                self.mState.GotoState<GetUnitShard.State_StartJobOpenAnimation>();
                return;
            Label_0082:
                this.at.SetTrigger("jobopen_close");
                self.mState.GotoState<GetUnitShard.State_CloseJobOpenAnimation>();
            Label_009D:
                return;
            }
        }

        private class State_CheckRemainPiece : State<GetUnitShard>
        {
            public State_CheckRemainPiece()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                if (self.mremain_shard <= 0)
                {
                    goto Label_0035;
                }
                if ((self.NowAwakeLv + 1) >= self.AwakeLvCap)
                {
                    goto Label_0035;
                }
                self.RefreshShardValue();
                self.mState.GotoState<GetUnitShard.State_AddingGauge>();
                goto Label_0061;
            Label_0035:
                if (self.IsOpenJobAnimation() == null)
                {
                    goto Label_0056;
                }
                self.RefreshJobWindow();
                self.mState.GotoState<GetUnitShard.State_StartJobOpenAnimation>();
                goto Label_0061;
            Label_0056:
                self.mState.GotoState<GetUnitShard.State_EndShardGaugeBarAnimation>();
            Label_0061:
                return;
            }
        }

        private class State_CloseJobOpenAnimation : State<GetUnitShard>
        {
            public State_CloseJobOpenAnimation()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
            }

            public override void Update(GetUnitShard self)
            {
                if (self.isRunningAnimator != null)
                {
                    goto Label_000C;
                }
                return;
            Label_000C:
                if (GameUtility.CompareAnimatorStateName(self, "closed") == null)
                {
                    goto Label_002A;
                }
                self.isRunningAnimator = 0;
                self.mClicked = 0;
            Label_002A:
                return;
            }
        }

        private class State_EndShardGaugeBarAnimation : State<GetUnitShard>
        {
            public State_EndShardGaugeBarAnimation()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                Animator animator;
                animator = self.GetComponent<Animator>();
                animator.SetBool("is_skip", 0);
                animator.SetTrigger("close");
                self.mClicked = 0;
                return;
            }

            public override void Update(GetUnitShard self)
            {
                if (self.mClicked == null)
                {
                    goto Label_0017;
                }
                self.mState.GotoState<GetUnitShard.State_EndUnitShard>();
                return;
            Label_0017:
                if (self.isRunningAnimator != null)
                {
                    goto Label_0023;
                }
                return;
            Label_0023:
                if (GameUtility.CompareAnimatorStateName(self, "closed") == null)
                {
                    goto Label_0050;
                }
                GameUtility.DestroyGameObjects(self.mRebirthStars);
                self.mRebirthStars.Clear();
                self.isRunningAnimator = 0;
            Label_0050:
                return;
            }
        }

        private class State_EndUnitShard : State<GetUnitShard>
        {
            public State_EndUnitShard()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                GameUtility.DestroyGameObjects(self.mRebirthStars);
                self.mRebirthStars.Clear();
                self.mClicked = 0;
                self.isRunningAnimator = 0;
                return;
            }
        }

        private class State_GaugeSkip : State<GetUnitShard>
        {
            public State_GaugeSkip()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                self.mClicked = 0;
                if (self.mNotUnlockUnit != null)
                {
                    goto Label_0049;
                }
                self.RefreshAddStar();
                if (self.IsOpenJobAnimation() == null)
                {
                    goto Label_0039;
                }
                self.RefreshJobWindow();
                self.mState.GotoState<GetUnitShard.State_StartJobOpenAnimation>();
                goto Label_0044;
            Label_0039:
                self.mState.GotoState<GetUnitShard.State_EndShardGaugeBarAnimation>();
            Label_0044:
                goto Label_005A;
            Label_0049:
                self.RefreshAddGauge();
                self.mState.GotoState<GetUnitShard.State_EndShardGaugeBarAnimation>();
            Label_005A:
                return;
            }
        }

        private class State_Init : State<GetUnitShard>
        {
            public State_Init()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                self.mState.GotoState<GetUnitShard.State_WaitStartAnimation>();
                return;
            }
        }

        private class State_ShowRebirthText : State<GetUnitShard>
        {
            private GameObject anim;

            public State_ShowRebirthText()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                self.mClicked = 0;
                if (self.NextAwakeLv != self.NowAwakeLv)
                {
                    goto Label_0024;
                }
                self.mState.GotoState<GetUnitShard.State_CheckRemainPiece>();
                return;
            Label_0024:
                self.Rebirthtext_root.SetActive(1);
                this.anim = self.Rebirthtext_root.get_transform().FindChild("Rebirth_txtanim").get_gameObject();
                return;
            }

            public override void Update(GetUnitShard self)
            {
                if (self.mClicked == null)
                {
                    goto Label_0023;
                }
                self.Rebirthtext_root.SetActive(0);
                self.mState.GotoState<GetUnitShard.State_AddingGaugeSkip>();
                return;
            Label_0023:
                if (GameUtility.IsAnimatorRunning(this.anim) != null)
                {
                    goto Label_004A;
                }
                self.Rebirthtext_root.SetActive(0);
                self.mState.GotoState<GetUnitShard.State_WaitRebirthTextActioned>();
            Label_004A:
                return;
            }
        }

        private class State_StartJobOpenAnimation : State<GetUnitShard>
        {
            public State_StartJobOpenAnimation()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                Animator animator;
                animator = self.GetComponent<Animator>();
                animator.SetBool("is_skip", 0);
                if (GameUtility.CompareAnimatorStateName(self, "UnitShard_gauge_jobopen_loop") != null)
                {
                    goto Label_002E;
                }
                animator.SetTrigger("jobopen_start");
            Label_002E:
                return;
            }

            public override void Update(GetUnitShard self)
            {
                Animator animator;
                if (GameUtility.CompareAnimatorStateName(self, "UnitShard_gauge_jobopen_start") != null)
                {
                    goto Label_0020;
                }
                if (GameUtility.CompareAnimatorStateName(self, "UnitShard_gauge_jobopen_loop") == null)
                {
                    goto Label_00B1;
                }
            Label_0020:
                if (self.mClicked == null)
                {
                    goto Label_00B1;
                }
                self.mClicked = 0;
                animator = self.GetComponent<Animator>();
                self.mCurrentAwakeJobIndex += 1;
                if (self.mJobAwakeLv == null)
                {
                    goto Label_009B;
                }
                if (self.mJobAwakeLv.Count <= self.mCurrentAwakeJobIndex)
                {
                    goto Label_009B;
                }
                if (self.NowAwakeLv < self.mJobAwakeLv[self.mCurrentAwakeJobIndex])
                {
                    goto Label_009B;
                }
                animator.SetTrigger("job_next");
                self.mState.GotoState<GetUnitShard.State_StartJobOpenAnimation>();
                return;
            Label_009B:
                animator.SetTrigger("jobopen_end");
                self.mState.GotoState<GetUnitShard.State_CheckJobOpen>();
            Label_00B1:
                return;
            }
        }

        private class State_WaitAddingRebirthStarActioned : State<GetUnitShard>
        {
            private float mWaitTime;
            private float mTimer;

            public State_WaitAddingRebirthStarActioned()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                float num;
                self.mClicked = 0;
                this.mWaitTime = this.mTimer = 0f;
                this.mWaitTime = self.WaitRebirthStarActioned;
                return;
            }

            public override void Update(GetUnitShard self)
            {
                if (this.mWaitTime >= this.mTimer)
                {
                    goto Label_002C;
                }
                this.mTimer = 0f;
                self.mState.GotoState<GetUnitShard.State_ShowRebirthText>();
                goto Label_003E;
            Label_002C:
                this.mTimer += Time.get_deltaTime();
            Label_003E:
                return;
            }
        }

        private class State_WaitEndUnitShard : State<GetUnitShard>
        {
            private float mWaitTime;
            private float mTimer;

            public State_WaitEndUnitShard()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                float num;
                self.mClicked = 0;
                this.mWaitTime = this.mTimer = 0f;
                this.mWaitTime = 2f;
                return;
            }

            public override void Update(GetUnitShard self)
            {
                if (self.mClicked == null)
                {
                    goto Label_0017;
                }
                self.mState.GotoState<GetUnitShard.State_EndUnitShard>();
                return;
            Label_0017:
                if (this.mWaitTime >= this.mTimer)
                {
                    goto Label_0043;
                }
                this.mTimer = 0f;
                self.mState.GotoState<GetUnitShard.State_EndUnitShard>();
                goto Label_0055;
            Label_0043:
                this.mTimer += Time.get_deltaTime();
            Label_0055:
                return;
            }
        }

        private class State_WaitGaugeActioned : State<GetUnitShard>
        {
            private float mWaitTime;
            private float mTimer;

            public State_WaitGaugeActioned()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                float num;
                self.mClicked = 0;
                this.mWaitTime = this.mTimer = 0f;
                this.mWaitTime = self.WaitGaugeActioned;
                return;
            }

            public override void Update(GetUnitShard self)
            {
                if (self.mClicked == null)
                {
                    goto Label_0017;
                }
                self.mState.GotoState<GetUnitShard.State_AddingGaugeSkip>();
                return;
            Label_0017:
                if (this.mWaitTime >= this.mTimer)
                {
                    goto Label_005E;
                }
                this.mTimer = 0f;
                if (self.mNotUnlockUnit != null)
                {
                    goto Label_004E;
                }
                self.mState.GotoState<GetUnitShard.State_AddingRebirthStar>();
                goto Label_0059;
            Label_004E:
                self.mState.GotoState<GetUnitShard.State_CheckRemainPiece>();
            Label_0059:
                goto Label_0070;
            Label_005E:
                this.mTimer += Time.get_deltaTime();
            Label_0070:
                return;
            }
        }

        private class State_WaitGaugeActionStarted : State<GetUnitShard>
        {
            private float mWaitTime;
            private float mTimer;

            public State_WaitGaugeActionStarted()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                float num;
                this.mWaitTime = this.mTimer = 0f;
                this.mWaitTime = self.WaitGaugeActionStarted;
                return;
            }

            public override void Update(GetUnitShard self)
            {
                if (self.mClicked == null)
                {
                    goto Label_0016;
                }
                self.mState.GotoState<GetUnitShard.State_AddingGaugeSkip>();
            Label_0016:
                if (this.mWaitTime >= this.mTimer)
                {
                    goto Label_0042;
                }
                this.mTimer = 0f;
                self.mState.GotoState<GetUnitShard.State_AddingGauge>();
                goto Label_0054;
            Label_0042:
                this.mTimer += Time.get_deltaTime();
            Label_0054:
                return;
            }
        }

        private class State_WaitRebirthTextActioned : State<GetUnitShard>
        {
            private float mWaitTime;
            private float mTimer;

            public State_WaitRebirthTextActioned()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
                float num;
                self.mClicked = 0;
                this.mWaitTime = this.mTimer = 0f;
                this.mWaitTime = self.WaitRebirthTextActioned;
                return;
            }

            public override void Update(GetUnitShard self)
            {
                if (self.mClicked == null)
                {
                    goto Label_0017;
                }
                self.mState.GotoState<GetUnitShard.State_AddingGaugeSkip>();
                return;
            Label_0017:
                if (this.mWaitTime >= this.mTimer)
                {
                    goto Label_0043;
                }
                this.mTimer = 0f;
                self.mState.GotoState<GetUnitShard.State_CheckRemainPiece>();
                goto Label_0055;
            Label_0043:
                this.mTimer += Time.get_deltaTime();
            Label_0055:
                return;
            }
        }

        private class State_WaitStartAnimation : State<GetUnitShard>
        {
            public State_WaitStartAnimation()
            {
                base..ctor();
                return;
            }

            public override void Begin(GetUnitShard self)
            {
            }

            public override void Update(GetUnitShard self)
            {
                if (self.mClicked != null)
                {
                    goto Label_0033;
                }
                if (GameUtility.IsAnimatorRunning(self) == null)
                {
                    goto Label_0032;
                }
                if (GameUtility.CompareAnimatorStateName(self, self.ShardGaugeStartAnim) == null)
                {
                    goto Label_0032;
                }
                self.mState.GotoState<GetUnitShard.State_WaitGaugeActionStarted>();
            Label_0032:
                return;
            Label_0033:
                self.mState.GotoState<GetUnitShard.State_AddingGaugeSkip>();
                return;
            }
        }
    }
}

