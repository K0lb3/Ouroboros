namespace SRPG
{
    using System;
    using UnityEngine;

    public class RoomPlayerViewer : MonoBehaviour
    {
        public GameObject[] PartyUnitSlots;

        public RoomPlayerViewer()
        {
            this.PartyUnitSlots = new GameObject[3];
            base..ctor();
            return;
        }

        private void Start()
        {
            JSON_MyPhotonPlayerParam param;
            int num;
            int num2;
            param = GlobalVars.SelectedMultiPlayerParam;
            if (param == null)
            {
                goto Label_0022;
            }
            if (param.units == null)
            {
                goto Label_0022;
            }
            if (this.PartyUnitSlots != null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            num = 0;
            goto Label_0079;
        Label_002A:
            num2 = 0;
            goto Label_0067;
        Label_0031:
            if (param.units[num2].slotID != num)
            {
                goto Label_0063;
            }
            DataSource.Bind<UnitData>(this.PartyUnitSlots[num], param.units[num2].unit);
            goto Label_0075;
        Label_0063:
            num2 += 1;
        Label_0067:
            if (num2 < ((int) param.units.Length))
            {
                goto Label_0031;
            }
        Label_0075:
            num += 1;
        Label_0079:
            if (num < ((int) this.PartyUnitSlots.Length))
            {
                goto Label_002A;
            }
            DataSource.Bind<JSON_MyPhotonPlayerParam>(base.get_gameObject(), param);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }
    }
}

