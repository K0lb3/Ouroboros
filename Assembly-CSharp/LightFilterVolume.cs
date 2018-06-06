// Decompiled with JetBrains decompiler
// Type: LightFilterVolume
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("")]
[ExecuteInEditMode]
public class LightFilterVolume : MonoBehaviour
{
  public LightFilterVolume()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    ((Component) this).set_tag("EditorOnly");
    ((Object) this).set_hideFlags((HideFlags) 2);
    ((Component) this).get_transform().set_localScale(Vector3.op_Multiply(Vector3.get_one(), 3f));
    ((Object) ((Component) this).get_gameObject()).set_hideFlags((HideFlags) 52);
  }

  public Bounds Bounds
  {
    get
    {
      return new Bounds(((Component) this).get_transform().get_position(), ((Component) this).get_transform().get_localScale());
    }
  }
}
