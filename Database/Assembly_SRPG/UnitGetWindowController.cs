namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class UnitGetWindowController : MonoBehaviour
    {
        private UnitGetWindow mController;
        private bool mIsEnd;

        public UnitGetWindowController()
        {
            base..ctor();
            return;
        }

        public void Init(UnitGetParam rewards)
        {
            UnitGetParam param;
            bool flag;
            string[] strArray;
            bool[] flagArray;
            int num;
            param = (rewards == null) ? GlobalVars.UnitGetReward : rewards;
            if (param == null)
            {
                goto Label_0029;
            }
            if (param.Params.Count > 0)
            {
                goto Label_0031;
            }
        Label_0029:
            this.mIsEnd = 1;
            return;
        Label_0031:
            flag = 1;
            strArray = new string[param.Params.Count];
            flagArray = new bool[param.Params.Count];
            num = 0;
            goto Label_00DF;
        Label_005D:
            if (param.Params[num].ItemType == 0x10)
            {
                goto Label_0084;
            }
            strArray[num] = string.Empty;
            goto Label_00D9;
        Label_0084:
            if (flag == null)
            {
                goto Label_008C;
            }
            flag = 0;
        Label_008C:
            strArray[num] = param.Params[num].ItemId;
            flagArray[num] = param.Params[num].IsConvert;
            if (flagArray[num] != null)
            {
                goto Label_00D9;
            }
            DownloadUtility.DownloadUnit(param.Params[num].UnitParam, null);
        Label_00D9:
            num += 1;
        Label_00DF:
            if (num < param.Params.Count)
            {
                goto Label_005D;
            }
            this.mIsEnd = flag;
            if (this.mIsEnd == null)
            {
                goto Label_0104;
            }
            return;
        Label_0104:
            base.StartCoroutine(this.SpawnEffectAsync(strArray, flagArray));
            return;
        }

        [DebuggerHidden]
        private IEnumerator SpawnEffectAsync(string[] unitIds, bool[] isConvert)
        {
            <SpawnEffectAsync>c__Iterator169 iterator;
            iterator = new <SpawnEffectAsync>c__Iterator169();
            iterator.unitIds = unitIds;
            iterator.isConvert = isConvert;
            iterator.<$>unitIds = unitIds;
            iterator.<$>isConvert = isConvert;
            iterator.<>f__this = this;
            return iterator;
        }

        public bool IsEnd
        {
            get
            {
                return this.mIsEnd;
            }
        }

        [CompilerGenerated]
        private sealed class <SpawnEffectAsync>c__Iterator169 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal GameObject <effectTemplate>__1;
            internal int <i>__2;
            internal string[] unitIds;
            internal GameObject <effectWindow>__3;
            internal bool[] isConvert;
            internal int $PC;
            internal object $current;
            internal string[] <$>unitIds;
            internal bool[] <$>isConvert;
            internal UnitGetWindowController <>f__this;

            public <SpawnEffectAsync>c__Iterator169()
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
                        goto Label_002D;

                    case 1:
                        goto Label_005D;

                    case 2:
                        goto Label_00A9;

                    case 3:
                        goto Label_01BE;

                    case 4:
                        goto Label_01F3;
                }
                goto Label_0260;
            Label_002D:
                if (AssetDownloader.isDone != null)
                {
                    goto Label_0067;
                }
                AssetDownloader.StartDownload(0, 1, 2);
                ProgressWindow.OpenGenericDownloadWindow();
                goto Label_005D;
            Label_004A:
                this.$current = null;
                this.$PC = 1;
                goto Label_0262;
            Label_005D:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_004A;
                }
            Label_0067:
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(GameSettings.Instance.UnitGet_EffectTemplate);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_00A9;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 2;
                goto Label_0262;
            Label_00A9:
                if ((this.<req>__0.asset == null) == null)
                {
                    goto Label_00C4;
                }
                goto Label_0260;
            Label_00C4:
                this.<effectTemplate>__1 = (GameObject) this.<req>__0.asset;
                if ((this.<effectTemplate>__1 == null) == null)
                {
                    goto Label_00F0;
                }
                goto Label_0260;
            Label_00F0:
                ProgressWindow.Close();
                this.<i>__2 = 0;
                goto Label_0234;
            Label_0101:
                if (string.IsNullOrEmpty(this.unitIds[this.<i>__2]) == null)
                {
                    goto Label_011D;
                }
                goto Label_0226;
            Label_011D:
                this.<effectWindow>__3 = Object.Instantiate<GameObject>(this.<effectTemplate>__1);
                if ((this.<effectWindow>__3 != null) == null)
                {
                    goto Label_0226;
                }
                this.<>f__this.mController = this.<effectWindow>__3.GetComponent<UnitGetWindow>();
                if ((this.<>f__this.mController != null) == null)
                {
                    goto Label_0226;
                }
                this.<>f__this.mController.get_gameObject().SetActive(1);
                this.<>f__this.mController.Init(this.unitIds[this.<i>__2], this.isConvert[this.<i>__2]);
                this.$current = null;
                this.$PC = 3;
                goto Label_0262;
            Label_01BE:
                this.<>f__this.mController.PlayAnim(this.isConvert[this.<i>__2]);
                goto Label_01F3;
            Label_01E0:
                this.$current = null;
                this.$PC = 4;
                goto Label_0262;
            Label_01F3:
                if (this.<>f__this.mController.IsEnd == null)
                {
                    goto Label_01E0;
                }
                Object.Destroy(this.<effectWindow>__3);
                this.<effectWindow>__3 = null;
                this.<>f__this.mController = null;
            Label_0226:
                this.<i>__2 += 1;
            Label_0234:
                if (this.<i>__2 < ((int) this.unitIds.Length))
                {
                    goto Label_0101;
                }
                this.<>f__this.mIsEnd = 1;
                GlobalVars.UnitGetReward = null;
                this.$PC = -1;
            Label_0260:
                return 0;
            Label_0262:
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

