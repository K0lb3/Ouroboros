// Decompiled with JetBrains decompiler
// Type: Billboard
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Rendering/Billboard")]
public class Billboard : MonoBehaviour
{
  public Billboard()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
    CameraHook.AddPreCullEventListener(new CameraHook.PreCullEvent(this.PreCull));
  }

  private void OnDisable()
  {
    CameraHook.RemovePreCullEventListener(new CameraHook.PreCullEvent(this.PreCull));
  }

  private void PreCull(Camera camera)
  {
    Transform transform1 = ((Component) this).get_transform();
    Transform transform2 = ((Component) camera).get_transform();
    transform1.set_rotation(Quaternion.LookRotation(transform2.get_forward(), transform2.get_up()));
  }
}
