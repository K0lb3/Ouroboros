// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultArtifactDetail
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refreshed", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
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
      if (!Object.op_Inequality((Object) this.BackBtn, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.BackBtn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnCloseDetail)));
    }

    private void OnEnable()
    {
      Animator component1 = (Animator) ((Component) this).GetComponent<Animator>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        component1.SetBool("close", false);
      if (!Object.op_Inequality((Object) this.Bg, (Object) null))
        return;
      CanvasGroup component2 = (CanvasGroup) this.Bg.GetComponent<CanvasGroup>();
      if (!Object.op_Inequality((Object) component2, (Object) null))
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
      if (Object.op_Equality((Object) this.ArtifactInfo, (Object) null))
        return;
      ArtifactParam artifact = GachaResultData.drops[int.Parse(FlowNode_Variable.Get("GachaResultDataIndex"))].artifact;
      if (artifact == null)
        return;
      this.mCurrentArtifact = new ArtifactData();
      this.mCurrentArtifact = this.CreateArtifactData(artifact);
      DataSource.Bind<ArtifactData>(this.ArtifactInfo, this.mCurrentArtifact);
      GameParameter.UpdateAll(this.ArtifactInfo);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }

    private ArtifactData CreateArtifactData(ArtifactParam param)
    {
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iid = 1L,
        exp = 0,
        iname = param.iname,
        fav = 0,
        rare = param.rareini
      });
      return artifactData;
    }
  }
}
