namespace SRPG
{
    using System;
    using UnityEngine;

    public class UnitTobiraParamLevel : MonoBehaviour
    {
        [SerializeField]
        private GameObject OnGO;
        [SerializeField]
        private GameObject OffGO;
        [SerializeField]
        private int OwnLevel;

        public UnitTobiraParamLevel()
        {
            base..ctor();
            return;
        }

        public void Refresh(int targetLevel)
        {
            bool flag;
            if ((this.OnGO == null) != null)
            {
                goto Label_0022;
            }
            if ((this.OffGO == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            flag = (targetLevel < this.OwnLevel) == 0;
            this.OnGO.SetActive(flag);
            this.OffGO.SetActive(flag == 0);
            return;
        }
    }
}

