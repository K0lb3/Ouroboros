// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayDebug
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
