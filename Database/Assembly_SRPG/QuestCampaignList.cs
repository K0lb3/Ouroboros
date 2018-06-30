namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class QuestCampaignList : MonoBehaviour
    {
        public GameObject TemplateExpPlayer;
        public GameObject TemplateExpUnit;
        public GameObject TemplateExpUnitAll;
        public GameObject TemplateDrapItem;
        public GameObject TemplateAp;
        public GameObject TemplateTrustUp;
        public GameObject TemplateTrustIncidence;
        public GameObject TemplateTrustSpecific;
        [Space(10f)]
        public Text TextConsumeAp;
        public Color TextConsumeApColor;

        public QuestCampaignList()
        {
            this.TextConsumeApColor = Color.get_white();
            base..ctor();
            return;
        }

        public void RefreshIcons()
        {
            QuestParam param;
            QuestCampaignData[] dataArray;
            bool flag;
            int num;
            GameObject obj2;
            QuestCampaignData data;
            QuestCampaignValueTypes types;
            this.ResetTemplateActive();
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_0026;
            }
            if (param.type != 7)
            {
                goto Label_0026;
            }
            return;
        Label_0026:
            dataArray = DataSource.FindDataOfClass<QuestCampaignData[]>(base.get_gameObject(), null);
            if (dataArray == null)
            {
                goto Label_0041;
            }
            if (((int) dataArray.Length) != null)
            {
                goto Label_0042;
            }
        Label_0041:
            return;
        Label_0042:
            flag = 0;
            num = 0;
            goto Label_0159;
        Label_004B:
            if (num != 2)
            {
                goto Label_0057;
            }
            goto Label_0162;
        Label_0057:
            obj2 = null;
            data = dataArray[num];
            switch (data.type)
            {
                case 0:
                    goto Label_0094;

                case 1:
                    goto Label_00A1;

                case 2:
                    goto Label_00CC;

                case 3:
                    goto Label_00CC;

                case 4:
                    goto Label_00E1;

                case 5:
                    goto Label_0110;

                case 6:
                    goto Label_011D;

                case 7:
                    goto Label_012A;
            }
            goto Label_0137;
        Label_0094:
            obj2 = this.TemplateExpPlayer;
            goto Label_0137;
        Label_00A1:
            if (string.IsNullOrEmpty(data.unit) == null)
            {
                goto Label_00BF;
            }
            obj2 = this.TemplateExpUnitAll;
            goto Label_00C7;
        Label_00BF:
            obj2 = this.TemplateExpUnit;
        Label_00C7:
            goto Label_0137;
        Label_00CC:
            if (flag != null)
            {
                goto Label_0137;
            }
            obj2 = this.TemplateDrapItem;
            flag = 1;
            goto Label_0137;
        Label_00E1:
            obj2 = this.TemplateAp;
            if ((this.TextConsumeAp != null) == null)
            {
                goto Label_0137;
            }
            this.TextConsumeAp.set_color(this.TextConsumeApColor);
            goto Label_0137;
        Label_0110:
            obj2 = this.TemplateTrustUp;
            goto Label_0137;
        Label_011D:
            obj2 = this.TemplateTrustIncidence;
            goto Label_0137;
        Label_012A:
            obj2 = this.TemplateTrustSpecific;
        Label_0137:
            if ((obj2 != null) == null)
            {
                goto Label_0155;
            }
            DataSource.Bind<QuestCampaignData>(obj2, data);
            obj2.SetActive(1);
        Label_0155:
            num += 1;
        Label_0159:
            if (num < ((int) dataArray.Length))
            {
                goto Label_004B;
            }
        Label_0162:
            if (base.get_gameObject().get_activeSelf() != null)
            {
                goto Label_017E;
            }
            base.get_gameObject().SetActive(1);
        Label_017E:
            return;
        }

        private void ResetTemplateActive()
        {
            if ((this.TemplateExpPlayer != null) == null)
            {
                goto Label_001D;
            }
            this.TemplateExpPlayer.SetActive(0);
        Label_001D:
            if ((this.TemplateExpUnit != null) == null)
            {
                goto Label_003A;
            }
            this.TemplateExpUnit.SetActive(0);
        Label_003A:
            if ((this.TemplateExpUnitAll != null) == null)
            {
                goto Label_0057;
            }
            this.TemplateExpUnitAll.SetActive(0);
        Label_0057:
            if ((this.TemplateDrapItem != null) == null)
            {
                goto Label_0074;
            }
            this.TemplateDrapItem.SetActive(0);
        Label_0074:
            if ((this.TemplateAp != null) == null)
            {
                goto Label_0091;
            }
            this.TemplateAp.SetActive(0);
        Label_0091:
            if ((this.TemplateTrustUp != null) == null)
            {
                goto Label_00AE;
            }
            this.TemplateTrustUp.SetActive(0);
        Label_00AE:
            if ((this.TemplateTrustIncidence != null) == null)
            {
                goto Label_00CB;
            }
            this.TemplateTrustIncidence.SetActive(0);
        Label_00CB:
            if ((this.TemplateTrustSpecific != null) == null)
            {
                goto Label_00E8;
            }
            this.TemplateTrustSpecific.SetActive(0);
        Label_00E8:
            return;
        }

        private void Start()
        {
            this.RefreshIcons();
            return;
        }
    }
}

