// Decompiled with JetBrains decompiler
// Type: TemporaryCanvas
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class TemporaryCanvas : MonoBehaviour
{
  public GameObject Instance;

  public TemporaryCanvas()
  {
    base.\u002Ector();
  }

  private void OnApplicationQuit()
  {
    this.Instance = (GameObject) null;
  }

  private void Update()
  {
    if (!Object.op_Equality((Object) this.Instance, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this).get_gameObject());
  }
}
