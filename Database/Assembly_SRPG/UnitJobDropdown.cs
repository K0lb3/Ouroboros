namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Apply Job", 0, 0), Pin(10, "Job Change(TmpUnit)", 1, 10), Pin(11, "Job Change&Close", 1, 11)]
    public class UnitJobDropdown : Pulldown, IFlowInterface
    {
        public static JobChangeEvent OnJobChange;
        public RawImage JobIcon;
        public RawImage ItemJobIcon;
        public bool RefreshOnStart;
        public GameObject GameParamterRoot;
        public ParentObjectEvent UpdateValue;
        private bool mRequestSent;
        private UnitData mTargetUnit;
        private string mOriginalJobID;
        [CompilerGenerated]
        private static JobChangeEvent <>f__am$cache9;

        static UnitJobDropdown()
        {
            if (<>f__am$cache9 != null)
            {
                goto Label_0018;
            }
            <>f__am$cache9 = new JobChangeEvent(UnitJobDropdown.<OnJobChange>m__463);
        Label_0018:
            OnJobChange = <>f__am$cache9;
            return;
        }

        public UnitJobDropdown()
        {
            this.RefreshOnStart = 1;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <OnJobChange>m__463(long unitUniqueID)
        {
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0046;
            }
            if (this.mTargetUnit == null)
            {
                goto Label_003E;
            }
            if ((this.mTargetUnit.CurrentJob.JobID != this.mOriginalJobID) == null)
            {
                goto Label_003E;
            }
            this.RequestJobChange(0);
            goto Label_0046;
        Label_003E:
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
        Label_0046:
            return;
        }

        private void JobChanged(int value)
        {
            if (this.mTargetUnit != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (value == this.mTargetUnit.JobIndex)
            {
                goto Label_004D;
            }
            this.mTargetUnit.SetJobIndex(value);
            if ((this.GameParamterRoot != null) == null)
            {
                goto Label_0045;
            }
            GameParameter.UpdateAll(this.GameParamterRoot);
        Label_0045:
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
        Label_004D:
            return;
        }

        private unsafe void JobChangeResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0039;
            }
            switch ((Network.ErrCode - 0x8fc))
            {
                case 0:
                    goto Label_002D;

                case 1:
                    goto Label_002D;

                case 2:
                    goto Label_002D;
            }
            goto Label_0033;
        Label_002D:
            FlowNode_Network.Failed();
            return;
        Label_0033:
            FlowNode_Network.Retry();
            return;
        Label_0039:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
        Label_0046:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                Network.RemoveAPI();
                goto Label_00A5;
            }
            catch (Exception exception1)
            {
            Label_008F:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_00B2;
            }
        Label_00A5:
            this.mRequestSent = 0;
            this.PostJobChange();
        Label_00B2:
            return;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus != null)
            {
                goto Label_000D;
            }
            this.OnApplicationPause(1);
        Label_000D:
            return;
        }

        private void OnApplicationPause(bool pausing)
        {
            if (pausing == null)
            {
                goto Label_0038;
            }
            if (this.mTargetUnit == null)
            {
                goto Label_0038;
            }
            if ((this.mTargetUnit.CurrentJob.JobID != this.mOriginalJobID) == null)
            {
                goto Label_0038;
            }
            this.RequestJobChange(1);
        Label_0038:
            return;
        }

        private unsafe void PostJobChange()
        {
            GameManager manager;
            PlayerPartyTypes types;
            int num;
            List<PartyEditData> list;
            int num2;
            int num3;
            this.mRequestSent = 0;
            if (this.mTargetUnit == null)
            {
                goto Label_0158;
            }
            MonoSingleton<GameManager>.Instance.Player.OnJobChange(this.mTargetUnit.UnitID);
            this.mOriginalJobID = this.mTargetUnit.CurrentJob.JobID;
            if (this.UpdateValue == null)
            {
                goto Label_005A;
            }
            this.UpdateValue();
        Label_005A:
            if (DataSource.FindDataOfClass<PlayerPartyTypes>(base.get_gameObject(), 11) != 8)
            {
                goto Label_0158;
            }
            num = 0;
            list = PartyUtility.LoadTeamPresets(9, &num, 0);
            if (list == null)
            {
                goto Label_0158;
            }
            if (num < 0)
            {
                goto Label_0158;
            }
            num2 = 0;
            goto Label_0136;
        Label_0091:
            if (list[num2] != null)
            {
                goto Label_00A3;
            }
            goto Label_0130;
        Label_00A3:
            num3 = 0;
            goto Label_011A;
        Label_00AB:
            if (list[num2].Units[num3] != null)
            {
                goto Label_00C5;
            }
            goto Label_0114;
        Label_00C5:
            if ((list[num2].Units[num3].UnitParam.iname == this.mTargetUnit.UnitParam.iname) == null)
            {
                goto Label_0114;
            }
            list[num2].Units[num3] = this.mTargetUnit;
            goto Label_0130;
        Label_0114:
            num3 += 1;
        Label_011A:
            if (num3 < ((int) list[num2].Units.Length))
            {
                goto Label_00AB;
            }
        Label_0130:
            num2 += 1;
        Label_0136:
            if (num2 < list.Count)
            {
                goto Label_0091;
            }
            PartyUtility.SaveTeamPresets(9, num, list, 0);
            GlobalEvent.Invoke("SELECT_PARTY_END", null);
        Label_0158:
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            return;
        }

        public void Refresh()
        {
            int num;
            JobPulldownItem item;
            string str;
            base.ClearItems();
            if (this.mTargetUnit != null)
            {
                goto Label_0023;
            }
            this.mTargetUnit = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
        Label_0023:
            if (this.mTargetUnit == null)
            {
                goto Label_00FC;
            }
            this.mOriginalJobID = this.mTargetUnit.CurrentJob.JobID;
            num = 0;
            goto Label_00E9;
        Label_004B:
            if (this.mTargetUnit.Jobs[num].IsActivated == null)
            {
                goto Label_00E5;
            }
            item = this.AddItem(this.mTargetUnit.Jobs[num].Name, num) as JobPulldownItem;
            if ((item.JobIcon != null) == null)
            {
                goto Label_00C6;
            }
            str = AssetPath.JobIconSmall(this.mTargetUnit.Jobs[num].Param);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00C6;
            }
            item.JobIcon.set_texture(AssetManager.Load<Texture2D>(str));
        Label_00C6:
            if (num != this.mTargetUnit.JobIndex)
            {
                goto Label_00E5;
            }
            base.Selection = base.ItemCount - 1;
        Label_00E5:
            num += 1;
        Label_00E9:
            if (num < ((int) this.mTargetUnit.Jobs.Length))
            {
                goto Label_004B;
            }
        Label_00FC:
            return;
        }

        private void RequestJobChange(bool immediate)
        {
            PlayerPartyTypes types;
            UnitData data;
            ReqUnitJob job;
            if (this.mRequestSent == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            types = DataSource.FindDataOfClass<PlayerPartyTypes>(base.get_gameObject(), 11);
            this.mRequestSent = 1;
            if ((this.mTargetUnit.TempFlags & 1) == null)
            {
                goto Label_0060;
            }
            MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mTargetUnit.UniqueID).SetJobFor(types, this.mTargetUnit.CurrentJob);
        Label_0060:
            OnJobChange(this.mTargetUnit.UniqueID);
            if ((this.mTargetUnit.TempFlags & 1) != null)
            {
                goto Label_00B9;
            }
            job = new ReqUnitJob(this.mTargetUnit.UniqueID, this.mTargetUnit.CurrentJob.UniqueID, new Network.ResponseCallback(this.JobChangeResult));
            goto Label_00EC;
        Label_00B9:
            job = new ReqUnitJob(this.mTargetUnit.UniqueID, this.mTargetUnit.CurrentJob.UniqueID, PartyData.GetStringFromPartyType(types), new Network.ResponseCallback(this.JobChangeResult));
        Label_00EC:
            if (immediate == null)
            {
                goto Label_00FE;
            }
            Network.RequestAPIImmediate(job, 1);
            goto Label_0105;
        Label_00FE:
            Network.RequestAPI(job, 0);
        Label_0105:
            return;
        }

        protected override PulldownItem SetupPulldownItem(GameObject itemObject)
        {
            JobPulldownItem item;
            item = itemObject.AddComponent<JobPulldownItem>();
            item.JobIcon = this.ItemJobIcon;
            return item;
        }

        protected override void Start()
        {
            base.Start();
            base.OnSelectionChange = (UnityAction<int>) Delegate.Combine(base.OnSelectionChange, new UnityAction<int>(this, this.JobChanged));
            if (this.RefreshOnStart == null)
            {
                goto Label_0039;
            }
            this.Refresh();
        Label_0039:
            return;
        }

        protected override void UpdateSelection()
        {
            JobPulldownItem item;
            if ((this.JobIcon != null) == null)
            {
                goto Label_0056;
            }
            item = base.GetItemAt(base.Selection) as JobPulldownItem;
            if ((item != null) == null)
            {
                goto Label_0056;
            }
            if ((item.JobIcon != null) == null)
            {
                goto Label_0056;
            }
            this.JobIcon.set_texture(item.JobIcon.get_texture());
        Label_0056:
            return;
        }

        public delegate void JobChangeEvent(long unitUniqueID);

        public class JobPulldownItem : PulldownItem
        {
            public string JobID;
            public RawImage JobIcon;

            public JobPulldownItem()
            {
                base..ctor();
                return;
            }
        }

        public delegate void ParentObjectEvent();
    }
}

