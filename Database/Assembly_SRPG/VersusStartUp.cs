namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class VersusStartUp : MonoBehaviour
    {
        public VersusStartUp()
        {
            base..ctor();
            return;
        }

        private unsafe void Start()
        {
            int num;
            List<PartyEditData> list;
            PartyEditData data;
            UnitData[] dataArray;
            int num2;
            list = PartyUtility.LoadTeamPresets(7, &num, 0);
            if (list == null)
            {
                goto Label_0081;
            }
            if (list.Count <= num)
            {
                goto Label_0081;
            }
            data = list[num];
            dataArray = new UnitData[data.PartyData.MAX_UNIT];
            num2 = 0;
            goto Label_0050;
        Label_003D:
            dataArray[num2] = data.Units[num2];
            num2 += 1;
        Label_0050:
            if (num2 >= ((int) data.Units.Length))
            {
                goto Label_0071;
            }
            if (num2 < data.PartyData.VSWAITMEMBER_START)
            {
                goto Label_003D;
            }
        Label_0071:
            data.SetUnits(dataArray);
            PartyUtility.SaveTeamPresets(8, num, list, 0);
        Label_0081:
            return;
        }
    }
}

