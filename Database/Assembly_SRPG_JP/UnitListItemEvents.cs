// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListItemEvents
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitListItemEvents : ListItemEvents
  {
    private const float CHANGE_INTERBAL = 2f;
    public Image[] EqIcons;
    public Image[] AttrIcons;
    public GameObject Badge;
    public GameObject CharacterQuestBadge;
    public GameObject FavoriteBadge;
    public bool DispAlternate;
    private int m_Index;
    private float m_Time;

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
      EquipData[] currentEquips = dataOfClass.CurrentEquips;
      if (currentEquips != null && this.EqIcons != null && (this.EqIcons.Length >= currentEquips.Length && this.AttrIcons != null) && this.AttrIcons.Length >= currentEquips.Length)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
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
                  this.AttrIcons[index].set_sprite(spriteSheet.GetSprite(equipData.ItemParam.equipLv <= dataOfClass.Lv ? "plus0" : "plus1"));
                else if (player.CheckEnableCreateItem(equipData.ItemParam, true, 1, (NeedEquipItemList) null))
                  this.AttrIcons[index].set_sprite(spriteSheet.GetSprite(equipData.ItemParam.equipLv <= dataOfClass.Lv ? "plus0" : "plus1"));
                else
                  this.AttrIcons[index].set_sprite(spriteSheet.GetSprite("plus2"));
              }
            }
          }
        }
      }
      this.m_Time = 2f;
      this.Update();
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void Update()
    {
      if (!this.DispAlternate)
        this.UpdateBadgeDefault();
      else
        this.UpdateBadgeAlternate();
    }

    private void UpdateBadgeDefault()
    {
      UnitData dataOfClass1 = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (dataOfClass1 != null)
      {
        if (Object.op_Inequality((Object) this.Badge, (Object) null))
          this.Badge.SetActive(dataOfClass1.BadgeState != (UnitBadgeTypes) 0);
        if (Object.op_Inequality((Object) this.CharacterQuestBadge, (Object) null))
          this.CharacterQuestBadge.SetActive(dataOfClass1.IsOpenCharacterQuest() && dataOfClass1.GetCurrentCharaEpisodeData() != null);
        if (!Object.op_Inequality((Object) this.FavoriteBadge, (Object) null))
          return;
        this.FavoriteBadge.SetActive(dataOfClass1.IsFavorite);
      }
      else
      {
        UnitParam dataOfClass2 = DataSource.FindDataOfClass<UnitParam>(((Component) this).get_gameObject(), (UnitParam) null);
        if (dataOfClass2 == null || !Object.op_Inequality((Object) this.Badge, (Object) null))
          return;
        this.Badge.SetActive(MonoSingleton<GameManager>.Instance.CheckEnableUnitUnlock(dataOfClass2));
      }
    }

    private void UpdateBadgeAlternate()
    {
      List<GameObject> gameObjectList1 = new List<GameObject>();
      List<GameObject> gameObjectList2 = new List<GameObject>();
      UnitData dataOfClass1 = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (dataOfClass1 != null)
      {
        if (Object.op_Inequality((Object) this.Badge, (Object) null))
        {
          if (dataOfClass1.BadgeState != (UnitBadgeTypes) 0)
            gameObjectList1.Add(this.Badge);
          else
            gameObjectList2.Add(this.Badge);
        }
        if (Object.op_Inequality((Object) this.CharacterQuestBadge, (Object) null))
        {
          if (dataOfClass1.IsOpenCharacterQuest() && dataOfClass1.GetCurrentCharaEpisodeData() != null)
            gameObjectList1.Add(this.CharacterQuestBadge);
          else
            gameObjectList2.Add(this.CharacterQuestBadge);
        }
        if (Object.op_Inequality((Object) this.FavoriteBadge, (Object) null))
        {
          if (dataOfClass1.IsFavorite)
            gameObjectList1.Add(this.FavoriteBadge);
          else
            gameObjectList2.Add(this.FavoriteBadge);
        }
      }
      else
      {
        UnitParam dataOfClass2 = DataSource.FindDataOfClass<UnitParam>(((Component) this).get_gameObject(), (UnitParam) null);
        if (dataOfClass2 != null)
        {
          if (Object.op_Inequality((Object) this.Badge, (Object) null))
          {
            if (MonoSingleton<GameManager>.Instance.CheckEnableUnitUnlock(dataOfClass2))
              gameObjectList1.Add(this.Badge);
            else
              gameObjectList2.Add(this.Badge);
          }
          if (Object.op_Inequality((Object) this.CharacterQuestBadge, (Object) null))
            gameObjectList2.Add(this.CharacterQuestBadge);
          if (Object.op_Inequality((Object) this.FavoriteBadge, (Object) null))
            gameObjectList2.Add(this.FavoriteBadge);
        }
      }
      for (int index = 0; index < gameObjectList2.Count; ++index)
      {
        if (Object.op_Inequality((Object) gameObjectList2[index], (Object) null))
          gameObjectList2[index].SetActive(false);
      }
      this.m_Time += Time.get_deltaTime();
      if ((double) this.m_Time > 2.0)
      {
        this.m_Time -= 2f;
        ++this.m_Index;
        if (this.m_Index >= gameObjectList1.Count)
          this.m_Index = 0;
      }
      for (int index = 0; index < gameObjectList1.Count; ++index)
      {
        if (Object.op_Inequality((Object) gameObjectList1[index], (Object) null))
          gameObjectList1[index].SetActive(this.m_Index == index);
      }
    }
  }
}
