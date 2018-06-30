namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class HomeUnitController : UnitController
    {
        private const string ID_WALK = "WALK";
        public Color DirectLitColor;
        public Color IndirectLitColor;

        public HomeUnitController()
        {
            this.DirectLitColor = Color.get_white();
            this.IndirectLitColor = Color.get_clear();
            base..ctor();
            return;
        }

        public static SectionParam GetHomeWorld()
        {
            GameManager manager;
            SectionParam param;
            QuestParam param2;
            ChapterParam param3;
            manager = MonoSingleton<GameManager>.Instance;
            param = null;
            if (string.IsNullOrEmpty(GlobalVars.SelectedSection) != null)
            {
                goto Label_0050;
            }
            param = manager.FindWorld(GlobalVars.SelectedSection);
            if (param == null)
            {
                goto Label_0050;
            }
            if (string.IsNullOrEmpty(param.home) != null)
            {
                goto Label_004E;
            }
            if (param.hidden == null)
            {
                goto Label_0050;
            }
        Label_004E:
            param = null;
        Label_0050:
            if (param != null)
            {
                goto Label_0088;
            }
            param2 = manager.Player.FindLastStoryQuest();
            if (param2 == null)
            {
                goto Label_0088;
            }
            param3 = manager.FindArea(param2.ChapterID);
            if (param3 == null)
            {
                goto Label_0088;
            }
            param = manager.FindWorld(param3.section);
        Label_0088:
            return param;
        }

        [DebuggerHidden]
        private IEnumerator LoadAsync()
        {
            <LoadAsync>c__Iterator7A iteratora;
            iteratora = new <LoadAsync>c__Iterator7A();
            iteratora.<>f__this = this;
            return iteratora;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            return;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            return;
        }

        protected override void PostSetup()
        {
            base.PostSetup();
            base.LoadAnimationAsync("WALK", "CHM/Home_" + base.UnitData.UnitParam.model + "_walk0");
            base.StartCoroutine(this.LoadAsync());
            return;
        }

        protected override void Start()
        {
            SectionParam param;
            GameManager manager;
            UnitData data;
            CriticalSection.Enter(4);
            param = GetHomeWorld();
            if (param != null)
            {
                goto Label_001D;
            }
            Debug.LogError("home world is null.");
            return;
        Label_001D:
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(param.unit);
            if (data != null)
            {
                goto Label_0042;
            }
            CriticalSection.Leave(4);
            return;
        Label_0042:
            this.SetupUnit(data, -1);
            base.Start();
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadAsync>c__Iterator7A : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal int $PC;
            internal object $current;
            internal HomeUnitController <>f__this;

            public <LoadAsync>c__Iterator7A()
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
                        goto Label_0041;

                    case 2:
                        goto Label_0111;
                }
                goto Label_011E;
            Label_0025:
                goto Label_0041;
            Label_002A:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0120;
            Label_0041:
                if (this.<>f__this.IsLoading != null)
                {
                    goto Label_002A;
                }
                this.<i>__0 = 0;
                goto Label_00C1;
            Label_005D:
                this.<>f__this.CharacterMaterials[this.<i>__0].SetColor("_directLitColor", this.<>f__this.DirectLitColor);
                this.<>f__this.CharacterMaterials[this.<i>__0].SetColor("_indirectLitColor", this.<>f__this.IndirectLitColor);
                this.<i>__0 += 1;
            Label_00C1:
                if (this.<i>__0 < this.<>f__this.CharacterMaterials.Count)
                {
                    goto Label_005D;
                }
                this.<>f__this.PlayAnimation("WALK", 1);
                GameUtility.SetLayer(this.<>f__this, GameUtility.LayerCH, 1);
                this.$current = null;
                this.$PC = 2;
                goto Label_0120;
            Label_0111:
                CriticalSection.Leave(4);
                this.$PC = -1;
            Label_011E:
                return 0;
            Label_0120:
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

