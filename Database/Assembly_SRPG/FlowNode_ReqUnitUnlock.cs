namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(2, "Download Assets", 1, 2), NodeType("System/ReqUnitUnlock", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1)]
    public class FlowNode_ReqUnitUnlock : FlowNode_Network
    {
        [StringIsResourcePath(typeof(GameObject))]
        public string ResultPrefabPath;
        public string RarityIntName;
        private LoadRequest mReq;
        private UnitParam mUnlockUnitParam;

        public FlowNode_ReqUnitUnlock()
        {
            this.RarityIntName = "rarity";
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            if (pinID != null)
            {
                goto Label_0074;
            }
            if (Network.Mode != 1)
            {
                goto Label_0018;
            }
            this.Success();
            return;
        Label_0018:
            str = GlobalVars.UnlockUnitID;
            this.mUnlockUnitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(str);
            if (string.IsNullOrEmpty(this.ResultPrefabPath) != null)
            {
                goto Label_0055;
            }
            this.mReq = AssetManager.LoadAsync<GameObject>(this.ResultPrefabPath);
        Label_0055:
            base.ExecRequest(new ReqUnitUnlock(str, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0074:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnFailed();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response.body != null)
            {
                goto Label_004C;
            }
            this.OnFailed();
            return;
        Label_004C:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                goto Label_00A7;
            }
            catch (Exception exception1)
            {
            Label_0090:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnFailed();
                goto Label_00BA;
            }
        Label_00A7:
            CriticalSection.Enter(2);
            base.StartCoroutine(this.WaitDownloadAsync());
        Label_00BA:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }

        [DebuggerHidden]
        private IEnumerator WaitDownloadAsync()
        {
            <WaitDownloadAsync>c__IteratorC6 rc;
            rc = new <WaitDownloadAsync>c__IteratorC6();
            rc.<>f__this = this;
            return rc;
        }

        [CompilerGenerated]
        private sealed class <WaitDownloadAsync>c__IteratorC6 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal Animator <anim>__1;
            internal GameManager <gm>__2;
            internal RarityParam <rp>__3;
            internal UnitData <unit>__4;
            internal int $PC;
            internal object $current;
            internal FlowNode_ReqUnitUnlock <>f__this;

            public <WaitDownloadAsync>c__IteratorC6()
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
                        goto Label_0031;

                    case 1:
                        goto Label_0051;

                    case 2:
                        goto Label_0083;

                    case 3:
                        goto Label_009B;

                    case 4:
                        goto Label_01DF;

                    case 5:
                        goto Label_0226;
                }
                goto Label_022D;
            Label_0031:
                this.<>f__this.ActivateOutputLinks(2);
                this.$current = null;
                this.$PC = 1;
                goto Label_022F;
            Label_0051:
                if (this.<>f__this.mReq == null)
                {
                    goto Label_009B;
                }
                this.$current = this.<>f__this.mReq.StartCoroutine();
                this.$PC = 2;
                goto Label_022F;
            Label_0083:
                goto Label_009B;
            Label_0088:
                this.$current = null;
                this.$PC = 3;
                goto Label_022F;
            Label_009B:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0088;
                }
                if (this.<>f__this.mReq == null)
                {
                    goto Label_01F0;
                }
                if ((this.<>f__this.mReq.asset != null) == null)
                {
                    goto Label_01F0;
                }
                this.<go>__0 = Object.Instantiate(this.<>f__this.mReq.asset) as GameObject;
                this.<anim>__1 = this.<go>__0.GetComponent<Animator>();
                if ((this.<anim>__1 != null) == null)
                {
                    goto Label_0138;
                }
                this.<anim>__1.SetInteger(this.<>f__this.RarityIntName, this.<>f__this.mUnlockUnitParam.rare);
            Label_0138:
                this.<gm>__2 = MonoSingleton<GameManager>.GetInstanceDirect();
                this.<rp>__3 = this.<gm>__2.GetRarityParam(this.<>f__this.mUnlockUnitParam.rare);
                if (this.<rp>__3 == null)
                {
                    goto Label_018A;
                }
                MonoSingleton<MySound>.Instance.PlayJingle(this.<rp>__3.DropSE, 0f, null);
            Label_018A:
                this.<unit>__4 = this.<gm>__2.Player.FindUnitDataByUniqueParam(this.<>f__this.mUnlockUnitParam);
                DataSource.Bind<UnitData>(this.<go>__0, this.<unit>__4);
                GameParameter.UpdateAll(this.<go>__0);
                goto Label_01DF;
            Label_01CC:
                this.$current = null;
                this.$PC = 4;
                goto Label_022F;
            Label_01DF:
                if ((this.<go>__0 != null) != null)
                {
                    goto Label_01CC;
                }
            Label_01F0:
                CriticalSection.Leave(2);
                MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(0);
                this.<>f__this.ActivateOutputLinks(1);
                this.$current = null;
                this.$PC = 5;
                goto Label_022F;
            Label_0226:
                this.$PC = -1;
            Label_022D:
                return 0;
            Label_022F:
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

