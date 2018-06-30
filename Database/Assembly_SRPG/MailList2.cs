namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(10, "メールリスト空", 1, 10), Pin(1, "Refresh", 0, 1)]
    public class MailList2 : MonoBehaviour, IFlowInterface
    {
        private const int PIN_ID_REFRESH = 1;
        private const int PIN_ID_LIST_EMPTY = 10;
        [SerializeField]
        private GameObject ItemTemplate;
        [SerializeField]
        private ListExtras ScrollView;
        private List<GameObject> mMailListItems;

        public MailList2()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_000E;
            }
            goto Label_0019;
        Label_000E:
            this.OnRefresh();
        Label_0019:
            return;
        }

        private void ActivateOutputLinks(int pinID)
        {
            FlowNode_GameObject.ActivateOutputLinks(this, pinID);
            return;
        }

        private void Awake()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_002D;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_002D;
            }
            this.ItemTemplate.SetActive(0);
        Label_002D:
            return;
        }

        private GameObject CreateListItem()
        {
            GameObject obj2;
            MailListItem item;
            ListItemEvents events;
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            item = obj2.GetComponent<MailListItem>();
            if ((item.listItemEvents != null) == null)
            {
                goto Label_0065;
            }
            item.listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
            events = item.Button.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0065;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Label_0065:
            return obj2;
        }

        private void InitializeList()
        {
            if (this.mMailListItems != null)
            {
                goto Label_0016;
            }
            this.mMailListItems = new List<GameObject>();
        Label_0016:
            return;
        }

        private void OnRefresh()
        {
            this.UpdateItems();
            if (MonoSingleton<GameManager>.Instance.Player.CurrentMails.Count >= 1)
            {
                goto Label_0028;
            }
            this.ActivateOutputLinks(10);
        Label_0028:
            return;
        }

        private void OnSelect(GameObject go)
        {
            MailData data;
            FlowNode_OnMailSelect select;
            data = DataSource.FindDataOfClass<MailData>(go, null);
            if (data == null)
            {
                goto Label_0059;
            }
            GlobalVars.SelectedMailUniqueID.Set(data.mid);
            GlobalVars.SelectedMailPeriod.Set(data.period);
            select = base.GetComponentInParent<FlowNode_OnMailSelect>();
            if ((select == null) == null)
            {
                goto Label_0047;
            }
            select = Object.FindObjectOfType<FlowNode_OnMailSelect>();
        Label_0047:
            if ((select != null) == null)
            {
                goto Label_0059;
            }
            select.Selected();
        Label_0059:
            this.UpdateItems();
            return;
        }

        private void Start()
        {
            this.InitializeList();
            return;
        }

        private void UpdateItems()
        {
            List<MailData> list;
            Transform transform;
            int num;
            int num2;
            GameObject obj2;
            int num3;
            GameObject obj3;
            MailData data;
            int num4;
            int num5;
            GiftData data2;
            MailIcon icon;
            MailIcon icon2;
            int num6;
            GiftData data3;
            ItemData data4;
            ArtifactData data5;
            AwardParam param;
            ItemData data6;
            ConceptCardParam param2;
            ConceptCardData data7;
            ConceptCardIcon icon3;
            MailListItem item;
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.mMailListItems != null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            list = MonoSingleton<GameManager>.Instance.Player.CurrentMails;
            if (this.mMailListItems.Count >= list.Count)
            {
                goto Label_0093;
            }
            transform = base.get_transform();
            num = list.Count - this.mMailListItems.Count;
            num2 = 0;
            goto Label_008C;
        Label_0065:
            obj2 = this.CreateListItem();
            obj2.get_transform().SetParent(transform, 0);
            this.mMailListItems.Add(obj2);
            num2 += 1;
        Label_008C:
            if (num2 < num)
            {
                goto Label_0065;
            }
        Label_0093:
            num3 = 0;
            goto Label_063F;
        Label_009B:
            obj3 = this.mMailListItems[num3];
            if (num3 < list.Count)
            {
                goto Label_00C4;
            }
            obj3.SetActive(0);
            goto Label_0639;
        Label_00C4:
            obj3.SetActive(1);
            data = list[num3];
            DataSource.Bind<MailData>(obj3, data);
            DataSource.Bind<MailData>(obj3.GetComponent<MailListItem>().Button, data);
            num4 = 0;
            num5 = 0;
            goto Label_012E;
        Label_00FD:
            data2 = data.gifts[num5];
            if (data2.giftTypes == null)
            {
                goto Label_011B;
            }
            num4 += 1;
        Label_011B:
            if (num4 < 2)
            {
                goto Label_0128;
            }
            goto Label_013E;
        Label_0128:
            num5 += 1;
        Label_012E:
            if (num5 < ((int) data.gifts.Length))
            {
                goto Label_00FD;
            }
        Label_013E:
            if (num4 < 2)
            {
                goto Label_019B;
            }
            icon = obj3.GetComponent<MailIcon>();
            if ((icon != null) == null)
            {
                goto Label_0600;
            }
            if ((icon.CurrentIcon != null) == null)
            {
                goto Label_017B;
            }
            icon.CurrentIcon.SetActive(0);
        Label_017B:
            icon.CurrentIcon = icon.SetIconTemplate;
            icon.CurrentIcon.SetActive(1);
            goto Label_0600;
        Label_019B:
            icon2 = obj3.GetComponent<MailIcon>();
            num6 = 0;
            goto Label_05F0;
        Label_01AC:
            data3 = data.gifts[num6];
            if (data3.NotSet == null)
            {
                goto Label_01C9;
            }
            goto Label_05EA;
        Label_01C9:
            if (data3.CheckGiftTypeIncluded(0x2781L) == null)
            {
                goto Label_024F;
            }
            data4 = new ItemData();
            data4.Setup(0L, data3.iname, data3.num);
            DataSource.Bind<ItemData>(obj3, data4);
            if ((icon2 != null) == null)
            {
                goto Label_0600;
            }
            if ((icon2.CurrentIcon != null) == null)
            {
                goto Label_022F;
            }
            icon2.CurrentIcon.SetActive(0);
        Label_022F:
            icon2.CurrentIcon = icon2.ItemIconTemplate;
            icon2.CurrentIcon.SetActive(1);
            goto Label_0600;
        Label_024F:
            if (data3.CheckGiftTypeIncluded(0x40L) == null)
            {
                goto Label_02C3;
            }
            data5 = data3.CreateArtifactData();
            if (data5 == null)
            {
                goto Label_0277;
            }
            DataSource.Bind<ArtifactData>(obj3, data5);
        Label_0277:
            if ((icon2 != null) == null)
            {
                goto Label_0600;
            }
            if ((icon2.CurrentIcon != null) == null)
            {
                goto Label_02A3;
            }
            icon2.CurrentIcon.SetActive(0);
        Label_02A3:
            icon2.CurrentIcon = icon2.ArtifactIconTemplate;
            icon2.CurrentIcon.SetActive(1);
            goto Label_0600;
        Label_02C3:
            if (data3.CheckGiftTypeIncluded(0x800L) == null)
            {
                goto Label_035C;
            }
            param = MonoSingleton<GameManager>.Instance.GetAwardParam(data3.iname);
            data6 = new ItemData();
            data6.Setup(0L, param.ToItemParam(), data3.num);
            DataSource.Bind<ItemData>(obj3, data6);
            if ((icon2 != null) == null)
            {
                goto Label_0600;
            }
            if ((icon2.CurrentIcon != null) == null)
            {
                goto Label_033C;
            }
            icon2.CurrentIcon.SetActive(0);
        Label_033C:
            icon2.CurrentIcon = icon2.ItemIconTemplate;
            icon2.CurrentIcon.SetActive(1);
            goto Label_0600;
        Label_035C:
            if (data3.CheckGiftTypeIncluded(4L) == null)
            {
                goto Label_03B6;
            }
            if ((icon2 != null) == null)
            {
                goto Label_0600;
            }
            if ((icon2.CurrentIcon != null) == null)
            {
                goto Label_0396;
            }
            icon2.CurrentIcon.SetActive(0);
        Label_0396:
            icon2.CurrentIcon = icon2.CoinIconTemplate;
            icon2.CurrentIcon.SetActive(1);
            goto Label_0600;
        Label_03B6:
            if (data3.CheckGiftTypeIncluded(2L) == null)
            {
                goto Label_0410;
            }
            if ((icon2 != null) == null)
            {
                goto Label_0600;
            }
            if ((icon2.CurrentIcon != null) == null)
            {
                goto Label_03F0;
            }
            icon2.CurrentIcon.SetActive(0);
        Label_03F0:
            icon2.CurrentIcon = icon2.GoldIconTemplate;
            icon2.CurrentIcon.SetActive(1);
            goto Label_0600;
        Label_0410:
            if (data3.CheckGiftTypeIncluded(8L) == null)
            {
                goto Label_046A;
            }
            if ((icon2 != null) == null)
            {
                goto Label_05EA;
            }
            if ((icon2.CurrentIcon != null) == null)
            {
                goto Label_044A;
            }
            icon2.CurrentIcon.SetActive(0);
        Label_044A:
            icon2.CurrentIcon = icon2.ArenaCoinIconTemplate;
            icon2.CurrentIcon.SetActive(1);
            goto Label_05EA;
        Label_046A:
            if (data3.CheckGiftTypeIncluded(0x10L) == null)
            {
                goto Label_04C5;
            }
            if ((icon2 != null) == null)
            {
                goto Label_0600;
            }
            if ((icon2.CurrentIcon != null) == null)
            {
                goto Label_04A5;
            }
            icon2.CurrentIcon.SetActive(0);
        Label_04A5:
            icon2.CurrentIcon = icon2.MultiCoinIconTemplate;
            icon2.CurrentIcon.SetActive(1);
            goto Label_0600;
        Label_04C5:
            if (data3.CheckGiftTypeIncluded(0x20L) == null)
            {
                goto Label_0520;
            }
            if ((icon2 != null) == null)
            {
                goto Label_0600;
            }
            if ((icon2.CurrentIcon != null) == null)
            {
                goto Label_0500;
            }
            icon2.CurrentIcon.SetActive(0);
        Label_0500:
            icon2.CurrentIcon = icon2.KakeraCoinIconTemplate;
            icon2.CurrentIcon.SetActive(1);
            goto Label_0600;
        Label_0520:
            if (data3.CheckGiftTypeIncluded(0x1000L) == null)
            {
                goto Label_05EA;
            }
            if ((icon2 != null) == null)
            {
                goto Label_0600;
            }
            if (MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(data3.ConceptCardIname) != null)
            {
                goto Label_0579;
            }
            DebugUtility.LogError(string.Format("MasterParam.ConceptCardParamに「{0}」が存在しない", data3.ConceptCardIname));
            goto Label_0600;
        Label_0579:
            data7 = ConceptCardData.CreateConceptCardDataForDisplay(data3.ConceptCardIname);
            if ((icon2.CurrentIcon != null) == null)
            {
                goto Label_05A6;
            }
            icon2.CurrentIcon.SetActive(0);
        Label_05A6:
            icon2.CurrentIcon = icon2.ConceptCardIconTemplate;
            icon2.CurrentIcon.SetActive(1);
            icon3 = icon2.CurrentIcon.GetComponent<ConceptCardIcon>();
            if ((icon3 != null) == null)
            {
                goto Label_0600;
            }
            icon3.Setup(data7);
            goto Label_0600;
        Label_05EA:
            num6 += 1;
        Label_05F0:
            if (num6 < ((int) data.gifts.Length))
            {
                goto Label_01AC;
            }
        Label_0600:
            item = obj3.GetComponent<MailListItem>();
            if ((item != null) == null)
            {
                goto Label_0639;
            }
            item.Set(data.IsPeriod, data.IsReadMail(), data.post_at, data.read);
        Label_0639:
            num3 += 1;
        Label_063F:
            if (num3 < this.mMailListItems.Count)
            {
                goto Label_009B;
            }
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }
    }
}

