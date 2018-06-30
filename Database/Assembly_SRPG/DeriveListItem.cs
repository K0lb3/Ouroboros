namespace SRPG
{
    using System;
    using UnityEngine;

    public class DeriveListItem : MonoBehaviour
    {
        [SerializeField, HeaderBar("▼派生先のスキル/アビリティの罫線")]
        private RectTransform m_DeriveLineV;
        [SerializeField]
        private RectTransform m_DeriveLineH;

        public DeriveListItem()
        {
            base..ctor();
            return;
        }

        public void SetLineActive(bool lineActive, bool verticalActive)
        {
            GameUtility.SetGameObjectActive(this.m_DeriveLineH, lineActive);
            if (lineActive == null)
            {
                goto Label_0023;
            }
            GameUtility.SetGameObjectActive(this.m_DeriveLineV, verticalActive);
            goto Label_002F;
        Label_0023:
            GameUtility.SetGameObjectActive(this.m_DeriveLineV, 0);
        Label_002F:
            return;
        }
    }
}

