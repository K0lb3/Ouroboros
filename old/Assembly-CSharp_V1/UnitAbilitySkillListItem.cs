// Decompiled with JetBrains decompiler
// Type: UnitAbilitySkillListItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UnitAbilitySkillListItem : MonoBehaviour
{
  public GameObject MaxObject;
  public GameObject RemainObject;
  public GameObject LimitObject;
  public GameObject NoLimitObject;
  public GameObject CastSpeedObject;
  public GameObject SpeedObject;

  public UnitAbilitySkillListItem()
  {
    base.\u002Ector();
  }

  public void SetSkillCount(int Remaining, int Limit, bool noLimit)
  {
    if (noLimit)
    {
      this.NoLimitObject.SetActive(true);
      this.LimitObject.SetActive(false);
    }
    else
    {
      this.NoLimitObject.SetActive(false);
      Text component1 = (Text) this.MaxObject.GetComponent<Text>();
      Text component2 = (Text) this.RemainObject.GetComponent<Text>();
      component1.set_text(Limit.ToString());
      component2.set_text(Remaining.ToString());
      this.LimitObject.SetActive(true);
    }
  }

  public void SetCastSpeed(OInt Speed)
  {
    if ((int) Speed > 0)
    {
      ((Text) this.SpeedObject.GetComponent<Text>()).set_text(Speed.ToString());
      this.CastSpeedObject.SetActive(true);
    }
    else
      this.CastSpeedObject.SetActive(false);
  }
}
