// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_HighlightGrid
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
  [FlowNode.NodeType("Tutorial/HighlightGrid", 32741)]
  [FlowNode.Pin(10, "Highlight", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "Remove", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_HighlightGrid : FlowNode
  {
    [FlowNode.DropTarget(typeof (GameObject), true)]
    [FlowNode.ShowInInfo]
    [SerializeField]
    private int gridX;
    [SerializeField]
    private int gridY;
    [SerializeField]
    [FlowNode.ShowInInfo]
    private bool interactable;
    [SerializeField]
    [FlowNode.ShowInInfo]
    private bool portraitvisible;
    private string UnitID;
    [SerializeField]
    [StringIsTextID(true)]
    private string TextID;
    [SerializeField]
    private EventDialogBubble.Anchors dialogBubbleAnchor;
    private LoadRequest mResourceRequest;
    private static SGHighlightObject highlight;

    private void Start()
    {
      if (MonoSingleton<GameManager>.Instance.IsTutorial())
        return;
      ((Behaviour) this).set_enabled(false);
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.CreateHighlightObject();
          FlowNode_HighlightGrid.highlight.highlightedGrid = new IntVector2(this.gridX, this.gridY);
          FlowNode_HighlightGrid.highlight.Highlight(this.UnitID, this.TextID, (SGHighlightObject.OnActivateCallback) (() => this.ActivateOutputLinks(2)), this.dialogBubbleAnchor, this.portraitvisible, this.interactable, false);
          break;
        case 11:
          if (Object.op_Inequality((Object) FlowNode_HighlightGrid.highlight, (Object) null))
          {
            Object.Destroy((Object) ((Component) FlowNode_HighlightGrid.highlight).get_gameObject());
            FlowNode_HighlightGrid.highlight = (SGHighlightObject) null;
            break;
          }
          break;
        case 12:
          if (Object.op_Inequality((Object) FlowNode_HighlightGrid.highlight, (Object) null))
          {
            Object.Destroy((Object) ((Component) FlowNode_HighlightGrid.highlight).get_gameObject());
            FlowNode_HighlightGrid.highlight = (SGHighlightObject) null;
          }
          this.CreateHighlightObject();
          break;
      }
      this.ActivateOutputLinks(1);
    }

    private void CreateHighlightObject()
    {
      if (!Object.op_Equality((Object) FlowNode_HighlightGrid.highlight, (Object) null))
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
        FlowNode_HighlightGrid.highlight = (SGHighlightObject) gameObject2.GetComponent<SGHighlightObject>();
      }
    }
  }
}
