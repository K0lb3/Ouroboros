// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTabList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class GachaTabList : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject TabTemplate;
    [SerializeField]
    private GameObject RareTab;
    [SerializeField]
    private GameObject NormalTab;
    [SerializeField]
    private GameObject ArtifactTab;
    [SerializeField]
    private GameObject TicketTab;

    public GachaTabList()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.TabTemplate, (Object) null))
        this.TabTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.RareTab, (Object) null))
        this.RareTab.SetActive(false);
      if (Object.op_Inequality((Object) this.NormalTab, (Object) null))
        this.NormalTab.SetActive(false);
      if (Object.op_Inequality((Object) this.ArtifactTab, (Object) null))
        this.ArtifactTab.SetActive(false);
      if (!Object.op_Inequality((Object) this.TicketTab, (Object) null))
        return;
      this.TicketTab.SetActive(false);
    }

    private void Update()
    {
    }

    private void Refresh()
    {
    }
  }
}
