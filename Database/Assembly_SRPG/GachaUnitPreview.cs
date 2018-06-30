namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class GachaUnitPreview : UnitController
    {
        public const string ID_IDLE = "idle";
        public const string ID_ACTION = "action";
        public const string ID_BATTLE_CHANT = "B_CHA";
        public const string ID_BATTLE_SKILL = "B_SKL";
        public const string ID_BATTLE_BACKSTEP = "B_BS";
        public bool PlayAction;
        private bool mPlayingAction;
        public int DefaultLayer;
        public UnitData mGUnitData;
        private string mCurrentJobID;

        public GachaUnitPreview()
        {
            this.DefaultLayer = GameUtility.LayerDefault;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <PostSetup>m__102(JobData p)
        {
            return (p.JobID == this.mCurrentJobID);
        }

        protected override bool IsEventAllowed(AnimEvent e)
        {
            return 0;
        }

        [DebuggerHidden]
        private IEnumerator LoadThread()
        {
            <LoadThread>c__Iterator78 iterator;
            iterator = new <LoadThread>c__Iterator78();
            iterator.<>f__this = this;
            return iterator;
        }

        protected override unsafe void PostSetup()
        {
            JobData data;
            string str;
            SkillSequence sequence;
            base.PostSetup();
            base.LoadUnitAnimationAsync("idle", "unit_info_idle0", 1, 0);
            base.LoadUnitAnimationAsync("action", "unit_info_act0", 1, 0);
            data = Array.Find<JobData>(this.GUnitData.Jobs, new Predicate<JobData>(this.<PostSetup>m__102));
            str = (data == null) ? this.GUnitData.CurrentJob.GetAttackSkill().SkillParam.motion : data.GetAttackSkill().SkillParam.motion;
            sequence = SkillSequence.FindSequence(str);
            if (sequence != null)
            {
                goto Label_008B;
            }
            return;
        Label_008B:
            if (&sequence.SkillAnimation.Name.Length <= 0)
            {
                goto Label_00B9;
            }
            base.LoadUnitAnimationAsync("B_SKL", &sequence.SkillAnimation.Name, 0, 0);
        Label_00B9:
            base.StartCoroutine(this.LoadThread());
            return;
        }

        public void SetGachaUnitData(UnitData unit, string jobId)
        {
            this.mGUnitData = unit;
            this.mCurrentJobID = jobId;
            return;
        }

        protected override void Start()
        {
            base.KeepUnitHidden = 1;
            base.LoadEquipments = 1;
            base.Start();
            return;
        }

        protected override void Update()
        {
            base.Update();
            if (this.IsLoading != null)
            {
                goto Label_0078;
            }
            if (this.PlayAction == null)
            {
                goto Label_003B;
            }
            this.PlayAction = 0;
            base.PlayAnimation("B_SKL", 0);
            this.mPlayingAction = 1;
            goto Label_0078;
        Label_003B:
            if (this.mPlayingAction == null)
            {
                goto Label_0078;
            }
            if (base.GetRemainingTime("B_SKL") > 0f)
            {
                goto Label_0078;
            }
            base.PlayAnimation("idle", 1, 0.1f, 0f);
            this.mPlayingAction = 0;
        Label_0078:
            return;
        }

        public UnitData GUnitData
        {
            get
            {
                return this.mGUnitData;
            }
            set
            {
                this.mGUnitData = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <LoadThread>c__Iterator78 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal GachaUnitPreview <>f__this;

            public <LoadThread>c__Iterator78()
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
                        goto Label_003D;
                }
                goto Label_008E;
            Label_0021:
                goto Label_003D;
            Label_0026:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0090;
            Label_003D:
                if (this.<>f__this.LoadingAnimationCount > 0)
                {
                    goto Label_0026;
                }
                this.<>f__this.PlayAnimation("idle", 1);
                GameUtility.RequireComponent<CharacterLighting>(this.<>f__this.get_gameObject());
                GameUtility.SetLayer(this.<>f__this, this.<>f__this.DefaultLayer, 1);
                this.$PC = -1;
            Label_008E:
                return 0;
            Label_0090:
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

