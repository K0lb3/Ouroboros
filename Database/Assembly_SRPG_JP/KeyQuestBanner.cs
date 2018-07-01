// Decompiled with JetBrains decompiler
// Type: SRPG.KeyQuestBanner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class KeyQuestBanner : BaseIcon
  {
    public GameObject IconRoot;
    public RawImage Icon;
    public Image Frame;
    public Text UseNum;
    public Text Amount;
    public GameObject Locked;
    public QuestTimeLimit QuestTimer;

    public override void UpdateValue()
    {
      KeyItem dataOfClass1 = DataSource.FindDataOfClass<KeyItem>(((Component) this).get_gameObject(), (KeyItem) null);
      if (dataOfClass1 != null)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(dataOfClass1.iname);
        if (itemParam != null)
        {
          ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(itemParam);
          int num = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
          if (Object.op_Inequality((Object) this.Icon, (Object) null))
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon(itemParam));
          if (Object.op_Inequality((Object) this.Frame, (Object) null))
            this.Frame.set_sprite(GameSettings.Instance.GetItemFrame(itemParam));
          if (Object.op_Inequality((Object) this.UseNum, (Object) null))
            this.UseNum.set_text(dataOfClass1.num.ToString());
          if (Object.op_Inequality((Object) this.Amount, (Object) null))
            this.Amount.set_text(num.ToString());
          if (Object.op_Inequality((Object) this.Locked, (Object) null))
          {
            ChapterParam dataOfClass2 = DataSource.FindDataOfClass<ChapterParam>(((Component) this).get_gameObject(), (ChapterParam) null);
            this.Locked.SetActive(dataOfClass2 == null || !dataOfClass2.IsKeyQuest() || !dataOfClass2.IsKeyUnlock(Network.GetServerTime()));
          }
        }
      }
      if (Object.op_Inequality((Object) this.QuestTimer, (Object) null))
        this.QuestTimer.UpdateValue();
      if (!Object.op_Inequality((Object) this.IconRoot, (Object) null) || !Object.op_Inequality((Object) this.Locked, (Object) null))
        return;
      this.IconRoot.SetActive(this.Locked.get_activeSelf());
    }
  }
}
