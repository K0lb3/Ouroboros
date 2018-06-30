namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ConceptCardNotSaleCheck : MonoBehaviour
    {
        [SerializeField]
        private GameObject mCardObjectTemplate;
        [SerializeField]
        private RectTransform mCardObjectParent;

        public ConceptCardNotSaleCheck()
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
            enumerator = manager.SelectedMaterials.GetList().GetEnumerator();
        Label_005D:
            try
            {
                goto Label_00C0;
            Label_0062:
                data = &enumerator.Current;
                if (data.Param.not_sale == null)
                {
                    goto Label_00C0;
                }
                obj2 = Object.Instantiate<GameObject>(this.mCardObjectTemplate);
                obj2.get_transform().SetParent(this.mCardObjectParent, 0);
                icon = obj2.GetComponent<ConceptCardIcon>();
                if ((icon != null) == null)
                {
                    goto Label_00B9;
                }
                icon.Setup(data);
            Label_00B9:
                obj2.SetActive(1);
            Label_00C0:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0062;
                }
                goto Label_00DD;
            }
            finally
            {
            Label_00D1:
                ((List<ConceptCardData>.Enumerator) enumerator).Dispose();
            }
        Label_00DD:
            return;
        }
    }
}

