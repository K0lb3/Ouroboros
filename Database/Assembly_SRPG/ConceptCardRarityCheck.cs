namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ConceptCardRarityCheck : MonoBehaviour
    {
        [SerializeField]
        private GameObject mCardObjectTemplate;
        [SerializeField]
        private RectTransform mCardObjectParent;
        [SerializeField]
        private LText mLText;
        [SerializeField]
        private GameObject mButtonEnhance;
        [SerializeField]
        private GameObject mButtonSell;

        public ConceptCardRarityCheck()
        {
            base..ctor();
            return;
        }

        private unsafe void Start()
        {
            ConceptCardManager manager;
            ConceptCardData data;
            List<ConceptCardData>.Enumerator enumerator;
            GameObject obj2;
            Transform transform;
            ConceptCardIcon icon;
            if ((this.mCardObjectTemplate == null) == null)
            {
                goto Label_002D;
            }
            if ((this.mCardObjectParent == null) == null)
            {
                goto Label_002D;
            }
            Debug.LogWarning("mCardObject is null");
            return;
        Label_002D:
            this.mCardObjectTemplate.SetActive(0);
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_004C;
            }
            return;
        Label_004C:
            this.mLText.set_text(string.Format(LocalizedText.Get(this.mLText.get_text()), &manager.CostConceptCardRare.ToString()));
            if (manager.IsDetailActive == null)
            {
                goto Label_009F;
            }
            this.mButtonEnhance.SetActive(1);
            this.mButtonSell.SetActive(0);
            goto Label_00D2;
        Label_009F:
            if (manager.IsSellListActive == null)
            {
                goto Label_00C7;
            }
            this.mButtonEnhance.SetActive(0);
            this.mButtonSell.SetActive(1);
            goto Label_00D2;
        Label_00C7:
            Debug.LogWarning("Must be from Sell or Enhance");
            return;
        Label_00D2:
            enumerator = manager.SelectedMaterials.GetList().GetEnumerator();
        Label_00E3:
            try
            {
                goto Label_014E;
            Label_00E8:
                data = &enumerator.Current;
                if ((data.Rarity + 1) < manager.CostConceptCardRare)
                {
                    goto Label_014E;
                }
                obj2 = Object.Instantiate<GameObject>(this.mCardObjectTemplate);
                obj2.get_transform().SetParent(this.mCardObjectParent, 0);
                icon = obj2.GetComponent<ConceptCardIcon>();
                if ((icon != null) == null)
                {
                    goto Label_0147;
                }
                icon.Setup(data);
            Label_0147:
                obj2.SetActive(1);
            Label_014E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00E8;
                }
                goto Label_016B;
            }
            finally
            {
            Label_015F:
                ((List<ConceptCardData>.Enumerator) enumerator).Dispose();
            }
        Label_016B:
            return;
        }
    }
}

