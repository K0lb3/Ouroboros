// Decompiled with JetBrains decompiler
// Type: SRPG.ItemConvertWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "Output", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 0)]
  public class ItemConvertWindow : MonoBehaviour, IFlowInterface
  {
    public Transform ItemLayout;
    public GameObject ItemTemplate;
    public Text ConvertItemName;
    public Text ConvertItemNum;
    public Text ConvertResult;

    public ItemConvertWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null) || !this.ItemTemplate.get_activeInHierarchy())
        return;
      this.ItemTemplate.get_gameObject().SetActive(false);
    }

    private void Refresh()
    {
      if (GlobalVars.SellItemList == null)
        GlobalVars.SellItemList = new List<SellItem>();
      else
        GlobalVars.SellItemList.Clear();
      int num = 0;
      List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        ItemData itemData = items[index];
        if (itemData.ItemType == EItemType.GoldConvert && itemData.Num != 0)
        {
          this.ConvertItemName.set_text(itemData.Param.name);
          this.ConvertItemNum.set_text(itemData.Num.ToString());
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent(this.ItemLayout, false);
          gameObject.SetActive(true);
          GlobalVars.SellItemList.Add(new SellItem()
          {
            item = itemData,
            num = itemData.Num
          });
          num += (int) itemData.Param.sell * itemData.Num;
        }
      }
      if (Object.op_Inequality((Object) this.ConvertResult, (Object) null))
        this.ConvertResult.set_text(string.Format(LocalizedText.Get("sys.CONVERT_TO_GOLD"), (object) num));
      ((Behaviour) this).set_enabled(true);
    }
  }
}
