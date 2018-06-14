// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayDebug
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiPlayDebug : MonoBehaviour
  {
    public GameObject debuginfo;
    public Button m_DebugBtn;
    private bool active;

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
