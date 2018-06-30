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

    public class GachaManager : MonoSingleton<GachaManager>
    {
        public GameObject GachaPanel;
        private List<GachaTopParam> GachaList;
        public Button NextGachaButton;
        public Button PrevGachaButton;
        private List<GachaTopParam2> mGachaListRare;
        private List<GachaTopParam2> mGachaListNormal;
        private List<GachaTopParam2> mGachaListTicket;
        private List<GachaTopParam2> mGachaListArtifact;
        private List<GachaTopParam2> mGachaListSpecial;
        private int mUseTicketNum;
        private string mUseTicketIname;
        private int mCurrentGachaIndex;
        private bool mInitalize;

        public GachaManager()
        {
            base..ctor();
            return;
        }

        private GachaTopParam2 Deserialize(GachaParam param)
        {
            GachaTopParam2 param2;
            if (param != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            param2 = new GachaTopParam2();
            param2.iname = param.iname;
            param2.category = param.category;
            param2.coin = param.coin;
            param2.coin_p = param.coin_p;
            param2.gold = param.gold;
            param2.num = param.num;
            param2.ticket = param.ticket_iname;
            param2.ticket_num = param.ticket_num;
            param2.units = param.units;
            param2.step = param.step;
            param2.step_index = param.step_index;
            param2.step_num = param.step_num;
            param2.limit = param.limit;
            param2.limit_num = param.limit_num;
            param2.limit_stock = param.limit_stock;
            param2.type = string.Empty;
            param2.asset_bg = param.asset_bg;
            param2.asset_title = param.asset_title;
            param2.group = param.group;
            param2.btext = param.btext;
            param2.confirm = param.confirm;
            return param2;
        }

        public GachaTopParam GetCurrentGacha()
        {
            return this.GachaList[this.CurrentGachaIndex];
        }

        private int GetGachaParamIndex(List<GachaTopParam> list, string iname)
        {
            int num;
            GachaTopParam param;
            string str;
            string[] strArray;
            int num2;
            num = 0;
            goto Label_005B;
        Label_0007:
            param = list[num];
            if (param == null)
            {
                goto Label_0057;
            }
            if (param.iname != null)
            {
                goto Label_0025;
            }
            goto Label_0057;
        Label_0025:
            strArray = param.iname;
            num2 = 0;
            goto Label_004D;
        Label_0034:
            str = strArray[num2];
            if ((str == iname) == null)
            {
                goto Label_0047;
            }
            return num;
        Label_0047:
            num2 += 1;
        Label_004D:
            if (num2 < ((int) strArray.Length))
            {
                goto Label_0034;
            }
        Label_0057:
            num += 1;
        Label_005B:
            if (num < list.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        protected override void Initialize()
        {
            base.Initialize();
            return;
        }

        private void OnShiftGacha(Button button)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (this.mInitalize != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            if ((button == this.NextGachaButton) == null)
            {
                goto Label_0026;
            }
            num = -1;
            goto Label_0028;
        Label_0026:
            num = 1;
        Label_0028:
            num2 = this.GachaList.Count;
            num4 = ((this.mCurrentGachaIndex + num) + num2) % num2;
            base.StartCoroutine(this.ShiftGachaAsync(num4));
            return;
        }

        public void RefreshGachaList()
        {
        }

        private void SetupGachaList(GachaParam[] gparams)
        {
            int num;
            int num2;
            GachaTopParam param;
            <SetupGachaList>c__AnonStorey212 storey;
            this.GachaList = new List<GachaTopParam>();
            num = 0;
            goto Label_0272;
        Label_0012:
            storey = new <SetupGachaList>c__AnonStorey212();
            storey.group = gparams[num].group;
            num2 = 0;
            if (((this.GachaList == null) || (string.IsNullOrEmpty(storey.group) != null)) || (this.GachaList.FindIndex(new Predicate<GachaTopParam>(storey.<>m__100)) == -1))
            {
                goto Label_0095;
            }
            param = this.GachaList[this.GachaList.FindIndex(new Predicate<GachaTopParam>(storey.<>m__101))];
            num2 = Array.IndexOf<string>(param.iname, null);
            goto Label_009B;
        Label_0095:
            param = new GachaTopParam();
        Label_009B:
            param.iname[num2] = gparams[num].iname;
            param.category[num2] = gparams[num].category;
            param.coin[num2] = gparams[num].coin;
            param.gold[num2] = gparams[num].gold;
            param.coin_p[num2] = gparams[num].coin_p;
            param.num[num2] = gparams[num].num;
            param.ticket[num2] = (string.IsNullOrEmpty(gparams[num].ticket_iname) != null) ? string.Empty : gparams[num].ticket_iname;
            param.ticket_num[num2] = gparams[num].ticket_num;
            param.units = gparams[num].units;
            param.step[num2] = gparams[num].step;
            param.step_index[num2] = gparams[num].step_index;
            param.step_num[num2] = gparams[num].step_num;
            param.limit[num2] = gparams[num].limit;
            param.limit_num[num2] = gparams[num].limit_num;
            param.limit_stock[num2] = gparams[num].limit_stock;
            param.type = string.Empty;
            param.asset_bg = (string.IsNullOrEmpty(gparams[num].asset_bg) != null) ? string.Empty : gparams[num].asset_bg;
            param.asset_title = (string.IsNullOrEmpty(gparams[num].asset_title) != null) ? string.Empty : gparams[num].asset_title;
            param.group = storey.group;
            param.btext[num2] = gparams[num].btext;
            param.confirm[num2] = gparams[num].confirm;
            if (param.coin_p[num2] <= 0)
            {
                goto Label_0250;
            }
            param.sort.Insert(0, num2);
            goto Label_025C;
        Label_0250:
            param.sort.Add(num2);
        Label_025C:
            if (num2 != null)
            {
                goto Label_026E;
            }
            this.GachaList.Add(param);
        Label_026E:
            num += 1;
        Label_0272:
            if (num < ((int) gparams.Length))
            {
                goto Label_0012;
            }
            return;
        }

        private void SetupGachaList2(GachaParam[] gparams)
        {
            int num;
            GachaTopParam2 param;
            this.mGachaListRare = new List<GachaTopParam2>();
            this.mGachaListNormal = new List<GachaTopParam2>();
            this.mGachaListArtifact = new List<GachaTopParam2>();
            this.mGachaListTicket = new List<GachaTopParam2>();
            this.mGachaListSpecial = new List<GachaTopParam2>();
            num = 0;
            goto Label_00F9;
        Label_003E:
            param = new GachaTopParam2();
            param = this.Deserialize(gparams[num]);
            if (gparams[num].category.Contains("coin") == null)
            {
                goto Label_0076;
            }
            this.mGachaListRare.Add(param);
            goto Label_00F5;
        Label_0076:
            if (gparams[num].category.Contains("gold") == null)
            {
                goto Label_009E;
            }
            this.mGachaListNormal.Add(param);
            goto Label_00F5;
        Label_009E:
            if (gparams[num].group.Contains("bugu-") == null)
            {
                goto Label_00C6;
            }
            this.mGachaListArtifact.Add(param);
            goto Label_00F5;
        Label_00C6:
            if (string.IsNullOrEmpty(gparams[num].ticket_iname) != null)
            {
                goto Label_00E9;
            }
            this.mGachaListTicket.Add(param);
            goto Label_00F5;
        Label_00E9:
            this.mGachaListSpecial.Add(param);
        Label_00F5:
            num += 1;
        Label_00F9:
            if (num < ((int) gparams.Length))
            {
                goto Label_003E;
            }
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShiftGachaAsync(int index)
        {
            <ShiftGachaAsync>c__Iterator75 iterator;
            iterator = new <ShiftGachaAsync>c__Iterator75();
            iterator.index = index;
            iterator.<$>index = index;
            iterator.<>f__this = this;
            return iterator;
        }

        private void Start()
        {
            GameManager manager;
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Gachas == null)
            {
                goto Label_001F;
            }
            if (((int) manager.Gachas.Length) > 0)
            {
                goto Label_0020;
            }
        Label_001F:
            return;
        Label_0020:
            this.SetupGachaList2(manager.Gachas);
            this.mInitalize = 1;
            return;
        }

        public GachaTopParam2[] GachaListRare
        {
            get
            {
                return this.mGachaListRare.ToArray();
            }
        }

        public GachaTopParam2[] GachaListNormal
        {
            get
            {
                return this.mGachaListNormal.ToArray();
            }
        }

        public GachaTopParam2[] GachaListTicket
        {
            get
            {
                return this.mGachaListTicket.ToArray();
            }
        }

        public GachaTopParam2[] GachaListArtifact
        {
            get
            {
                return this.mGachaListArtifact.ToArray();
            }
        }

        public GachaTopParam2[] GachaListSpecial
        {
            get
            {
                return this.mGachaListSpecial.ToArray();
            }
        }

        public int UseTicketNum
        {
            get
            {
                return this.mUseTicketNum;
            }
            set
            {
                this.mUseTicketNum = value;
                return;
            }
        }

        public string UseTicketIname
        {
            get
            {
                return this.mUseTicketIname;
            }
            set
            {
                this.mUseTicketIname = value;
                return;
            }
        }

        public int CurrentGachaIndex
        {
            get
            {
                return this.mCurrentGachaIndex;
            }
            set
            {
                this.mCurrentGachaIndex = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <SetupGachaList>c__AnonStorey212
        {
            internal string group;

            public <SetupGachaList>c__AnonStorey212()
            {
                base..ctor();
                return;
            }

            internal bool <>m__100(GachaTopParam s)
            {
                return (s.group == this.group);
            }

            internal bool <>m__101(GachaTopParam s)
            {
                return (s.group == this.group);
            }
        }

        [CompilerGenerated]
        private sealed class <ShiftGachaAsync>c__Iterator75 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GachaWindow <gWindow>__0;
            internal int index;
            internal int $PC;
            internal object $current;
            internal int <$>index;
            internal GachaManager <>f__this;

            public <ShiftGachaAsync>c__Iterator75()
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
                        goto Label_005F;

                    case 2:
                        goto Label_00AF;
                }
                goto Label_00B6;
            Label_0025:
                this.<gWindow>__0 = this.<>f__this.GachaPanel.GetComponent<GachaWindow>();
                if ((this.<gWindow>__0 == null) == null)
                {
                    goto Label_005F;
                }
                this.$current = null;
                this.$PC = 1;
                goto Label_00B8;
            Label_005F:
                this.<>f__this.mCurrentGachaIndex = this.index;
                if (this.<>f__this.mCurrentGachaIndex < this.<>f__this.GachaList.Count)
                {
                    goto Label_009C;
                }
                this.<>f__this.mCurrentGachaIndex = 0;
            Label_009C:
                this.$current = null;
                this.$PC = 2;
                goto Label_00B8;
            Label_00AF:
                this.$PC = -1;
            Label_00B6:
                return 0;
            Label_00B8:
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

