// Decompiled with JetBrains decompiler
// Type: TemporaryCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
