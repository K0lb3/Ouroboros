// Decompiled with JetBrains decompiler
// Type: SRPG.VersusFriendMatch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class VersusFriendMatch : MonoBehaviour
  {
    public VersusFriendMatch()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int maxUnit = player.Partys[7].MAX_UNIT;
      for (int idx = 0; idx < maxUnit; ++idx)
      {
        if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.VERSUS_ID_KEY + (object) idx))
          player.SetVersusPlacement(PlayerPrefsUtility.VERSUS_ID_KEY + (object) idx, idx);
      }
      player.SavePlayerPrefs();
    }

    public void OnApplicationFocus(bool hasFocus)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (hasFocus)
      {
        if (!Object.op_Inequality((Object) instance, (Object) null) || !instance.IsDisconnected() || !GlobalVars.VersusRoomReuse)
          return;
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "RECONNECT");
      }
      else
      {
        if (!GlobalVars.VersusRoomReuse || !instance.IsConnected())
          return;
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "REUSE_ROOM");
      }
    }

    public void OnApplicationPause(bool pauseStatus)
    {
      this.OnApplicationFocus(!pauseStatus);
    }
  }
}
