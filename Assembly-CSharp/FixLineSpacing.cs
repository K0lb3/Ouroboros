// Decompiled with JetBrains decompiler
// Type: FixLineSpacing
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Text))]
public class FixLineSpacing : MonoBehaviour
{
  public bool NoBestFit;

  public FixLineSpacing()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
  }

  private void Awake()
  {
    if (!((Behaviour) this).get_enabled())
      return;
    Text component = (Text) ((Component) this).GetComponent<Text>();
    Text text = component;
    text.set_lineSpacing(text.get_lineSpacing() * 2f);
    if (this.NoBestFit)
      component.set_resizeTextForBestFit(false);
    ((Behaviour) this).set_enabled(false);
  }
}
