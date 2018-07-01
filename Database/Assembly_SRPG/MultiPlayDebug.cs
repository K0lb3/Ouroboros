// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayDebug
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiPlayDebug : MonoBehaviour
  {
    public GameObject debuginfo;
    public Button m_DebugBtn;

    public MultiPlayDebug()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      ((Component) this.m_DebugBtn).get_gameObject().SetActive(false);
    }

    private void OnDestroy()
    {
    }

    private void OnClick()
    {
    }
  }
}
