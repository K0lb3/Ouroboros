// Decompiled with JetBrains decompiler
// Type: GUIEventListener
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class GUIEventListener : MonoBehaviour
{
  public GUIEventListener.GUIEvent Listeners;

  public GUIEventListener()
  {
    base.\u002Ector();
  }

  private void OnGUI()
  {
    if (this.Listeners == null)
      return;
    this.Listeners(((Component) this).get_gameObject());
  }

  public delegate void GUIEvent(GameObject go);
}
