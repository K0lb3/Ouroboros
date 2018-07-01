// Decompiled with JetBrains decompiler
// Type: SRPG.GallerySortWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "ソート順の変更", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Save Setting", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "ソート種別の変更", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  public class GallerySortWindow : MonoBehaviour, IFlowInterface
  {
    private const int SORT_ORDER_CHANGE = 0;
    private const int SAVE_SETTING = 1;
    private const int SORT_TYPE_CHANGE = 2;
    private const int OUTPUT_CLOSE = 100;
    [SerializeField]
    private Toggle RarityButton;
    [SerializeField]
    private Toggle NameButton;
    [SerializeField]
    private Toggle AscButton;
    [SerializeField]
    private Toggle DescButton;
    private GalleryItemListWindow.Settings mSettings;
    private bool mIsRarityAscending;
    private bool mIsNameAscending;
    private GalleryItemListWindow.SortType mCurrentSortType;

    public GallerySortWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          SerializeValueList currentValue1 = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue1 == null)
            break;
          Toggle uiToggle = currentValue1.GetUIToggle("toggle");
          if (Object.op_Equality((Object) uiToggle, (Object) this.AscButton))
          {
            if (this.mCurrentSortType == GalleryItemListWindow.SortType.Rarity)
            {
              this.mIsRarityAscending = uiToggle.get_isOn();
              break;
            }
            this.mIsNameAscending = uiToggle.get_isOn();
            break;
          }
          if (this.mCurrentSortType == GalleryItemListWindow.SortType.Rarity)
          {
            this.mIsRarityAscending = !uiToggle.get_isOn();
            break;
          }
          this.mIsNameAscending = !uiToggle.get_isOn();
          break;
        case 1:
          this.mSettings.sortType = this.mCurrentSortType;
          this.mSettings.isRarityAscending = this.mIsRarityAscending;
          this.mSettings.isNameAscending = this.mIsNameAscending;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 2:
          SerializeValueList currentValue2 = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue2 == null)
            break;
          if (Object.op_Equality((Object) currentValue2.GetUIToggle("toggle"), (Object) this.RarityButton))
          {
            this.mCurrentSortType = GalleryItemListWindow.SortType.Rarity;
            this.AscButton.set_isOn(this.mIsRarityAscending);
            this.DescButton.set_isOn(!this.mIsRarityAscending);
            break;
          }
          this.mCurrentSortType = GalleryItemListWindow.SortType.Name;
          this.AscButton.set_isOn(this.mIsNameAscending);
          this.DescButton.set_isOn(!this.mIsNameAscending);
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
      this.AscButton.set_isOn(this.mSettings.isRarityAscending);
      this.DescButton.set_isOn(!this.mSettings.isRarityAscending);
      this.mCurrentSortType = this.mSettings.sortType;
      this.mIsRarityAscending = this.mSettings.isRarityAscending;
      this.mIsNameAscending = this.mSettings.isNameAscending;
      if (this.mCurrentSortType == GalleryItemListWindow.SortType.Rarity)
      {
        this.RarityButton.set_isOn(true);
        this.NameButton.set_isOn(false);
        this.AscButton.set_isOn(this.mIsRarityAscending);
        this.DescButton.set_isOn(!this.mIsRarityAscending);
      }
      else
      {
        this.RarityButton.set_isOn(false);
        this.NameButton.set_isOn(true);
        this.AscButton.set_isOn(this.mIsNameAscending);
        this.DescButton.set_isOn(!this.mIsNameAscending);
      }
    }
  }
}
