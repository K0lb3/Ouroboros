// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultArtifactDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refreshed", FlowNode.PinTypes.Output, 2)]
  public class GachaResultArtifactDetail : MonoBehaviour, IFlowInterface
  {
    private readonly int OUT_CLOSE_DETAIL;
    public GameObject ArtifactInfo;
    public GameObject Bg;
    private ArtifactData mCurrentArtifact;
    [SerializeField]
    private Button BackBtn;

    public GachaResultArtifactDetail()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Start()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackBtn, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.BackBtn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnCloseDetail)));
    }

    private void OnEnable()
    {
      Animator component1 = (Animator) ((Component) this).GetComponent<Animator>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        component1.SetBool("close", false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Bg, (UnityEngine.Object) null))
        return;
      CanvasGroup component2 = (CanvasGroup) this.Bg.GetComponent<CanvasGroup>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        return;
      component2.set_interactable(true);
      component2.set_blocksRaycasts(true);
    }

    private void OnCloseDetail()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_CLOSE_DETAIL);
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ArtifactInfo, (UnityEngine.Object) null))
        return;
      int index = int.Parse(FlowNode_Variable.Get("GachaResultDataIndex"));
      ArtifactParam artifact = GachaResultData.drops[index].artifact;
      if (artifact == null)
        return;
      this.mCurrentArtifact = new ArtifactData();
      this.mCurrentArtifact = this.CreateArtifactData(artifact, GachaResultData.drops[index].Rare);
      DataSource.Bind<ArtifactData>(this.ArtifactInfo, this.mCurrentArtifact);
      GameParameter.UpdateAll(this.ArtifactInfo);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }

    private ArtifactData CreateArtifactData(ArtifactParam param, int rarity)
    {
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iid = 1L,
        exp = 0,
        iname = param.iname,
        fav = 0,
        rare = Math.Min(Math.Max(param.rareini, rarity), param.raremax)
      });
      return artifactData;
    }
  }
}
