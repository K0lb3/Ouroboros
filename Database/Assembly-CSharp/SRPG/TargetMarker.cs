// Decompiled with JetBrains decompiler
// Type: SRPG.TargetMarker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TargetMarker : MonoBehaviour
  {
    private Transform m_Transform;

    public TargetMarker()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      this.m_Transform = (Transform) ((Component) this).GetComponent<Transform>();
    }

    private void LateUpdate()
    {
      SceneBattle instance = SceneBattle.Instance;
      Vector3 zero = Vector3.get_zero();
      if (Object.op_Inequality((Object) instance, (Object) null) && instance.isUpView)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local = @zero;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).y = (__Null) ((^local).y + 0.649999976158142);
      }
      this.m_Transform.set_localPosition(zero);
    }
  }
}
