namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "Close", 1, 1), Pin(2, "NotRequiredHeal", 1, 2), Pin(3, "HealCoin", 1, 3), Pin(4, "Heal", 1, 4), Pin(5, "HealOverFlow", 1, 5), Pin(10, "Click Use", 0, 10), Pin(0, "Refresh", 0, 0)]
    public class HealAp : MonoBehaviour, IFlowInterface
    {
        private List<ItemData> mHealItemList;
        public GameObject mItemParent;
        public GameObject mItemBase;
        public Text LackAp;
        public QuestParam mQuestParam;
        public Slider silder;
        public GameObject QuestInfo;
        public HealApBar bar;
        public Text now_ap;
        public Text max_ap;
        public Text heal_coin_text;
        public Text heal_coin_num;
        public Text pre_ap;
        public Text new_ap;
        [CompilerGenerated]
        private static Func<ItemParam, bool> <>f__am$cacheE;

        public HealAp()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Refresh>m__347(ItemParam x)
        {
            return (x.type == 20);
        }

        [CompilerGenerated]
        private void <Refresh>m__349(GameObject go)
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 2);
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_000F;
            }
            this.OnClickHeal();
            return;
        Label_000F:
            return;
        }

        public void HealApCoin()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 3);
            return;
        }

        public void OnClickHeal()
        {
            if (this.bar.IsOverFlow == null)
            {
                goto Label_001C;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 5);
            goto Label_0023;
        Label_001C:
            FlowNode_GameObject.ActivateOutputLinks(this, 4);
        Label_0023:
            return;
        }

        public void OnSelect(GameObject go)
        {
            ItemData data;
            data = DataSource.FindDataOfClass<ItemData>(go, null);
            DataSource.Bind<ItemData>(base.get_gameObject(), data);
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
            return;
        }

        public void Refresh(bool is_quest, FlowNode_HealApWindow heal_ap_window)
        {
            PartyWindow2 window;
            window = heal_ap_window.get_gameObject().GetComponentInChildren<PartyWindow2>();
            this.Refresh(is_quest, window);
            return;
        }

        public unsafe void Refresh(bool is_quest, PartyWindow2 _party_window)
        {
            object[] objArray2;
            object[] objArray1;
            PlayerData data;
            List<ItemParam> list;
            int num;
            ItemData data2;
            GameObject obj2;
            int num2;
            <Refresh>c__AnonStorey34C storeyc;
            int num3;
            int num4;
            int num5;
            int num6;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (<>f__am$cacheE != null)
            {
                goto Label_0032;
            }
            <>f__am$cacheE = new Func<ItemParam, bool>(HealAp.<Refresh>m__347);
        Label_0032:
            list = Enumerable.ToList<ItemParam>(Enumerable.Where<ItemParam>(MonoSingleton<GameManager>.Instance.MasterParam.Items, <>f__am$cacheE));
            if (list == null)
            {
                goto Label_0109;
            }
            num = 0;
            goto Label_00FD;
        Label_004F:
            storeyc = new <Refresh>c__AnonStorey34C();
            storeyc.iparam = list[num];
            data2 = data.Items.Find(new Predicate<ItemData>(storeyc.<>m__348));
            if (data2 != null)
            {
                goto Label_009F;
            }
            data2 = new ItemData();
            data2.Setup(0L, storeyc.iparam.iname, 0);
        Label_009F:
            obj2 = Object.Instantiate<GameObject>(this.mItemBase);
            obj2.GetComponent<ListItemEvents>().OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
            obj2.GetComponent<Button>().set_interactable(data2.Num > 0);
            DataSource.Bind<ItemData>(obj2, data2);
            obj2.get_transform().SetParent(this.mItemParent.get_transform(), 0);
            num += 1;
        Label_00FD:
            if (num < list.Count)
            {
                goto Label_004F;
            }
        Label_0109:
            this.mItemBase.SetActive(0);
            if (is_quest == null)
            {
                goto Label_013F;
            }
            if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID) != null)
            {
                goto Label_013F;
            }
            this.mQuestParam = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        Label_013F:
            this.QuestInfo.SetActive((this.mQuestParam == null) == 0);
            this.silder.set_maxValue((float) data.StaminaStockCap);
            this.silder.set_minValue(0f);
            this.silder.set_value((float) data.Stamina);
            this.now_ap.set_text(&data.Stamina.ToString());
            this.max_ap.set_text(&data.StaminaStockCap.ToString());
            if (this.mQuestParam == null)
            {
                goto Label_023D;
            }
            num2 = this.mQuestParam.RequiredApWithPlayerLv(data.Lv, 1);
            if ((_party_window != null) == null)
            {
                goto Label_0211;
            }
            if ((_party_window.RaidSettings != null) == null)
            {
                goto Label_0211;
            }
            if (_party_window.MultiRaidNum <= 0)
            {
                goto Label_0211;
            }
            num2 *= _party_window.MultiRaidNum;
        Label_0211:
            objArray1 = new object[] { (int) (num2 - data.Stamina) };
            this.LackAp.set_text(LocalizedText.Get("sys.TEXT_APHEAL_LACK_POINT", objArray1));
        Label_023D:
            objArray2 = new object[] { &MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StaminaAdd.ToString() };
            this.heal_coin_text.set_text(LocalizedText.Get("sys.SKIPBATTLE_HEAL_NUM", objArray2));
            this.heal_coin_num.set_text(&data.GetStaminaRecoveryCost(0).ToString());
            if (data.StaminaStockCap > data.Stamina)
            {
                goto Label_02C0;
            }
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.STAMINAFULL"), new UIUtility.DialogResultEvent(this.<Refresh>m__349), null, 0, -1);
        Label_02C0:
            this.pre_ap.set_text(&data.Stamina.ToString());
            return;
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey34C
        {
            internal ItemParam iparam;

            public <Refresh>c__AnonStorey34C()
            {
                base..ctor();
                return;
            }

            internal bool <>m__348(ItemData x)
            {
                return (x.ItemID == this.iparam.iname);
            }
        }
    }
}

