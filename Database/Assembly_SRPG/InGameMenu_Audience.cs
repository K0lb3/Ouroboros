namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "Close Give Up Window", 0, 0), Pin(2, "Give Up", 0, 0), Pin(1, "Start Debug", 0, 0)]
    public class InGameMenu_Audience : MonoBehaviour, IFlowInterface
    {
        public const int PINID_DEBUG = 1;
        public const int PINID_GIVEUP = 2;
        public const int PINID_CLOSE_GIVEUP_WINDOW = 100;
        public GameObject ExitButton;
        public GenericSlot[] Units_1P;
        public GenericSlot[] Units_2P;
        public Text Name1P;
        public Text Name2P;
        public Text TotalAtk1P;
        public Text TotalAtk2P;
        public Text Lv1P;
        public Text Lv2P;
        private GameObject mExitWindow;
        private List<GameObject> mButtonObj;

        public InGameMenu_Audience()
        {
            this.Units_1P = new GenericSlot[0];
            this.Units_2P = new GenericSlot[0];
            this.mButtonObj = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            string str;
            Win_Btn_DecideCancel_FL_C l_fl_c;
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_008B;
            }
            if (num == 2)
            {
                goto Label_0022;
            }
            if (num == 100)
            {
                goto Label_0050;
            }
            goto Label_008B;
            goto Label_008B;
        Label_0022:
            str = LocalizedText.Get("sys.MULTI_VERSUS_COMFIRM_EXIT");
            this.mExitWindow = UIUtility.ConfirmBox(str, new UIUtility.DialogResultEvent(this.OnGiveUp), null, null, 1, 1, null, null);
            goto Label_008B;
        Label_0050:
            if ((this.mExitWindow != null) == null)
            {
                goto Label_008B;
            }
            l_fl_c = this.mExitWindow.GetComponent<Win_Btn_DecideCancel_FL_C>();
            if ((l_fl_c != null) == null)
            {
                goto Label_007F;
            }
            l_fl_c.BeginClose();
        Label_007F:
            this.mExitWindow = null;
        Label_008B:
            return;
        }

        private void OnDestroy()
        {
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0036;
            }
            SceneBattle.Instance.OnQuestEnd = (SceneBattle.QuestEndEvent) Delegate.Remove(SceneBattle.Instance.OnQuestEnd, new SceneBattle.QuestEndEvent(this.OnQuestEnd));
        Label_0036:
            return;
        }

        private void OnGiveUp(GameObject go)
        {
            CanvasGroup group;
            Network.Abort();
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_001F;
            }
            SceneBattle.Instance.ForceEndQuest();
        Label_001F:
            group = base.GetComponent<CanvasGroup>();
            if ((group != null) == null)
            {
                goto Label_0039;
            }
            group.set_blocksRaycasts(0);
        Label_0039:
            return;
        }

        private void OnQuestEnd()
        {
            this.Activated(100);
            return;
        }

        private unsafe void Start()
        {
            SceneBattle battle;
            List<JSON_MyPhotonPlayerParam> list;
            List<Unit> list2;
            GenericSlot[] slotArray;
            Text text;
            Text text2;
            Text text3;
            Button button;
            Unit unit;
            <Start>c__AnonStorey34F storeyf;
            <Start>c__AnonStorey350 storey;
            <Start>c__AnonStorey351 storey2;
            battle = SceneBattle.Instance;
            if (this.mButtonObj == null)
            {
                goto Label_0021;
            }
            this.mButtonObj.Clear();
            goto Label_002C;
        Label_0021:
            this.mButtonObj = new List<GameObject>();
        Label_002C:
            if ((this.ExitButton != null) == null)
            {
                goto Label_0049;
            }
            this.ExitButton.SetActive(1);
        Label_0049:
            if ((battle != null) == null)
            {
                goto Label_02C0;
            }
            list = battle.AudiencePlayer;
            list2 = battle.Battle.AllUnits;
            slotArray = null;
            text = null;
            text2 = null;
            text3 = null;
            storeyf = new <Start>c__AnonStorey34F();
            storeyf.i = 0;
            goto Label_02AE;
        Label_0087:
            storey = new <Start>c__AnonStorey350();
            storey.units = list[storeyf.i].units;
            slotArray = (storeyf.i != null) ? this.Units_2P : this.Units_1P;
            if (slotArray == null)
            {
                goto Label_01C5;
            }
            storey2 = new <Start>c__AnonStorey351();
            storey2.<>f__ref$847 = storeyf;
            storey2.<>f__ref$848 = storey;
            storey2.j = 0;
            goto Label_01B6;
        Label_00F1:
            if (storey2.j >= ((int) storey.units.Length))
            {
                goto Label_0197;
            }
            slotArray[storey2.j].SetSlotData<UnitData>(storey.units[storey2.j].unit);
            button = slotArray[storey2.j].GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_01A6;
            }
            unit = list2.Find(new Predicate<Unit>(storey2.<>m__34D));
            if (unit == null)
            {
                goto Label_01A6;
            }
            DataSource.Bind<Unit>(button.get_gameObject(), unit);
            this.mButtonObj.Add(button.get_gameObject());
            button.set_interactable(unit.IsDeadCondition() == 0);
            goto Label_01A6;
        Label_0197:
            slotArray[storey2.j].SetSlotData<UnitData>(null);
        Label_01A6:
            storey2.j += 1;
        Label_01B6:
            if (storey2.j < ((int) slotArray.Length))
            {
                goto Label_00F1;
            }
        Label_01C5:
            text = (storeyf.i != null) ? this.Name2P : this.Name1P;
            if ((text != null) == null)
            {
                goto Label_020A;
            }
            text.set_text(list[storeyf.i].playerName);
        Label_020A:
            text2 = (storeyf.i != null) ? this.TotalAtk2P : this.TotalAtk1P;
            if ((text2 != null) == null)
            {
                goto Label_0254;
            }
            text2.set_text(&list[storeyf.i].totalAtk.ToString());
        Label_0254:
            text3 = (storeyf.i != null) ? this.Lv2P : this.Lv1P;
            if ((text2 != null) == null)
            {
                goto Label_029E;
            }
            text3.set_text(&list[storeyf.i].playerLevel.ToString());
        Label_029E:
            storeyf.i += 1;
        Label_02AE:
            if (storeyf.i < list.Count)
            {
                goto Label_0087;
            }
        Label_02C0:
            return;
        }

        private void Update()
        {
            int num;
            Unit unit;
            Button button;
            if (this.mButtonObj == null)
            {
                goto Label_006D;
            }
            num = 0;
            goto Label_005C;
        Label_0012:
            unit = DataSource.FindDataOfClass<Unit>(this.mButtonObj[num], null);
            if (unit == null)
            {
                goto Label_0058;
            }
            button = this.mButtonObj[num].GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_0058;
            }
            button.set_interactable(unit.IsDeadCondition() == 0);
        Label_0058:
            num += 1;
        Label_005C:
            if (num < this.mButtonObj.Count)
            {
                goto Label_0012;
            }
        Label_006D:
            return;
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey34F
        {
            internal int i;

            public <Start>c__AnonStorey34F()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey350
        {
            internal JSON_MyPhotonPlayerParam.UnitDataElem[] units;

            public <Start>c__AnonStorey350()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey351
        {
            internal int j;
            internal InGameMenu_Audience.<Start>c__AnonStorey34F <>f__ref$847;
            internal InGameMenu_Audience.<Start>c__AnonStorey350 <>f__ref$848;

            public <Start>c__AnonStorey351()
            {
                base..ctor();
                return;
            }

            internal bool <>m__34D(Unit un)
            {
                return ((un.OwnerPlayerIndex != (this.<>f__ref$847.i + 1)) ? 0 : (un.UnitData.UnitParam.iname == this.<>f__ref$848.units[this.j].unit.UnitParam.iname));
            }
        }
    }
}

