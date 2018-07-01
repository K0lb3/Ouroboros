// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_HighlightObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(12, "Reinstantiate and Enter", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Highlight Next", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(11, "Remove", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Tutorial/HighlightObject", 32741)]
  [FlowNode.Pin(10, "Highlight", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_HighlightObject : FlowNode
  {
    [FlowNode.DropTarget(typeof (GameObject), true)]
    [FlowNode.ShowInInfo]
    [SerializeField]
    private GameObject HighlightTarget;
    [SerializeField]
    [FlowNode.ShowInInfo]
    private bool interactable;
    [SerializeField]
    [FlowNode.ShowInInfo]
    private bool portraitvisible;
    private bool smallhighlight;
    private string UnitID;
    [StringIsTextID(true)]
    [SerializeField]
    private string TextID;
    [SerializeField]
    private EventDialogBubble.Anchors dialogBubbleAnchor;
    private LoadRequest mResourceRequest;
    private static SGHighlightObject highlight;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.CreateHighlightObject();
          FlowNode_HighlightObject.highlight.highlightedObject = this.HighlightTarget;
          FlowNode_HighlightObject.highlight.Highlight(this.UnitID, this.TextID, (SGHighlightObject.OnActivateCallback) (() => this.ActivateOutputLinks(2)), this.dialogBubbleAnchor, this.portraitvisible, this.interactable, this.smallhighlight);
          break;
        case 11:
          if (Object.op_Inequality((Object) FlowNode_HighlightObject.highlight, (Object) null))
          {
            Object.Destroy((Object) ((Component) FlowNode_HighlightObject.highlight).get_gameObject());
            FlowNode_HighlightObject.highlight = (SGHighlightObject) null;
            break;
          }
          break;
        case 12:
          if (Object.op_Inequality((Object) FlowNode_HighlightObject.highlight, (Object) null))
          {
            Object.Destroy((Object) ((Component) FlowNode_HighlightObject.highlight).get_gameObject());
            FlowNode_HighlightObject.highlight = (SGHighlightObject) null;
          }
          this.CreateHighlightObject();
          break;
      }
      this.ActivateOutputLinks(1);
    }

    private void Start()
    {
      if (!MonoSingleton<GameManager>.Instance.IsTutorial())
        return;
      Object.Destroy((Object) this);
    }

    private void CreateHighlightObject()
    {
      if (!Object.op_Equality((Object) FlowNode_HighlightObject.highlight, (Object) null))
        return;
      GameObject gameObject1 = AssetManager.Load<GameObject>("SGDevelopment/Tutorial/Tutorial_Guidance");
      if (Object.op_Equality((Object) gameObject1, (Object) null))
      {
        DebugUtility.LogError("Failed to load");
      }
      else
      {
        GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) gameObject1);
        RectTransform component1 = (RectTransform) gameObject1.GetComponent<RectTransform>();
        if (Object.op_Inequality((Object) component1, (Object) null) && Object.op_Inequality((Object) gameObject1.GetComponent<Canvas>(), (Object) null))
        {
          RectTransform component2 = (RectTransform) gameObject2.GetComponent<RectTransform>();
          component2.set_anchorMax(component1.get_anchorMax());
          component2.set_anchorMin(component1.get_anchorMin());
          component2.set_anchoredPosition(component1.get_anchoredPosition());
          component2.set_sizeDelta(component1.get_sizeDelta());
        }
        FlowNode_HighlightObject.highlight = (SGHighlightObject) gameObject2.GetComponent<SGHighlightObject>();
        DebugUtility.LogWarning("highlight:" + (object) FlowNode_HighlightObject.highlight);
      }
    }
  }
}
