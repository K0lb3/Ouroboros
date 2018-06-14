// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
    public GameObject IconChallenge;
    public GameObject IconClear;
    public GameObject IconNext;
    public ChallengeMissionItem.ButtonObject ButtonNormal;
    public ChallengeMissionItem.ButtonObject ButtonHighlight;
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
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).get_gameObject(), (TrophyParam) null);
      if (Object.op_Equality((Object) instanceDirect, (Object) null) || dataOfClass == null)
      {
        ((Behaviour) this).set_enabled(false);
      }
      else
      {
        TrophyState trophyCounter1 = ChallengeMission.GetTrophyCounter(dataOfClass);
        if (trophyCounter1.IsEnded)
        {
          ((Component) this).get_gameObject().SetActive(false);
        }
        else
        {
          ((Component) this).get_gameObject().SetActive(true);
          ChallengeMissionItem.State state = ChallengeMissionItem.State.Challenge;
          if (dataOfClass.RequiredTrophies != null && dataOfClass.RequiredTrophies.Length > 0)
          {
            TrophyParam trophy = ChallengeMission.GetTrophy(dataOfClass.RequiredTrophies[0]);
            if (trophy != null)
            {
              TrophyState trophyCounter2 = ChallengeMission.GetTrophyCounter(trophy);
              state = !trophyCounter2.IsEnded ? (!trophyCounter2.IsCompleted ? ChallengeMissionItem.State.None : ChallengeMissionItem.State.Next) : ChallengeMissionItem.State.Challenge;
            }
          }
          if (trophyCounter1.IsCompleted)
            state = ChallengeMissionItem.State.Clear;
          this.SetStateIcon(state);
          ChallengeMissionItem.ButtonObject buttonObject;
          if (state == ChallengeMissionItem.State.Clear)
          {
            ((Component) this.ButtonHighlight.button).get_gameObject().SetActive(true);
            ((Component) this.ButtonNormal.button).get_gameObject().SetActive(false);
            buttonObject = this.ButtonHighlight;
          }
          else
          {
            ((Component) this.ButtonHighlight.button).get_gameObject().SetActive(false);
            ((Component) this.ButtonNormal.button).get_gameObject().SetActive(true);
            buttonObject = this.ButtonNormal;
          }
          if (buttonObject != null && Object.op_Inequality((Object) buttonObject.title, (Object) null))
          {
            if (state == ChallengeMissionItem.State.None)
              buttonObject.title.set_text(LocalizedText.Get("sys.CHALLENGE_LOCKED"));
            else
              buttonObject.title.set_text(dataOfClass.Name);
          }
          if (buttonObject != null && Object.op_Inequality((Object) buttonObject.button, (Object) null))
          {
            ((UnityEventBase) buttonObject.button.get_onClick()).RemoveAllListeners();
            ((UnityEvent) buttonObject.button.get_onClick()).AddListener(this.OnClick);
            ((Selectable) buttonObject.button).set_interactable(state == ChallengeMissionItem.State.Challenge || state == ChallengeMissionItem.State.Clear);
          }
          if (buttonObject == null || !Object.op_Inequality((Object) buttonObject.reward, (Object) null))
            return;
          if (dataOfClass.Gold != 0)
            buttonObject.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) dataOfClass.Gold));
          else if (dataOfClass.Exp != 0)
            buttonObject.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) dataOfClass.Exp));
          else if (dataOfClass.Coin != 0)
            buttonObject.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) dataOfClass.Coin));
          else if (dataOfClass.Stamina != 0)
          {
            buttonObject.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) dataOfClass.Stamina));
          }
          else
          {
            if (dataOfClass.Items == null || dataOfClass.Items.Length <= 0)
              return;
            ItemParam itemParam = instanceDirect.GetItemParam(dataOfClass.Items[0].iname);
            if (itemParam == null)
              return;
            buttonObject.reward.set_text(itemParam.name);
          }
        }
      }
    }

    private void SetStateIcon(ChallengeMissionItem.State value)
    {
      if (Object.op_Equality((Object) this.IconChallenge, (Object) null) || Object.op_Equality((Object) this.IconClear, (Object) null) || Object.op_Equality((Object) this.IconNext, (Object) null))
        return;
      this.IconChallenge.SetActive(false);
      this.IconClear.SetActive(false);
      this.IconNext.SetActive(false);
      switch (value)
      {
        case ChallengeMissionItem.State.Challenge:
          this.IconChallenge.SetActive(true);
          break;
        case ChallengeMissionItem.State.Clear:
          this.IconClear.SetActive(true);
          break;
        case ChallengeMissionItem.State.Next:
          this.IconNext.SetActive(true);
          break;
      }
    }

    private enum State
    {
      None,
      Challenge,
      Clear,
      Next,
      Ended,
    }

    [Serializable]
    public class ButtonObject
    {
      public Button button;
      public Text title;
      public Text reward;
    }
  }
}
