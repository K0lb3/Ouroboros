namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(10, "演出開始", 0, 10), Pin(0x21, "降格演出", 1, 0x21), Pin(0x20, "昇格演出", 1, 0x20), Pin(0x1f, "終了", 1, 0x1f), Pin(30, "終了チェック", 0, 30), Pin(20, "演出終了", 1, 20), Pin(11, "演出スキップ", 0, 11)]
    public class RankMatchResult : MonoBehaviour, IFlowInterface
    {
        private const int MAX_FRAME = 60;
        [SerializeField]
        private GameObject ResultWin;
        [SerializeField]
        private GameObject ResultLose;
        [SerializeField]
        private GameObject ResultDraw;
        [SerializeField]
        private Text RankLabel;
        [SerializeField]
        private Text UpRankLabel;
        [SerializeField]
        private Text ScoreLabel;
        [SerializeField]
        private Text UpScoreLabel;
        [SerializeField]
        private Text NextLabel;
        [SerializeField]
        private Slider NextSlider;
        [SerializeField]
        private GameObject Result;
        [SerializeField]
        private GameObject UpEffect;
        [SerializeField]
        private GameObject DownEffect;
        [SerializeField]
        private GameObject UpImage;
        [SerializeField]
        private GameObject DownImage;
        private int mCounter;

        public RankMatchResult()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 10)
            {
                goto Label_001F;
            }
            if (num == 11)
            {
                goto Label_0038;
            }
            if (num == 30)
            {
                goto Label_0045;
            }
            goto Label_0050;
        Label_001F:
            this.mCounter = 0;
            base.StartCoroutine(this.PlayAnimationAsync());
            goto Label_0050;
        Label_0038:
            this.mCounter = 60;
            goto Label_0050;
        Label_0045:
            this.CheckFinish();
        Label_0050:
            return;
        }

        private void CheckFinish()
        {
            GameManager manager;
            PlayerData data;
            GameObject obj2;
            GameObject obj3;
            string str;
            UnitData data2;
            Animator animator;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data.RankMatchClass != data.RankMatchOldClass)
            {
                goto Label_0027;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x1f);
            return;
        Label_0027:
            if (data.RankMatchClass <= data.RankMatchOldClass)
            {
                goto Label_005A;
            }
            obj2 = this.UpEffect;
            obj3 = this.UpImage;
            str = "rankup";
            FlowNode_GameObject.ActivateOutputLinks(this, 0x20);
            goto Label_0077;
        Label_005A:
            obj2 = this.DownEffect;
            obj3 = this.DownImage;
            str = "rankdown";
            FlowNode_GameObject.ActivateOutputLinks(this, 0x21);
        Label_0077:
            if ((obj2 == null) == null)
            {
                goto Label_0084;
            }
            return;
        Label_0084:
            obj2.SetActive(1);
            data2 = this.GetUnitData();
            if ((obj3 != null) == null)
            {
                goto Label_00B4;
            }
            if (data2 == null)
            {
                goto Label_00B4;
            }
            DataSource.Bind<UnitData>(obj3, data2);
            GameParameter.UpdateAll(obj3);
        Label_00B4:
            animator = obj2.GetComponent<Animator>();
            if ((animator == null) == null)
            {
                goto Label_00CA;
            }
            return;
        Label_00CA:
            animator.SetBool(str, 1);
            return;
        }

        private UnitData GetUnitData()
        {
            JSON_MyPhotonPlayerParam param;
            string str;
            FlowNode_StartMultiPlay.PlayerList list;
            JSON_MyPhotonPlayerParam[] paramArray;
            UnitData data;
            <GetUnitData>c__AnonStorey397 storey;
            storey = new <GetUnitData>c__AnonStorey397();
            storey.pt = PunMonoSingleton<MyPhoton>.Instance;
            param = null;
            if ((storey.pt != null) == null)
            {
                goto Label_008E;
            }
            str = storey.pt.GetRoomParam("started");
            if (str == null)
            {
                goto Label_008E;
            }
            paramArray = JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(str).players;
            if (((int) paramArray.Length) <= 0)
            {
                goto Label_008E;
            }
            param = Array.Find<JSON_MyPhotonPlayerParam>(paramArray, new Predicate<JSON_MyPhotonPlayerParam>(storey.<>m__3E9));
            if (param == null)
            {
                goto Label_008E;
            }
            data = new UnitData();
            data.Deserialize(param.units[0].unitJson);
            return data;
        Label_008E:
            return null;
        }

        [DebuggerHidden]
        private IEnumerator PlayAnimationAsync()
        {
            int num;
            <PlayAnimationAsync>c__Iterator13A iteratora;
            iteratora = new <PlayAnimationAsync>c__Iterator13A();
            iteratora.<>f__this = this;
            return iteratora;
        }

        private unsafe void Refresh(int score, int rank, int up_rank, int up_score, ref int class_num)
        {
            GameManager manager;
            int num;
            int num2;
            VersusRankClassParam param;
            int num3;
            int num4;
            manager = MonoSingleton<GameManager>.Instance;
            num = 0;
            num2 = 1;
            param = manager.GetVersusRankClass(manager.RankMatchScheduleId, *((int*) class_num));
            if (param == null)
            {
                goto Label_008E;
            }
            num = param.UpPoint - score;
            if (num >= 0)
            {
                goto Label_0032;
            }
            num = 0;
        Label_0032:
            if ((param.UpPoint <= 0) || (param.UpPoint > score))
            {
                goto Label_0057;
            }
            *((int*) class_num) += 1;
            goto Label_0077;
        Label_0057:
            if ((param.DownPoint <= 0) || (param.DownPoint <= score))
            {
                goto Label_0077;
            }
            *((int*) class_num) -= 1;
        Label_0077:
            num2 = param.UpPoint - param.DownPoint;
            if (num2 > 0)
            {
                goto Label_008E;
            }
            num2 = 1;
        Label_008E:
            this.RankLabel.set_text(&rank.ToString());
            num3 = up_rank;
            this.UpRankLabel.set_text(((num3 <= 0) ? string.Empty : "+") + &num3.ToString());
            this.ScoreLabel.set_text(&score.ToString());
            num4 = up_score;
            this.UpScoreLabel.set_text(((num4 <= 0) ? string.Empty : "+") + &num4.ToString());
            this.NextLabel.set_text(&num.ToString());
            this.NextSlider.set_value(1f - (((float) num) / ((float) num2)));
            return;
        }

        private unsafe void Start()
        {
            GameManager manager;
            PlayerData data;
            float num;
            float num2;
            float num3;
            float num4;
            int num5;
            data = MonoSingleton<GameManager>.Instance.Player;
            num = (float) data.RankMatchOldRank;
            if (data.RankMatchOldRank != null)
            {
                goto Label_0028;
            }
            num = (float) data.RankMatchRank;
        Label_0028:
            num2 = ((float) data.RankMatchRank) - num;
            num3 = (float) data.RankMatchOldScore;
            num4 = ((float) data.RankMatchScore) - num3;
            num5 = data.RankMatchOldClass;
            this.Refresh((int) num3, (int) num, (int) num2, (int) num4, &num5);
            return;
        }

        [CompilerGenerated]
        private sealed class <GetUnitData>c__AnonStorey397
        {
            internal MyPhoton pt;

            public <GetUnitData>c__AnonStorey397()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3E9(JSON_MyPhotonPlayerParam p)
            {
                return (p.playerID == this.pt.GetMyPlayer().playerID);
            }
        }

        [CompilerGenerated]
        private sealed class <PlayAnimationAsync>c__Iterator13A : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameManager <gm>__0;
            internal PlayerData <player>__1;
            internal float <rank>__2;
            internal float <up_rank>__3;
            internal float <rank_step>__4;
            internal float <score>__5;
            internal float <up_score>__6;
            internal float <score_step>__7;
            internal int <class_num>__8;
            internal int $PC;
            internal object $current;
            internal RankMatchResult <>f__this;

            public <PlayAnimationAsync>c__Iterator13A()
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

            public unsafe bool MoveNext()
            {
                uint num;
                BtlResultTypes types;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_0254;

                    case 2:
                        goto Label_027F;

                    case 3:
                        goto Label_0341;
                }
                goto Label_03A1;
            Label_0029:
                this.<gm>__0 = MonoSingleton<GameManager>.Instance;
                this.<player>__1 = this.<gm>__0.Player;
                types = this.<player>__1.RankMatchResult;
                if (types == null)
                {
                    goto Label_0063;
                }
                if (types == 4)
                {
                    goto Label_008F;
                }
                goto Label_00BB;
            Label_0063:
                if ((this.<>f__this.ResultWin != null) == null)
                {
                    goto Label_00E7;
                }
                this.<>f__this.ResultWin.SetActive(1);
                goto Label_00E7;
            Label_008F:
                if ((this.<>f__this.ResultDraw != null) == null)
                {
                    goto Label_00E7;
                }
                this.<>f__this.ResultDraw.SetActive(1);
                goto Label_00E7;
            Label_00BB:
                if ((this.<>f__this.ResultLose != null) == null)
                {
                    goto Label_00E7;
                }
                this.<>f__this.ResultLose.SetActive(1);
            Label_00E7:
                if ((this.<>f__this.RankLabel == null) != null)
                {
                    goto Label_016B;
                }
                if ((this.<>f__this.UpRankLabel == null) != null)
                {
                    goto Label_016B;
                }
                if ((this.<>f__this.ScoreLabel == null) != null)
                {
                    goto Label_016B;
                }
                if ((this.<>f__this.UpScoreLabel == null) != null)
                {
                    goto Label_016B;
                }
                if ((this.<>f__this.NextLabel == null) != null)
                {
                    goto Label_016B;
                }
                if ((this.<>f__this.NextSlider == null) == null)
                {
                    goto Label_017D;
                }
            Label_016B:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 20);
                goto Label_03A1;
            Label_017D:
                this.<rank>__2 = (float) this.<player>__1.RankMatchOldRank;
                this.<up_rank>__3 = ((float) this.<player>__1.RankMatchRank) - this.<rank>__2;
                this.<rank_step>__4 = this.<up_rank>__3 / 60f;
                this.<score>__5 = (float) this.<player>__1.RankMatchOldScore;
                this.<up_score>__6 = ((float) this.<player>__1.RankMatchScore) - this.<score>__5;
                this.<score_step>__7 = this.<up_score>__6 / 60f;
                this.<class_num>__8 = this.<player>__1.RankMatchOldClass;
                this.<>f__this.Refresh((int) this.<score>__5, (int) this.<rank>__2, (int) this.<up_rank>__3, (int) this.<up_score>__6, &this.<class_num>__8);
                this.<>f__this.mCounter = 0;
                this.$current = null;
                this.$PC = 1;
                goto Label_03A3;
            Label_0254:
                goto Label_027F;
            Label_0259:
                this.<>f__this.mCounter += 1;
                this.$current = null;
                this.$PC = 2;
                goto Label_03A3;
            Label_027F:
                if (this.<>f__this.mCounter < 30)
                {
                    goto Label_0259;
                }
                this.<>f__this.mCounter = 0;
                goto Label_0341;
            Label_02A2:
                this.<>f__this.mCounter += 1;
                this.<rank>__2 += this.<rank_step>__4;
                this.<up_rank>__3 -= this.<rank_step>__4;
                this.<score>__5 += this.<score_step>__7;
                this.<up_score>__6 -= this.<score_step>__7;
                this.<>f__this.Refresh((int) this.<score>__5, (int) this.<rank>__2, (int) this.<up_rank>__3, (int) this.<up_score>__6, &this.<class_num>__8);
                this.$current = null;
                this.$PC = 3;
                goto Label_03A3;
            Label_0341:
                if (this.<>f__this.mCounter <= 60)
                {
                    goto Label_02A2;
                }
                this.<class_num>__8 = this.<player>__1.RankMatchClass;
                this.<>f__this.Refresh(this.<player>__1.RankMatchScore, this.<player>__1.RankMatchRank, 0, 0, &this.<class_num>__8);
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 20);
                this.$PC = -1;
            Label_03A1:
                return 0;
            Label_03A3:
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
}

