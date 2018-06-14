// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListItemEvents
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitListItemEvents : ListItemEvents
  {
    public Image[] EqIcons;
    public Image[] AttrIcons;
    public GameObject Badge;
    public GameObject CharacterQuestBadge;

    private void OnEnable()
    {
      this.Refresh();
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.Badge, (Object) null))
        return;
      this.Badge.SetActive(false);
    }

    public void Refresh()
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (dataOfClass == null)
        return;
      if (this.EqIcons != null && this.AttrIcons != null)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
        EquipData[] currentEquips = dataOfClass.CurrentEquips;
        for (int index = 0; index < currentEquips.Length; ++index)
        {
          EquipData equipData = currentEquips[index];
          if (!Object.op_Equality((Object) this.EqIcons[index], (Object) null) && !Object.op_Equality((Object) this.AttrIcons[index], (Object) null))
          {
            ((Component) this.EqIcons[index]).get_gameObject().SetActive(false);
            ((Component) this.AttrIcons[index]).get_gameObject().SetActive(false);
            if (equipData == null || !equipData.IsValid())
            {
              this.EqIcons[index].set_sprite((Sprite) null);
            }
            else
            {
              ((Component) this.EqIcons[index]).get_gameObject().SetActive(true);
              this.EqIcons[index].set_sprite(spriteSheet.GetSprite(equipData.ItemID));
              if (!equipData.IsEquiped())
              {
                ((Component) this.AttrIcons[index]).get_gameObject().SetActive(true);
                if (player.HasItem(equipData.ItemID))
                  this.AttrIcons[index].set_sprite(spriteSheet.GetSprite((int) equipData.ItemParam.equipLv <= dataOfClass.Lv ? "plus0" : "plus1"));
                else if (player.CheckEnableCreateItem(equipData.ItemParam, true, 1))
                  this.AttrIcons[index].set_sprite(spriteSheet.GetSprite((int) equipData.ItemParam.equipLv <= dataOfClass.Lv ? "plus0" : "plus1"));
                else
                  this.AttrIcons[index].set_sprite(spriteSheet.GetSprite("plus2"));
              }
            }
          }
        }
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void Update()
    {
      if (Object.op_Inequality((Object) this.Badge, (Object) null))
      {
        UnitData dataOfClass1 = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
        if (dataOfClass1 != null)
        {
          this.Badge.SetActive(dataOfClass1.BadgeState != (UnitBadgeTypes) 0);
        }
        else
        {
          UnitParam dataOfClass2 = DataSource.FindDataOfClass<UnitParam>(((Component) this).get_gameObject(), (UnitParam) null);
          if (dataOfClass2 != null)
            this.Badge.SetActive(MonoSingleton<GameManager>.Instance.CheckEnableUnitUnlock(dataOfClass2));
        }
      }
      if (!Object.op_Inequality((Object) this.CharacterQuestBadge, (Object) null))
        return;
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (dataOfClass == null)
        return;
      this.CharacterQuestBadge.SetActive(dataOfClass.IsOpenCharacterQuest() && dataOfClass.GetCurrentCharaEpisodeData() != null);
    }
  }
}
