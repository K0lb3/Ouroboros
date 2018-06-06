// Decompiled with JetBrains decompiler
// Type: SRPG.GachaScene
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "終了", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "表示開始", FlowNode.PinTypes.Output, 100)]
  public class GachaScene : SceneRoot, IFlowInterface
  {
    public int MaxGridColumnCount = 5;
    public GameObject Result2D;
    public GameObject Result3D;
    public GridLayoutGroup GridLayout;
    public string[] PreviewUnitID;
    public string[] PreviewItemID;
    private GachaScene.DropClasses mDropClass;
    private string[] mDropID;
    private bool mDropSet;

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.StartCoroutine(this.ExitGachaAsync());
    }

    public void DropUnits(string[] unitID)
    {
      this.mDropClass = GachaScene.DropClasses.Unit;
      this.mDropID = unitID;
      this.mDropSet = true;
    }

    public void DropItems(string[] itemID)
    {
      this.mDropClass = GachaScene.DropClasses.Item;
      this.mDropID = itemID;
      this.mDropSet = true;
    }

    protected override void Awake()
    {
      base.Awake();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.Result2D, (Object) null))
        this.Result2D.SetActive(false);
      this.StartCoroutine(this.AsyncUpdate());
    }

    [DebuggerHidden]
    private IEnumerator AsyncUpdate()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaScene.\u003CAsyncUpdate\u003Ec__Iterator52() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator ExitGachaAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaScene.\u003CExitGachaAsync\u003Ec__Iterator53() { \u003C\u003Ef__this = this };
    }

    public enum DropClasses
    {
      Unit,
      Item,
    }
  }
}
