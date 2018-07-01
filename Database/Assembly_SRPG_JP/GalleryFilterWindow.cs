// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryFilterWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "Enable All Toggle", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Disable All Toggle", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Save Setting", FlowNode.PinTypes.Input, 1)]
  public class GalleryFilterWindow : MonoBehaviour, IFlowInterface
  {
    private const int SAVE_SETTING = 1;
    private const int ENABLE_ALL_TOGGLE = 2;
    private const int DISABLE_ALL_TOGGLE = 3;
    private const int OUTPUT_CLOSE = 100;
    [SerializeField]
    private Toggle[] mToggles;
    private GalleryItemListWindow.Settings mSettings;
    private List<int> mRareFiltters;

    public GalleryFilterWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.mSettings.rareFilters = this.mRareFiltters.ToArray();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 2:
          if (this.mToggles == null)
            break;
          foreach (Toggle mToggle in this.mToggles)
            mToggle.set_isOn(true);
          break;
        case 3:
          if (this.mToggles == null)
            break;
          foreach (Toggle mToggle in this.mToggles)
            mToggle.set_isOn(false);
          break;
      }
    }

    private void Awake()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      this.mSettings = currentValue.GetObject("settings") as GalleryItemListWindow.Settings;
      if (this.mSettings == null)
        return;
      this.mRareFiltters = ((IEnumerable<int>) this.mSettings.rareFilters).OrderBy<int, int>((Func<int, int>) (x => x)).ToList<int>();
      foreach (Toggle mToggle in this.mToggles)
        mToggle.set_isOn(false);
      if (this.mToggles != null && this.mToggles.Length >= 0)
      {
        using (List<int>.Enumerator enumerator = this.mRareFiltters.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            int current = enumerator.Current;
            if (current >= 0 && current < this.mToggles.Length)
              this.mToggles[current].set_isOn(true);
          }
        }
      }
      if (this.mToggles == null || this.mToggles.Length < 0)
        return;
      for (int index = 0; index < this.mToggles.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.mToggles[index].onValueChanged).AddListener(new UnityAction<bool>((object) new GalleryFilterWindow.\u003CAwake\u003Ec__AnonStorey348()
        {
          \u003C\u003Ef__this = this,
          index = index
        }, __methodptr(\u003C\u003Em__33C)));
      }
    }
  }
}
