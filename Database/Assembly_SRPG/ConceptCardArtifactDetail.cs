namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class ConceptCardArtifactDetail : MonoBehaviour
    {
        private ArtifactData UnlockArtifact;
        [SerializeField]
        private StatusList mArtifactStatus;
        [SerializeField]
        private GameObject mAbilityListItem;
        [SerializeField]
        private Animator mAbilityAnimator;
        [SerializeField]
        private string mAbilityListItemState;
        public GameObject DetailWindow;
        private GameObject mDetailWindow;

        public ConceptCardArtifactDetail()
        {
            base..ctor();
            return;
        }

        private unsafe void Refresh()
        {
            ArtifactData data;
            ArtifactParam param;
            Json_Artifact artifact;
            MasterParam param2;
            GameObject obj2;
            bool flag;
            ArtifactParam param3;
            List<AbilityData> list;
            AbilityParam param4;
            int num;
            AbilityData data2;
            BaseStatus status;
            BaseStatus status2;
            <Refresh>c__AnonStorey321 storey;
            data = null;
            data = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_004D;
            }
            param = DataSource.FindDataOfClass<ArtifactParam>(base.get_gameObject(), null);
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iname = param.iname;
            artifact.rare = param.rareini;
            data.Deserialize(artifact);
        Label_004D:
            this.UnlockArtifact = data;
            DataSource.Bind<ArtifactData>(base.get_gameObject(), this.UnlockArtifact);
            DataSource.Bind<ArtifactParam>(base.get_gameObject(), this.UnlockArtifact.ArtifactParam);
            if ((this.mAbilityListItem != null) == null)
            {
                goto Label_01F3;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam;
            obj2 = this.mAbilityListItem;
            flag = 0;
            param3 = data.ArtifactParam;
            list = data.LearningAbilities;
            if (param3.abil_inames == null)
            {
                goto Label_01C3;
            }
            storey = new <Refresh>c__AnonStorey321();
            param4 = null;
            storey.abil_iname = null;
            num = 0;
            goto Label_0135;
        Label_00D8:
            if (string.IsNullOrEmpty(param3.abil_inames[num]) != null)
            {
                goto Label_012F;
            }
            if (param3.abil_shows[num] != null)
            {
                goto Label_0100;
            }
            goto Label_012F;
        Label_0100:
            storey.abil_iname = param3.abil_inames[num];
            param4 = param2.GetAbilityParam(param3.abil_inames[num]);
            if (param4 == null)
            {
                goto Label_012F;
            }
            goto Label_0145;
        Label_012F:
            num += 1;
        Label_0135:
            if (num < ((int) param3.abil_inames.Length))
            {
                goto Label_00D8;
            }
        Label_0145:
            if (param4 != null)
            {
                goto Label_0177;
            }
            this.mAbilityAnimator.SetInteger(this.mAbilityListItemState, 0);
            DataSource.Bind<AbilityParam>(this.mAbilityListItem, null);
            DataSource.Bind<AbilityData>(this.mAbilityListItem, null);
            return;
        Label_0177:
            DataSource.Bind<AbilityParam>(this.mAbilityListItem, param4);
            DataSource.Bind<AbilityData>(obj2, null);
            if (list == null)
            {
                goto Label_01C3;
            }
            if (list == null)
            {
                goto Label_01C3;
            }
            data2 = list.Find(new Predicate<AbilityData>(storey.<>m__2C8));
            if (data2 == null)
            {
                goto Label_01C3;
            }
            DataSource.Bind<AbilityData>(obj2, data2);
            flag = 1;
        Label_01C3:
            if (flag == null)
            {
                goto Label_01E1;
            }
            this.mAbilityAnimator.SetInteger(this.mAbilityListItemState, 2);
            goto Label_01F3;
        Label_01E1:
            this.mAbilityAnimator.SetInteger(this.mAbilityListItemState, 0);
        Label_01F3:
            status = new BaseStatus();
            status2 = new BaseStatus();
            this.UnlockArtifact.GetHomePassiveBuffStatus(&status, &status2, null, 0, 1);
            this.mArtifactStatus.SetValues(status, status2, 0);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public void SetArtifactData()
        {
            ArtifactData data;
            data = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_0023;
            }
            GlobalVars.ConditionJobs = data.ArtifactParam.condition_jobs;
        Label_0023:
            return;
        }

        public void ShowDetail()
        {
            if (this.UnlockArtifact != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.DetailWindow == null) == null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            base.StartCoroutine(this.ShowDetailAsync());
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShowDetailAsync()
        {
            <ShowDetailAsync>c__IteratorF3 rf;
            rf = new <ShowDetailAsync>c__IteratorF3();
            rf.<>f__this = this;
            return rf;
        }

        private void Start()
        {
            this.Refresh();
            return;
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey321
        {
            internal string abil_iname;

            public <Refresh>c__AnonStorey321()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2C8(AbilityData x)
            {
                return (x.Param.iname == this.abil_iname);
            }
        }

        [CompilerGenerated]
        private sealed class <ShowDetailAsync>c__IteratorF3 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal ConceptCardArtifactDetail <>f__this;

            public <ShowDetailAsync>c__IteratorF3()
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
                        goto Label_009F;

                    case 2:
                        goto Label_00D4;
                }
                goto Label_00DB;
            Label_0025:
                if ((this.<>f__this.DetailWindow != null) == null)
                {
                    goto Label_00C1;
                }
                if ((this.<>f__this.mDetailWindow == null) == null)
                {
                    goto Label_00C1;
                }
                this.<>f__this.mDetailWindow = Object.Instantiate<GameObject>(this.<>f__this.DetailWindow);
                DataSource.Bind<ArtifactData>(this.<>f__this.mDetailWindow, this.<>f__this.UnlockArtifact);
                goto Label_009F;
            Label_008C:
                this.$current = null;
                this.$PC = 1;
                goto Label_00DD;
            Label_009F:
                if ((this.<>f__this.mDetailWindow != null) != null)
                {
                    goto Label_008C;
                }
                this.<>f__this.mDetailWindow = null;
            Label_00C1:
                this.$current = null;
                this.$PC = 2;
                goto Label_00DD;
            Label_00D4:
                this.$PC = -1;
            Label_00DB:
                return 0;
            Label_00DD:
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

        private enum AbilityAnimatorType
        {
            Hidden,
            Locked,
            Unlocked
        }
    }
}

