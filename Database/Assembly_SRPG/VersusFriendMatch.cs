namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class VersusFriendMatch : MonoBehaviour
    {
        public VersusFriendMatch()
        {
            base..ctor();
            return;
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            MyPhoton photon;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (hasFocus == null)
            {
                goto Label_003D;
            }
            if ((photon != null) == null)
            {
                goto Label_005D;
            }
            if (photon.IsDisconnected() == null)
            {
                goto Label_005D;
            }
            if (GlobalVars.VersusRoomReuse == null)
            {
                goto Label_005D;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "RECONNECT");
            goto Label_005D;
        Label_003D:
            if (GlobalVars.VersusRoomReuse == null)
            {
                goto Label_005D;
            }
            if (photon.IsConnected() == null)
            {
                goto Label_005D;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "REUSE_ROOM");
        Label_005D:
            return;
        }

        public void OnApplicationPause(bool pauseStatus)
        {
            this.OnApplicationFocus(pauseStatus == 0);
            return;
        }

        private void Start()
        {
            GameManager manager;
            PlayerData data;
            int num;
            int num2;
            data = MonoSingleton<GameManager>.Instance.Player;
            num = data.Partys[7].MAX_UNIT;
            num2 = 0;
            goto Label_005B;
        Label_0026:
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.VERSUS_ID_KEY + ((int) num2)) != null)
            {
                goto Label_0057;
            }
            data.SetVersusPlacement(PlayerPrefsUtility.VERSUS_ID_KEY + ((int) num2), num2);
        Label_0057:
            num2 += 1;
        Label_005B:
            if (num2 < num)
            {
                goto Label_0026;
            }
            data.SavePlayerPrefs();
            return;
        }
    }
}

