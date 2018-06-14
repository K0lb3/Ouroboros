// Decompiled with JetBrains decompiler
// Type: SRPG.BattleSpeedUpController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleSpeedUpController : MonoBehaviour
  {
    [SerializeField]
    private Toggle speedUp;
    [SerializeField]
    private float speedMul;

    public BattleSpeedUpController()
    {
      base.\u002Ector();
    }

    public bool isSpeedUpAllowed
    {
      get
      {
        if (GameUtility.GetCurrentScene() == GameUtility.EScene.BATTLE_MULTI)
          return false;
        QuestParam currentQuest = SceneBattle.Instance.CurrentQuest;
        return currentQuest != null && currentQuest.CheckAllowedAutoBattle() || currentQuest.type == QuestTypes.Arena;
      }
    }

    private void Start()
    {
      if (this.isSpeedUpAllowed)
      {
        bool enabled = false;
        if (PlayerPrefs.HasKey("SPEED_UP") && PlayerPrefs.GetInt("SPEED_UP") == 1)
          enabled = true;
        ((Component) this.speedUp).get_gameObject().SetActive(true);
        GameUtility.SetToggle(this.speedUp, enabled);
        this.ToggleSpeedUp(enabled);
      }
      else
        ((Component) this.speedUp).get_gameObject().SetActive(false);
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.speedUp.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__1B2)));
    }

    private void ToggleSpeedUp(bool enabled)
    {
      float num = 1f;
      if (enabled)
      {
        num = this.speedMul;
        PlayerPrefs.SetInt("SPEED_UP", 1);
      }
      else
        PlayerPrefs.SetInt("SPEED_UP", 0);
      TimeManager.SetTimeScaleWithPrevSaved(TimeManager.TimeScaleGroups.Game, num);
    }
  }
}
