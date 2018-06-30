namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class GachaHistoryItem : MonoBehaviour
    {
        [SerializeField]
        private GameObject UnitIconObj;
        [SerializeField]
        private GameObject ItemIconObj;
        [SerializeField]
        private GameObject ArtifactIconObj;
        [SerializeField]
        private GameObject ConceptCardIconObj;
        [SerializeField]
        private GameObject TitleText;
        [SerializeField]
        private Transform ListParent;
        private List<GameObject> mItems;

        public GachaHistoryItem()
        {
            this.mItems = new List<GameObject>();
            base..ctor();
            return;
        }

        private unsafe void Initalize()
        {
            object[] objArray1;
            GachaHistoryItemData data;
            int num;
            GachaHistoryData data2;
            GameObject obj2;
            bool flag;
            ConceptCardData data3;
            ConceptCardIcon icon;
            SerializeValueBehaviour behaviour;
            GameObject obj3;
            Text text;
            string str;
            string str2;
            DateTime time;
            data = DataSource.FindDataOfClass<GachaHistoryItemData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_001E;
            }
            DebugUtility.LogError("履歴が存在しません");
            return;
        Label_001E:
            num = ((int) data.historys.Length) - 1;
            goto Label_0283;
        Label_002E:
            data2 = data.historys[num];
            if (data2 != null)
            {
                goto Label_0042;
            }
            goto Label_027F;
        Label_0042:
            obj2 = null;
            flag = 0;
            if (data2.type != 2)
            {
                goto Label_00B9;
            }
            obj2 = Object.Instantiate<GameObject>(this.UnitIconObj);
            if ((obj2 != null) == null)
            {
                goto Label_0235;
            }
            obj2.get_transform().SetParent(this.ListParent, 0);
            obj2.SetActive(1);
            DataSource.Bind<UnitData>(obj2, GachaHistoryWindow.CreateUnitData(data2.unit));
            flag = data2.isNew;
            this.mItems.Add(obj2);
            obj2.get_transform().SetAsFirstSibling();
            goto Label_0235;
        Label_00B9:
            if (data2.type != 1)
            {
                goto Label_0131;
            }
            obj2 = Object.Instantiate<GameObject>(this.ItemIconObj);
            if ((obj2 != null) == null)
            {
                goto Label_0235;
            }
            obj2.get_transform().SetParent(this.ListParent, 0);
            obj2.SetActive(1);
            DataSource.Bind<ItemData>(obj2, GachaHistoryWindow.CreateItemData(data2.item, data2.num));
            flag = data2.isNew;
            this.mItems.Add(obj2);
            obj2.get_transform().SetAsFirstSibling();
            goto Label_0235;
        Label_0131:
            if (data2.type != 3)
            {
                goto Label_01A9;
            }
            obj2 = Object.Instantiate<GameObject>(this.ArtifactIconObj);
            if ((obj2 != null) == null)
            {
                goto Label_0235;
            }
            obj2.get_transform().SetParent(this.ListParent, 0);
            obj2.SetActive(1);
            DataSource.Bind<ArtifactData>(obj2, GachaHistoryWindow.CreateArtifactData(data2.artifact, data2.rarity));
            flag = data2.isNew;
            this.mItems.Add(obj2);
            obj2.get_transform().SetAsFirstSibling();
            goto Label_0235;
        Label_01A9:
            if (data2.type != 4)
            {
                goto Label_0235;
            }
            obj2 = Object.Instantiate<GameObject>(this.ConceptCardIconObj);
            if ((obj2 != null) == null)
            {
                goto Label_0235;
            }
            obj2.get_transform().SetParent(this.ListParent, 0);
            obj2.SetActive(1);
            data3 = ConceptCardData.CreateConceptCardDataForDisplay(data2.conceptcard.iname);
            icon = obj2.GetComponent<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_0216;
            }
            icon.Setup(data3);
        Label_0216:
            flag = data2.isNew;
            this.mItems.Add(obj2);
            obj2.get_transform().SetAsFirstSibling();
        Label_0235:
            if ((obj2 != null) == null)
            {
                goto Label_027F;
            }
            behaviour = obj2.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_027F;
            }
            obj3 = behaviour.list.GetGameObject("new");
            if ((obj3 != null) == null)
            {
                goto Label_027F;
            }
            obj3.SetActive(flag);
        Label_027F:
            num -= 1;
        Label_0283:
            if (num >= 0)
            {
                goto Label_002E;
            }
            if ((this.TitleText != null) == null)
            {
                goto Label_02F4;
            }
            text = this.TitleText.GetComponent<Text>();
            if ((text != null) == null)
            {
                goto Label_02F4;
            }
            str = &data.GetDropAt().ToString("yyyy/MM/dd HH:mm:ss");
            objArray1 = new object[] { str, data.gachaTitle };
            str2 = LocalizedText.Get("sys.TEXT_GACHA_HISTORY_FOOTER", objArray1);
            text.set_text(str2);
        Label_02F4:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Start()
        {
            if ((this.UnitIconObj != null) == null)
            {
                goto Label_001D;
            }
            this.UnitIconObj.SetActive(0);
        Label_001D:
            if ((this.ItemIconObj != null) == null)
            {
                goto Label_003A;
            }
            this.ItemIconObj.SetActive(0);
        Label_003A:
            if ((this.ArtifactIconObj != null) == null)
            {
                goto Label_0057;
            }
            this.ArtifactIconObj.SetActive(0);
        Label_0057:
            if ((this.ConceptCardIconObj != null) == null)
            {
                goto Label_0074;
            }
            this.ConceptCardIconObj.SetActive(0);
        Label_0074:
            if ((this.ListParent == null) == null)
            {
                goto Label_0090;
            }
            DebugUtility.LogError("ListParentが設定されていません");
            return;
        Label_0090:
            this.Initalize();
            return;
        }

        private void Update()
        {
        }
    }
}

