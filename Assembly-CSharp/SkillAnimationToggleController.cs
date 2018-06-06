// Decompiled with JetBrains decompiler
// Type: SkillAnimationToggleController
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillAnimationToggleController : MonoBehaviour
{
  [SerializeField]
  private Button skillAnimationOff;
  [SerializeField]
  private Button skillAnimationOn;

  public SkillAnimationToggleController()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    // ISSUE: method pointer
    ((UnityEvent) this.skillAnimationOn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(TurnOffSkillAnimation)));
    // ISSUE: method pointer
    ((UnityEvent) this.skillAnimationOff.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(TurnOnSkillAnimation)));
    this.updateToggleButtons(GameUtility.Config_SkillAnimation);
  }

  protected void updateToggleButtons(bool withSkillAnimation)
  {
    ((Component) this.skillAnimationOn).get_gameObject().SetActive(withSkillAnimation);
    ((Component) this.skillAnimationOff).get_gameObject().SetActive(!withSkillAnimation);
  }

  protected void TurnOffSkillAnimation()
  {
    GameUtility.Config_SkillAnimation = false;
    this.updateToggleButtons(false);
  }

  protected void TurnOnSkillAnimation()
  {
    GameUtility.Config_SkillAnimation = true;
    this.updateToggleButtons(true);
  }
}
