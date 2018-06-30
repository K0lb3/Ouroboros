namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class BattleUnitDetail : MonoBehaviour
    {
        private const int TAG_BOUNDARY_LEN = 2;
        private const int TAG_ENTRY_GRID_BASE = 1;
        private const int TAG_ENTRY_GRID_WIDE = 2;
        private const int TAG_ENTRY_GRID_MAX = 8;
        [Space(10f)]
        public GameObject GoLeaderSkill;
        public GameObject GoLeaderSkillBadge;
        [Space(5f)]
        public GameObject GoLeader2Skill;
        [Space(5f)]
        public GameObject GoFriendSkill;
        [Space(10f)]
        public GameObject GoStatusParent;
        public BattleUnitDetailStatus StatusBaseItem;
        [Space(10f)]
        public GameObject GoElementParent;
        public BattleUnitDetailElement ElementBaseItem;
        [Space(10f)]
        public GameObject GoTagParent;
        public BattleUnitDetailTag TagBaseItem;
        public BattleUnitDetailTag TagBaseWideItem;
        [Space(10f)]
        public GameObject GoAtkDetailParent;
        public BattleUnitDetailAtkDetail AtkDetailBaseItem;
        [Space(10f)]
        public GameObject GoCondParent;
        public BattleUnitDetailCond CondBaseItem;
        private SceneBattle mSb;
        private BattleCore mBc;
        private TargetPlate mTargetSub;
        private TowerFloorParam mTF_Param;
        private static BattleUnitDetail mInstance;
        private static int[][] mFluctValues;
        [CompilerGenerated]
        private static Predicate<Unit> <>f__am$cache15;
        [CompilerGenerated]
        private static Predicate<Unit> <>f__am$cache16;

        static BattleUnitDetail()
        {
            int[] numArray3;
            int[] numArray2;
            int[] numArray1;
            int[][] numArrayArray1;
            mInstance = null;
            numArrayArray1 = new int[3][];
            numArray1 = new int[] { 1, 20, 50 };
            numArrayArray1[0] = numArray1;
            numArray2 = new int[] { 5, 20, 0x2d };
            numArrayArray1[1] = numArray2;
            numArray3 = new int[] { 1, 20, 50 };
            numArrayArray1[2] = numArray3;
            mFluctValues = numArrayArray1;
            return;
        }

        public BattleUnitDetail()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Refresh>m__CE(Unit data)
        {
            return (data.OwnerPlayerIndex == 1);
        }

        [CompilerGenerated]
        private static bool <Refresh>m__CF(Unit data)
        {
            return (data.OwnerPlayerIndex == 2);
        }

        public static void DestroyChildGameObjects(GameObject go_parent, List<GameObject> go_ignore_lists)
        {
            int num;
            Transform transform;
            if (go_parent != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = go_parent.get_transform().get_childCount() - 1;
            goto Label_0067;
        Label_001F:
            transform = go_parent.get_transform().GetChild(num);
            if (transform != null)
            {
                goto Label_003C;
            }
            goto Label_0063;
        Label_003C:
            if (go_ignore_lists == null)
            {
                goto Label_0058;
            }
            if (go_ignore_lists.Contains(transform.get_gameObject()) == null)
            {
                goto Label_0058;
            }
            goto Label_0063;
        Label_0058:
            GameUtility.DestroyGameObject(transform.get_gameObject());
        Label_0063:
            num -= 1;
        Label_0067:
            if (num >= 0)
            {
                goto Label_001F;
            }
            return;
        }

        public static void DestroyChildGameObjects<T>(GameObject go_parent, List<GameObject> go_ignore_lists)
        {
            int num;
            Transform transform;
            if (go_parent != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = go_parent.get_transform().get_childCount() - 1;
            goto Label_007C;
        Label_001F:
            transform = go_parent.get_transform().GetChild(num);
            if (transform != null)
            {
                goto Label_003C;
            }
            goto Label_0078;
        Label_003C:
            if (((T) transform.GetComponent<T>()) != null)
            {
                goto Label_0051;
            }
            goto Label_0078;
        Label_0051:
            if (go_ignore_lists == null)
            {
                goto Label_006D;
            }
            if (go_ignore_lists.Contains(transform.get_gameObject()) == null)
            {
                goto Label_006D;
            }
            goto Label_0078;
        Label_006D:
            GameUtility.DestroyGameObject(transform.get_gameObject());
        Label_0078:
            num -= 1;
        Label_007C:
            if (num >= 0)
            {
                goto Label_001F;
            }
            return;
        }

        public static eBudFluct ExchgBudFluct(int per, eFluctChk fluct_chk)
        {
            int[] numArray;
            numArray = mFluctValues[fluct_chk];
            if (per <= 0)
            {
                goto Label_0035;
            }
            if (per <= numArray[2])
            {
                goto Label_001A;
            }
            return 6;
        Label_001A:
            if (per <= numArray[1])
            {
                goto Label_0025;
            }
            return 5;
        Label_0025:
            if (per <= numArray[0])
            {
                goto Label_0060;
            }
            return 4;
            goto Label_0060;
        Label_0035:
            if (per >= 0)
            {
                goto Label_0060;
            }
            if (per >= -numArray[2])
            {
                goto Label_0048;
            }
            return 1;
        Label_0048:
            if (per >= -numArray[1])
            {
                goto Label_0054;
            }
            return 2;
        Label_0054:
            if (per >= -numArray[0])
            {
                goto Label_0060;
            }
            return 3;
        Label_0060:
            return 0;
        }

        public static eBudFluct ExchgBudFluct(int val, int max, eFluctChk fluct_chk)
        {
            int num;
            if (max == null)
            {
                goto Label_0015;
            }
            num = (val * 100) / max;
            return ExchgBudFluct(num, fluct_chk);
        Label_0015:
            return 0;
        }

        private void OnDisable()
        {
            if ((mInstance == this) == null)
            {
                goto Label_0016;
            }
            mInstance = null;
        Label_0016:
            return;
        }

        private void OnEnable()
        {
            if ((mInstance == null) == null)
            {
                goto Label_0016;
            }
            mInstance = this;
        Label_0016:
            return;
        }

        public unsafe void Refresh(Unit unit)
        {
            GameObject[] objArray5;
            int[] numArray1;
            GameObject[] objArray4;
            GameObject[] objArray3;
            GameObject[] objArray2;
            GameObject[] objArray1;
            DataSource source;
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            SkillData data;
            SkillData data2;
            SkillData data3;
            List<Unit> list;
            int num;
            int num2;
            int num3;
            BattleUnitDetailStatus status4;
            int num4;
            int num5;
            int num6;
            int num7;
            BattleUnitDetailElement element;
            int num8;
            eBudFluct fluct;
            int num9;
            string[] strArray;
            string str;
            string[] strArray2;
            int num10;
            BattleUnitDetailTag tag;
            int[] numArray;
            int num11;
            eBudFluct fluct2;
            BattleUnitDetailAtkDetail detail;
            int num12;
            int num13;
            BattleUnitDetailAtkDetail.eType type;
            int num14;
            int num15;
            eBudFluct fluct3;
            BattleUnitDetailAtkDetail detail2;
            Unit.UnitShield shield;
            List<Unit.UnitShield>.Enumerator enumerator;
            BattleUnitDetailCond cond;
            EUnitCondition[] conditionArray;
            int num16;
            BattleUnitDetailCond cond2;
            BattleUnitDetailStatus.eBudStat stat;
            EElement element2;
            AttackDetailTypes types;
            BattleUnitDetailAtkDetail.eType type2;
            if (this.mBc == null)
            {
                goto Label_0011;
            }
            if (unit != null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            source = base.get_gameObject().GetComponent<DataSource>();
            if (source == null)
            {
                goto Label_002F;
            }
            source.Clear();
        Label_002F:
            if (this.GoLeaderSkill == null)
            {
                goto Label_005C;
            }
            source = this.GoLeaderSkill.GetComponent<DataSource>();
            if (source == null)
            {
                goto Label_005C;
            }
            source.Clear();
        Label_005C:
            if (this.GoLeader2Skill == null)
            {
                goto Label_0089;
            }
            source = this.GoLeader2Skill.GetComponent<DataSource>();
            if (source == null)
            {
                goto Label_0089;
            }
            source.Clear();
        Label_0089:
            if (this.GoFriendSkill == null)
            {
                goto Label_00B6;
            }
            source = this.GoFriendSkill.GetComponent<DataSource>();
            if (source == null)
            {
                goto Label_00B6;
            }
            source.Clear();
        Label_00B6:
            DataSource.Bind<Unit>(base.get_gameObject(), unit);
            status = unit.UnitData.Status;
            status2 = unit.CurrentStatus;
            status3 = unit.MaximumStatus;
            data = null;
            data2 = null;
            data3 = null;
            if (this.mBc.IsMultiTower != null)
            {
                goto Label_0197;
            }
            if (unit.Side != null)
            {
                goto Label_0154;
            }
            if (this.mBc.Leader == null)
            {
                goto Label_0122;
            }
            data = this.mBc.Leader.LeaderSkill;
        Label_0122:
            if (this.mBc.Friend == null)
            {
                goto Label_0154;
            }
            if (this.mBc.IsFriendStatus == null)
            {
                goto Label_0154;
            }
            data3 = this.mBc.Friend.LeaderSkill;
        Label_0154:
            if (this.mBc.IsMultiVersus == null)
            {
                goto Label_0250;
            }
            if (unit.Side != 1)
            {
                goto Label_0250;
            }
            if (this.mBc.EnemyLeader == null)
            {
                goto Label_0250;
            }
            data = this.mBc.EnemyLeader.LeaderSkill;
            goto Label_0250;
        Label_0197:
            list = new List<Unit>();
            if (unit.Side != null)
            {
                goto Label_01BB;
            }
            list = this.mBc.Player;
            goto Label_01D4;
        Label_01BB:
            if (unit.Side != 1)
            {
                goto Label_01D4;
            }
            list = this.mBc.Enemys;
        Label_01D4:
            if (<>f__am$cache15 != null)
            {
                goto Label_01EE;
            }
            <>f__am$cache15 = new Predicate<Unit>(BattleUnitDetail.<Refresh>m__CE);
        Label_01EE:
            num = list.FindIndex(<>f__am$cache15);
            if (num < 0)
            {
                goto Label_0212;
            }
            data = list[num].LeaderSkill;
        Label_0212:
            if (<>f__am$cache16 != null)
            {
                goto Label_022C;
            }
            <>f__am$cache16 = new Predicate<Unit>(BattleUnitDetail.<Refresh>m__CF);
        Label_022C:
            num2 = list.FindIndex(<>f__am$cache16);
            if (num2 < 0)
            {
                goto Label_0250;
            }
            data2 = list[num2].LeaderSkill;
        Label_0250:
            if (this.GoLeaderSkill == null)
            {
                goto Label_0274;
            }
            if (data == null)
            {
                goto Label_0274;
            }
            DataSource.Bind<SkillData>(this.GoLeaderSkill, data);
        Label_0274:
            if (this.GoLeader2Skill == null)
            {
                goto Label_0298;
            }
            if (data2 == null)
            {
                goto Label_0298;
            }
            DataSource.Bind<SkillData>(this.GoLeader2Skill, data2);
        Label_0298:
            if (this.GoFriendSkill == null)
            {
                goto Label_02BC;
            }
            if (data3 == null)
            {
                goto Label_02BC;
            }
            DataSource.Bind<SkillData>(this.GoFriendSkill, data3);
        Label_02BC:
            if (this.GoStatusParent == null)
            {
                goto Label_069A;
            }
            if (this.StatusBaseItem == null)
            {
                goto Label_069A;
            }
            this.StatusBaseItem.get_gameObject().SetActive(0);
            objArray1 = new GameObject[] { this.StatusBaseItem.get_gameObject() };
            DestroyChildGameObjects(this.GoStatusParent, new List<GameObject>(objArray1));
            num3 = 0;
            goto Label_0691;
        Label_0319:
            status4 = Object.Instantiate<BattleUnitDetailStatus>(this.StatusBaseItem);
            if (status4 != null)
            {
                goto Label_0337;
            }
            goto Label_068B;
        Label_0337:
            status4.get_transform().SetParent(this.GoStatusParent.get_transform());
            status4.get_transform().set_localScale(Vector3.get_one());
            num4 = 0;
            num5 = 0;
            stat = num3;
            switch (stat)
            {
                case 0:
                    goto Label_03A9;

                case 1:
                    goto Label_03E3;

                case 2:
                    goto Label_041D;

                case 3:
                    goto Label_0457;

                case 4:
                    goto Label_0491;

                case 5:
                    goto Label_04CB;

                case 6:
                    goto Label_0505;

                case 7:
                    goto Label_053F;

                case 8:
                    goto Label_0579;

                case 9:
                    goto Label_05B3;

                case 10:
                    goto Label_05ED;

                case 11:
                    goto Label_05FD;

                case 12:
                    goto Label_0637;
            }
            goto Label_0671;
        Label_03A9:
            num4 = status3.param.hp;
            num5 = status3.param.hp - status.param.hp;
            goto Label_0671;
        Label_03E3:
            num4 = status3.param.mp;
            num5 = status3.param.mp - status.param.mp;
            goto Label_0671;
        Label_041D:
            num4 = status2.param.atk;
            num5 = status2.param.atk - status.param.atk;
            goto Label_0671;
        Label_0457:
            num4 = status2.param.def;
            num5 = status2.param.def - status.param.def;
            goto Label_0671;
        Label_0491:
            num4 = status2.param.mag;
            num5 = status2.param.mag - status.param.mag;
            goto Label_0671;
        Label_04CB:
            num4 = status2.param.mnd;
            num5 = status2.param.mnd - status.param.mnd;
            goto Label_0671;
        Label_0505:
            num4 = status2.param.dex;
            num5 = status2.param.dex - status.param.dex;
            goto Label_0671;
        Label_053F:
            num4 = status2.param.spd;
            num5 = status2.param.spd - status.param.spd;
            goto Label_0671;
        Label_0579:
            num4 = status2.param.cri;
            num5 = status2.param.cri - status.param.cri;
            goto Label_0671;
        Label_05B3:
            num4 = status2.param.luk;
            num5 = status2.param.luk - status.param.luk;
            goto Label_0671;
        Label_05ED:
            num4 = unit.GetCombination();
            num5 = 0;
            goto Label_0671;
        Label_05FD:
            num4 = status2.param.mov;
            num5 = status2.param.mov - status.param.mov;
            goto Label_0671;
        Label_0637:
            num4 = status2.param.jmp;
            num5 = status2.param.jmp - status.param.jmp;
        Label_0671:
            status4.SetStatus(num3, num4, num5);
            status4.get_gameObject().SetActive(1);
        Label_068B:
            num3 += 1;
        Label_0691:
            if (num3 < 13)
            {
                goto Label_0319;
            }
        Label_069A:
            if (this.GoElementParent == null)
            {
                goto Label_0838;
            }
            if (this.ElementBaseItem == null)
            {
                goto Label_0838;
            }
            this.ElementBaseItem.get_gameObject().SetActive(0);
            objArray2 = new GameObject[] { this.ElementBaseItem.get_gameObject() };
            DestroyChildGameObjects(this.GoElementParent, new List<GameObject>(objArray2));
            num6 = (int) Enum.GetNames(typeof(EElement)).Length;
            num7 = 1;
            goto Label_082F;
        Label_070A:
            element = Object.Instantiate<BattleUnitDetailElement>(this.ElementBaseItem);
            if (element != null)
            {
                goto Label_0728;
            }
            goto Label_0829;
        Label_0728:
            element.get_transform().SetParent(this.GoElementParent.get_transform());
            element.get_transform().set_localScale(Vector3.get_one());
            num8 = 0;
            element2 = num7;
            switch ((element2 - 1))
            {
                case 0:
                    goto Label_077D;

                case 1:
                    goto Label_0794;

                case 2:
                    goto Label_07AB;

                case 3:
                    goto Label_07C2;

                case 4:
                    goto Label_07D9;

                case 5:
                    goto Label_07F0;
            }
            goto Label_0807;
        Label_077D:
            num8 = status2.element_resist.fire;
            goto Label_0807;
        Label_0794:
            num8 = status2.element_resist.water;
            goto Label_0807;
        Label_07AB:
            num8 = status2.element_resist.wind;
            goto Label_0807;
        Label_07C2:
            num8 = status2.element_resist.thunder;
            goto Label_0807;
        Label_07D9:
            num8 = status2.element_resist.shine;
            goto Label_0807;
        Label_07F0:
            num8 = status2.element_resist.dark;
        Label_0807:
            fluct = ExchgBudFluct(num8, 1);
            element.SetElement(num7, fluct);
            element.get_gameObject().SetActive(1);
        Label_0829:
            num7 += 1;
        Label_082F:
            if (num7 < num6)
            {
                goto Label_070A;
            }
        Label_0838:
            if (this.GoTagParent == null)
            {
                goto Label_099A;
            }
            if (this.TagBaseItem == null)
            {
                goto Label_099A;
            }
            if (this.TagBaseWideItem == null)
            {
                goto Label_099A;
            }
            this.TagBaseItem.get_gameObject().SetActive(0);
            this.TagBaseWideItem.get_gameObject().SetActive(0);
            objArray3 = new GameObject[] { this.TagBaseItem.get_gameObject(), this.TagBaseWideItem.get_gameObject() };
            DestroyChildGameObjects(this.GoTagParent, new List<GameObject>(objArray3));
            num9 = 0;
            strArray = unit.GetTags();
            if (strArray == null)
            {
                goto Label_099A;
            }
            strArray2 = strArray;
            num10 = 0;
            goto Label_098F;
        Label_08DA:
            str = strArray2[num10];
            tag = null;
            if (str.Length > 2)
            {
                goto Label_0918;
            }
            if ((num9 + 1) <= 8)
            {
                goto Label_0900;
            }
            goto Label_099A;
        Label_0900:
            tag = Object.Instantiate<BattleUnitDetailTag>(this.TagBaseItem);
            num9 += 1;
            goto Label_093A;
        Label_0918:
            if ((num9 + 2) <= 8)
            {
                goto Label_0927;
            }
            goto Label_099A;
        Label_0927:
            tag = Object.Instantiate<BattleUnitDetailTag>(this.TagBaseWideItem);
            num9 += 2;
        Label_093A:
            if (tag != null)
            {
                goto Label_094B;
            }
            goto Label_0989;
        Label_094B:
            tag.get_transform().SetParent(this.GoTagParent.get_transform());
            tag.get_transform().set_localScale(Vector3.get_one());
            tag.SetTag(str);
            tag.get_gameObject().SetActive(1);
        Label_0989:
            num10 += 1;
        Label_098F:
            if (num10 < ((int) strArray2.Length))
            {
                goto Label_08DA;
            }
        Label_099A:
            if (this.GoAtkDetailParent == null)
            {
                goto Label_0DCC;
            }
            if (this.AtkDetailBaseItem == null)
            {
                goto Label_0DCC;
            }
            this.AtkDetailBaseItem.get_gameObject().SetActive(0);
            objArray4 = new GameObject[] { this.AtkDetailBaseItem.get_gameObject() };
            DestroyChildGameObjects<BattleUnitDetailAtkDetail>(this.GoAtkDetailParent, new List<GameObject>(objArray4));
            numArray1 = new int[] { status2[3], status2[4], status2[9] - 100 };
            numArray = numArray1;
            num11 = 0;
            goto Label_0AAF;
        Label_0A30:
            fluct2 = ExchgBudFluct(numArray[num11], 0);
            if (fluct2 != null)
            {
                goto Label_0A49;
            }
            goto Label_0AA9;
        Label_0A49:
            detail = Object.Instantiate<BattleUnitDetailAtkDetail>(this.AtkDetailBaseItem);
            if (detail != null)
            {
                goto Label_0A67;
            }
            goto Label_0AA9;
        Label_0A67:
            detail.get_transform().SetParent(this.GoAtkDetailParent.get_transform());
            detail.get_transform().set_localScale(Vector3.get_one());
            detail.SetAll(7 + num11, fluct2);
            detail.get_gameObject().SetActive(1);
        Label_0AA9:
            num11 += 1;
        Label_0AAF:
            if (num11 < ((int) numArray.Length))
            {
                goto Label_0A30;
            }
            num12 = (int) Enum.GetNames(typeof(AttackDetailTypes)).Length;
            num13 = 0;
            goto Label_0DC4;
        Label_0AD5:
            type = num13;
            num14 = 1;
            goto Label_0DB5;
        Label_0AE1:
            num15 = 0;
            types = num14;
            switch ((types - 1))
            {
                case 0:
                    goto Label_0B0E;

                case 1:
                    goto Label_0B6A;

                case 2:
                    goto Label_0BC6;

                case 3:
                    goto Label_0C22;

                case 4:
                    goto Label_0C7F;

                case 5:
                    goto Label_0CDC;
            }
            goto Label_0D39;
        Label_0B0E:
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_0B2A;

                case 1:
                    goto Label_0B3D;

                case 2:
                    goto Label_0B51;
            }
            goto Label_0B65;
        Label_0B2A:
            num15 = status2[6];
            goto Label_0B65;
        Label_0B3D:
            num15 = status2[0x1b];
            goto Label_0B65;
        Label_0B51:
            num15 = status2[0x22];
        Label_0B65:
            goto Label_0D39;
        Label_0B6A:
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_0B86;

                case 1:
                    goto Label_0B99;

                case 2:
                    goto Label_0BAD;
            }
            goto Label_0BC1;
        Label_0B86:
            num15 = status2[7];
            goto Label_0BC1;
        Label_0B99:
            num15 = status2[0x1c];
            goto Label_0BC1;
        Label_0BAD:
            num15 = status2[0x23];
        Label_0BC1:
            goto Label_0D39;
        Label_0BC6:
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_0BE2;

                case 1:
                    goto Label_0BF5;

                case 2:
                    goto Label_0C09;
            }
            goto Label_0C1D;
        Label_0BE2:
            num15 = status2[8];
            goto Label_0C1D;
        Label_0BF5:
            num15 = status2[0x1d];
            goto Label_0C1D;
        Label_0C09:
            num15 = status2[0x24];
        Label_0C1D:
            goto Label_0D39;
        Label_0C22:
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_0C3E;

                case 1:
                    goto Label_0C52;

                case 2:
                    goto Label_0C66;
            }
            goto Label_0C7A;
        Label_0C3E:
            num15 = status2[9];
            goto Label_0C7A;
        Label_0C52:
            num15 = status2[30];
            goto Label_0C7A;
        Label_0C66:
            num15 = status2[0x25];
        Label_0C7A:
            goto Label_0D39;
        Label_0C7F:
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_0C9B;

                case 1:
                    goto Label_0CAF;

                case 2:
                    goto Label_0CC3;
            }
            goto Label_0CD7;
        Label_0C9B:
            num15 = status2[10];
            goto Label_0CD7;
        Label_0CAF:
            num15 = status2[0x1f];
            goto Label_0CD7;
        Label_0CC3:
            num15 = status2[0x26];
        Label_0CD7:
            goto Label_0D39;
        Label_0CDC:
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_0CF8;

                case 1:
                    goto Label_0D0C;

                case 2:
                    goto Label_0D20;
            }
            goto Label_0D34;
        Label_0CF8:
            num15 = status2[12];
            goto Label_0D34;
        Label_0D0C:
            num15 = status2[0x21];
            goto Label_0D34;
        Label_0D20:
            num15 = status2[40];
        Label_0D34:;
        Label_0D39:
            fluct3 = ExchgBudFluct(num15, 2);
            if (fluct3 != null)
            {
                goto Label_0D4F;
            }
            goto Label_0DAF;
        Label_0D4F:
            detail2 = Object.Instantiate<BattleUnitDetailAtkDetail>(this.AtkDetailBaseItem);
            if (detail2 != null)
            {
                goto Label_0D6D;
            }
            goto Label_0DAF;
        Label_0D6D:
            detail2.get_transform().SetParent(this.GoAtkDetailParent.get_transform());
            detail2.get_transform().set_localScale(Vector3.get_one());
            detail2.SetAtkDetail(num14, type, fluct3);
            detail2.get_gameObject().SetActive(1);
        Label_0DAF:
            num14 += 1;
        Label_0DB5:
            if (num14 < num12)
            {
                goto Label_0AE1;
            }
            num13 += 1;
        Label_0DC4:
            if (num13 < 3)
            {
                goto Label_0AD5;
            }
        Label_0DCC:
            if (this.GoCondParent == null)
            {
                goto Label_0F65;
            }
            if (this.CondBaseItem == null)
            {
                goto Label_0F65;
            }
            this.CondBaseItem.get_gameObject().SetActive(0);
            objArray5 = new GameObject[] { this.CondBaseItem.get_gameObject() };
            DestroyChildGameObjects<BattleUnitDetailCond>(this.GoCondParent, new List<GameObject>(objArray5));
            enumerator = unit.Shields.GetEnumerator();
        Label_0E2E:
            try
            {
                goto Label_0EA9;
            Label_0E33:
                shield = &enumerator.Current;
                cond = Object.Instantiate<BattleUnitDetailCond>(this.CondBaseItem);
                if (cond != null)
                {
                    goto Label_0E5A;
                }
                goto Label_0EA9;
            Label_0E5A:
                cond.get_transform().SetParent(this.GoCondParent.get_transform());
                cond.get_transform().set_localScale(Vector3.get_one());
                cond.SetCondShield(shield.shieldType, shield.hp);
                cond.get_gameObject().SetActive(1);
            Label_0EA9:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0E33;
                }
                goto Label_0EC7;
            }
            finally
            {
            Label_0EBA:
                ((List<Unit.UnitShield>.Enumerator) enumerator).Dispose();
            }
        Label_0EC7:
            conditionArray = (EUnitCondition[]) Enum.GetValues(typeof(EUnitCondition));
            num16 = 0;
            goto Label_0F5A;
        Label_0EE5:
            if (unit.IsUnitCondition(conditionArray[num16]) == null)
            {
                goto Label_0F54;
            }
            cond2 = Object.Instantiate<BattleUnitDetailCond>(this.CondBaseItem);
            if (cond2 != null)
            {
                goto Label_0F13;
            }
            goto Label_0F54;
        Label_0F13:
            cond2.get_transform().SetParent(this.GoCondParent.get_transform());
            cond2.get_transform().set_localScale(Vector3.get_one());
            cond2.SetCond(conditionArray[num16]);
            cond2.get_gameObject().SetActive(1);
        Label_0F54:
            num16 += 1;
        Label_0F5A:
            if (num16 < ((int) conditionArray.Length))
            {
                goto Label_0EE5;
            }
        Label_0F65:
            GameParameter.UpdateAll(base.get_gameObject());
            GlobalEvent.Invoke("BATTLE_UNIT_DETAIL_REFRESH", this);
            return;
        }

        private void Start()
        {
            TowerFloorParam param;
            this.mSb = SceneBattle.Instance;
            if (this.mSb == null)
            {
                goto Label_0030;
            }
            if (this.mSb.BattleUI != null)
            {
                goto Label_0031;
            }
        Label_0030:
            return;
        Label_0031:
            this.mBc = this.mSb.Battle;
            if (this.mBc != null)
            {
                goto Label_004E;
            }
            return;
        Label_004E:
            this.mTargetSub = this.mSb.BattleUI.TargetSub;
            if (this.mTargetSub != null)
            {
                goto Label_0075;
            }
            return;
        Label_0075:
            if (this.mBc.IsMultiTower == null)
            {
                goto Label_00DE;
            }
            if (this.GoLeaderSkill == null)
            {
                goto Label_00A1;
            }
            this.GoLeaderSkill.SetActive(1);
        Label_00A1:
            if (this.GoLeader2Skill == null)
            {
                goto Label_00BD;
            }
            this.GoLeader2Skill.SetActive(1);
        Label_00BD:
            if (this.GoFriendSkill == null)
            {
                goto Label_01FF;
            }
            this.GoFriendSkill.SetActive(0);
            goto Label_01FF;
        Label_00DE:
            if (this.mBc.IsMultiPlay == null)
            {
                goto Label_0161;
            }
            if (this.mBc.IsMultiVersus != null)
            {
                goto Label_0161;
            }
            if (this.GoLeaderSkill == null)
            {
                goto Label_0124;
            }
            this.GoLeaderSkill.SetActive(this.mBc.IsMultiLeaderSkill);
        Label_0124:
            if (this.GoLeader2Skill == null)
            {
                goto Label_0140;
            }
            this.GoLeader2Skill.SetActive(0);
        Label_0140:
            if (this.GoFriendSkill == null)
            {
                goto Label_01FF;
            }
            this.GoFriendSkill.SetActive(0);
            goto Label_01FF;
        Label_0161:
            if (this.GoLeaderSkill == null)
            {
                goto Label_017D;
            }
            this.GoLeaderSkill.SetActive(1);
        Label_017D:
            if (this.GoLeader2Skill == null)
            {
                goto Label_0199;
            }
            this.GoLeader2Skill.SetActive(0);
        Label_0199:
            if (this.mBc.IsTower == null)
            {
                goto Label_01D6;
            }
            param = this.TF_Param;
            if (this.GoFriendSkill == null)
            {
                goto Label_01FF;
            }
            this.GoFriendSkill.SetActive(param.can_help);
            goto Label_01FF;
        Label_01D6:
            if (this.GoFriendSkill == null)
            {
                goto Label_01FF;
            }
            this.GoFriendSkill.SetActive(this.mBc.IsMultiVersus == 0);
        Label_01FF:
            if (this.GoLeaderSkillBadge == null)
            {
                goto Label_0225;
            }
            this.GoLeaderSkillBadge.SetActive(this.mBc.IsMultiTower);
        Label_0225:
            this.Refresh(this.mTargetSub.SelectedUnit);
            return;
        }

        private TowerFloorParam TF_Param
        {
            get
            {
                GameManager manager;
                if (this.mTF_Param != null)
                {
                    goto Label_0063;
                }
                manager = MonoSingleton<GameManager>.GetInstanceDirect();
                if (manager == null)
                {
                    goto Label_0063;
                }
                if (this.mSb == null)
                {
                    goto Label_0063;
                }
                if (this.mBc == null)
                {
                    goto Label_0063;
                }
                if (this.mBc.IsTower == null)
                {
                    goto Label_0063;
                }
                this.mTF_Param = manager.FindTowerFloor(this.mSb.CurrentQuest.iname);
            Label_0063:
                return this.mTF_Param;
            }
        }

        public static BattleUnitDetail Instance
        {
            get
            {
                return mInstance;
            }
        }

        public enum eBudFluct
        {
            NONE,
            DW_L,
            DW_M,
            DW_S,
            UP_S,
            UP_M,
            UP_L
        }

        public enum eFluctChk
        {
            DEFAULT,
            ELEMENT,
            ATK_DETAIL
        }

        private enum eFluctSize
        {
            VAL_S,
            VAL_M,
            VAL_L
        }
    }
}

