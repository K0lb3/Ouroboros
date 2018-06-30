namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(10, "Reset", 0, 5), Pin(20, "Finish", 0, 20), Pin(120, "To Map", 1, 0x15), Pin(0x6f, "Charged Party", 1, 11), Pin(1, "Initialize", 0, 1), Pin(0x65, "Initialize Complete", 1, 2), Pin(110, "Not Charged Party", 1, 10)]
    public class VersusDraftPartyEdit : MonoBehaviour, IFlowInterface
    {
        private const int PARTY_SLOT_COUNT = 5;
        private const int PARTY_ENABLE_COUNT = 3;
        private const int INPUT_PIN_INITIALIZE = 1;
        private const int OUTPUT_PIN_INITIALIZE = 0x65;
        private const int INPUT_PIN_RESET = 10;
        private const int OUTPUT_PIN_NOT_CHARGE = 110;
        private const int OUTPUT_PIN_CHARGED = 0x6f;
        private const int INPUT_PIN_FINISH = 20;
        private const int OUTPUT_PIN_TO_MAP = 120;
        [SerializeField]
        private Transform mPartyTransform;
        [SerializeField]
        private VersusDraftPartySlot mUnitSlotItem;
        [SerializeField]
        private Transform mUnitTransform;
        [SerializeField]
        private VersusDraftPartyUnit mUnitItem;
        [SerializeField]
        private Text mTotalAtk;
        [SerializeField]
        private GameObject mGOLeaderSkill;
        [SerializeField]
        private Text mTimerText;
        private DataSource mLSDataSource;
        private string mDefaultTotalAtkText;
        private float mOrganizeSec;
        private float mTimer;
        private bool mIsFinish;
        private List<VersusDraftPartySlot> mVersusDraftPartySlotList;
        private List<VersusDraftPartyUnit> mVersusDraftPartyUnitList;
        [CompilerGenerated]
        private static Predicate<VersusDraftPartyUnit> <>f__am$cacheE;

        public VersusDraftPartyEdit()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Timeout>m__494(VersusDraftPartyUnit vdpUnit)
        {
            return (vdpUnit.IsSetSlot == 0);
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_001E;
            }
            if (num == 10)
            {
                goto Label_0029;
            }
            if (num == 20)
            {
                goto Label_0034;
            }
            goto Label_003F;
        Label_001E:
            this.Initialize();
            goto Label_003F;
        Label_0029:
            this.AllReset();
            goto Label_003F;
        Label_0034:
            this.ToMap();
        Label_003F:
            return;
        }

        private void AllReset()
        {
            int num;
            num = 0;
            goto Label_006F;
        Label_0007:
            if ((this.mVersusDraftPartySlotList[num].PartyUnit == null) != null)
            {
                goto Label_006B;
            }
            if (this.mVersusDraftPartySlotList[num].PartyUnit.UnitData != null)
            {
                goto Label_0043;
            }
            goto Label_006B;
        Label_0043:
            this.mVersusDraftPartySlotList[num].PartyUnit.Reset();
            this.mVersusDraftPartySlotList[num].SetUnit(null);
        Label_006B:
            num += 1;
        Label_006F:
            if (num < this.mVersusDraftPartySlotList.Count)
            {
                goto Label_0007;
            }
            this.UpdateParty(1);
            return;
        }

        private void Initialize()
        {
            int num;
            VersusDraftPartySlot slot;
            int num2;
            VersusDraftPartyUnit unit;
            this.mVersusDraftPartySlotList = new List<VersusDraftPartySlot>();
            num = 0;
            goto Label_0051;
        Label_0012:
            slot = Object.Instantiate<VersusDraftPartySlot>(this.mUnitSlotItem);
            slot.get_transform().SetParent(this.mPartyTransform, 0);
            slot.SetUp(num == 0, (num < 3) == 0);
            this.mVersusDraftPartySlotList.Add(slot);
            num += 1;
        Label_0051:
            if (num < 5)
            {
                goto Label_0012;
            }
            this.mVersusDraftPartyUnitList = new List<VersusDraftPartyUnit>();
            num2 = 0;
            goto Label_00A9;
        Label_006A:
            unit = Object.Instantiate<VersusDraftPartyUnit>(this.mUnitItem);
            unit.get_transform().SetParent(this.mUnitTransform, 0);
            unit.SetUp(VersusDraftList.VersusDraftUnitDataListPlayer[num2]);
            this.mVersusDraftPartyUnitList.Add(unit);
            num2 += 1;
        Label_00A9:
            if (num2 < VersusDraftList.VersusDraftUnitDataListPlayer.Count)
            {
                goto Label_006A;
            }
            this.UpdateParty(1);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        public void SelectNextSlot()
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            num = -1;
            num2 = -1;
            num3 = 0;
            goto Label_004C;
        Label_000B:
            if (this.mVersusDraftPartySlotList[num3].IsLock == null)
            {
                goto Label_0026;
            }
            goto Label_0048;
        Label_0026:
            if ((this.mVersusDraftPartySlotList[num3] == VersusDraftPartySlot.CurrentSelected) == null)
            {
                goto Label_0048;
            }
            num2 = num3;
            goto Label_005D;
        Label_0048:
            num3 += 1;
        Label_004C:
            if (num3 < this.mVersusDraftPartySlotList.Count)
            {
                goto Label_000B;
            }
        Label_005D:
            num4 = 0;
            goto Label_00D9;
        Label_0064:
            num5 = num2 + 1;
            goto Label_00B5;
        Label_006E:
            if (this.mVersusDraftPartySlotList[num5].IsLock == null)
            {
                goto Label_008A;
            }
            goto Label_00AF;
        Label_008A:
            if ((this.mVersusDraftPartySlotList[num5].PartyUnit == null) == null)
            {
                goto Label_00AF;
            }
            num = num5;
            goto Label_00C7;
        Label_00AF:
            num5 += 1;
        Label_00B5:
            if (num5 < this.mVersusDraftPartySlotList.Count)
            {
                goto Label_006E;
            }
        Label_00C7:
            if (num < 0)
            {
                goto Label_00D3;
            }
            goto Label_00E0;
        Label_00D3:
            num2 = -1;
            num4 += 1;
        Label_00D9:
            if (num4 < 2)
            {
                goto Label_0064;
            }
        Label_00E0:
            if (num < 0)
            {
                goto Label_00F9;
            }
            this.mVersusDraftPartySlotList[num].SelectSlot(1);
        Label_00F9:
            return;
        }

        private void Start()
        {
            this.mUnitSlotItem.get_gameObject().SetActive(0);
            this.mUnitItem.get_gameObject().SetActive(0);
            this.mOrganizeSec = (float) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DraftOrganizeSeconds;
            this.mLSDataSource = DataSource.Create(this.mGOLeaderSkill);
            VersusDraftPartySlot.VersusDraftPartyEdit = this;
            VersusDraftPartyUnit.VersusDraftPartyEdit = this;
            this.mDefaultTotalAtkText = this.mTotalAtk.get_text();
            return;
        }

        private void Timeout()
        {
            int num;
            VersusDraftPartyUnit unit;
            num = 0;
            goto Label_0078;
        Label_0007:
            if ((this.mVersusDraftPartySlotList[num].PartyUnit != null) == null)
            {
                goto Label_0028;
            }
            goto Label_0074;
        Label_0028:
            if (<>f__am$cacheE != null)
            {
                goto Label_0046;
            }
            <>f__am$cacheE = new Predicate<VersusDraftPartyUnit>(VersusDraftPartyEdit.<Timeout>m__494);
        Label_0046:
            unit = this.mVersusDraftPartyUnitList.Find(<>f__am$cacheE);
            if ((unit == null) == null)
            {
                goto Label_0062;
            }
            goto Label_0074;
        Label_0062:
            unit.Select(this.mVersusDraftPartySlotList[num]);
        Label_0074:
            num += 1;
        Label_0078:
            if (num < 3)
            {
                goto Label_0007;
            }
            this.ToMap();
            return;
        }

        private void ToMap()
        {
            int num;
            if (this.mIsFinish == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mIsFinish = 1;
            VersusDraftList.VersusDraftPartyUnits = new List<UnitData>();
            num = 0;
            goto Label_0084;
        Label_0024:
            if ((this.mVersusDraftPartySlotList[num].PartyUnit == null) != null)
            {
                goto Label_0080;
            }
            if (this.mVersusDraftPartySlotList[num].PartyUnit.UnitData != null)
            {
                goto Label_0060;
            }
            goto Label_0080;
        Label_0060:
            VersusDraftList.VersusDraftPartyUnits.Add(this.mVersusDraftPartySlotList[num].PartyUnit.UnitData);
        Label_0080:
            num += 1;
        Label_0084:
            if (num < 3)
            {
                goto Label_0024;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 120);
            return;
        }

        private unsafe void Update()
        {
            int num;
            if (this.mIsFinish == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mTimer += Time.get_unscaledDeltaTime();
            num = (int) (this.mOrganizeSec - this.mTimer);
            this.mTimerText.set_text(&num.ToString());
            if (this.mTimer < this.mOrganizeSec)
            {
                goto Label_0056;
            }
            this.Timeout();
        Label_0056:
            return;
        }

        public unsafe void UpdateParty(bool is_leader)
        {
            int num;
            int num2;
            int num3;
            if (is_leader == null)
            {
                goto Label_0094;
            }
            this.mLSDataSource.Clear();
            if (this.mVersusDraftPartySlotList.Count <= 0)
            {
                goto Label_0089;
            }
            if ((this.mVersusDraftPartySlotList[0].PartyUnit != null) == null)
            {
                goto Label_0089;
            }
            if (this.mVersusDraftPartySlotList[0].PartyUnit.UnitData == null)
            {
                goto Label_0089;
            }
            this.mLSDataSource.Add(typeof(SkillData), this.mVersusDraftPartySlotList[0].PartyUnit.UnitData.LeaderSkill);
        Label_0089:
            GameParameter.UpdateAll(this.mGOLeaderSkill);
        Label_0094:
            num = 0;
            num2 = 0;
            num3 = 0;
            goto Label_013D;
        Label_009F:
            if ((this.mVersusDraftPartySlotList[num3].PartyUnit == null) != null)
            {
                goto Label_0139;
            }
            if (this.mVersusDraftPartySlotList[num3].PartyUnit.UnitData != null)
            {
                goto Label_00DB;
            }
            goto Label_0139;
        Label_00DB:
            num2 += this.mVersusDraftPartySlotList[num3].PartyUnit.UnitData.Status.param.atk;
            num2 += this.mVersusDraftPartySlotList[num3].PartyUnit.UnitData.Status.param.mag;
            num += 1;
        Label_0139:
            num3 += 1;
        Label_013D:
            if (num3 < this.mVersusDraftPartySlotList.Count)
            {
                goto Label_009F;
            }
            if (num2 <= 0)
            {
                goto Label_016C;
            }
            this.mTotalAtk.set_text(&num2.ToString());
            goto Label_017D;
        Label_016C:
            this.mTotalAtk.set_text(this.mDefaultTotalAtkText);
        Label_017D:
            if (num < 3)
            {
                goto Label_0191;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x6f);
            goto Label_0199;
        Label_0191:
            FlowNode_GameObject.ActivateOutputLinks(this, 110);
        Label_0199:
            return;
        }
    }
}

