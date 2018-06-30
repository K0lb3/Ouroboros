namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class VersusSeasonRewardInfo : MonoBehaviour
    {
        public GameObject template;
        public GameObject parent;
        private List<GameObject> mItems;

        public VersusSeasonRewardInfo()
        {
            this.mItems = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Refresh()
        {
            VersusTowerParam param;
            GameObject obj2;
            int num;
            GameObject obj3;
            VersusTowerRewardItem item;
            if ((this.template == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            param = DataSource.FindDataOfClass<VersusTowerParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_00EC;
            }
            goto Label_0076;
        Label_002A:
            obj2 = Object.Instantiate<GameObject>(this.template);
            if ((obj2 != null) == null)
            {
                goto Label_0076;
            }
            if ((this.parent != null) == null)
            {
                goto Label_006A;
            }
            obj2.get_transform().SetParent(this.parent.get_transform(), 0);
        Label_006A:
            this.mItems.Add(obj2);
        Label_0076:
            if (this.mItems.Count < ((int) param.SeasonIteminame.Length))
            {
                goto Label_002A;
            }
            num = 0;
            goto Label_00DE;
        Label_0095:
            obj3 = this.mItems[num];
            if ((obj3 != null) == null)
            {
                goto Label_00DA;
            }
            DataSource.Bind<VersusTowerParam>(obj3, param);
            obj3.SetActive(1);
            item = obj3.GetComponent<VersusTowerRewardItem>();
            if ((item != null) == null)
            {
                goto Label_00DA;
            }
            item.Refresh(1, num);
        Label_00DA:
            num += 1;
        Label_00DE:
            if (num < ((int) param.SeasonIteminame.Length))
            {
                goto Label_0095;
            }
        Label_00EC:
            this.template.SetActive(0);
            return;
        }
    }
}

