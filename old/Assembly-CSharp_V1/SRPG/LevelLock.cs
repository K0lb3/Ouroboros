// Decompiled with JetBrains decompiler
// Type: SRPG.LevelLock
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (Selectable))]
  public class LevelLock : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
  {
    private static List<LevelLock> mInstances = new List<LevelLock>();
    public UnlockTargets Condition;
    public Text ConditionText;
    public GameObject ShowLocked;
    public GameObject ShowUnlocked;
    public bool ToggleInteractable;
    private int mUnlockLevel;
    private int mUnlockVipRank;

    public LevelLock()
    {
      base.\u002Ector();
    }

    public static void UpdateLockStates()
    {
      for (int index = 0; index < LevelLock.mInstances.Count; ++index)
        LevelLock.mInstances[index].UpdateLockState();
    }

    private void OnEnable()
    {
      LevelLock.mInstances.Add(this);
    }

    private void OnDisable()
    {
      LevelLock.mInstances.Remove(this);
    }

    private void Start()
    {
      foreach (UnlockParam unlock in MonoSingleton<GameManager>.Instance.MasterParam.Unlocks)
      {
        if (unlock != null && unlock.UnlockTarget == this.Condition)
        {
          this.mUnlockLevel = unlock.PlayerLevel;
          this.mUnlockVipRank = unlock.VipRank;
          break;
        }
      }
      this.UpdateLockState();
    }

    private void UpdateLockState()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool flag = MonoSingleton<GameManager>.Instance.Player.CheckUnlock(this.Condition);
      if (this.ToggleInteractable)
      {
        Selectable component = (Selectable) ((Component) this).GetComponent<Selectable>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.set_interactable(flag);
      }
      if (Object.op_Inequality((Object) this.ShowUnlocked, (Object) null))
        this.ShowUnlocked.SetActive(flag);
      if (Object.op_Inequality((Object) this.ShowLocked, (Object) null))
        this.ShowLocked.SetActive(!flag);
      if (!Object.op_Inequality((Object) this.ConditionText, (Object) null))
        return;
      if (this.mUnlockLevel > 0 && player.Lv < this.mUnlockLevel)
      {
        this.ConditionText.set_text(string.Format(LocalizedText.Get("sys.UNLOCK_LV"), (object) this.mUnlockLevel));
      }
      else
      {
        if (this.mUnlockVipRank <= 0 || player.VipRank >= this.mUnlockVipRank)
          return;
        this.ConditionText.set_text(string.Format(LocalizedText.Get("sys.UNLOCK_VIP"), (object) this.mUnlockVipRank));
      }
    }

    public static bool ShowLockMessage(int playerLv, int playerVipRank, UnlockTargets target)
    {
      foreach (UnlockParam unlock in MonoSingleton<GameManager>.Instance.MasterParam.Unlocks)
      {
        if (unlock != null && unlock.UnlockTarget == target)
          return LevelLock.ShowLockMessage(playerLv, unlock.PlayerLevel, playerVipRank, unlock.VipRank);
      }
      return false;
    }

    public static bool ShowLockMessage(int playerLv, int reqLv, int vipRank, int reqVipRank)
    {
      if (reqLv > playerLv)
      {
        UIUtility.SystemMessage((string) null, string.Format(LocalizedText.Get("sys.UNLOCK_REQLV"), (object) reqLv), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        return true;
      }
      if (reqVipRank <= vipRank)
        return false;
      UIUtility.SystemMessage((string) null, string.Format(LocalizedText.Get("sys.UNLOCK_REQVIP"), (object) reqVipRank), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      LevelLock.ShowLockMessage(player.Lv, player.VipRank, this.Condition);
      ((AbstractEventData) eventData).Use();
    }
  }
}
