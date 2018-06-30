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

    [Pin(100, "表示開始", 1, 100), Pin(10, "終了", 0, 10)]
    public class GachaScene : SceneRoot, IFlowInterface
    {
        public GameObject Result2D;
        public GameObject Result3D;
        public GridLayoutGroup GridLayout;
        public int MaxGridColumnCount;
        public string[] PreviewUnitID;
        public string[] PreviewItemID;
        private DropClasses mDropClass;
        private string[] mDropID;
        private bool mDropSet;

        public GachaScene()
        {
            this.MaxGridColumnCount = 5;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_0016;
            }
            base.StartCoroutine(this.ExitGachaAsync());
            return;
        Label_0016:
            return;
        }

        [DebuggerHidden]
        private IEnumerator AsyncUpdate()
        {
            <AsyncUpdate>c__Iterator76 iterator;
            iterator = new <AsyncUpdate>c__Iterator76();
            iterator.<>f__this = this;
            return iterator;
        }

        protected override void Awake()
        {
            base.Awake();
            return;
        }

        public void DropItems(string[] itemID)
        {
            this.mDropClass = 1;
            this.mDropID = itemID;
            this.mDropSet = 1;
            return;
        }

        public void DropUnits(string[] unitID)
        {
            this.mDropClass = 0;
            this.mDropID = unitID;
            this.mDropSet = 1;
            return;
        }

        [DebuggerHidden]
        private IEnumerator ExitGachaAsync()
        {
            <ExitGachaAsync>c__Iterator77 iterator;
            iterator = new <ExitGachaAsync>c__Iterator77();
            iterator.<>f__this = this;
            return iterator;
        }

        private void Start()
        {
            if ((this.Result2D != null) == null)
            {
                goto Label_001D;
            }
            this.Result2D.SetActive(0);
        Label_001D:
            base.StartCoroutine(this.AsyncUpdate());
            return;
        }

        [CompilerGenerated]
        private sealed class <AsyncUpdate>c__Iterator76 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal UnitPreview <unitPreview>__0;
            internal int <i>__1;
            internal UnitData <unitData>__2;
            internal GameObject <go>__3;
            internal GachaResultData_old <resultData>__4;
            internal GameObject <item>__5;
            internal int <i>__6;
            internal ItemParam <itemParam>__7;
            internal GachaResultData_old <resultData>__8;
            internal GameObject <item>__9;
            internal int $PC;
            internal object $current;
            internal GachaScene <>f__this;

            public <AsyncUpdate>c__Iterator76()
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
                Type[] typeArray1;
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_0045;

                    case 2:
                        goto Label_0352;

                    case 3:
                        goto Label_039F;
                }
                goto Label_03BD;
            Label_0029:
                goto Label_0045;
            Label_002E:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_03BF;
            Label_0045:
                if (this.<>f__this.mDropSet == null)
                {
                    goto Label_002E;
                }
                this.<unitPreview>__0 = null;
                if ((this.<>f__this.GridLayout != null) == null)
                {
                    goto Label_009F;
                }
                this.<>f__this.GridLayout.set_constraintCount(Mathf.Min((int) this.<>f__this.mDropID.Length, this.<>f__this.MaxGridColumnCount));
            Label_009F:
                if (this.<>f__this.mDropClass != null)
                {
                    goto Label_0246;
                }
                this.<i>__1 = 0;
                goto Label_0229;
            Label_00BB:
                this.<unitData>__2 = new UnitData();
                if (this.<unitData>__2.Setup(this.<>f__this.mDropID[this.<i>__1], 0, 0, 0, null, 1, 0, 0) != null)
                {
                    goto Label_00F4;
                }
                goto Label_021B;
            Label_00F4:
                if (((int) this.<>f__this.mDropID.Length) != 1)
                {
                    goto Label_018B;
                }
                if ((this.<>f__this.Result3D != null) == null)
                {
                    goto Label_018B;
                }
                typeArray1 = new Type[] { typeof(UnitPreview) };
                this.<go>__3 = new GameObject("Unit", typeArray1);
                this.<unitPreview>__0 = this.<go>__3.GetComponent<UnitPreview>();
                this.<unitPreview>__0.KeepUnitHidden = 1;
                this.<unitPreview>__0.SetupUnit(this.<unitData>__2, -1);
                this.<unitPreview>__0.get_transform().SetParent(this.<>f__this.get_transform(), 0);
            Label_018B:
                this.<resultData>__4 = new GachaResultData_old();
                this.<resultData>__4.Name = this.<unitData>__2.UnitParam.name;
                this.<item>__5 = Object.Instantiate<GameObject>(this.<>f__this.Result2D);
                DataSource.Bind<UnitData>(this.<item>__5, this.<unitData>__2);
                DataSource.Bind<GachaResultData_old>(this.<item>__5, this.<resultData>__4);
                this.<item>__5.get_transform().SetParent(this.<>f__this.Result2D.get_transform().get_parent(), 0);
                this.<item>__5.SetActive(1);
            Label_021B:
                this.<i>__1 += 1;
            Label_0229:
                if (this.<i>__1 < ((int) this.<>f__this.mDropID.Length))
                {
                    goto Label_00BB;
                }
                goto Label_0325;
            Label_0246:
                this.<i>__6 = 0;
                goto Label_030D;
            Label_0252:
                this.<itemParam>__7 = MonoSingleton<GameManager>.Instance.GetItemParam(this.<>f__this.mDropID[this.<i>__6]);
                this.<resultData>__8 = new GachaResultData_old();
                this.<resultData>__8.Name = this.<itemParam>__7.name;
                this.<item>__9 = Object.Instantiate<GameObject>(this.<>f__this.Result2D);
                DataSource.Bind<ItemParam>(this.<item>__9, this.<itemParam>__7);
                DataSource.Bind<GachaResultData_old>(this.<item>__9, this.<resultData>__8);
                this.<item>__9.get_transform().SetParent(this.<>f__this.Result2D.get_transform().get_parent(), 0);
                this.<item>__9.SetActive(1);
                this.<i>__6 += 1;
            Label_030D:
                if (this.<i>__6 < ((int) this.<>f__this.mDropID.Length))
                {
                    goto Label_0252;
                }
            Label_0325:
                if ((this.<unitPreview>__0 != null) == null)
                {
                    goto Label_039F;
                }
                goto Label_0352;
            Label_033B:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_03BF;
            Label_0352:
                if (this.<unitPreview>__0.IsLoading != null)
                {
                    goto Label_033B;
                }
                this.<unitPreview>__0.get_transform().SetParent(this.<>f__this.Result3D.get_transform(), 0);
                goto Label_039F;
            Label_0388:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_03BF;
            Label_039F:
                if (AssetManager.IsLoading != null)
                {
                    goto Label_0388;
                }
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 100);
                this.$PC = -1;
            Label_03BD:
                return 0;
            Label_03BF:
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

        [CompilerGenerated]
        private sealed class <ExitGachaAsync>c__Iterator77 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal GachaScene <>f__this;

            public <ExitGachaAsync>c__Iterator77()
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
                        goto Label_005C;
                }
                goto Label_008C;
            Label_0021:
                FadeController.Instance.FadeTo(Color.get_clear(), 0f, 0);
                GameUtility.FadeOut(1f);
                goto Label_005C;
            Label_0045:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_008E;
            Label_005C:
                if (GameUtility.IsScreenFading != null)
                {
                    goto Label_0045;
                }
                CanvasStack.ShowAllCanvases();
                GameUtility.FadeIn(1f);
                Object.Destroy(this.<>f__this.get_gameObject());
                this.$PC = -1;
            Label_008C:
                return 0;
            Label_008E:
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

        public enum DropClasses
        {
            Unit,
            Item
        }
    }
}

