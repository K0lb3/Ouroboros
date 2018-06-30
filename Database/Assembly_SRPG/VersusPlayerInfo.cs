namespace SRPG
{
    using System;
    using UnityEngine;

    public class VersusPlayerInfo : MonoBehaviour
    {
        public GameObject template;
        public GameObject parent;

        public VersusPlayerInfo()
        {
            base..ctor();
            return;
        }

        private void RefreshData()
        {
            JSON_MyPhotonPlayerParam param;
            int num;
            GameObject obj2;
            param = GlobalVars.SelectedMultiPlayerParam;
            if (param == null)
            {
                goto Label_0080;
            }
            num = 0;
            goto Label_0072;
        Label_0013:
            if (param.units[num] != null)
            {
                goto Label_0025;
            }
            goto Label_006E;
        Label_0025:
            obj2 = Object.Instantiate<GameObject>(this.template);
            if ((obj2 != null) == null)
            {
                goto Label_0050;
            }
            DataSource.Bind<UnitData>(obj2, param.units[num].unit);
        Label_0050:
            obj2.SetActive(1);
            obj2.get_transform().SetParent(this.parent.get_transform(), 0);
        Label_006E:
            num += 1;
        Label_0072:
            if (num < ((int) param.units.Length))
            {
                goto Label_0013;
            }
        Label_0080:
            return;
        }

        private void Start()
        {
            if ((this.template == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.RefreshData();
            return;
        }
    }
}

