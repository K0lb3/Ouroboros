// Decompiled with JetBrains decompiler
// Type: DestructTimer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class DestructTimer : MonoBehaviour
{
  public float Timer;

  public DestructTimer()
  {
    base.\u002Ector();
  }

  private void Update()
  {
    this.Timer -= Time.get_deltaTime();
    if ((double) this.Timer > 0.0)
      return;
    Object.Destroy((Object) ((Component) this).get_gameObject());
  }
}
