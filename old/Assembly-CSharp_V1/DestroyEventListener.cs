// Decompiled with JetBrains decompiler
// Type: DestroyEventListener
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class DestroyEventListener : MonoBehaviour
{
  public DestroyEventListener.DestroyEvent Listeners;

  public DestroyEventListener()
  {
    base.\u002Ector();
  }

  private void OnApplicationQuit()
  {
    this.Listeners = (DestroyEventListener.DestroyEvent) (_param0 => {});
  }

  private void OnDestroy()
  {
    if (this.Listeners == null)
      return;
    this.Listeners(((Component) this).get_gameObject());
  }

  public delegate void DestroyEvent(GameObject go);
}
