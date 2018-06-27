// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.TextButtonTransition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExitGames.UtilityScripts
{
  [RequireComponent(typeof (Text))]
  public class TextButtonTransition : MonoBehaviour, IPointerExitHandler, IEventSystemHandler, IPointerEnterHandler
  {
    private Text _text;
    public Color NormalColor;
    public Color HoverColor;

    public TextButtonTransition()
    {
      base.\u002Ector();
    }

    public void Awake()
    {
      this._text = (Text) ((Component) this).GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      ((Graphic) this._text).set_color(this.HoverColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      ((Graphic) this._text).set_color(this.NormalColor);
    }
  }
}
