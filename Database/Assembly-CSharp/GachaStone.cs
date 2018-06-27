// Decompiled with JetBrains decompiler
// Type: GachaStone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class GachaStone : MonoBehaviour
{
  public Camera TargetCamera;

  public GachaStone()
  {
    base.\u002Ector();
  }

  public string DROP_ID { get; set; }

  private void Start()
  {
    if (!Object.op_Equality((Object) this.TargetCamera, (Object) null))
      return;
    this.TargetCamera = Camera.get_main();
  }

  private void Update()
  {
    ((Component) this).get_transform().LookAt(((Component) this.TargetCamera).get_transform());
  }
}
