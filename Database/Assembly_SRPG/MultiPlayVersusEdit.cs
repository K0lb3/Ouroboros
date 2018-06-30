namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class MultiPlayVersusEdit : MonoBehaviour
    {
        public MultiPlayVersusEdit()
        {
            base..ctor();
            return;
        }

        public void Set()
        {
            GameManager manager;
            PlayerData data;
            int num;
            string str;
            int num2;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (GlobalVars.SelectedMultiPlayVersusType != 3)
            {
                goto Label_0036;
            }
            num = data.Partys[10].MAX_UNIT;
            str = PlayerPrefsUtility.RANKMATCH_ID_KEY;
            goto Label_004E;
        Label_0036:
            num = data.Partys[7].MAX_UNIT;
            str = PlayerPrefsUtility.VERSUS_ID_KEY;
        Label_004E:
            num2 = 0;
            goto Label_0088;
        Label_0056:
            if (PlayerPrefsUtility.HasKey(str + ((int) num2)) != null)
            {
                goto Label_0082;
            }
            data.SetVersusPlacement(str + ((int) num2), num2);
        Label_0082:
            num2 += 1;
        Label_0088:
            if (num2 < num)
            {
                goto Label_0056;
            }
            PlayerPrefsUtility.Save();
            return;
        }

        private void Start()
        {
            this.Set();
            return;
        }
    }
}

