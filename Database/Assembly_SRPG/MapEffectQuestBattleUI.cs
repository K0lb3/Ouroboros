namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class MapEffectQuestBattleUI : MonoBehaviour
    {
        public SRPG_Button ButtonMapEffect;
        public string PrefabMapEffectQuest;
        private LoadRequest mReqMapEffect;

        public MapEffectQuestBattleUI()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__365(SRPG_Button button)
        {
            this.ReqOpenMapEffect();
            return;
        }

        [DebuggerHidden]
        private IEnumerator OpenMapEffect()
        {
            <OpenMapEffect>c__Iterator126 iterator;
            iterator = new <OpenMapEffect>c__Iterator126();
            iterator.<>f__this = this;
            return iterator;
        }

        private void ReqOpenMapEffect()
        {
            base.StartCoroutine(this.OpenMapEffect());
            return;
        }

        private void Start()
        {
            if (this.ButtonMapEffect == null)
            {
                goto Label_0027;
            }
            this.ButtonMapEffect.AddListener(new SRPG_Button.ButtonClickEvent(this.<Start>m__365));
        Label_0027:
            return;
        }

        [CompilerGenerated]
        private sealed class <OpenMapEffect>c__Iterator126 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameManager <gm>__0;
            internal SceneBattle <sb>__1;
            internal QuestParam <quest_param>__2;
            internal GameObject <asset>__3;
            internal GameObject <go>__4;
            internal MapEffectQuest <me_quest>__5;
            internal int $PC;
            internal object $current;
            internal MapEffectQuestBattleUI <>f__this;

            public <OpenMapEffect>c__Iterator126()
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
                        goto Label_0104;
                }
                goto Label_01FA;
            Label_0021:
                if (this.<>f__this.mReqMapEffect == null)
                {
                    goto Label_0036;
                }
                goto Label_01FA;
            Label_0036:
                if (string.IsNullOrEmpty(this.<>f__this.PrefabMapEffectQuest) == null)
                {
                    goto Label_0050;
                }
                goto Label_01FA;
            Label_0050:
                this.<gm>__0 = MonoSingleton<GameManager>.GetInstanceDirect();
                this.<sb>__1 = SceneBattle.Instance;
                if (this.<gm>__0 == null)
                {
                    goto Label_01FA;
                }
                if (this.<sb>__1 != null)
                {
                    goto Label_008B;
                }
                goto Label_01FA;
            Label_008B:
                this.<quest_param>__2 = this.<gm>__0.FindQuest(this.<sb>__1.CurrentQuest.iname);
                if (this.<quest_param>__2 != null)
                {
                    goto Label_00BC;
                }
                goto Label_01FA;
            Label_00BC:
                this.<>f__this.mReqMapEffect = AssetManager.LoadAsync<GameObject>(this.<>f__this.PrefabMapEffectQuest);
                if (this.<>f__this.mReqMapEffect != null)
                {
                    goto Label_0104;
                }
                goto Label_01FA;
                goto Label_0104;
            Label_00F1:
                this.$current = null;
                this.$PC = 1;
                goto Label_01FC;
            Label_0104:
                if (this.<>f__this.mReqMapEffect.isDone == null)
                {
                    goto Label_00F1;
                }
                if (this.<>f__this.mReqMapEffect.asset != null)
                {
                    goto Label_0138;
                }
                goto Label_01FA;
            Label_0138:
                this.<asset>__3 = this.<>f__this.mReqMapEffect.asset as GameObject;
                if (this.<asset>__3 != null)
                {
                    goto Label_0168;
                }
                goto Label_01FA;
            Label_0168:
                this.<go>__4 = MapEffectQuest.CreateInstance(this.<asset>__3, this.<>f__this.get_transform().get_parent());
                if (this.<go>__4 != null)
                {
                    goto Label_019E;
                }
                goto Label_01FA;
            Label_019E:
                DataSource.Bind<QuestParam>(this.<go>__4, this.<quest_param>__2);
                this.<go>__4.SetActive(1);
                this.<me_quest>__5 = this.<go>__4.GetComponent<MapEffectQuest>();
                if (this.<me_quest>__5 == null)
                {
                    goto Label_01E7;
                }
                this.<me_quest>__5.Setup();
            Label_01E7:
                this.<>f__this.mReqMapEffect = null;
                this.$PC = -1;
            Label_01FA:
                return 0;
            Label_01FC:
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

