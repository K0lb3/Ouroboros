namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(14, "Re PlayAnim", 1, 14), Pin(3, "SelectRedraw", 0, 3), Pin(4, "CancelRedraw", 0, 4), Pin(13, "CancelRedraw", 1, 13), Pin(2, "RedrawGacha", 0, 2), Pin(0, "PlayAnim", 0, 0), Pin(1, "ShowResult", 0, 1), Pin(10, "PlayAnim", 1, 10), Pin(11, "RedrawGacha", 1, 11), Pin(12, "ShowResult", 1, 12)]
    public class TutorialGacha : MonoBehaviour, IFlowInterface
    {
        private const int PIN_IN_PLAY_ANIM = 0;
        private const int PIN_IN_SHOW_RESULT = 1;
        private const int PIN_IN_REDRAW_GACHA = 2;
        private const int PIN_IN_SELECT_REDRAW = 3;
        private const int PIN_IN_CANCEL_REDRAW = 4;
        private const int PIN_OU_PLAY_ANIM = 10;
        private const int PIN_OU_REDRAW_GACHA = 11;
        private const int PIN_OU_SHOW_RESULT = 12;
        private const int PIN_OU_CANCEL_REDRAW = 13;
        private const int PIN_OU_RE_PLAY_ANIM = 14;
        [SerializeField, StringIsResourcePath(typeof(GachaController))]
        private string GachaAnimPrefab;
        [SerializeField]
        private string GachaResultPrefab;
        [SerializeField]
        private Transform UIRoot;
        [SerializeField]
        private GameObject[] ResultOptionObject;
        private GachaController m_GachaController;
        private GameObject m_GachaResult;

        public TutorialGacha()
        {
            this.GachaAnimPrefab = "UI/GachaAnim2";
            this.GachaResultPrefab = "UI/GachaResult";
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_0021;

                case 1:
                    goto Label_0035;

                case 2:
                    goto Label_0047;

                case 3:
                    goto Label_0054;

                case 4:
                    goto Label_0061;
            }
            goto Label_006C;
        Label_0021:
            base.StartCoroutine(this.PlayGachaAsync(10));
            goto Label_006C;
        Label_0035:
            base.StartCoroutine(this.ShowGachaResult());
            goto Label_006C;
        Label_0047:
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            goto Label_006C;
        Label_0054:
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            goto Label_006C;
        Label_0061:
            this.DecideTutorialGacha();
        Label_006C:
            return;
        }

        private void DecideTutorialGacha()
        {
            if ((this.m_GachaController != null) == null)
            {
                goto Label_0028;
            }
            Object.Destroy(this.m_GachaController.get_gameObject());
            this.m_GachaController = null;
        Label_0028:
            if ((this.m_GachaResult != null) == null)
            {
                goto Label_004B;
            }
            Object.Destroy(this.m_GachaResult);
            this.m_GachaResult = null;
        Label_004B:
            FlowNode_GameObject.ActivateOutputLinks(this, 13);
            return;
        }

        [DebuggerHidden]
        private IEnumerator PlayGachaAsync(int pinID)
        {
            <PlayGachaAsync>c__Iterator146 iterator;
            iterator = new <PlayGachaAsync>c__Iterator146();
            iterator.pinID = pinID;
            iterator.<$>pinID = pinID;
            iterator.<>f__this = this;
            return iterator;
        }

        private bool SetupDropData(UnitParam _unit)
        {
            GachaDropData data;
            List<GachaDropData> list;
            if (_unit != null)
            {
                goto Label_0012;
            }
            DebugUtility.LogError("召喚したいユニットの情報がありません.");
            return 0;
        Label_0012:
            data = new GachaDropData();
            data.type = 2;
            data.unit = _unit;
            data.Rare = _unit.rare;
            list = new List<GachaDropData>();
            list.Add(data);
            GachaResultData.Init(list, null, null, null, 0, -1, -1);
            return 1;
        }

        private void SetupOptionObject(bool _enable)
        {
            int num;
            if (this.ResultOptionObject == null)
            {
                goto Label_0019;
            }
            if (((int) this.ResultOptionObject.Length) > 0)
            {
                goto Label_0024;
            }
        Label_0019:
            DebugUtility.LogWarning("召喚結果で表示するオブションオブジェクトの指定がありません.");
            return;
        Label_0024:
            num = 0;
            goto Label_003D;
        Label_002B:
            this.ResultOptionObject[num].SetActive(_enable);
            num += 1;
        Label_003D:
            if (num < ((int) this.ResultOptionObject.Length))
            {
                goto Label_002B;
            }
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShowGachaResult()
        {
            <ShowGachaResult>c__Iterator147 iterator;
            iterator = new <ShowGachaResult>c__Iterator147();
            iterator.<>f__this = this;
            return iterator;
        }

        [CompilerGenerated]
        private sealed class <PlayGachaAsync>c__Iterator146 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal GachaResultThumbnailWindow <window>__1;
            internal int pinID;
            internal int $PC;
            internal object $current;
            internal int <$>pinID;
            internal TutorialGacha <>f__this;

            public <PlayGachaAsync>c__Iterator146()
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
                        goto Label_00D0;
                }
                goto Label_0198;
            Label_0021:
                if (GachaResultData.drops == null)
                {
                    goto Label_0038;
                }
                if (((int) GachaResultData.drops.Length) > 0)
                {
                    goto Label_0047;
                }
            Label_0038:
                DebugUtility.LogError("召喚したいユニットの情報がセット出来ません.");
                goto Label_0198;
            Label_0047:
                this.<>f__this.SetupOptionObject(0);
                if ((this.<>f__this.m_GachaController == null) == null)
                {
                    goto Label_0136;
                }
                if (string.IsNullOrEmpty(this.<>f__this.GachaAnimPrefab) == null)
                {
                    goto Label_008D;
                }
                DebugUtility.LogError("召喚演出のprefabが指定されていません.");
                goto Label_0198;
            Label_008D:
                this.<req>__0 = AssetManager.LoadAsync<GachaController>(this.<>f__this.GachaAnimPrefab);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_00D0;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_019A;
            Label_00D0:
                if ((this.<req>__0.asset == null) == null)
                {
                    goto Label_00F5;
                }
                DebugUtility.LogError("召喚演出のprefabのLoadに失敗しました.");
                goto Label_0198;
            Label_00F5:
                this.<>f__this.m_GachaController = Object.Instantiate(this.<req>__0.asset) as GachaController;
                this.<>f__this.m_GachaController.get_transform().SetParent(this.<>f__this.get_transform(), 0);
            Label_0136:
                if ((this.<>f__this.m_GachaResult != null) == null)
                {
                    goto Label_0180;
                }
                this.<window>__1 = this.<>f__this.m_GachaResult.GetComponentInChildren<GachaResultThumbnailWindow>(1);
                if ((this.<window>__1 != null) == null)
                {
                    goto Label_0180;
                }
                this.<window>__1.SetDetailActiveStatus(0);
            Label_0180:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, this.pinID);
                this.$PC = -1;
            Label_0198:
                return 0;
            Label_019A:
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
        private sealed class <ShowGachaResult>c__Iterator147 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal int $PC;
            internal object $current;
            internal TutorialGacha <>f__this;

            public <ShowGachaResult>c__Iterator147()
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
                        goto Label_00E1;
                }
                goto Label_015B;
            Label_0021:
                if ((this.<>f__this.m_GachaController != null) == null)
                {
                    goto Label_0058;
                }
                Object.Destroy(this.<>f__this.m_GachaController.get_gameObject());
                this.<>f__this.m_GachaController = null;
            Label_0058:
                this.<>f__this.SetupOptionObject(1);
                if ((this.<>f__this.m_GachaResult == null) == null)
                {
                    goto Label_0147;
                }
                if (string.IsNullOrEmpty(this.<>f__this.GachaResultPrefab) == null)
                {
                    goto Label_009E;
                }
                DebugUtility.LogError("召喚結果のprefabが指定されていません.");
                goto Label_015B;
            Label_009E:
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.GachaResultPrefab);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_00E1;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_015D;
            Label_00E1:
                if ((this.<req>__0.asset == null) == null)
                {
                    goto Label_0106;
                }
                DebugUtility.LogError("召喚結果のprefabのロードに失敗しました.");
                goto Label_015B;
            Label_0106:
                this.<>f__this.m_GachaResult = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<>f__this.m_GachaResult.get_transform().SetParent(this.<>f__this.UIRoot, 0);
            Label_0147:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 12);
                this.$PC = -1;
            Label_015B:
                return 0;
            Label_015D:
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

