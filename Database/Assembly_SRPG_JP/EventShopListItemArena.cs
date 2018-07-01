// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopListItemArena
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventShopListItemArena : MonoBehaviour
  {
    public GameObject mLockObject;
    public Text mLockText;

    public EventShopListItemArena()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      Button component = (Button) ((Component) this).GetComponent<Button>();
      if (!Object.op_Implicit((Object) component) || !Object.op_Implicit((Object) this.mLockObject) || !Object.op_Implicit((Object) this.mLockText))
        return;
      if (MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.Arena))
      {
        ((Selectable) component).set_interactable(true);
        this.mLockObject.SetActive(false);
      }
      else
      {
        int num = 0;
        UnlockParam[] unlocks = MonoSingleton<GameManager>.Instance.MasterParam.Unlocks;
        if (unlocks == null)
          return;
        for (int index = 0; index < unlocks.Length; ++index)
        {
          UnlockParam unlockParam = unlocks[index];
          if (unlockParam != null && unlockParam.UnlockTarget == UnlockTargets.Arena)
          {
            num = unlockParam.PlayerLevel;
            break;
          }
        }
        ((Selectable) component).set_interactable(false);
        this.mLockObject.SetActive(true);
        this.mLockText.set_text(LocalizedText.Get("sys.COINLIST_ARENA_LOCK", new object[1]
        {
          (object) num
        }));
      }
    }
  }
}
