namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class UnitPreview : UnitController
    {
        private const string ID_IDLE = "idle";
        private const string ID_ACTION = "action";
        public bool PlayAction;
        private bool mPlayingAction;
        public int DefaultLayer;

        public UnitPreview()
        {
            this.DefaultLayer = GameUtility.LayerDefault;
            base..ctor();
            return;
        }

        public Transform GetHeadPosition()
        {
            if ((base.Rig != null) == null)
            {
                goto Label_0028;
            }
            return GameUtility.findChildRecursively(base.get_transform(), base.Rig.Head);
        Label_0028:
            return null;
        }

        [DebuggerHidden]
        private IEnumerator LoadThread()
        {
            <LoadThread>c__Iterator86 iterator;
            iterator = new <LoadThread>c__Iterator86();
            iterator.<>f__this = this;
            return iterator;
        }

        protected override void PostSetup()
        {
            base.PostSetup();
            base.LoadUnitAnimationAsync("idle", "unit_info_idle0", 1, 0);
            base.LoadUnitAnimationAsync("action", "unit_info_act0", 1, 0);
            base.StartCoroutine(this.LoadThread());
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
                goto Label_0082;
            }
            if (this.PlayAction == null)
            {
                goto Label_0045;
            }
            this.PlayAction = 0;
            base.PlayAnimation("action", 0, 0.1f, 0f);
            this.mPlayingAction = 1;
            goto Label_0082;
        Label_0045:
            if (this.mPlayingAction == null)
            {
                goto Label_0082;
            }
            if (base.GetRemainingTime("action") > 0f)
            {
                goto Label_0082;
            }
            base.PlayAnimation("idle", 1, 0.1f, 0f);
            this.mPlayingAction = 0;
        Label_0082:
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadThread>c__Iterator86 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitPreview <>f__this;

            public <LoadThread>c__Iterator86()
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
                goto Label_0088;
            Label_0021:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_008A;
            Label_0038:
                if (this.<>f__this.IsLoading != null)
                {
                    goto Label_0021;
                }
                this.<>f__this.PlayAnimation("idle", 1);
                GameUtility.RequireComponent<CharacterLighting>(this.<>f__this.get_gameObject());
                GameUtility.SetLayer(this.<>f__this, this.<>f__this.DefaultLayer, 1);
                this.$PC = -1;
            Label_0088:
                return 0;
            Label_008A:
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

