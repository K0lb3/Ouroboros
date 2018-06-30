namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [NodeType("System/ExecGacha", 0x7fe5), Pin(7, "コイン不足", 1, 7), Pin(5, "Failed", 1, 5), Pin(4, "Success", 1, 4), Pin(30, "VIPガチャ", 0, 30), Pin(0x15, "レアガチャ10連", 0, 0x15), Pin(20, "レアガチャ", 0, 20), Pin(6, "ゴールド不足", 1, 6), Pin(10, "ノーマルガチャ", 0, 10), Pin(11, "ノーマルガチャ10連", 0, 11)]
    public class FlowNode_ExecGacha : FlowNode_Network
    {
        public string GachaScene_Normal;
        public string GachaScene_NormalX;
        public string GachaScene_Rare;
        public string GachaScene_RareX;
        public string GachaScene_VIP;
        private GachaOffline mGacha;
        private GachaModes mGachaMode;
        private static readonly int GACHA_GOLD_COST;
        private static readonly int GACHA_GOLD_MANY_COST;
        private static readonly int GACHA_COIN_COST;
        private static readonly int GACHA_COIN_MANY_COST;
        private string[] mGachaResult;
        private bool mGachaResultSet;
        private GachaScene mGachaScene;

        static FlowNode_ExecGacha()
        {
            GACHA_GOLD_COST = 0x3e8;
            GACHA_GOLD_MANY_COST = GACHA_GOLD_COST * 9;
            GACHA_COIN_COST = 1;
            GACHA_COIN_MANY_COST = GACHA_COIN_COST * 9;
            return;
        }

        public FlowNode_ExecGacha()
        {
            base..ctor();
            return;
        }

        private void ExecNormalGacha()
        {
            string[] textArray1;
            string str;
            this.StartGachaScene(0);
            MonoSingleton<GameManager>.Instance.Player.OnGacha(0, 1);
            if (Network.Mode != 1)
            {
                goto Label_006B;
            }
            str = this.mGacha.ExecGacha("Gacha_gold");
            MonoSingleton<GameManager>.Instance.Player.GainItem(str, 1);
            MonoSingleton<GameManager>.Instance.Player.DEBUG_ADD_GOLD(-GACHA_GOLD_COST);
            textArray1 = new string[] { str };
            this.SetGachaResult(textArray1);
            return;
        Label_006B:
            base.set_enabled(1);
            return;
        }

        private unsafe void ExecNormalGachaMany()
        {
            int num;
            string[] strArray;
            int num2;
            string str;
            int num3;
            this.StartGachaScene(1);
            MonoSingleton<GameManager>.Instance.Player.OnGacha(0, 10);
            if (Network.Mode != 1)
            {
                goto Label_0096;
            }
            num = 10;
            strArray = new string[num];
            num2 = 0;
            goto Label_005F;
        Label_0035:
            str = this.mGacha.ExecGacha("Gacha_gold");
            MonoSingleton<GameManager>.Instance.Player.GainItem(str, 1);
            strArray[num2] = str;
            num2 += 1;
        Label_005F:
            if (num2 < num)
            {
                goto Label_0035;
            }
            DebugUtility.Log(&GACHA_GOLD_MANY_COST.ToString());
            MonoSingleton<GameManager>.Instance.Player.DEBUG_ADD_GOLD(-GACHA_GOLD_MANY_COST);
            this.SetGachaResult(strArray);
            return;
        Label_0096:
            base.set_enabled(1);
            return;
        }

        private void ExecRareGacha()
        {
            string[] textArray1;
            string str;
            this.StartGachaScene(2);
            MonoSingleton<GameManager>.Instance.Player.OnGacha(1, 1);
            if (Network.Mode != 1)
            {
                goto Label_006A;
            }
            str = this.mGacha.ExecGacha("Gacha_kakin");
            MonoSingleton<GameManager>.Instance.Player.GainUnit(str);
            MonoSingleton<GameManager>.Instance.Player.DEBUG_CONSUME_COIN(GACHA_COIN_COST);
            textArray1 = new string[] { str };
            this.SetGachaResult(textArray1);
            return;
        Label_006A:
            base.set_enabled(1);
            return;
        }

        private void ExecRareGachaMany()
        {
            int num;
            string[] strArray;
            int num2;
            string str;
            this.StartGachaScene(3);
            MonoSingleton<GameManager>.Instance.Player.OnGacha(1, 10);
            if (Network.Mode != 1)
            {
                goto Label_0082;
            }
            num = 10;
            strArray = new string[num];
            num2 = 0;
            goto Label_005E;
        Label_0035:
            str = this.mGacha.ExecGacha("Gacha_kakin");
            MonoSingleton<GameManager>.Instance.Player.GainUnit(str);
            strArray[num2] = str;
            num2 += 1;
        Label_005E:
            if (num2 < num)
            {
                goto Label_0035;
            }
            MonoSingleton<GameManager>.Instance.Player.DEBUG_CONSUME_COIN(GACHA_COIN_MANY_COST);
            this.SetGachaResult(strArray);
            return;
        Label_0082:
            base.set_enabled(1);
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(5);
            return;
        }

        private string MakeResultString(Json_DropInfo[] drops)
        {
            MasterParam param;
            UnitParam[] paramArray;
            string str;
            Json_DropInfo info;
            Json_DropInfo[] infoArray;
            int num;
            ItemParam param2;
            UnitParam param3;
            UnitParam[] paramArray2;
            int num2;
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            paramArray = param.GetAllUnits();
            str = string.Empty;
            infoArray = drops;
            num = 0;
            goto Label_00AE;
        Label_0023:
            info = infoArray[num];
            param2 = param.GetItemParam(info.iname);
            if (param2 == null)
            {
                goto Label_0056;
            }
            str = str + param2.name + "\n";
            goto Label_00A8;
        Label_0056:
            paramArray2 = paramArray;
            num2 = 0;
            goto Label_009D;
        Label_0061:
            param3 = paramArray2[num2];
            if ((param3.iname == info.iname) == null)
            {
                goto Label_0097;
            }
            str = str + param3.name + "\n";
            goto Label_00A8;
        Label_0097:
            num2 += 1;
        Label_009D:
            if (num2 < ((int) paramArray2.Length))
            {
                goto Label_0061;
            }
        Label_00A8:
            num += 1;
        Label_00AE:
            if (num < ((int) infoArray.Length))
            {
                goto Label_0023;
            }
            return str;
        }

        public override void OnActivate(int pinID)
        {
            PlayerData data;
            int num;
            this.mGacha = new GachaOffline();
            data = MonoSingleton<GameManager>.Instance.Player;
            num = pinID;
            if (num == 10)
            {
                goto Label_0045;
            }
            if (num == 11)
            {
                goto Label_0074;
            }
            if (num == 20)
            {
                goto Label_00A3;
            }
            if (num == 0x15)
            {
                goto Label_00D2;
            }
            if (num == 30)
            {
                goto Label_0101;
            }
            goto Label_010C;
        Label_0045:
            if (data.Gold >= GACHA_GOLD_COST)
            {
                goto Label_0069;
            }
            base.ActivateOutputLinks(6);
            base.set_enabled(0);
            goto Label_006F;
        Label_0069:
            this.ExecNormalGacha();
        Label_006F:
            goto Label_0117;
        Label_0074:
            if (data.Gold >= GACHA_GOLD_MANY_COST)
            {
                goto Label_0098;
            }
            base.ActivateOutputLinks(6);
            base.set_enabled(0);
            goto Label_009E;
        Label_0098:
            this.ExecNormalGachaMany();
        Label_009E:
            goto Label_0117;
        Label_00A3:
            if (data.Coin >= GACHA_COIN_COST)
            {
                goto Label_00C7;
            }
            base.ActivateOutputLinks(7);
            base.set_enabled(0);
            goto Label_00CD;
        Label_00C7:
            this.ExecRareGacha();
        Label_00CD:
            goto Label_0117;
        Label_00D2:
            if (data.Coin >= GACHA_COIN_MANY_COST)
            {
                goto Label_00F6;
            }
            base.ActivateOutputLinks(7);
            base.set_enabled(0);
            goto Label_00FC;
        Label_00F6:
            this.ExecRareGachaMany();
        Label_00FC:
            goto Label_0117;
        Label_0101:
            this.Success();
            goto Label_0117;
        Label_010C:
            this.Failure();
        Label_0117:
            return;
        }

        private void OnClickYes(GameObject dialog)
        {
            this.Success();
            return;
        }

        private void OnGachaSceneLoad(GameObject scene)
        {
            this.mGachaScene = scene.GetComponent<GachaScene>();
            if ((this.mGachaScene != null) == null)
            {
                goto Label_002E;
            }
            SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnGachaSceneLoad));
        Label_002E:
            CriticalSection.Leave(4);
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_GachaResult> response;
            Exception exception;
            string[] strArray;
            int num;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_004B;
            }
            switch ((Network.ErrCode - 0xfa0))
            {
                case 0:
                    goto Label_002F;

                case 1:
                    goto Label_0036;

                case 2:
                    goto Label_003D;
            }
            goto Label_0044;
        Label_002F:
            this.OnFailed();
            return;
        Label_0036:
            this.OnBack();
            return;
        Label_003D:
            this.OnBack();
            return;
        Label_0044:
            this.OnRetry();
            return;
        Label_004B:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaResult>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_006E:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body);
                goto Label_009A;
            }
            catch (Exception exception1)
            {
            Label_0083:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.Failure();
                goto Label_00ED;
            }
        Label_009A:
            strArray = new string[(int) response.body.add.Length];
            num = 0;
            goto Label_00CD;
        Label_00B4:
            strArray[num] = response.body.add[num].iname;
            num += 1;
        Label_00CD:
            if (num < ((int) response.body.add.Length))
            {
                goto Label_00B4;
            }
            this.SetGachaResult(strArray);
            this.Success();
        Label_00ED:
            return;
        }

        private void SetGachaResult(string[] items)
        {
            this.mGachaResult = items;
            this.mGachaResultSet = 1;
            return;
        }

        private void ShowResultDialog(string result)
        {
            UIUtility.SystemMessage("獲得", result, new UIUtility.DialogResultEvent(this.OnClickYes), null, 0, -1);
            return;
        }

        private void StartGachaScene(GachaModes mode)
        {
            CriticalSection.Enter(4);
            this.mGachaMode = mode;
            this.mGachaResultSet = 0;
            base.set_enabled(1);
            base.StartCoroutine(this.StartGachaSceneAsync(mode));
            return;
        }

        [DebuggerHidden]
        private IEnumerator StartGachaSceneAsync(GachaModes mode)
        {
            <StartGachaSceneAsync>c__IteratorB6 rb;
            rb = new <StartGachaSceneAsync>c__IteratorB6();
            rb.<>f__this = this;
            return rb;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(4);
            return;
        }

        [CompilerGenerated]
        private sealed class <StartGachaSceneAsync>c__IteratorB6 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string <sceneName>__0;
            internal bool <itemGacha>__1;
            internal SceneRequest <sceneRequest>__2;
            internal CanvasStack <stack>__3;
            internal int $PC;
            internal object $current;
            internal FlowNode_ExecGacha <>f__this;

            public <StartGachaSceneAsync>c__IteratorB6()
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
                FlowNode_ExecGacha.GachaModes modes;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0039;

                    case 1:
                        goto Label_0087;

                    case 2:
                        goto Label_0184;

                    case 3:
                        goto Label_01BC;

                    case 4:
                        goto Label_01F4;

                    case 5:
                        goto Label_0251;

                    case 6:
                        goto Label_0277;

                    case 7:
                        goto Label_02E9;
                }
                goto Label_030C;
            Label_0039:
                this.<sceneName>__0 = null;
                this.<>f__this.mGachaScene = null;
                FadeController.Instance.FadeTo(Color.get_clear(), 0f, 0);
                GameUtility.FadeOut(1f);
                goto Label_0087;
            Label_0070:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_030E;
            Label_0087:
                if (GameUtility.IsScreenFading != null)
                {
                    goto Label_0070;
                }
                this.<itemGacha>__1 = 0;
                switch (this.<>f__this.mGachaMode)
                {
                    case 0:
                        goto Label_00DC;

                    case 1:
                        goto Label_00BF;

                    case 2:
                        goto Label_010F;

                    case 3:
                        goto Label_00F9;
                }
                goto Label_0125;
            Label_00BF:
                this.<sceneName>__0 = this.<>f__this.GachaScene_NormalX;
                this.<itemGacha>__1 = 1;
                goto Label_0125;
            Label_00DC:
                this.<sceneName>__0 = this.<>f__this.GachaScene_Normal;
                this.<itemGacha>__1 = 1;
                goto Label_0125;
            Label_00F9:
                this.<sceneName>__0 = this.<>f__this.GachaScene_RareX;
                goto Label_0125;
            Label_010F:
                this.<sceneName>__0 = this.<>f__this.GachaScene_Rare;
            Label_0125:
                if (string.IsNullOrEmpty(this.<sceneName>__0) == null)
                {
                    goto Label_013A;
                }
                goto Label_030C;
            Label_013A:
                CriticalSection.Enter(4);
                SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.<>f__this.OnGachaSceneLoad));
                this.<sceneRequest>__2 = AssetManager.LoadSceneAsync(this.<sceneName>__0, 1);
                goto Label_0184;
            Label_016D:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_030E;
            Label_0184:
                if (this.<sceneRequest>__2.canBeActivated == null)
                {
                    goto Label_016D;
                }
                this.<sceneRequest>__2.ActivateScene();
                goto Label_01BC;
            Label_01A5:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_030E;
            Label_01BC:
                if (this.<sceneRequest>__2.isDone == null)
                {
                    goto Label_01A5;
                }
                this.<sceneRequest>__2.ActivateScene();
                goto Label_01F4;
            Label_01DD:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 4;
                goto Label_030E;
            Label_01F4:
                if ((this.<>f__this.mGachaScene == null) != null)
                {
                    goto Label_01DD;
                }
                this.<stack>__3 = this.<>f__this.GetComponentInParent<CanvasStack>();
                CanvasStack.HideCanvases(this.<stack>__3.Priority);
                GameUtility.FadeIn(1f);
                goto Label_0251;
            Label_023A:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 5;
                goto Label_030E;
            Label_0251:
                if (GameUtility.IsScreenFading != null)
                {
                    goto Label_023A;
                }
                goto Label_0277;
            Label_0260:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 6;
                goto Label_030E;
            Label_0277:
                if (this.<>f__this.mGachaResultSet == null)
                {
                    goto Label_0260;
                }
                if (this.<itemGacha>__1 == null)
                {
                    goto Label_02B2;
                }
                this.<>f__this.mGachaScene.DropItems(this.<>f__this.mGachaResult);
                goto Label_02CD;
            Label_02B2:
                this.<>f__this.mGachaScene.DropUnits(this.<>f__this.mGachaResult);
            Label_02CD:
                goto Label_02E9;
            Label_02D2:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 7;
                goto Label_030E;
            Label_02E9:
                if ((this.<>f__this.mGachaScene != null) != null)
                {
                    goto Label_02D2;
                }
                CriticalSection.Leave(4);
                this.$PC = -1;
            Label_030C:
                return 0;
            Label_030E:
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

        private enum GachaModes
        {
            Normal,
            NormalX,
            Rare,
            RareX,
            VIP
        }
    }
}

