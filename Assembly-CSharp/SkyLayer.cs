// Decompiled with JetBrains decompiler
// Type: SkyLayer
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Rendering/Sky Layer")]
[RequireComponent(typeof (Renderer))]
public class SkyLayer : MonoBehaviour
{
  public float SkyDistance;
  public Vector3 SkyOffset;
  private bool mVisible;
  private Vector3 mOldPosition;
  private Camera mCamera;

  public SkyLayer()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
    CameraHook.AddPreCullEventListener(new CameraHook.PreCullEvent(this.OnCameraPreCull));
  }

  private void OnDisable()
  {
    CameraHook.RemovePreCullEventListener(new CameraHook.PreCullEvent(this.OnCameraPreCull));
  }

  private void OnCameraPreCull(Camera camera)
  {
    this.OnWillRenderObject();
  }

  private void OnWillRenderObject()
  {
    Transform transform1 = ((Component) Camera.get_current()).get_transform();
    Transform transform2 = ((Component) this).get_transform();
    this.mVisible = true;
    this.mOldPosition = transform2.get_position();
    transform2.set_position(Vector3.op_Addition(Vector3.op_Multiply(Vector3.get_forward(), (float) transform1.get_position().z + this.SkyDistance), this.SkyOffset));
  }

  private void OnRenderObject()
  {
    if (!this.mVisible)
      return;
    ((Component) this).get_transform().set_position(this.mOldPosition);
    this.mVisible = false;
  }
}
