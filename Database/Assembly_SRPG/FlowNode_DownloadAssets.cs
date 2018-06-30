namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [NodeType("System/アセットのダウンロード", 0xff00ff), Pin(0x63, "エラー発生", 1, 0x63), Pin(100, "キャンセル", 1, 12), Pin(11, "ダウンロード完了", 1, 11), Pin(10, "ダウンロード開始", 1, 10), Pin(1, "確認", 0, 1), Pin(0, "ダウンロード", 0, 0)]
    public class FlowNode_DownloadAssets : FlowNode
    {
        public bool UpdateFileList;
        public string[] AssetPaths;
        public string[] DownloadQuests;
        public string[] DownloadUnits;
        [BitMask]
        public DownloadAssets Download;
        public string ConfirmText;
        public string AlreadyDownloadText;
        public string CompleteText;
        public bool ProgressBar;
        public bool SkipIfTutIncomplete;
        public bool AutoRetry;
        private List<AssetList.Item> mQueue;

        public FlowNode_DownloadAssets()
        {
            this.AssetPaths = new string[0];
            this.DownloadQuests = new string[0];
            this.DownloadUnits = new string[0];
            this.AutoRetry = 1;
            base..ctor();
            return;
        }

        private void AddAssets()
        {
            GameManager manager;
            int num;
            PlayerData data;
            int num2;
            MasterParam param;
            UnitParam[] paramArray;
            int num3;
            Json_LoginBonusTable table;
            MasterParam param2;
            string[] strArray;
            int num4;
            UnitParam param3;
            List<ArtifactData> list;
            int num5;
            List<ConceptCardData> list2;
            int num6;
            List<ConceptCardMaterialData> list3;
            int num7;
            int num8;
            int num9;
            int num10;
            UnitParam param4;
            int num11;
            QuestParam param5;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if (this.mQueue == null)
            {
                goto Label_004A;
            }
            num = 0;
            goto Label_0032;
        Label_0018:
            AssetDownloader.Add(this.mQueue[num].IDStr);
            num += 1;
        Label_0032:
            if (num < this.mQueue.Count)
            {
                goto Label_0018;
            }
            this.mQueue = null;
        Label_004A:
            if (this.Download != -1)
            {
                goto Label_0057;
            }
            return;
        Label_0057:
            if (((this.Download & 1) == null) || (manager.HasTutorialDLAssets == null))
            {
                goto Label_0075;
            }
            manager.DownloadTutorialAssets();
        Label_0075:
            if ((this.Download & 2) == null)
            {
                goto Label_0107;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data == null)
            {
                goto Label_0107;
            }
            num2 = 0;
            goto Label_00F6;
        Label_009A:
            DownloadUtility.DownloadUnit(data.Units[num2].UnitParam, data.Units[num2].Jobs);
            if (data.Units[num2].ConceptCard == null)
            {
                goto Label_00F2;
            }
            DownloadUtility.DownloadConceptCard(data.Units[num2].ConceptCard.Param);
        Label_00F2:
            num2 += 1;
        Label_00F6:
            if (num2 < data.Units.Count)
            {
                goto Label_009A;
            }
        Label_0107:
            if ((this.Download & 4) == null)
            {
                goto Label_0161;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            paramArray = (param != null) ? param.GetAllUnits() : null;
            if (paramArray == null)
            {
                goto Label_0161;
            }
            num3 = 0;
            goto Label_0156;
        Label_0145:
            DownloadUtility.DownloadUnit(paramArray[num3], null);
            num3 += 1;
        Label_0156:
            if (num3 < ((int) paramArray.Length))
            {
                goto Label_0145;
            }
        Label_0161:
            if ((this.Download & 0x40) == null)
            {
                goto Label_01E6;
            }
            table = MonoSingleton<GameManager>.Instance.Player.LoginBonus28days;
            if (table == null)
            {
                goto Label_01E6;
            }
            if (table.bonus_units == null)
            {
                goto Label_01E6;
            }
            if (((int) table.bonus_units.Length) <= 0)
            {
                goto Label_01E6;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam;
            strArray = table.bonus_units;
            num4 = 0;
            goto Label_01DB;
        Label_01BF:
            DownloadUtility.DownloadUnit(param2.GetUnitParam(strArray[num4]), null);
            num4 += 1;
        Label_01DB:
            if (num4 < ((int) strArray.Length))
            {
                goto Label_01BF;
            }
        Label_01E6:
            if ((this.Download & 0x20) == null)
            {
                goto Label_0248;
            }
            list = MonoSingleton<GameManager>.Instance.Player.Artifacts;
            if (list == null)
            {
                goto Label_0248;
            }
            if (list.Count <= 0)
            {
                goto Label_0248;
            }
            num5 = 0;
            goto Label_023A;
        Label_0221:
            DownloadUtility.DownloadArtifact(list[num5].ArtifactParam);
            num5 += 1;
        Label_023A:
            if (num5 < list.Count)
            {
                goto Label_0221;
            }
        Label_0248:
            if ((this.Download & 0x80) == null)
            {
                goto Label_0355;
            }
            list2 = MonoSingleton<GameManager>.Instance.Player.ConceptCards;
            if (list2 == null)
            {
                goto Label_02AD;
            }
            if (list2.Count <= 0)
            {
                goto Label_02AD;
            }
            num6 = 0;
            goto Label_029F;
        Label_0286:
            DownloadUtility.DownloadConceptCard(list2[num6].Param);
            num6 += 1;
        Label_029F:
            if (num6 < list2.Count)
            {
                goto Label_0286;
            }
        Label_02AD:
            list3 = MonoSingleton<GameManager>.Instance.Player.ConceptCardExpMaterials;
            if (list3 == null)
            {
                goto Label_0301;
            }
            if (list3.Count <= 0)
            {
                goto Label_0301;
            }
            num7 = 0;
            goto Label_02F3;
        Label_02DA:
            DownloadUtility.DownloadConceptCard(list3[num7].Param);
            num7 += 1;
        Label_02F3:
            if (num7 < list3.Count)
            {
                goto Label_02DA;
            }
        Label_0301:
            list3 = MonoSingleton<GameManager>.Instance.Player.ConceptCardTrustMaterials;
            if (list3 == null)
            {
                goto Label_0355;
            }
            if (list3.Count <= 0)
            {
                goto Label_0355;
            }
            num8 = 0;
            goto Label_0347;
        Label_032E:
            DownloadUtility.DownloadConceptCard(list3[num8].Param);
            num8 += 1;
        Label_0347:
            if (num8 < list3.Count)
            {
                goto Label_032E;
            }
        Label_0355:
            num9 = 0;
            goto Label_0384;
        Label_035D:
            if (string.IsNullOrEmpty(this.AssetPaths[num9]) != null)
            {
                goto Label_037E;
            }
            AssetManager.PrepareAssets(this.AssetPaths[num9]);
        Label_037E:
            num9 += 1;
        Label_0384:
            if (num9 < ((int) this.AssetPaths.Length))
            {
                goto Label_035D;
            }
            if ((manager != null) == null)
            {
                goto Label_0460;
            }
            num10 = 0;
            goto Label_03E0;
        Label_03A7:
            if (string.IsNullOrEmpty(this.DownloadUnits[num10]) != null)
            {
                goto Label_03DA;
            }
            param4 = manager.GetUnitParam(this.DownloadUnits[num10]);
            if (param4 == null)
            {
                goto Label_03DA;
            }
            DownloadUtility.DownloadUnit(param4, null);
        Label_03DA:
            num10 += 1;
        Label_03E0:
            if (num10 < ((int) this.DownloadUnits.Length))
            {
                goto Label_03A7;
            }
            num11 = 0;
            goto Label_0451;
        Label_03F7:
            if (string.IsNullOrEmpty(this.DownloadQuests[num11]) == null)
            {
                goto Label_040F;
            }
            goto Label_044B;
        Label_040F:
            param5 = manager.FindQuest(this.DownloadQuests[num11]);
            if (param5 != null)
            {
                goto Label_0444;
            }
            DebugUtility.LogError("Can't download " + this.DownloadQuests[num11]);
            goto Label_044B;
        Label_0444:
            DownloadUtility.DownloadQuestBase(param5);
        Label_044B:
            num11 += 1;
        Label_0451:
            if (num11 < ((int) this.DownloadQuests.Length))
            {
                goto Label_03F7;
            }
        Label_0460:
            return;
        }

        [DebuggerHidden]
        private IEnumerator AsyncWork(bool confirm)
        {
            <AsyncWork>c__IteratorB2 rb;
            rb = new <AsyncWork>c__IteratorB2();
            rb.confirm = confirm;
            rb.<$>confirm = confirm;
            rb.<>f__this = this;
            return rb;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID == null)
            {
                goto Label_000D;
            }
            if (pinID != 1)
            {
                goto Label_0076;
            }
        Label_000D:
            if (base.get_enabled() == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            if (GameUtility.Config_UseAssetBundles.Value != null)
            {
                goto Label_0032;
            }
            base.ActivateOutputLinks(11);
            return;
        Label_0032:
            if (this.SkipIfTutIncomplete == null)
            {
                goto Label_005E;
            }
            if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) != null)
            {
                goto Label_005E;
            }
            base.ActivateOutputLinks(11);
            return;
        Label_005E:
            base.set_enabled(1);
            base.StartCoroutine(this.AsyncWork(pinID == 1));
        Label_0076:
            return;
        }

        private void OnDownloadCancel(GameObject dialog)
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(100);
            return;
        }

        private void OnDownloadStart(GameObject dialog)
        {
            FlowNode_Variable.Set("IS_EXTERNAL_API_PERMIT", "1");
            base.StartCoroutine(this.AsyncWork(0));
            base.ActivateOutputLinks(10);
            return;
        }

        [CompilerGenerated]
        private sealed class <AsyncWork>c__IteratorB2 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool confirm;
            internal AssetList.Item[] <items>__0;
            internal Stopwatch <sw>__1;
            internal int <i>__2;
            internal long <totalSize>__3;
            internal int <i>__4;
            internal string <sizeText>__5;
            internal int $PC;
            internal object $current;
            internal bool <$>confirm;
            internal FlowNode_DownloadAssets <>f__this;

            public <AsyncWork>c__IteratorB2()
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
                object[] objArray1;
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_002D;

                    case 1:
                        goto Label_014D;

                    case 2:
                        goto Label_034A;

                    case 3:
                        goto Label_03AD;

                    case 4:
                        goto Label_0498;
                }
                goto Label_049F;
            Label_002D:
                if (this.confirm == null)
                {
                    goto Label_0324;
                }
                CriticalSection.Enter(1);
                if (this.<>f__this.mQueue != null)
                {
                    goto Label_0063;
                }
                this.<>f__this.mQueue = new List<AssetList.Item>();
                goto Label_0073;
            Label_0063:
                this.<>f__this.mQueue.Clear();
            Label_0073:
                if (this.<>f__this.Download != -1)
                {
                    goto Label_0179;
                }
                this.<items>__0 = AssetManager.AssetList.Assets;
                this.<sw>__1 = new Stopwatch();
                this.<sw>__1.Start();
                this.<i>__2 = 0;
                goto Label_0166;
            Label_00B6:
                if (AssetManager.IsAssetInCache(this.<items>__0[this.<i>__2].IDStr) != null)
                {
                    goto Label_0111;
                }
                if ((this.<items>__0[this.<i>__2].Flags & 0x80) == null)
                {
                    goto Label_00F4;
                }
                goto Label_0158;
            Label_00F4:
                this.<>f__this.mQueue.Add(this.<items>__0[this.<i>__2]);
            Label_0111:
                this.<sw>__1.Stop();
                if (this.<sw>__1.ElapsedMilliseconds < 10L)
                {
                    goto Label_014D;
                }
                this.<sw>__1.Reset();
                this.$current = null;
                this.$PC = 1;
                goto Label_04A1;
            Label_014D:
                this.<sw>__1.Start();
            Label_0158:
                this.<i>__2 += 1;
            Label_0166:
                if (this.<i>__2 < ((int) this.<items>__0.Length))
                {
                    goto Label_00B6;
                }
            Label_0179:
                this.<totalSize>__3 = 0L;
                this.<i>__4 = 0;
                goto Label_01C4;
            Label_018D:
                this.<totalSize>__3 += (long) this.<>f__this.mQueue[this.<i>__4].Size;
                this.<i>__4 += 1;
            Label_01C4:
                if (this.<i>__4 < this.<>f__this.mQueue.Count)
                {
                    goto Label_018D;
                }
                CriticalSection.Leave(1);
                if (this.<totalSize>__3 <= 0L)
                {
                    goto Label_02D5;
                }
                if (this.<totalSize>__3 >= 0x400L)
                {
                    goto Label_0223;
                }
                this.<sizeText>__5 = ((long) this.<totalSize>__3) + "B";
                goto Label_0284;
            Label_0223:
                if (this.<totalSize>__3 >= 0x100000L)
                {
                    goto Label_025B;
                }
                this.<sizeText>__5 = ((long) (this.<totalSize>__3 / 0x400L)) + "KB";
                goto Label_0284;
            Label_025B:
                this.<sizeText>__5 = ((long) ((this.<totalSize>__3 / 0x400L) / 0x400L)) + "MB";
            Label_0284:
                objArray1 = new object[] { this.<sizeText>__5 };
                UIUtility.ConfirmBox(LocalizedText.Get(this.<>f__this.ConfirmText, objArray1), new UIUtility.DialogResultEvent(this.<>f__this.OnDownloadStart), new UIUtility.DialogResultEvent(this.<>f__this.OnDownloadCancel), null, 0, -1, null, null);
                goto Label_049F;
            Label_02D5:
                if (string.IsNullOrEmpty(this.<>f__this.AlreadyDownloadText) != null)
                {
                    goto Label_0305;
                }
                UIUtility.SystemMessage(null, LocalizedText.Get(this.<>f__this.AlreadyDownloadText), null, null, 0, -1);
            Label_0305:
                this.<>f__this.set_enabled(0);
                this.<>f__this.ActivateOutputLinks(11);
                goto Label_049F;
            Label_0324:
                if (AssetDownloader.isDone != null)
                {
                    goto Label_0354;
                }
                goto Label_034A;
            Label_0333:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_04A1;
            Label_034A:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0333;
                }
            Label_0354:
                this.<>f__this.AddAssets();
                if (AssetDownloader.isDone != null)
                {
                    goto Label_037E;
                }
                if (this.<>f__this.ProgressBar == null)
                {
                    goto Label_037E;
                }
                ProgressWindow.OpenGenericDownloadWindow();
            Label_037E:
                AssetDownloader.StartDownload(this.<>f__this.UpdateFileList, 1, 2);
                goto Label_03CC;
            Label_0396:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_04A1;
            Label_03AD:
                if (AssetDownloader.HasError == null)
                {
                    goto Label_03CC;
                }
                if (this.<>f__this.AutoRetry != null)
                {
                    goto Label_03CC;
                }
                goto Label_03D6;
            Label_03CC:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0396;
                }
            Label_03D6:
                if (this.<>f__this.ProgressBar == null)
                {
                    goto Label_03EB;
                }
                ProgressWindow.Close();
            Label_03EB:
                this.<>f__this.set_enabled(0);
                if (AssetDownloader.HasError == null)
                {
                    goto Label_0438;
                }
                if (this.<>f__this.AutoRetry != null)
                {
                    goto Label_0438;
                }
                AssetDownloader.Reset();
                FlowNode_Variable.Set("IS_EXTERNAL_API_PERMIT", string.Empty);
                this.<>f__this.ActivateOutputLinks(0x63);
                goto Label_0485;
            Label_0438:
                if (string.IsNullOrEmpty(this.<>f__this.CompleteText) != null)
                {
                    goto Label_0468;
                }
                UIUtility.SystemMessage(null, LocalizedText.Get(this.<>f__this.CompleteText), null, null, 0, -1);
            Label_0468:
                FlowNode_Variable.Set("IS_EXTERNAL_API_PERMIT", string.Empty);
                this.<>f__this.ActivateOutputLinks(11);
            Label_0485:
                this.$current = null;
                this.$PC = 4;
                goto Label_04A1;
            Label_0498:
                this.$PC = -1;
            Label_049F:
                return 0;
            Label_04A1:
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

        [Flags]
        public enum DownloadAssets
        {
            Tutorial = 1,
            OwnUnits = 2,
            AllUnits = 4,
            ItemIcons = 8,
            Multiplay = 0x10,
            Artifacts = 0x20,
            LoginBonus = 0x40,
            OwnConceptCard = 0x80
        }
    }
}

