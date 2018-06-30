namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class EventScript : ScriptableObject
    {
        public const string ScenePreviewName = "@EventScenePreview";
        public const string EventScriptDir = "Events/";
        public const int MAX_UNMANAGED_FILE = 10;
        private static UnityEngine.Canvas mCanvas;
        public static string[] StrCompTypeRestHP;
        public static string[] StrCalcTypeRestHP;
        public static string[] StrSkillTiming;
        public static string[] StrShortSkillTiming;
        public string QuestID;
        public ScriptSequence[] mSequences;
        [CompilerGenerated]
        private static TestCondition <>f__am$cache7;
        [CompilerGenerated]
        private static TestCondition <>f__am$cache8;
        [CompilerGenerated]
        private static TestCondition <>f__am$cache9;
        [CompilerGenerated]
        private static TestCondition <>f__am$cacheA;

        static EventScript()
        {
            string[] textArray4;
            string[] textArray3;
            string[] textArray2;
            string[] textArray1;
            textArray1 = new string[] { "==", "!=", ">", ">=", "<", "<=", string.Empty };
            StrCompTypeRestHP = textArray1;
            textArray2 = new string[] { "％", "　", string.Empty };
            StrCalcTypeRestHP = textArray2;
            textArray3 = new string[] { "スキル使用前", "スキル使用後", string.Empty };
            StrSkillTiming = textArray3;
            textArray4 = new string[] { "前", "後", string.Empty };
            StrShortSkillTiming = textArray4;
            return;
        }

        public EventScript()
        {
            this.mSequences = new ScriptSequence[1];
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <OnPostMapLoad>m__171(ScriptSequence trigger)
        {
            return (trigger.Trigger == 5);
        }

        [CompilerGenerated]
        private static bool <OnQuestLose>m__17E(ScriptSequence trigger)
        {
            return (trigger.Trigger == 13);
        }

        [CompilerGenerated]
        private static bool <OnQuestWin>m__173(ScriptSequence trigger)
        {
            return (trigger.Trigger == 3);
        }

        [CompilerGenerated]
        private static bool <OnStart>m__172(ScriptSequence trigger)
        {
            return (trigger.Trigger == 0);
        }

        public static unsafe IntVector2 ConvToIntVector2Grid(string str_grid)
        {
            char[] chArray1;
            IntVector2 vector;
            string[] strArray;
            &vector = new IntVector2(0, 0);
            if (string.IsNullOrEmpty(str_grid) != null)
            {
                goto Label_005E;
            }
            chArray1 = new char[] { 0x2c };
            strArray = str_grid.Split(chArray1);
            if (strArray == null)
            {
                goto Label_005E;
            }
            if (((int) strArray.Length) <= 0)
            {
                goto Label_0045;
            }
            int.TryParse(strArray[0], &&vector.x);
        Label_0045:
            if (((int) strArray.Length) <= 1)
            {
                goto Label_005E;
            }
            int.TryParse(strArray[1], &&vector.y);
        Label_005E:
            return vector;
        }

        public static unsafe cRestHP ConvToObjectRestHP(string str_rest_hp)
        {
            char[] chArray2;
            char[] chArray1;
            cRestHP thp;
            string[] strArray;
            string str;
            string[] strArray2;
            int num;
            string[] strArray3;
            int num2;
            int num3;
            int num4;
            cRestHP.Cond cond;
            thp = new cRestHP();
            if (string.IsNullOrEmpty(str_rest_hp) != null)
            {
                goto Label_00B3;
            }
            chArray1 = new char[] { 0x2c };
            strArray = str_rest_hp.Split(chArray1);
            if (strArray == null)
            {
                goto Label_00B3;
            }
            strArray2 = strArray;
            num = 0;
            goto Label_00A9;
        Label_0033:
            str = strArray2[num];
            chArray2 = new char[] { 0x2d };
            strArray3 = str.Split(chArray2);
            if (strArray3 == null)
            {
                goto Label_00A3;
            }
            if (((int) strArray3.Length) < 3)
            {
                goto Label_00A3;
            }
            num2 = 0;
            num3 = 0;
            num4 = 0;
            int.TryParse(strArray3[0], &num2);
            int.TryParse(strArray3[1], &num3);
            int.TryParse(strArray3[2], &num4);
            cond = new cRestHP.Cond(num2, num3, num4);
            thp.mCondLists.Add(cond);
        Label_00A3:
            num += 1;
        Label_00A9:
            if (num < ((int) strArray2.Length))
            {
                goto Label_0033;
            }
        Label_00B3:
            return thp;
        }

        public static unsafe string ConvToStringGrid(IntVector2 iv_grid)
        {
            return string.Format("{0},{1}", (int) &iv_grid.x, (int) &iv_grid.y);
        }

        public static string ConvToStringRestHP(cRestHP rest_hp)
        {
            object[] objArray1;
            string str;
            int num;
            cRestHP.Cond cond;
            string str2;
            str = string.Empty;
            if (rest_hp == null)
            {
                goto Label_0093;
            }
            num = 0;
            goto Label_0082;
        Label_0013:
            cond = rest_hp.mCondLists[num];
            if (num == null)
            {
                goto Label_0032;
            }
            str = str + ",";
        Label_0032:
            str2 = str;
            objArray1 = new object[] { str2, (int) cond.mComp, "-", (int) cond.mVal, "-", (int) cond.mCalc };
            str = string.Concat(objArray1);
            num += 1;
        Label_0082:
            if (num < rest_hp.mCondLists.Count)
            {
                goto Label_0013;
            }
        Label_0093:
            return str;
        }

        private void CreateCanvas()
        {
            Type[] typeArray1;
            GameObject obj2;
            CanvasStack stack;
            if ((mCanvas != null) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            typeArray1 = new Type[] { typeof(UnityEngine.Canvas), typeof(GraphicRaycaster), typeof(SRPG_CanvasScaler), typeof(CanvasStack), typeof(Button), typeof(NullGraphic) };
            obj2 = new GameObject("EventCanvas", typeArray1);
            mCanvas = obj2.GetComponent<UnityEngine.Canvas>();
            mCanvas.set_renderMode(0);
            stack = obj2.GetComponent<CanvasStack>();
            stack.Priority = -1;
            stack.Modal = 1;
            return;
        }

        public static void DestroyCanvas()
        {
            Object.Destroy(mCanvas.get_gameObject(), 1f);
            mCanvas = null;
            return;
        }

        private bool IsContainsSkill(string skill_name, SkillData skill)
        {
            char[] chArray1;
            bool flag;
            string[] strArray;
            string str;
            string[] strArray2;
            int num;
            if (string.IsNullOrEmpty(skill_name) != null)
            {
                goto Label_0011;
            }
            if (skill != null)
            {
                goto Label_0013;
            }
        Label_0011:
            return 0;
        Label_0013:
            if (skill_name.IndexOf("sall") < 0)
            {
                goto Label_0026;
            }
            return 1;
        Label_0026:
            flag = 0;
            chArray1 = new char[] { 0x2c };
            strArray = skill_name.Split(chArray1);
            if (strArray == null)
            {
                goto Label_007C;
            }
            strArray2 = strArray;
            num = 0;
            goto Label_0072;
        Label_004A:
            str = strArray2[num];
            if ((str == skill.SkillParam.iname) == null)
            {
                goto Label_006C;
            }
            flag = 1;
            goto Label_007C;
        Label_006C:
            num += 1;
        Label_0072:
            if (num < ((int) strArray2.Length))
            {
                goto Label_004A;
            }
        Label_007C:
            return flag;
        }

        private bool IsContainsUnit(string unit_name, TacticsUnitController self, TacticsUnitController opp)
        {
            char[] chArray1;
            bool flag;
            string[] strArray;
            string str;
            string[] strArray2;
            int num;
            if (string.IsNullOrEmpty(unit_name) != null)
            {
                goto Label_0017;
            }
            if ((self == null) == null)
            {
                goto Label_0019;
            }
        Label_0017:
            return 0;
        Label_0019:
            flag = 0;
            chArray1 = new char[] { 0x2c };
            strArray = unit_name.Split(chArray1);
            if (strArray == null)
            {
                goto Label_00E9;
            }
            strArray2 = strArray;
            num = 0;
            goto Label_00DF;
        Label_003D:
            str = strArray2[num];
            if ((str == "pall") == null)
            {
                goto Label_0069;
            }
            if (self.Unit.Side != null)
            {
                goto Label_00CE;
            }
            flag = 1;
            goto Label_00CE;
        Label_0069:
            if ((str == "eall") == null)
            {
                goto Label_0091;
            }
            if (self.Unit.Side != 1)
            {
                goto Label_00CE;
            }
            flag = 1;
            goto Label_00CE;
        Label_0091:
            if ((opp != null) == null)
            {
                goto Label_00C0;
            }
            if ((str == "other") == null)
            {
                goto Label_00C0;
            }
            if ((self != opp) == null)
            {
                goto Label_00CE;
            }
            flag = 1;
            goto Label_00CE;
        Label_00C0:
            if (self.IsA(str) == null)
            {
                goto Label_00CE;
            }
            flag = 1;
        Label_00CE:
            if (flag == null)
            {
                goto Label_00D9;
            }
            goto Label_00E9;
        Label_00D9:
            num += 1;
        Label_00DF:
            if (num < ((int) strArray2.Length))
            {
                goto Label_003D;
            }
        Label_00E9:
            return flag;
        }

        public Sequence OnPostMapLoad()
        {
            if (<>f__am$cache7 != null)
            {
                goto Label_0019;
            }
            <>f__am$cache7 = new TestCondition(EventScript.<OnPostMapLoad>m__171);
        Label_0019:
            return this.StartSequence(<>f__am$cache7, 1, 0);
        }

        public Sequence OnQuestLose()
        {
            if (<>f__am$cacheA != null)
            {
                goto Label_0019;
            }
            <>f__am$cacheA = new TestCondition(EventScript.<OnQuestLose>m__17E);
        Label_0019:
            return this.StartSequence(<>f__am$cacheA, 1, 0);
        }

        public Sequence OnQuestWin()
        {
            if (<>f__am$cache9 != null)
            {
                goto Label_0019;
            }
            <>f__am$cache9 = new TestCondition(EventScript.<OnQuestWin>m__173);
        Label_0019:
            return this.StartSequence(<>f__am$cache9, 1, 0);
        }

        public Sequence OnRecvSkillCond(TacticsUnitController controller, EUnitCondition cond, bool isFirstPlay)
        {
            <OnRecvSkillCond>c__AnonStorey264 storey;
            storey = new <OnRecvSkillCond>c__AnonStorey264();
            storey.isFirstPlay = isFirstPlay;
            storey.controller = controller;
            storey.cond = cond;
            storey.<>f__this = this;
            return this.StartSequence(new TestCondition(storey.<>m__180), 1, 0);
        }

        public Sequence OnRecvSkillElem(TacticsUnitController controller, EElement elem, bool isFirstPlay)
        {
            <OnRecvSkillElem>c__AnonStorey263 storey;
            storey = new <OnRecvSkillElem>c__AnonStorey263();
            storey.isFirstPlay = isFirstPlay;
            storey.controller = controller;
            storey.elem = elem;
            storey.<>f__this = this;
            return this.StartSequence(new TestCondition(storey.<>m__17F), 1, 0);
        }

        public Sequence OnStandbyGrid(TacticsUnitController controller, bool isFirstPlay)
        {
            <OnStandbyGrid>c__AnonStorey25F storeyf;
            storeyf = new <OnStandbyGrid>c__AnonStorey25F();
            storeyf.isFirstPlay = isFirstPlay;
            storeyf.controller = controller;
            storeyf.<>f__this = this;
            return this.StartSequence(new TestCondition(storeyf.<>m__17A), 1, 0);
        }

        public Sequence OnStart(int startOffset, bool is_auto_forward)
        {
            if (<>f__am$cache8 != null)
            {
                goto Label_0019;
            }
            <>f__am$cache8 = new TestCondition(EventScript.<OnStart>m__172);
        Label_0019:
            return this.StartSequence(<>f__am$cache8, is_auto_forward, startOffset);
        }

        public Sequence OnTurnStart(int turnCount)
        {
            <OnTurnStart>c__AnonStorey259 storey;
            storey = new <OnTurnStart>c__AnonStorey259();
            storey.turnCount = turnCount;
            return this.StartSequence(new TestCondition(storey.<>m__174), 1, 0);
        }

        public Sequence OnUnitAppear(TacticsUnitController controller, bool isFirstPlay)
        {
            <OnUnitAppear>c__AnonStorey25D storeyd;
            storeyd = new <OnUnitAppear>c__AnonStorey25D();
            storeyd.isFirstPlay = isFirstPlay;
            storeyd.controller = controller;
            return this.StartSequence(new TestCondition(storeyd.<>m__178), 1, 0);
        }

        public Sequence OnUnitDead(TacticsUnitController controller, bool isFirstPlay)
        {
            <OnUnitDead>c__AnonStorey25E storeye;
            storeye = new <OnUnitDead>c__AnonStorey25E();
            storeye.isFirstPlay = isFirstPlay;
            storeye.controller = controller;
            return this.StartSequence(new TestCondition(storeye.<>m__179), 1, 0);
        }

        public Sequence OnUnitHPChange(TacticsUnitController controller)
        {
            <OnUnitHPChange>c__AnonStorey25B storeyb;
            storeyb = new <OnUnitHPChange>c__AnonStorey25B();
            storeyb.controller = controller;
            return this.StartSequence(new TestCondition(storeyb.<>m__176), 1, 0);
        }

        public Sequence OnUnitRestHP(TacticsUnitController controller, bool isFirstPlay)
        {
            <OnUnitRestHP>c__AnonStorey260 storey;
            storey = new <OnUnitRestHP>c__AnonStorey260();
            storey.controller = controller;
            storey.isFirstPlay = isFirstPlay;
            return this.StartSequence(new TestCondition(storey.<>m__17B), 1, 0);
        }

        public Sequence OnUnitStart(TacticsUnitController controller)
        {
            <OnUnitStart>c__AnonStorey25A storeya;
            storeya = new <OnUnitStart>c__AnonStorey25A();
            storeya.controller = controller;
            return this.StartSequence(new TestCondition(storeya.<>m__175), 1, 0);
        }

        public Sequence OnUnitTurnStart(TacticsUnitController controller, bool isFirstPlay)
        {
            <OnUnitTurnStart>c__AnonStorey25C storeyc;
            storeyc = new <OnUnitTurnStart>c__AnonStorey25C();
            storeyc.isFirstPlay = isFirstPlay;
            storeyc.controller = controller;
            return this.StartSequence(new TestCondition(storeyc.<>m__177), 1, 0);
        }

        public Sequence OnUnitWithdraw(TacticsUnitController controller, bool isFirstPlay)
        {
            <OnUnitWithdraw>c__AnonStorey262 storey;
            storey = new <OnUnitWithdraw>c__AnonStorey262();
            storey.isFirstPlay = isFirstPlay;
            storey.controller = controller;
            return this.StartSequence(new TestCondition(storey.<>m__17D), 1, 0);
        }

        public Sequence OnUseSkill(SkillTiming timing, TacticsUnitController controller, SkillData skill, List<TacticsUnitController> TargetLists, bool isFirstPlay)
        {
            <OnUseSkill>c__AnonStorey261 storey;
            storey = new <OnUseSkill>c__AnonStorey261();
            storey.TargetLists = TargetLists;
            storey.controller = controller;
            storey.timing = timing;
            storey.isFirstPlay = isFirstPlay;
            storey.skill = skill;
            storey.<>f__this = this;
            return this.StartSequence(new TestCondition(storey.<>m__17C), 1, 0);
        }

        public void ResetTriggers()
        {
            int num;
            num = 0;
            goto Label_0019;
        Label_0007:
            this.mSequences[num].Triggered = 0;
            num += 1;
        Label_0019:
            if (num < ((int) this.mSequences.Length))
            {
                goto Label_0007;
            }
            return;
        }

        private Sequence StartSequence(TestCondition test, bool is_auto_forward, int startOffset)
        {
            GameObject obj2;
            int num;
            GameObject obj3;
            Sequence sequence;
            int num2;
            int num3;
            obj2 = new GameObject("EventCameraStream");
            if ((obj2 != null) == null)
            {
                goto Label_001E;
            }
            obj2.AddComponent<Animation>();
        Label_001E:
            num = 0;
            goto Label_012A;
        Label_0025:
            if (this.mSequences[num].Triggered != null)
            {
                goto Label_0126;
            }
            if (test(this.mSequences[num]) == null)
            {
                goto Label_0126;
            }
            this.CreateCanvas();
            obj3 = new GameObject(base.get_name());
            this.mSequences[num].Triggered = 1;
            sequence = obj3.AddComponent<Sequence>();
            sequence.Actions = new EventAction[this.mSequences[num].Actions.Count - startOffset];
            sequence.IsAutoForward = is_auto_forward;
            sequence.ParentSequence = this.mSequences[num];
            num2 = startOffset;
            goto Label_010B;
        Label_00AD:
            num3 = num2 - startOffset;
            sequence.Actions[num3] = Object.Instantiate<EventAction>(this.mSequences[num].Actions[num2]);
            sequence.Actions[num3].Sequence = sequence;
            if (num2 <= startOffset)
            {
                goto Label_0105;
            }
            sequence.Actions[num3 - 1].NextAction = sequence.Actions[num3];
        Label_0105:
            num2 += 1;
        Label_010B:
            if (num2 < this.mSequences[num].Actions.Count)
            {
                goto Label_00AD;
            }
            return sequence;
        Label_0126:
            num += 1;
        Label_012A:
            if (num < ((int) this.mSequences.Length))
            {
                goto Label_0025;
            }
            return null;
        }

        public static UnityEngine.Canvas Canvas
        {
            get
            {
                return mCanvas;
            }
        }

        [CompilerGenerated]
        private sealed class <OnRecvSkillCond>c__AnonStorey264
        {
            internal bool isFirstPlay;
            internal TacticsUnitController controller;
            internal EUnitCondition cond;
            internal EventScript <>f__this;

            public <OnRecvSkillCond>c__AnonStorey264()
            {
                base..ctor();
                return;
            }

            internal bool <>m__180(EventScript.ScriptSequence trigger)
            {
                return ((((trigger.Trigger != 15) || ((trigger.IsFirstOnly != null) && (this.isFirstPlay == null))) || (this.<>f__this.IsContainsUnit(trigger.UnitName, this.controller, null) == null)) ? 0 : (((this.cond & ((long) trigger.SkillCond)) == 0L) == 0));
            }
        }

        [CompilerGenerated]
        private sealed class <OnRecvSkillElem>c__AnonStorey263
        {
            internal bool isFirstPlay;
            internal TacticsUnitController controller;
            internal EElement elem;
            internal EventScript <>f__this;

            public <OnRecvSkillElem>c__AnonStorey263()
            {
                base..ctor();
                return;
            }

            internal bool <>m__17F(EventScript.ScriptSequence trigger)
            {
                return ((((trigger.Trigger != 14) || ((trigger.IsFirstOnly != null) && (this.isFirstPlay == null))) || (this.<>f__this.IsContainsUnit(trigger.UnitName, this.controller, null) == null)) ? 0 : (this.elem == trigger.SkillElem));
            }
        }

        [CompilerGenerated]
        private sealed class <OnStandbyGrid>c__AnonStorey25F
        {
            internal bool isFirstPlay;
            internal TacticsUnitController controller;
            internal EventScript <>f__this;

            public <OnStandbyGrid>c__AnonStorey25F()
            {
                base..ctor();
                return;
            }

            internal unsafe bool <>m__17A(EventScript.ScriptSequence trigger)
            {
                IntVector2 vector;
                vector = EventScript.ConvToIntVector2Grid(trigger.GridXY);
                return ((((trigger.Trigger != 9) || ((trigger.IsFirstOnly != null) && (this.isFirstPlay == null))) || ((this.<>f__this.IsContainsUnit(trigger.UnitName, this.controller, null) == null) || (this.controller.Unit.x != &vector.x))) ? 0 : (this.controller.Unit.y == &vector.y));
            }
        }

        [CompilerGenerated]
        private sealed class <OnTurnStart>c__AnonStorey259
        {
            internal int turnCount;

            public <OnTurnStart>c__AnonStorey259()
            {
                base..ctor();
                return;
            }

            internal bool <>m__174(EventScript.ScriptSequence trigger)
            {
                return ((trigger.Trigger != 4) ? 0 : (trigger.Turn == this.turnCount));
            }
        }

        [CompilerGenerated]
        private sealed class <OnUnitAppear>c__AnonStorey25D
        {
            internal bool isFirstPlay;
            internal TacticsUnitController controller;

            public <OnUnitAppear>c__AnonStorey25D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__178(EventScript.ScriptSequence trigger)
            {
            Label_0022:
                return (((trigger.Trigger != 7) || ((trigger.IsFirstOnly != null) && (this.isFirstPlay == null))) ? 0 : this.controller.IsA(trigger.UnitName));
            }
        }

        [CompilerGenerated]
        private sealed class <OnUnitDead>c__AnonStorey25E
        {
            internal bool isFirstPlay;
            internal TacticsUnitController controller;

            public <OnUnitDead>c__AnonStorey25E()
            {
                base..ctor();
                return;
            }

            internal bool <>m__179(EventScript.ScriptSequence trigger)
            {
            Label_0022:
                return (((trigger.Trigger != 8) || ((trigger.IsFirstOnly != null) && (this.isFirstPlay == null))) ? 0 : this.controller.IsA(trigger.UnitName));
            }
        }

        [CompilerGenerated]
        private sealed class <OnUnitHPChange>c__AnonStorey25B
        {
            internal TacticsUnitController controller;

            public <OnUnitHPChange>c__AnonStorey25B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__176(EventScript.ScriptSequence trigger)
            {
                return (((trigger.Trigger != 2) || (this.controller.IsA(trigger.UnitName) == null)) ? 0 : ((this.controller.HPPercentage > trigger.Percentage) == 0));
            }
        }

        [CompilerGenerated]
        private sealed class <OnUnitRestHP>c__AnonStorey260
        {
            internal TacticsUnitController controller;
            internal bool isFirstPlay;

            public <OnUnitRestHP>c__AnonStorey260()
            {
                base..ctor();
                return;
            }

            internal unsafe bool <>m__17B(EventScript.ScriptSequence trigger)
            {
                EventScript.cRestHP thp;
                bool flag;
                EventScript.cRestHP.Cond cond;
                List<EventScript.cRestHP.Cond>.Enumerator enumerator;
                int num;
                EventScript.cRestHP.Cond.CalcType type;
                EventScript.cRestHP.Cond.CompType type2;
                thp = EventScript.ConvToObjectRestHP(trigger.RestHP);
                flag = 1;
                enumerator = thp.mCondLists.GetEnumerator();
            Label_001A:
                try
                {
                    goto Label_012D;
                Label_001F:
                    cond = &enumerator.Current;
                    num = 0;
                    type = cond.mCalc;
                    if (type == null)
                    {
                        goto Label_0046;
                    }
                    if (type == 1)
                    {
                        goto Label_0058;
                    }
                    goto Label_007E;
                Label_0046:
                    num = this.controller.HPPercentage;
                    goto Label_007E;
                Label_0058:
                    num = this.controller.Unit.CurrentStatus.param.hp;
                Label_007E:
                    switch (cond.mComp)
                    {
                        case 0:
                            goto Label_00AA;

                        case 1:
                            goto Label_00BE;

                        case 2:
                            goto Label_00D2;

                        case 3:
                            goto Label_00E6;

                        case 4:
                            goto Label_00FA;

                        case 5:
                            goto Label_010E;
                    }
                    goto Label_0122;
                Label_00AA:
                    if (num == cond.mVal)
                    {
                        goto Label_0122;
                    }
                    flag = 0;
                    goto Label_0122;
                Label_00BE:
                    if (num != cond.mVal)
                    {
                        goto Label_0122;
                    }
                    flag = 0;
                    goto Label_0122;
                Label_00D2:
                    if (num > cond.mVal)
                    {
                        goto Label_0122;
                    }
                    flag = 0;
                    goto Label_0122;
                Label_00E6:
                    if (num >= cond.mVal)
                    {
                        goto Label_0122;
                    }
                    flag = 0;
                    goto Label_0122;
                Label_00FA:
                    if (num < cond.mVal)
                    {
                        goto Label_0122;
                    }
                    flag = 0;
                    goto Label_0122;
                Label_010E:
                    if (num <= cond.mVal)
                    {
                        goto Label_0122;
                    }
                    flag = 0;
                Label_0122:
                    if (flag != null)
                    {
                        goto Label_012D;
                    }
                    goto Label_0139;
                Label_012D:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_001F;
                    }
                Label_0139:
                    goto Label_014A;
                }
                finally
                {
                Label_013E:
                    ((List<EventScript.cRestHP.Cond>.Enumerator) enumerator).Dispose();
                }
            Label_014A:
                return ((((trigger.Trigger != 10) || ((trigger.IsFirstOnly != null) && (this.isFirstPlay == null))) || (this.controller.IsA(trigger.UnitName) == null)) ? 0 : flag);
            }
        }

        [CompilerGenerated]
        private sealed class <OnUnitStart>c__AnonStorey25A
        {
            internal TacticsUnitController controller;

            public <OnUnitStart>c__AnonStorey25A()
            {
                base..ctor();
                return;
            }

            internal bool <>m__175(EventScript.ScriptSequence trigger)
            {
                return (((trigger.Trigger != 1) || (this.controller.IsA(trigger.UnitName) == null)) ? 0 : (trigger.Turn == this.controller.Unit.TurnCount));
            }
        }

        [CompilerGenerated]
        private sealed class <OnUnitTurnStart>c__AnonStorey25C
        {
            internal bool isFirstPlay;
            internal TacticsUnitController controller;

            public <OnUnitTurnStart>c__AnonStorey25C()
            {
                base..ctor();
                return;
            }

            internal bool <>m__177(EventScript.ScriptSequence trigger)
            {
                return ((((trigger.Trigger != 6) || ((trigger.IsFirstOnly != null) && (this.isFirstPlay == null))) || (this.controller.IsA(trigger.UnitName) == null)) ? 0 : (trigger.Turn == this.controller.Unit.TurnCount));
            }
        }

        [CompilerGenerated]
        private sealed class <OnUnitWithdraw>c__AnonStorey262
        {
            internal bool isFirstPlay;
            internal TacticsUnitController controller;

            public <OnUnitWithdraw>c__AnonStorey262()
            {
                base..ctor();
                return;
            }

            internal bool <>m__17D(EventScript.ScriptSequence trigger)
            {
            Label_0023:
                return (((trigger.Trigger != 12) || ((trigger.IsFirstOnly != null) && (this.isFirstPlay == null))) ? 0 : this.controller.IsA(trigger.UnitName));
            }
        }

        [CompilerGenerated]
        private sealed class <OnUseSkill>c__AnonStorey261
        {
            internal List<TacticsUnitController> TargetLists;
            internal TacticsUnitController controller;
            internal EventScript.SkillTiming timing;
            internal bool isFirstPlay;
            internal SkillData skill;
            internal EventScript <>f__this;

            public <OnUseSkill>c__AnonStorey261()
            {
                base..ctor();
                return;
            }

            internal unsafe bool <>m__17C(EventScript.ScriptSequence trigger)
            {
                bool flag;
                TacticsUnitController controller;
                List<TacticsUnitController>.Enumerator enumerator;
                flag = 0;
                if (this.TargetLists == null)
                {
                    goto Label_0067;
                }
                enumerator = this.TargetLists.GetEnumerator();
            Label_0019:
                try
                {
                    goto Label_004A;
                Label_001E:
                    controller = &enumerator.Current;
                    if (this.<>f__this.IsContainsUnit(trigger.TargetUnit, controller, this.controller) == null)
                    {
                        goto Label_004A;
                    }
                    flag = 1;
                    goto Label_0056;
                Label_004A:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_001E;
                    }
                Label_0056:
                    goto Label_0067;
                }
                finally
                {
                Label_005B:
                    ((List<TacticsUnitController>.Enumerator) enumerator).Dispose();
                }
            Label_0067:
                return (((((trigger.Trigger != 11) || (trigger.SkillTiming != this.timing)) || ((trigger.IsFirstOnly != null) && (this.isFirstPlay == null))) || ((this.<>f__this.IsContainsUnit(trigger.UnitName, this.controller, null) == null) || (flag == null))) ? 0 : this.<>f__this.IsContainsSkill(trigger.TargetSkill, this.skill));
            }
        }

        public class cRestHP
        {
            public List<Cond> mCondLists;

            public cRestHP()
            {
                this.mCondLists = new List<Cond>();
                base..ctor();
                return;
            }

            public class Cond
            {
                public CompType mComp;
                public int mVal;
                public CalcType mCalc;

                public Cond()
                {
                    base..ctor();
                    return;
                }

                public Cond(int comp, int val, int calc)
                {
                    base..ctor();
                    if (comp < 0)
                    {
                        goto Label_0014;
                    }
                    if (comp < 6)
                    {
                        goto Label_0017;
                    }
                Label_0014:
                    comp = 0;
                Label_0017:
                    if (calc < 0)
                    {
                        goto Label_0025;
                    }
                    if (calc < 2)
                    {
                        goto Label_0028;
                    }
                Label_0025:
                    calc = 0;
                Label_0028:
                    this.mComp = comp;
                    this.mVal = val;
                    this.mCalc = calc;
                    return;
                }

                public enum CalcType
                {
                    SCALE,
                    FIXED,
                    MAX
                }

                public enum CompType
                {
                    EQUAL,
                    NOT_EQUAL,
                    GREATER,
                    GREATER_EQUAL,
                    LESS,
                    LESS_EQUAL,
                    MAX
                }
            }
        }

        [Serializable]
        public class ScriptSequence
        {
            [StringIsActorID, ConditionAttr(new StartConditions[] { 1, 2, 6, 7, 8, 9, 10, 11, 12, 14, 15 })]
            public string UnitName;
            [ConditionAttr(new StartConditions[] { 2 }), Range(0f, 99f)]
            public int Percentage;
            [ConditionAttr(new StartConditions[] { 1, 4, 6 })]
            public int Turn;
            [StringIsGrid, ConditionAttr(new StartConditions[] { 9 })]
            public string GridXY;
            [StringIsRestHP, ConditionAttr(new StartConditions[] { 10 })]
            public string RestHP;
            [IntIsSkillTiming, ConditionAttr(new StartConditions[] { 11 })]
            public int SkillTiming;
            [ConditionAttr(new StartConditions[] { 11 }), StringIsTargetSkill]
            public string TargetSkill;
            [ConditionAttr(new StartConditions[] { 11 }), StringIsTargetUnit]
            public string TargetUnit;
            [ConditionAttr(new StartConditions[] { 6, 7, 8, 9, 10, 11, 12 })]
            public bool IsFirstOnly;
            [IntIsSkillElem, ConditionAttr(new StartConditions[] { 14 })]
            public int SkillElem;
            [IntIsSkillCond, ConditionAttr(new StartConditions[] { 15 })]
            public int SkillCond;
            public StartConditions Trigger;
            public List<EventAction> Actions;
            [NonSerialized]
            public bool Triggered;

            public ScriptSequence()
            {
                this.SkillCond = 1;
                base..ctor();
                return;
            }

            public bool IsSavePlayBgmID
            {
                get
                {
                    StartConditions conditions;
                    conditions = this.Trigger;
                    switch (conditions)
                    {
                        case 0:
                            goto Label_0032;

                        case 1:
                            goto Label_0025;

                        case 2:
                            goto Label_0025;

                        case 3:
                            goto Label_0032;

                        case 4:
                            goto Label_0025;

                        case 5:
                            goto Label_0032;
                    }
                Label_0025:
                    if (conditions == 13)
                    {
                        goto Label_0032;
                    }
                    goto Label_0034;
                Label_0032:
                    return 0;
                Label_0034:
                    return 1;
                }
            }

            public class ConditionAttr : Attribute
            {
                public EventScript.ScriptSequence.StartConditions[] Conditions;

                public ConditionAttr(params EventScript.ScriptSequence.StartConditions[] conditions)
                {
                    base..ctor();
                    this.Conditions = conditions;
                    return;
                }

                public bool Contains(EventScript.ScriptSequence.StartConditions condition)
                {
                    return ((Array.IndexOf<EventScript.ScriptSequence.StartConditions>(this.Conditions, condition) < 0) == 0);
                }
            }

            public enum StartConditions
            {
                Auto,
                UnitStart,
                HPBelowPercent,
                Win,
                TurnStart,
                PostMapLoad,
                UnitTurnStart,
                UnitAppear,
                UnitDead,
                StandbyGrid,
                RestHP,
                UseSkill,
                UnitWithdraw,
                Lose,
                RecvSkillElem,
                RecvSkillCond
            }
        }

        public class Sequence : MonoBehaviour
        {
            public EventScript Script;
            public EventAction[] Actions;
            private bool mReady;
            private bool mForceSkip;
            private int mLastActionIndex;
            private List<int> mForceSkipIndex;
            private UnityAction mClickAction;
            public bool IsAutoForward;
            private float mTimerAutoForward;
            private int mCurIdxAutoForward;
            public EventScript.ScriptSequence ParentSequence;
            private GameObject mScene;
            public List<GameObject> SpawnedObjects;

            public Sequence()
            {
                this.mLastActionIndex = -1;
                this.mForceSkipIndex = new List<int>();
                this.SpawnedObjects = new List<GameObject>();
                base..ctor();
                return;
            }

            public void GoToEndState()
            {
                int num;
                this.mForceSkip = 1;
                num = 0;
                goto Label_0030;
            Label_000E:
                if (this.Actions[num].enabled == null)
                {
                    goto Label_002C;
                }
                this.mForceSkipIndex.Add(num);
            Label_002C:
                num += 1;
            Label_0030:
                if (num < ((int) this.Actions.Length))
                {
                    goto Label_000E;
                }
                if (Enumerable.Any<int>(this.mForceSkipIndex) == null)
                {
                    goto Label_0071;
                }
                this.mLastActionIndex = this.mForceSkipIndex[this.mForceSkipIndex.Count - 1];
                goto Label_0078;
            Label_0071:
                this.mLastActionIndex = -1;
            Label_0078:
                return;
            }

            private void OnClick()
            {
                int num;
                num = 0;
                goto Label_002B;
            Label_0007:
                if (this.Actions[num].enabled == null)
                {
                    goto Label_0027;
                }
                this.Actions[num].Forward();
            Label_0027:
                num += 1;
            Label_002B:
                if (num < ((int) this.Actions.Length))
                {
                    goto Label_0007;
                }
                return;
            }

            private void OnDestroy()
            {
                int num;
                Button button;
                Object.Destroy(this.mScene);
                EventDialogBubble.DiscardAll();
                EventDialogBubbleCustom.DiscardAll();
                EventStandCharaController2.DiscardAll();
                num = 0;
                goto Label_0032;
            Label_0021:
                Object.Destroy(this.Actions[num]);
                num += 1;
            Label_0032:
                if (num < ((int) this.Actions.Length))
                {
                    goto Label_0021;
                }
                if ((EventScript.Canvas != null) == null)
                {
                    goto Label_0071;
                }
                EventScript.Canvas.GetComponent<Button>().get_onClick().RemoveListener(this.mClickAction);
                EventScript.DestroyCanvas();
            Label_0071:
                return;
            }

            public void OnQuit()
            {
                int num;
                int num2;
                num = -1;
                num2 = 0;
                goto Label_0064;
            Label_0009:
                if (this.Actions[num2].enabled == null)
                {
                    goto Label_0022;
                }
                num = num2;
                goto Label_0060;
            Label_0022:
                if (num == -1)
                {
                    goto Label_0060;
                }
                if ((this.Actions[num2] as Event2dAction_QuitDisable) != null)
                {
                    goto Label_0072;
                }
                if ((this.Actions[num2] as Event2dAction_Scene) == null)
                {
                    goto Label_0052;
                }
                goto Label_0072;
            Label_0052:
                this.Actions[num2].Skip = 1;
            Label_0060:
                num2 += 1;
            Label_0064:
                if (num2 < ((int) this.Actions.Length))
                {
                    goto Label_0009;
                }
            Label_0072:
                if (num == -1)
                {
                    goto Label_0087;
                }
                this.Actions[num].Forward();
            Label_0087:
                return;
            }

            public void OnQuitImmediate()
            {
                int num;
                int num2;
                num = -1;
                num2 = 0;
                goto Label_004F;
            Label_0009:
                if (this.Actions[num2].enabled == null)
                {
                    goto Label_003D;
                }
                this.Actions[num2].SkipImmediate();
                this.Actions[num2].Skip = 1;
                num = num2;
                goto Label_004B;
            Label_003D:
                this.Actions[num2].Skip = 1;
            Label_004B:
                num2 += 1;
            Label_004F:
                if (num2 < ((int) this.Actions.Length))
                {
                    goto Label_0009;
                }
                if (num == -1)
                {
                    goto Label_0072;
                }
                this.Actions[num].Forward();
            Label_0072:
                return;
            }

            [DebuggerHidden]
            private IEnumerator PreloadAssetsAsync()
            {
                <PreloadAssetsAsync>c__Iterator9D iteratord;
                iteratord = new <PreloadAssetsAsync>c__Iterator9D();
                iteratord.<>f__this = this;
                return iteratord;
            }

            public bool ReplaySkipButtonEnable()
            {
                int num;
                num = 0;
                goto Label_0031;
            Label_0007:
                if (this.Actions[num].enabled == null)
                {
                    goto Label_002D;
                }
                if (this.Actions[num].ReplaySkipButtonEnable() != null)
                {
                    goto Label_002D;
                }
                return 0;
            Label_002D:
                num += 1;
            Label_0031:
                if (num < ((int) this.Actions.Length))
                {
                    goto Label_0007;
                }
                return 1;
            }

            private void Start()
            {
                Button button;
                Debug.LogWarning(this);
                this.mClickAction = new UnityAction(this, this.OnClick);
                EventScript.Canvas.GetComponent<Button>().get_onClick().AddListener(this.mClickAction);
                this.mTimerAutoForward = 0f;
                this.mCurIdxAutoForward = -1;
                base.StartCoroutine(this.PreloadAssetsAsync());
                return;
            }

            private void StartActions()
            {
                int num;
                int num2;
                num = 0;
                goto Label_002A;
            Label_0007:
                if (this.Actions[num].Skip != null)
                {
                    goto Label_0026;
                }
                this.Actions[num].PreStart();
            Label_0026:
                num += 1;
            Label_002A:
                if (num < ((int) this.Actions.Length))
                {
                    goto Label_0007;
                }
                num2 = 0;
                goto Label_0068;
            Label_003F:
                if (this.Actions[num2].Skip != null)
                {
                    goto Label_0064;
                }
                this.Actions[num2].enabled = 1;
                goto Label_0076;
            Label_0064:
                num2 += 1;
            Label_0068:
                if (num2 < ((int) this.Actions.Length))
                {
                    goto Label_003F;
                }
            Label_0076:
                return;
            }

            private void StartForceSkip()
            {
                int num;
                int num2;
                num = this.mLastActionIndex + 1;
                num2 = num;
                goto Label_002F;
            Label_0010:
                this.Actions[num2].GoToEndState();
                this.Actions[num2].Skip = 1;
                num2 += 1;
            Label_002F:
                if (num2 < ((int) this.Actions.Length))
                {
                    goto Label_0010;
                }
                if (num >= ((int) this.Actions.Length))
                {
                    goto Label_0059;
                }
                this.Actions[num].Forward();
            Label_0059:
                return;
            }

            private unsafe void Update()
            {
                int num;
                int num2;
                EventAction action;
                int num3;
                int num4;
                SceneBattle battle;
                bool flag;
                int num5;
                if (this.mReady != null)
                {
                    goto Label_000C;
                }
                return;
            Label_000C:
                if (this.mForceSkip == null)
                {
                    goto Label_0144;
                }
                num = 0;
                goto Label_00F2;
            Label_001E:
                num2 = this.mForceSkipIndex[num];
                if (num2 >= ((int) this.Actions.Length))
                {
                    goto Label_00DE;
                }
                if (this.Actions[num2].enabled == null)
                {
                    goto Label_00DE;
                }
                action = this.Actions[num2];
                if ((action as EventAction_WaitTap) != null)
                {
                    goto Label_00BC;
                }
                if ((action as EventAction_Dialog) != null)
                {
                    goto Label_00BC;
                }
                if ((action as EventAction_Dialog2) != null)
                {
                    goto Label_00BC;
                }
                if ((action as EventAction_FadeCanvas) != null)
                {
                    goto Label_00BC;
                }
                if ((action as EventAction_FadeScreen) != null)
                {
                    goto Label_00BC;
                }
                if ((action as EventAction_WaitSeconds) != null)
                {
                    goto Label_00BC;
                }
                action.Update();
                if (action.enabled != null)
                {
                    goto Label_00EE;
                }
                this.mForceSkipIndex.RemoveAt(num);
                num -= 1;
                goto Label_00D9;
            Label_00BC:
                action.GoToEndState();
                action.enabled = 0;
                this.mForceSkipIndex.RemoveAt(num);
                num -= 1;
            Label_00D9:
                goto Label_00EE;
            Label_00DE:
                this.mForceSkipIndex.RemoveAt(num);
                num -= 1;
            Label_00EE:
                num += 1;
            Label_00F2:
                if (num < this.mForceSkipIndex.Count)
                {
                    goto Label_001E;
                }
                if (Enumerable.Any<int>(this.mForceSkipIndex) != null)
                {
                    goto Label_027C;
                }
                if (this.mLastActionIndex < 0)
                {
                    goto Label_0125;
                }
                this.StartForceSkip();
            Label_0125:
                this.mForceSkip = 0;
                this.mForceSkipIndex.Clear();
                this.mLastActionIndex = -1;
                return;
                goto Label_027C;
            Label_0144:
                num3 = -1;
                num4 = 0;
                goto Label_0178;
            Label_014E:
                if (this.Actions[num4].enabled == null)
                {
                    goto Label_0172;
                }
                this.Actions[num4].Update();
                num3 = num4;
            Label_0172:
                num4 += 1;
            Label_0178:
                if (num4 < ((int) this.Actions.Length))
                {
                    goto Label_014E;
                }
                if (this.IsAutoForward == null)
                {
                    goto Label_027C;
                }
                battle = SceneBattle.Instance;
                if (battle == null)
                {
                    goto Label_027C;
                }
                if (battle.Battle.RequestAutoBattle != null)
                {
                    goto Label_01C7;
                }
                if (battle.Battle.IsMultiPlay == null)
                {
                    goto Label_027C;
                }
            Label_01C7:
                if (this.mCurIdxAutoForward == num3)
                {
                    goto Label_01EF;
                }
                this.mCurIdxAutoForward = num3;
                this.mTimerAutoForward = &GameSettings.Instance.Quest.WaitTimeScriptEventForward;
            Label_01EF:
                if (this.mTimerAutoForward <= 0f)
                {
                    goto Label_027C;
                }
                this.mTimerAutoForward -= Time.get_deltaTime();
                if (this.mTimerAutoForward > 0f)
                {
                    goto Label_027C;
                }
                flag = 0;
                num5 = 0;
                goto Label_025B;
            Label_022C:
                if (this.Actions[num5].enabled == null)
                {
                    goto Label_0255;
                }
                if (this.Actions[num5].Forward() == null)
                {
                    goto Label_0255;
                }
                flag = 1;
            Label_0255:
                num5 += 1;
            Label_025B:
                if (num5 < ((int) this.Actions.Length))
                {
                    goto Label_022C;
                }
                if (flag != null)
                {
                    goto Label_027C;
                }
                this.mTimerAutoForward = 1f;
            Label_027C:
                return;
            }

            public GameObject Scene
            {
                get
                {
                    return this.mScene;
                }
                set
                {
                    if ((this.mScene != null) == null)
                    {
                        goto Label_001C;
                    }
                    Object.Destroy(this.mScene);
                Label_001C:
                    this.mScene = value;
                    return;
                }
            }

            public bool IsPlaying
            {
                get
                {
                    int num;
                    if (this.mReady != null)
                    {
                        goto Label_000D;
                    }
                    return 1;
                Label_000D:
                    num = 0;
                    goto Label_002C;
                Label_0014:
                    if (this.Actions[num].enabled == null)
                    {
                        goto Label_0028;
                    }
                    return 1;
                Label_0028:
                    num += 1;
                Label_002C:
                    if (num < ((int) this.Actions.Length))
                    {
                        goto Label_0014;
                    }
                    return 0;
                }
            }

            [CompilerGenerated]
            private sealed class <PreloadAssetsAsync>c__Iterator9D : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal int <i>__0;
                internal string[] <Assets>__1;
                internal int <j>__2;
                internal int <i>__3;
                internal int $PC;
                internal object $current;
                internal EventScript.Sequence <>f__this;

                public <PreloadAssetsAsync>c__Iterator9D()
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
                            goto Label_0068;

                        case 2:
                            goto Label_014B;

                        case 3:
                            goto Label_0190;

                        case 4:
                            goto Label_0212;

                        case 5:
                            goto Label_0272;
                    }
                    goto Label_0279;
                Label_0031:
                    Debug.Log("PreloadAssetsAsync Start");
                    EventAction.IsPreloading = 1;
                    if (GameUtility.Config_UseAssetBundles.Value == null)
                    {
                        goto Label_019A;
                    }
                    goto Label_0068;
                Label_0055:
                    this.$current = null;
                    this.$PC = 1;
                    goto Label_027B;
                Label_0068:
                    if (AssetDownloader.isDone == null)
                    {
                        goto Label_0055;
                    }
                    AssetDownloader.DeleteOldUnmanagedData(10);
                    this.<i>__0 = 0;
                    goto Label_0116;
                Label_0085:
                    if (this.<>f__this.Actions[this.<i>__0].Skip != null)
                    {
                        goto Label_0108;
                    }
                    this.<Assets>__1 = this.<>f__this.Actions[this.<i>__0].GetUnManagedAssetListData();
                    if (this.<Assets>__1 == null)
                    {
                        goto Label_0108;
                    }
                    this.<j>__2 = 0;
                    goto Label_00F5;
                Label_00D5:
                    AssetDownloader.AddUnManagedData(this.<Assets>__1[this.<j>__2]);
                    this.<j>__2 += 1;
                Label_00F5:
                    if (this.<j>__2 < ((int) this.<Assets>__1.Length))
                    {
                        goto Label_00D5;
                    }
                Label_0108:
                    this.<i>__0 += 1;
                Label_0116:
                    if (this.<i>__0 < ((int) this.<>f__this.Actions.Length))
                    {
                        goto Label_0085;
                    }
                    AssetDownloader.StartDownloadUnmanagedData();
                    goto Label_014B;
                Label_0138:
                    this.$current = null;
                    this.$PC = 2;
                    goto Label_027B;
                Label_014B:
                    if (AssetDownloader.isDone == null)
                    {
                        goto Label_0138;
                    }
                    AssetManager.PrepareAssets("Events/" + this.<>f__this.get_name());
                    AssetDownloader.StartDownload(0, 1, 2);
                    goto Label_0190;
                Label_017D:
                    this.$current = null;
                    this.$PC = 3;
                    goto Label_027B;
                Label_0190:
                    if (AssetDownloader.isDone == null)
                    {
                        goto Label_017D;
                    }
                Label_019A:
                    this.<i>__3 = 0;
                    goto Label_0220;
                Label_01A6:
                    if (this.<>f__this.Actions[this.<i>__3].Skip != null)
                    {
                        goto Label_0212;
                    }
                    if (this.<>f__this.Actions[this.<i>__3].IsPreloadAssets == null)
                    {
                        goto Label_0212;
                    }
                    this.$current = this.<>f__this.StartCoroutine(this.<>f__this.Actions[this.<i>__3].PreloadAssets());
                    this.$PC = 4;
                    goto Label_027B;
                Label_0212:
                    this.<i>__3 += 1;
                Label_0220:
                    if (this.<i>__3 < ((int) this.<>f__this.Actions.Length))
                    {
                        goto Label_01A6;
                    }
                    EventAction.IsPreloading = 0;
                    Debug.Log("PreloadAssetsAsync End");
                    this.<>f__this.mReady = 1;
                    this.<>f__this.StartActions();
                    this.$current = null;
                    this.$PC = 5;
                    goto Label_027B;
                Label_0272:
                    this.$PC = -1;
                Label_0279:
                    return 0;
                Label_027B:
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

        public enum SkillTiming
        {
            BEFORE,
            AFTER,
            MAX
        }

        private delegate bool TestCondition(EventScript.ScriptSequence trigger);
    }
}

