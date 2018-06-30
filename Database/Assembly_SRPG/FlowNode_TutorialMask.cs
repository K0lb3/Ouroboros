namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [Pin(0x66, "OPEND", 1, 0x66), NodeType("Tutorial/Mask", 0x7fe5), Pin(1, "CREATE", 0, 1), Pin(0x65, "DESTORY", 1, 0x65)]
    public class FlowNode_TutorialMask : FlowNode
    {
        private const int PIN_IN_CREATE = 1;
        private const int PIN_OUT_DESTORY = 0x65;
        private const int PIN_OUT_OPEND = 0x66;
        public eComponentId ComponentId;
        [SerializeField]
        private GameObject Mask;
        [SerializeField]
        private float NoResponseTime;
        private Vector3 mWorldPosition;
        private TutorialMask.eActionType mActionType;
        private bool mIsWorld2Screen;
        private string mText;
        private Vector2 mSize;

        public FlowNode_TutorialMask()
        {
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator CreateMask()
        {
            <CreateMask>c__IteratorD0 rd;
            rd = new <CreateMask>c__IteratorD0();
            rd.<>f__this = this;
            return rd;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            if ((this.Mask == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = pinID;
            if (num == 1)
            {
                goto Label_0020;
            }
            goto Label_0032;
        Label_0020:
            base.StartCoroutine(this.CreateMask());
        Label_0032:
            return;
        }

        private void OnDestroyMask()
        {
            base.ActivateOutputLinks(0x65);
            return;
        }

        private void OnOpendMask()
        {
            base.ActivateOutputLinks(0x66);
            return;
        }

        public void Setup(TutorialMask.eActionType act_type, Vector3 world_pos, bool is_world2screen, string text)
        {
            this.mActionType = act_type;
            this.mWorldPosition = world_pos;
            this.mIsWorld2Screen = is_world2screen;
            this.mText = text;
            return;
        }

        public void SetupMaskSize(float width, float height)
        {
            this.mSize = new Vector2(width, height);
            return;
        }

        [CompilerGenerated]
        private sealed class <CreateMask>c__IteratorD0 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <obj>__0;
            internal TutorialMask <mask>__1;
            internal DestroyEventListener <de>__2;
            internal int $PC;
            internal object $current;
            internal FlowNode_TutorialMask <>f__this;

            public <CreateMask>c__IteratorD0()
            {
                base..ctor();
                return;
            }

            internal void <>m__1D9(GameObject go)
            {
                this.<>f__this.OnDestroyMask();
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
                        goto Label_006C;
                }
                goto Label_014C;
            Label_0021:
                this.<obj>__0 = Object.Instantiate<GameObject>(this.<>f__this.Mask);
                this.<mask>__1 = this.<obj>__0.GetComponent<TutorialMask>();
                this.<mask>__1.get_gameObject().SetActive(0);
                this.$current = null;
                this.$PC = 1;
                goto Label_014E;
            Label_006C:
                if ((this.<mask>__1 != null) == null)
                {
                    goto Label_0145;
                }
                this.<mask>__1.get_gameObject().SetActive(1);
                this.<mask>__1.Setup(this.<>f__this.mActionType, this.<>f__this.mWorldPosition, this.<>f__this.mIsWorld2Screen, this.<>f__this.mText);
                this.<mask>__1.SetupMaskSize(this.<>f__this.mSize);
                this.<mask>__1.SetupNoResponseTime(this.<>f__this.NoResponseTime);
                this.<mask>__1.mOpendMethod = new TutorialMask.OpendMethod(this.<>f__this.OnOpendMask);
                this.<de>__2 = this.<obj>__0.AddComponent<DestroyEventListener>();
                this.<de>__2.Listeners = (DestroyEventListener.DestroyEvent) Delegate.Combine(this.<de>__2.Listeners, new DestroyEventListener.DestroyEvent(this.<>m__1D9));
            Label_0145:
                this.$PC = -1;
            Label_014C:
                return 0;
            Label_014E:
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

        public enum eComponentId
        {
            MOVE_UNIT,
            ATTACK_TARGET_DESC,
            NORMAL_ATTACK_DESC,
            ABILITY_DESC,
            TAP_NORMAL_ATTACK,
            EXEC_NORMAL_ATTACK,
            SELECT_DIR,
            CLOSE_BATTLE_INFO
        }
    }
}

