namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0, "Start", 0, 0), Pin(1, "Finished", 1, 1), NodeType("UI/Tutorial Gacha")]
    public class FlowNode_TutorialGacha : FlowNode
    {
        private const int PIN_IN_TUTORIAL_GACHA_START = 0;
        private const int PIN_OU_TUTORIAL_GACHA_FINISHED = 1;
        public int UnitIndex;
        [StringIsResourcePath(typeof(GachaController))]
        public string Prefab_GachaController;
        [StringIsResourcePath(typeof(TutorialGacha)), SerializeField]
        private string Prefab_TutorialGacha;
        private GachaController mGachaController;
        private TutorialGacha m_TutorialGacha;

        public FlowNode_TutorialGacha()
        {
            base..ctor();
            return;
        }

        private void Finished()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0026;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            base.set_enabled(1);
            base.StartCoroutine(this.PlayGachaAsync());
        Label_0026:
            return;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if ((this.mGachaController != null) == null)
            {
                goto Label_002E;
            }
            Object.Destroy(this.mGachaController.get_gameObject());
            this.mGachaController = null;
        Label_002E:
            return;
        }

        [DebuggerHidden]
        private IEnumerator PlayGachaAsync()
        {
            <PlayGachaAsync>c__IteratorCD rcd;
            rcd = new <PlayGachaAsync>c__IteratorCD();
            rcd.<>f__this = this;
            return rcd;
        }

        [CompilerGenerated]
        private sealed class <PlayGachaAsync>c__IteratorCD : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal int $PC;
            internal object $current;
            internal FlowNode_TutorialGacha <>f__this;

            public <PlayGachaAsync>c__IteratorCD()
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
                        goto Label_008C;

                    case 2:
                        goto Label_010A;
                }
                goto Label_013E;
            Label_0025:
                if (string.IsNullOrEmpty(this.<>f__this.Prefab_TutorialGacha) == null)
                {
                    goto Label_0049;
                }
                DebugUtility.LogError("チュートリアル召喚で使用するPrefabのPATHが指定されていない/間違っています.");
                goto Label_013E;
            Label_0049:
                this.<req>__0 = AssetManager.LoadAsync<TutorialGacha>(this.<>f__this.Prefab_TutorialGacha);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_008C;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_0140;
            Label_008C:
                if ((this.<req>__0.asset == null) == null)
                {
                    goto Label_00B1;
                }
                DebugUtility.LogError("チュートリアル召喚で使用するPrefabが読み込めませんでした.");
                goto Label_013E;
            Label_00B1:
                this.<>f__this.m_TutorialGacha = Object.Instantiate(this.<req>__0.asset) as TutorialGacha;
                this.<>f__this.m_TutorialGacha.get_transform().SetParent(this.<>f__this.get_transform(), 0);
                goto Label_010A;
            Label_00F7:
                this.$current = null;
                this.$PC = 2;
                goto Label_0140;
            Label_010A:
                if ((this.<>f__this.m_TutorialGacha != null) != null)
                {
                    goto Label_00F7;
                }
                this.<>f__this.m_TutorialGacha = null;
                this.<>f__this.Finished();
                this.$PC = -1;
            Label_013E:
                return 0;
            Label_0140:
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

