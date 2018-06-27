// Decompiled with JetBrains decompiler
// Type: DestroyEventListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
