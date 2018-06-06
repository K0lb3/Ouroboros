// Decompiled with JetBrains decompiler
// Type: NazimaseVolume
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("SRPG/背景ツール/なじませボリューム")]
[ExecuteInEditMode]
public class NazimaseVolume : MonoBehaviour
{
  public NazimaseVolume()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    ((Component) this).set_tag("EditorOnly");
  }

  public Bounds Bounds
  {
    get
    {
      return new Bounds(((Component) this).get_transform().get_position(), ((Component) this).get_transform().get_localScale());
    }
  }
}
