namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [Pin(0, "Job Change(True)", 0, 0), Pin(1, "Job Change(False)", 0, 1)]
    public class ArtifactSlots : MonoBehaviour, IFlowInterface
    {
        private static readonly string ArtiSelectPath;
        public GenericSlot ArtifactSlot;
        public GenericSlot ArtifactSlot2;
        public GenericSlot ArtifactSlot3;
        public GameObject RootObject;
        private UnitData mCurrentUnit;
        [CompilerGenerated]
        private static Comparison<int> <>f__am$cache6;

        static ArtifactSlots()
        {
            ArtiSelectPath = "UI/ArtifactEquip";
            return;
        }

        public ArtifactSlots()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <Refresh>m__28E(int x, int y)
        {
            return (x - y);
        }

        public void Activated(int pinID)
        {
            if ((pinID != null) && (pinID != 1))
            {
                goto Label_0020;
            }
            this.Refresh((pinID != null) ? 0 : 1);
        Label_0020:
            return;
        }

        private unsafe bool CheckArtifactSlot(int slot, JobData job, UnitData unit)
        {
            int num;
            FixParam param;
            if (job == null)
            {
                goto Label_000C;
            }
            if (unit != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            num = unit.AwakeLv;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            if (param == null)
            {
                goto Label_0044;
            }
            if (param.EquipArtifactSlotUnlock == null)
            {
                goto Label_0044;
            }
            if (((int) param.EquipArtifactSlotUnlock.Length) > 0)
            {
                goto Label_0046;
            }
        Label_0044:
            return 0;
        Label_0046:
            if (((int) param.EquipArtifactSlotUnlock.Length) >= slot)
            {
                goto Label_0056;
            }
            return 0;
        Label_0056:
            return ((*(&(param.EquipArtifactSlotUnlock[slot])) > num) == 0);
        }

        private unsafe void OnArtifactSetResult(WWWResult www)
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
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0068;
            }
            FlowNode_Network.Retry();
            return;
        Label_0068:
            try
            {
                MonoSingleton<GameManager>.GetInstanceDirect().Deserialize(response.body.player);
                MonoSingleton<GameManager>.GetInstanceDirect().Deserialize(response.body.units);
                MonoSingleton<GameManager>.GetInstanceDirect().Deserialize(response.body.artifacts, 0);
                MonoSingleton<GameManager>.GetInstanceDirect().Player.UpdateArtifactOwner();
                goto Label_00D2;
            }
            catch (Exception exception1)
            {
            Label_00BC:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Retry();
                goto Label_00E4;
            }
        Label_00D2:
            Network.RemoveAPI();
            this.UpdateRootObject();
            this.Refresh(1);
        Label_00E4:
            return;
        }

        private void OnClick(GenericSlot slot, bool interactable)
        {
            ArtifactData[] dataArray1;
            GameObject obj2;
            GameObject obj3;
            ArtifactWindow window;
            ArtifactData data;
            long num;
            long num2;
            int num3;
            ArtifactData data2;
            ArtifactTypes types;
            if (interactable != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            obj2 = AssetManager.Load<GameObject>(ArtiSelectPath);
            if ((obj2 == null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            obj3 = Object.Instantiate<GameObject>(obj2);
            if ((obj3 == null) == null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            window = obj3.GetComponent<ArtifactWindow>();
            if ((window == null) == null)
            {
                goto Label_0047;
            }
            return;
        Label_0047:
            if ((slot != null) == null)
            {
                goto Label_00A5;
            }
            if ((slot == this.ArtifactSlot) == null)
            {
                goto Label_0070;
            }
            window.SelectArtifactSlot = 1;
            goto Label_00A5;
        Label_0070:
            if ((slot == this.ArtifactSlot2) == null)
            {
                goto Label_008D;
            }
            window.SelectArtifactSlot = 2;
            goto Label_00A5;
        Label_008D:
            if ((slot == this.ArtifactSlot3) == null)
            {
                goto Label_00A5;
            }
            window.SelectArtifactSlot = 3;
        Label_00A5:
            window.OnEquip = new ArtifactWindow.EquipEvent(this.OnSelect);
            window.SetOwnerUnit(this.mCurrentUnit, this.mCurrentUnit.JobIndex);
            if ((window.ArtifactList != null) == null)
            {
                goto Label_0244;
            }
            if (window.ArtifactList.TestOwner == this.mCurrentUnit)
            {
                goto Label_0106;
            }
            window.ArtifactList.TestOwner = this.mCurrentUnit;
        Label_0106:
            data = DataSource.FindDataOfClass<ArtifactData>(slot.get_gameObject(), null);
            num = 0L;
            if (this.mCurrentUnit.CurrentJob.Artifacts == null)
            {
                goto Label_01C3;
            }
            num2 = (data == null) ? 0L : data.UniqueID;
            num3 = 0;
            goto Label_01AA;
        Label_0153:
            if ((((this.mCurrentUnit.CurrentJob.Artifacts[num3] == 0L) == 0) & (this.mCurrentUnit.CurrentJob.Artifacts[num3] == num2)) == null)
            {
                goto Label_01A4;
            }
            num = this.mCurrentUnit.CurrentJob.Artifacts[num3];
            goto Label_01C3;
        Label_01A4:
            num3 += 1;
        Label_01AA:
            if (num3 < ((int) this.mCurrentUnit.CurrentJob.Artifacts.Length))
            {
                goto Label_0153;
            }
        Label_01C3:
            data2 = null;
            if (num == null)
            {
                goto Label_01E0;
            }
            data2 = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(num);
        Label_01E0:
            if (data2 == null)
            {
                goto Label_0204;
            }
            dataArray1 = new ArtifactData[] { data2 };
            window.ArtifactList.SetSelection(dataArray1, 1, 1);
            goto Label_0217;
        Label_0204:
            window.ArtifactList.SetSelection(new ArtifactData[0], 1, 1);
        Label_0217:
            types = (data == null) ? 0 : data.ArtifactParam.type;
            window.ArtifactList.FiltersPriority = this.SetEquipArtifactFilters(data, types);
        Label_0244:
            return;
        }

        private unsafe void OnSelect(ArtifactData artifact, ArtifactTypes type)
        {
            PlayerData data;
            UnitData data2;
            JobData data3;
            int num;
            JobData data4;
            JobData data5;
            JobData[] dataArray;
            int num2;
            int num3;
            List<ArtifactData> list;
            int num4;
            List<ArtifactData> list2;
            List<ArtifactData> list3;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            <OnSelect>c__AnonStorey2FB storeyfb;
            <OnSelect>c__AnonStorey2FC storeyfc;
            storeyfb = new <OnSelect>c__AnonStorey2FB();
            if (artifact == null)
            {
                goto Label_0116;
            }
            data = MonoSingleton<GameManager>.GetInstanceDirect().Player;
            data2 = null;
            data3 = null;
            if (data.FindOwner(artifact, &data2, &data3) == null)
            {
                goto Label_0116;
            }
            num = 0;
            goto Label_005D;
        Label_0033:
            if (data3.Artifacts[num] != artifact.UniqueID)
            {
                goto Label_0059;
            }
            data3.SetEquipArtifact(num, null);
            goto Label_006B;
        Label_0059:
            num += 1;
        Label_005D:
            if (num < ((int) data3.Artifacts.Length))
            {
                goto Label_0033;
            }
        Label_006B:
            if (this.mCurrentUnit.UniqueID != data2.UniqueID)
            {
                goto Label_0116;
            }
            data4 = null;
            dataArray = this.mCurrentUnit.Jobs;
            num2 = 0;
            goto Label_00BC;
        Label_0099:
            data5 = dataArray[num2];
            if (data5.UniqueID != data3.UniqueID)
            {
                goto Label_00B6;
            }
            data4 = data5;
        Label_00B6:
            num2 += 1;
        Label_00BC:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_0099;
            }
            if (data4 == null)
            {
                goto Label_0116;
            }
            num3 = 0;
            goto Label_0106;
        Label_00D6:
            if (data4.Artifacts[num3] != artifact.UniqueID)
            {
                goto Label_0100;
            }
            data4.SetEquipArtifact(num3, null);
            goto Label_0116;
        Label_0100:
            num3 += 1;
        Label_0106:
            if (num3 < ((int) data4.Artifacts.Length))
            {
                goto Label_00D6;
            }
        Label_0116:
            list = new List<ArtifactData>();
            list.Add(DataSource.FindDataOfClass<ArtifactData>(this.ArtifactSlot.get_gameObject(), null));
            list.Add(DataSource.FindDataOfClass<ArtifactData>(this.ArtifactSlot2.get_gameObject(), null));
            list.Add(DataSource.FindDataOfClass<ArtifactData>(this.ArtifactSlot3.get_gameObject(), null));
            storeyfb.view_artifact_datas = list.ToArray();
            num4 = JobData.GetArtifactSlotIndex(type);
            list2 = new List<ArtifactData>(this.mCurrentUnit.CurrentJob.ArtifactDatas);
            if (list2.Count == ((int) storeyfb.view_artifact_datas.Length))
            {
                goto Label_01A8;
            }
            return;
        Label_01A8:
            storeyfc = new <OnSelect>c__AnonStorey2FC();
            storeyfc.<>f__ref$763 = storeyfb;
            storeyfc.i = 0;
            goto Label_0208;
        Label_01C5:
            if (storeyfb.view_artifact_datas[storeyfc.i] != null)
            {
                goto Label_01DE;
            }
            goto Label_01F8;
        Label_01DE:
            if (list2.Find(new Predicate<ArtifactData>(storeyfc.<>m__28F)) != null)
            {
                goto Label_01F8;
            }
            return;
        Label_01F8:
            storeyfc.i += 1;
        Label_0208:
            if (storeyfc.i < ((int) storeyfb.view_artifact_datas.Length))
            {
                goto Label_01C5;
            }
            storeyfb.view_artifact_datas[num4] = artifact;
            list3 = new List<ArtifactData>();
            num5 = 0;
            goto Label_0251;
        Label_0237:
            this.mCurrentUnit.CurrentJob.SetEquipArtifact(num5, null);
            num5 += 1;
        Label_0251:
            if (num5 < ((int) this.mCurrentUnit.CurrentJob.ArtifactDatas.Length))
            {
                goto Label_0237;
            }
            num6 = 0;
            goto Label_02F4;
        Label_0272:
            if (storeyfb.view_artifact_datas[num6] != null)
            {
                goto Label_0286;
            }
            goto Label_02EE;
        Label_0286:
            if (storeyfb.view_artifact_datas[num6].ArtifactParam.type != 3)
            {
                goto Label_02B6;
            }
            list3.Add(storeyfb.view_artifact_datas[num6]);
            goto Label_02EE;
        Label_02B6:
            num7 = JobData.GetArtifactSlotIndex(storeyfb.view_artifact_datas[num6].ArtifactParam.type);
            this.mCurrentUnit.CurrentJob.SetEquipArtifact(num7, storeyfb.view_artifact_datas[num6]);
        Label_02EE:
            num6 += 1;
        Label_02F4:
            if (num6 < ((int) storeyfb.view_artifact_datas.Length))
            {
                goto Label_0272;
            }
            num8 = 0;
            goto Label_0377;
        Label_030C:
            num9 = 0;
            goto Label_0358;
        Label_0314:
            if (this.mCurrentUnit.CurrentJob.ArtifactDatas[num9] == null)
            {
                goto Label_0331;
            }
            goto Label_0352;
        Label_0331:
            this.mCurrentUnit.CurrentJob.SetEquipArtifact(num9, list3[num8]);
            goto Label_0371;
        Label_0352:
            num9 += 1;
        Label_0358:
            if (num9 < ((int) this.mCurrentUnit.CurrentJob.ArtifactDatas.Length))
            {
                goto Label_0314;
            }
        Label_0371:
            num8 += 1;
        Label_0377:
            if (num8 < list3.Count)
            {
                goto Label_030C;
            }
            this.mCurrentUnit.UpdateArtifact(this.mCurrentUnit.JobIndex, 1, 1);
            Network.RequestAPI(new ReqArtifactSet(this.mCurrentUnit.UniqueID, this.mCurrentUnit.CurrentJob.UniqueID, this.mCurrentUnit.CurrentJob.Artifacts, new Network.ResponseCallback(this.OnArtifactSetResult)), 0);
            return;
        }

        public void Refresh(bool enable)
        {
            UnitData data;
            ArtifactData[] dataArray;
            int num;
            UnitData data2;
            List<ArtifactData> list;
            Dictionary<int, List<ArtifactData>> dictionary;
            int num2;
            int num3;
            List<int> list2;
            int num4;
            data = DataSource.FindDataOfClass<UnitData>(base.get_transform().get_parent().get_gameObject(), null);
            if (data != null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            this.mCurrentUnit = data;
            dataArray = new ArtifactData[3];
            if (enable == null)
            {
                goto Label_006E;
            }
            num = data.JobIndex;
            data2 = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
            if (data2 != null)
            {
                goto Label_005B;
            }
            return;
        Label_005B:
            dataArray = data2.Jobs[num].ArtifactDatas;
            goto Label_007A;
        Label_006E:
            dataArray = data.CurrentJob.ArtifactDatas;
        Label_007A:
            list = new List<ArtifactData>(dataArray);
            dictionary = new Dictionary<int, List<ArtifactData>>();
            num2 = 0;
            goto Label_00F2;
        Label_0091:
            if (list[num2] != null)
            {
                goto Label_00A4;
            }
            goto Label_00EC;
        Label_00A4:
            num3 = list[num2].ArtifactParam.type;
            if (dictionary.ContainsKey(num3) != null)
            {
                goto Label_00D5;
            }
            dictionary[num3] = new List<ArtifactData>();
        Label_00D5:
            dictionary[num3].Add(list[num2]);
        Label_00EC:
            num2 += 1;
        Label_00F2:
            if (num2 < list.Count)
            {
                goto Label_0091;
            }
            list.Clear();
            list2 = new List<int>(dictionary.Keys);
            if (<>f__am$cache6 != null)
            {
                goto Label_012F;
            }
            <>f__am$cache6 = new Comparison<int>(ArtifactSlots.<Refresh>m__28E);
        Label_012F:
            list2.Sort(<>f__am$cache6);
            num4 = 0;
            goto Label_015E;
        Label_0141:
            list.AddRange(dictionary[list2[num4]]);
            num4 += 1;
        Label_015E:
            if (num4 < list2.Count)
            {
                goto Label_0141;
            }
            dataArray = list.ToArray();
            this.RefreshSlots(this.ArtifactSlot, dataArray, 0, data.CurrentJob.Rank, this.CheckArtifactSlot(0, data.CurrentJob, data), enable);
            this.RefreshSlots(this.ArtifactSlot2, dataArray, 1, data.CurrentJob.Rank, this.CheckArtifactSlot(1, data.CurrentJob, data), enable);
            this.RefreshSlots(this.ArtifactSlot3, dataArray, 2, data.CurrentJob.Rank, this.CheckArtifactSlot(2, data.CurrentJob, data), enable);
            return;
        }

        private void RefreshSlots(GenericSlot slot, ArtifactData[] list, int index, int job_rank, bool is_locked, bool enable)
        {
            ArtifactData data;
            SRPG_Button button;
            if ((slot != null) == null)
            {
                goto Label_0055;
            }
            data = null;
            if (job_rank <= 0)
            {
                goto Label_0023;
            }
            if (((int) list.Length) <= index)
            {
                goto Label_0023;
            }
            data = list[index];
        Label_0023:
            slot.SetLocked(is_locked == 0);
            slot.SetSlotData<ArtifactData>(data);
            button = slot.get_gameObject().GetComponentInChildren<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0055;
            }
            button.set_enabled(enable);
        Label_0055:
            return;
        }

        private string[] SetEquipArtifactFilters(ArtifactData data, ArtifactTypes type)
        {
            List<string> list;
            int num;
            UnitData data2;
            JobData data3;
            ArtifactData[] dataArray;
            int num2;
            int num3;
            int num4;
            ArtifactData data4;
            list = new List<string>();
            num = this.mCurrentUnit.JobIndex;
            data2 = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
            if (data2 != null)
            {
                goto Label_003A;
            }
            return list.ToArray();
        Label_003A:
            data3 = data2.Jobs[num];
            dataArray = data3.ArtifactDatas;
            num2 = 0;
            goto Label_0070;
        Label_0053:
            list.Add("RARE:" + ((int) num2));
            num2 += 1;
        Label_0070:
            if (num2 < RarityParam.MAX_RARITY)
            {
                goto Label_0053;
            }
            if (dataArray == null)
            {
                goto Label_00EF;
            }
            num3 = 0;
            goto Label_00E4;
        Label_008B:
            if (dataArray[num3] != null)
            {
                goto Label_009A;
            }
            goto Label_00DE;
        Label_009A:
            if (data == null)
            {
                goto Label_00BF;
            }
            if (dataArray[num3].UniqueID == data.UniqueID)
            {
                goto Label_00DE;
            }
        Label_00BF:
            list.Add("SAME:" + dataArray[num3].ArtifactParam.iname);
        Label_00DE:
            num3 += 1;
        Label_00E4:
            if (num3 < ((int) dataArray.Length))
            {
                goto Label_008B;
            }
        Label_00EF:
            num4 = 0;
            goto Label_016A;
        Label_00F7:
            data4 = data3.ArtifactDatas[num4];
            if (data4 == null)
            {
                goto Label_011B;
            }
            if (data4.ArtifactParam.type != 3)
            {
                goto Label_0139;
            }
        Label_011B:
            list.Add("TYPE:" + ((int) (num4 + 1)));
            goto Label_0164;
        Label_0139:
            if (data4.ArtifactParam.type != type)
            {
                goto Label_0164;
            }
            list.Add("TYPE:" + ((int) (num4 + 1)));
        Label_0164:
            num4 += 1;
        Label_016A:
            if (num4 < ((int) data3.ArtifactDatas.Length))
            {
                goto Label_00F7;
            }
            return list.ToArray();
        }

        private void Start()
        {
            if ((this.ArtifactSlot != null) == null)
            {
                goto Label_0028;
            }
            this.ArtifactSlot.OnSelect = new GenericSlot.SelectEvent(this.OnClick);
        Label_0028:
            if ((this.ArtifactSlot2 != null) == null)
            {
                goto Label_0050;
            }
            this.ArtifactSlot2.OnSelect = new GenericSlot.SelectEvent(this.OnClick);
        Label_0050:
            if ((this.ArtifactSlot3 != null) == null)
            {
                goto Label_0078;
            }
            this.ArtifactSlot3.OnSelect = new GenericSlot.SelectEvent(this.OnClick);
        Label_0078:
            return;
        }

        private void UpdateRootObject()
        {
            if ((this.RootObject == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            GameParameter.UpdateAll(this.RootObject);
            return;
        }

        [CompilerGenerated]
        private sealed class <OnSelect>c__AnonStorey2FB
        {
            internal ArtifactData[] view_artifact_datas;

            public <OnSelect>c__AnonStorey2FB()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <OnSelect>c__AnonStorey2FC
        {
            internal int i;
            internal ArtifactSlots.<OnSelect>c__AnonStorey2FB <>f__ref$763;

            public <OnSelect>c__AnonStorey2FC()
            {
                base..ctor();
                return;
            }

            internal bool <>m__28F(ArtifactData x)
            {
                return ((x == null) ? 0 : (x.UniqueID == this.<>f__ref$763.view_artifact_datas[this.i].UniqueID));
            }
        }
    }
}

