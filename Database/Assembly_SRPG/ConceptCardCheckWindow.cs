namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ConceptCardCheckWindow : MonoBehaviour
    {
        [SerializeField]
        private RectTransform ListParent;
        [SerializeField]
        private GameObject ListItemTemplate;
        [SerializeField]
        private Text GetExp;
        [SerializeField]
        private Text UsedZenny;
        [SerializeField]
        private Text GetTrust;
        private Dictionary<string, int> mSelectedCard;
        private List<ConceptCardIcon> mConceptCardIcon;

        public ConceptCardCheckWindow()
        {
            this.mSelectedCard = new Dictionary<string, int>();
            this.mConceptCardIcon = new List<ConceptCardIcon>();
            base..ctor();
            return;
        }

        private unsafe void CreateMakeCardIcon()
        {
            ConceptCardManager manager;
            int num;
            ConceptCardData data;
            GameObject obj2;
            ConceptCardIcon icon;
            ConceptCardIcon icon2;
            List<ConceptCardIcon>.Enumerator enumerator;
            string str;
            int num2;
            if ((this.ListParent == null) != null)
            {
                goto Label_0022;
            }
            if ((this.ListItemTemplate == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            this.ListItemTemplate.SetActive(0);
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_0042;
            }
            return;
        Label_0042:
            if (manager.BulkSelectedMaterialList.Count != null)
            {
                goto Label_0053;
            }
            return;
        Label_0053:
            num = 0;
            goto Label_00F2;
        Label_005A:
            data = manager.BulkSelectedMaterialList[num].mSelectedData;
            if (data != null)
            {
                goto Label_0077;
            }
            goto Label_00EE;
        Label_0077:
            obj2 = Object.Instantiate<GameObject>(this.ListItemTemplate);
            obj2.SetActive(1);
            obj2.get_transform().SetParent(this.ListParent, 0);
            icon = obj2.GetComponent<ConceptCardIcon>();
            if ((icon == null) == null)
            {
                goto Label_00B2;
            }
            return;
        Label_00B2:
            icon.Setup(data);
            this.mConceptCardIcon.Add(icon);
            this.mSelectedCard.Add(data.Param.iname, manager.BulkSelectedMaterialList[num].mSelectNum);
        Label_00EE:
            num += 1;
        Label_00F2:
            if (num < manager.BulkSelectedMaterialList.Count)
            {
                goto Label_005A;
            }
            enumerator = this.mConceptCardIcon.GetEnumerator();
        Label_0110:
            try
            {
                goto Label_0149;
            Label_0115:
                icon2 = &enumerator.Current;
                str = icon2.ConceptCard.Param.iname;
                num2 = this.mSelectedCard[str];
                icon2.SetCardNum(num2);
            Label_0149:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0115;
                }
                goto Label_0167;
            }
            finally
            {
            Label_015A:
                ((List<ConceptCardIcon>.Enumerator) enumerator).Dispose();
            }
        Label_0167:
            return;
        }

        private unsafe void SetupText()
        {
            ConceptCardManager manager;
            int num;
            int num2;
            int num3;
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if (manager.BulkSelectedMaterialList.Count != null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            ConceptCardManager.CalcTotalExpTrustMaterialData(&num, &num2);
            if ((this.GetExp != null) == null)
            {
                goto Label_0050;
            }
            this.GetExp.set_text(&num.ToString());
        Label_0050:
            if ((this.GetTrust != null) == null)
            {
                goto Label_0074;
            }
            ConceptCardManager.SubstituteTrustFormat(manager.SelectedConceptCardData, this.GetTrust, num2, 0);
        Label_0074:
            num3 = 0;
            ConceptCardManager.GalcTotalMixZenyMaterialData(&num3);
            if ((this.UsedZenny != null) == null)
            {
                goto Label_00A0;
            }
            this.UsedZenny.set_text(&num3.ToString());
        Label_00A0:
            return;
        }

        private void Start()
        {
            this.CreateMakeCardIcon();
            this.SetupText();
            return;
        }
    }
}

