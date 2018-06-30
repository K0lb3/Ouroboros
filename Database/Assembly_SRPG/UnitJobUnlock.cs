namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("UI/UnitJobUnlock")]
    public class UnitJobUnlock : MonoBehaviour
    {
        public GameObject JobIcon;
        public Text JobName;

        public UnitJobUnlock()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            UnitData data;
            JobData data2;
            int num;
            string str;
            IconLoader loader;
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            data2 = null;
            num = 0;
            goto Label_005E;
        Label_0023:
            if (data.Jobs[num] == null)
            {
                goto Label_005A;
            }
            if (data.Jobs[num].UniqueID == GlobalVars.SelectedJobUniqueID)
            {
                goto Label_0051;
            }
            goto Label_005A;
        Label_0051:
            data2 = data.Jobs[num];
        Label_005A:
            num += 1;
        Label_005E:
            if (num < ((int) data.Jobs.Length))
            {
                goto Label_0023;
            }
            if (data2 != null)
            {
                goto Label_0073;
            }
            return;
        Label_0073:
            if ((this.JobIcon != null) == null)
            {
                goto Label_00C9;
            }
            str = AssetPath.JobIconSmall((data2 == null) ? null : data2.Param);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00C9;
            }
            loader = GameUtility.RequireComponent<IconLoader>(this.JobIcon);
            if ((loader != null) == null)
            {
                goto Label_00C9;
            }
            loader.ResourcePath = str;
        Label_00C9:
            if ((this.JobName != null) == null)
            {
                goto Label_00EB;
            }
            this.JobName.set_text(data2.Name);
        Label_00EB:
            return;
        }
    }
}

