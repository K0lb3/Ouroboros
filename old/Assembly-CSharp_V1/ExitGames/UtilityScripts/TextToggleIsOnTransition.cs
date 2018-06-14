// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.TextToggleIsOnTransition
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExitGames.UtilityScripts
{
  [RequireComponent(typeof (Text))]
  public class TextToggleIsOnTransition : MonoBehaviour, IPointerExitHandler, IEventSystemHandler, IPointerEnterHandler
  {
    public Toggle toggle;
    private Text _text;
    public Color NormalOnColor;
    public Color NormalOffColor;
    public Color HoverOnColor;
    public Color HoverOffColor;
    private bool isHover;

    public TextToggleIsOnTransition()
    {
      base.\u002Ector();
    }

    public void OnEnable()
    {
      this._text = (Text) ((Component) this).GetComponent<Text>();
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.toggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnValueChanged)));
    }

    public void OnDisable()
    {
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.toggle.onValueChanged).RemoveListener(new UnityAction<bool>((object) this, __methodptr(OnValueChanged)));
    }

    public void OnValueChanged(bool isOn)
    {
      ((Graphic) this._text).set_color(!isOn ? (!this.isHover ? this.NormalOffColor : this.NormalOnColor) : (!this.isHover ? this.HoverOffColor : this.HoverOnColor));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      this.isHover = true;
      ((Graphic) this._text).set_color(!this.toggle.get_isOn() ? this.HoverOffColor : this.HoverOnColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      this.isHover = false;
      ((Graphic) this._text).set_color(!this.toggle.get_isOn() ? this.NormalOffColor : this.NormalOnColor);
    }
  }
}
