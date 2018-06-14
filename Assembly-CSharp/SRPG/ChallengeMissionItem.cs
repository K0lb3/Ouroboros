// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class ChallengeMissionItem : MonoBehaviour
  {
    public ChallengeMissionItem.ButtonObject ButtonNormal;
    public ChallengeMissionItem.ButtonObject ButtonHighlight;
    public ChallengeMissionItem.ButtonObject ButtonSecret;
    public Image ClearBadge;
    public UnityAction OnClick;

    public ChallengeMissionItem()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.Refresh();
    }

    public void Refresh()
    {
      ((Component) this).get_gameObject().SetActive(true);
      ((Component) this.ClearBadge).get_gameObject().SetActive(false);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).get_gameObject(), (TrophyParam) null);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null) || dataOfClass == null)
      {
        ((Component) this.ButtonHighlight.button).get_gameObject().SetActive(false);
        ((Component) this.ButtonNormal.button).get_gameObject().SetActive(false);
        ((Component) this.ButtonSecret.button).get_gameObject().SetActive(true);
      }
      else
      {
        TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(dataOfClass);
        ChallengeMissionItem.State state = ChallengeMissionItem.State.Challenge;
        if (trophyCounter.IsEnded)
          state = ChallengeMissionItem.State.Ended;
        else if (trophyCounter.IsCompleted)
          state = ChallengeMissionItem.State.Clear;
        ChallengeMissionItem.ButtonObject buttonObject;
        if (state == ChallengeMissionItem.State.Clear)
        {
          ((Component) this.ButtonHighlight.button).get_gameObject().SetActive(true);
          ((Component) this.ButtonNormal.button).get_gameObject().SetActive(false);
          ((Component) this.ButtonSecret.button).get_gameObject().SetActive(false);
          buttonObject = this.ButtonHighlight;
        }
        else
        {
          ((Component) this.ButtonHighlight.button).get_gameObject().SetActive(false);
          ((Component) this.ButtonNormal.button).get_gameObject().SetActive(true);
          ((Component) this.ButtonSecret.button).get_gameObject().SetActive(false);
          buttonObject = this.ButtonNormal;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ClearBadge, (UnityEngine.Object) null))
          ((Component) this.ClearBadge).get_gameObject().SetActive(state == ChallengeMissionItem.State.Ended);
        if (buttonObject != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) buttonObject.title, (UnityEngine.Object) null))
          buttonObject.title.set_text(dataOfClass.Name);
        if (buttonObject != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) buttonObject.button, (UnityEngine.Object) null))
        {
          ((UnityEventBase) buttonObject.button.get_onClick()).RemoveAllListeners();
          ((UnityEvent) buttonObject.button.get_onClick()).AddListener(this.OnClick);
          ((Selectable) buttonObject.button).set_interactable(state != ChallengeMissionItem.State.Ended);
        }
        if (buttonObject != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) buttonObject.reward, (UnityEngine.Object) null))
        {
          if (dataOfClass.Gold != 0)
            buttonObject.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) dataOfClass.Gold));
          else if (dataOfClass.Exp != 0)
            buttonObject.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) dataOfClass.Exp));
          else if (dataOfClass.Coin != 0)
            buttonObject.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) dataOfClass.Coin));
          else if (dataOfClass.Stamina != 0)
            buttonObject.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) dataOfClass.Stamina));
          else if (dataOfClass.Items != null && dataOfClass.Items.Length > 0)
          {
            ItemParam itemParam = instanceDirect.GetItemParam(dataOfClass.Items[0].iname);
            if (itemParam != null)
              buttonObject.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM"), (object) itemParam.name, (object) dataOfClass.Items[0].Num));
          }
        }
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) buttonObject.icon, (UnityEngine.Object) null))
          return;
        buttonObject.icon.UpdateValue();
      }
    }

    private enum State
    {
      Challenge,
      Clear,
      Ended,
    }

    [Serializable]
    public class ButtonObject
    {
      public Button button;
      public Text title;
      public Text reward;
      public GameParameter icon;
    }
  }
}
