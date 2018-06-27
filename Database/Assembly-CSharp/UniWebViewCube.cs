// Decompiled with JetBrains decompiler
// Type: UniWebViewCube
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class UniWebViewCube : MonoBehaviour
{
  public UniWebDemo webViewDemo;
  private float startTime;
  private bool firstHit;

  public UniWebViewCube()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.startTime = Time.get_time();
  }

  private void OnCollisionEnter(Collision col)
  {
    if (!(((Object) col.get_gameObject()).get_name() == "Target"))
      return;
    this.webViewDemo.ShowAlertInWebview(Time.get_time() - this.startTime, this.firstHit);
    this.firstHit = false;
  }
}
