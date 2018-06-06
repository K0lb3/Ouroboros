// Decompiled with JetBrains decompiler
// Type: DestructTimer
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
