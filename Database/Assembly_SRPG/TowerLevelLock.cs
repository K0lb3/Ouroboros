// Decompiled with JetBrains decompiler
// Type: SRPG.TowerLevelLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (Selectable))]
  public class TowerLevelLock : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
  {
    public UnityEngine.UI.Text ConditionText;
    public GameObject ShowLocked;
    public GameObject ShowUnlocked;
    public bool ToggleInteractable;
    private int mUnlockLevel;
    private TowerParam Param;

    public TowerLevelLock()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.Param = DataSource.FindDataOfClass<TowerParam>(((Component) this).get_gameObject(), (TowerParam) null);
      this.mUnlockLevel = (int) this.Param.unlock_level;
      this.UpdateLockState();
    }

    public bool GetIsUnlock()
    {
      if (this.Param == null)
        return false;
      return this.Param.is_unlock;
    }

    private void UpdateLockState()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool isUnlock = this.GetIsUnlock();
      if (this.ToggleInteractable)
      {
        Selectable component = (Selectable) ((Component) this).GetComponent<Selectable>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.set_interactable(isUnlock);
      }
      if (Object.op_Inequality((Object) this.ShowUnlocked, (Object) null))
        this.ShowUnlocked.SetActive(isUnlock);
      if (Object.op_Inequality((Object) this.ShowLocked, (Object) null))
        this.ShowLocked.SetActive(!isUnlock);
      if (!Object.op_Inequality((Object) this.ConditionText, (Object) null) || this.mUnlockLevel <= 0 || player.Lv >= this.mUnlockLevel)
        return;
      this.ConditionText.set_text(string.Format(LocalizedText.Get("sys.UNLOCK_LV"), (object) this.mUnlockLevel));
    }

    public string ShowLockMessage(int playerLv, int reqLv)
    {
      return string.Format(LocalizedText.Get("sys.UNLOCK_COND_REQLV"), (object) reqLv);
    }

    public string ShowProgLockMessage()
    {
      if (this.Param == null || this.Param.is_unlock)
        return string.Empty;
      TowerFloorParam towerFloorParam = this.GetTowerFloorParam(this.Param.unlock_quest);
      if (towerFloorParam == null)
        return string.Empty;
      TowerParam towerParam = this.GetTowerParam(towerFloorParam.tower_id);
      if (towerParam == null)
        return string.Empty;
      return LocalizedText.Get("sys.UNLOCK_COND_TOWER_PROG", (object) towerParam.name, (object) towerFloorParam.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = this.ShowLockMessage(player.Lv, this.mUnlockLevel);
      string str2 = this.ShowProgLockMessage();
      if (this.Param != null && !this.Param.is_unlock && (!string.IsNullOrEmpty(str1) || !string.IsNullOrEmpty(str2)))
      {
        stringBuilder.Append(LocalizedText.Get("sys.UNLOCK_COND"));
        stringBuilder.Append(str1);
        stringBuilder.Append("\n");
        stringBuilder.Append(str2);
        string msg = stringBuilder.ToString();
        if (!string.IsNullOrEmpty(msg))
          UIUtility.SystemMessage((string) null, msg, (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      ((AbstractEventData) eventData).Use();
    }

    public TowerParam GetTowerParam(string tower_id)
    {
      return MonoSingleton<GameManager>.Instance.FindTower(tower_id);
    }

    public TowerFloorParam GetTowerFloorParam(string iname)
    {
      return MonoSingleton<GameManager>.Instance.FindTowerFloor(iname);
    }
  }
}
