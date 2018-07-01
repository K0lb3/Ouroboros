// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ArtifactIcon : BaseIcon
  {
    private int mLastLv = -1;
    private int mLastLvCap = -1;
    private int mLastExpNum = -1;
    public RawImage Icon;
    public Image Rarity;
    public Image RarityMax;
    public Text RarityText;
    public Text RarityMaxText;
    public Image Frame;
    public Text Lv;
    public Text LvCap;
    public Text PreLvCap;
    public Slider LvGauge;
    public Slider ExpGauge;
    public Slider PieceGauge;
    public Image Category;
    public GameObject Owner;
    public Image OwnerIcon;
    public Text DecCost;
    public Text DecKakeraNum;
    public Text TransmuteCost;
    public GameObject NotRarityUp;
    public GameObject CanRarityUp;
    public bool ForceMask;
    public ArtifactIcon.InstanceTypes InstanceType;
    public int DeriveTriggerIndex;
    [NonSerialized]
    public GameObject IndexBadge;
    public GameObject RarityUp;
    public GameObject CanCreate;
    public Image RarityUpBack;
    public Image CanCreateBack;
    public Image CanCreateGauge;
    public Image DefaultGauge;
    public Image DefaultBack;
    public Text RarityUpCost;
    public Text PieceNum;
    public Image[] NotCreateGrayIcon;
    public RawImage[] NotCreateGrayRawIcon;
    public GameObject Favorite;
    public GameObject LockMask;

    private void Start()
    {
      this.UpdateValue();
    }

    private void OnDisable()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Icon, (UnityEngine.Object) null))
        return;
      instanceDirect.CancelTextureLoadRequest(this.Icon);
    }

    public override void UpdateValue()
    {
      ArtifactData data = (ArtifactData) null;
      ArtifactParam artifactParam = (ArtifactParam) null;
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      if (this.InstanceType == ArtifactIcon.InstanceTypes.ArtifactData)
        data = DataSource.FindDataOfClass<ArtifactData>(((Component) this).get_gameObject(), (ArtifactData) null);
      else if (this.InstanceType == ArtifactIcon.InstanceTypes.SkillAbilityDeriveParam)
      {
        SkillAbilityDeriveParam dataOfClass = DataSource.FindDataOfClass<SkillAbilityDeriveParam>(((Component) this).get_gameObject(), (SkillAbilityDeriveParam) null);
        if (dataOfClass != null)
          artifactParam = dataOfClass.GetTriggerArtifactParam(this.DeriveTriggerIndex);
      }
      else if (this.InstanceType == ArtifactIcon.InstanceTypes.ArtifactDataOrParam)
      {
        data = DataSource.FindDataOfClass<ArtifactData>(((Component) this).get_gameObject(), (ArtifactData) null);
        if (data == null)
          artifactParam = DataSource.FindDataOfClass<ArtifactParam>(((Component) this).get_gameObject(), (ArtifactParam) null);
      }
      else
        artifactParam = DataSource.FindDataOfClass<ArtifactParam>(((Component) this).get_gameObject(), (ArtifactParam) null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Lv, (UnityEngine.Object) null))
      {
        if (data != null)
        {
          if ((int) data.Lv != this.mLastLv)
          {
            this.mLastLv = (int) data.Lv;
            this.Lv.set_text(data.Lv.ToString());
          }
          ((Component) this.Lv).get_gameObject().SetActive(true);
        }
        else
          ((Component) this.Lv).get_gameObject().SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PreLvCap, (UnityEngine.Object) null))
      {
        if (data != null && (int) data.Rarity > 0)
        {
          this.PreLvCap.set_text(MonoSingleton<GameManager>.Instance.GetRarityParam((int) data.Rarity - 1).ArtifactLvCap.ToString());
          ((Component) this.PreLvCap).get_gameObject().SetActive(true);
        }
        else
          ((Component) this.PreLvCap).get_gameObject().SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LvCap, (UnityEngine.Object) null))
      {
        if (data != null)
        {
          if ((int) data.LvCap != this.mLastLvCap)
          {
            this.mLastLvCap = (int) data.LvCap;
            this.LvCap.set_text(data.LvCap.ToString());
          }
          ((Component) this.LvCap).get_gameObject().SetActive(true);
        }
        else
          ((Component) this.LvCap).get_gameObject().SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LvGauge, (UnityEngine.Object) null))
      {
        if (data != null)
        {
          if (data.Exp != this.mLastExpNum)
          {
            this.LvGauge.set_minValue(1f);
            this.LvGauge.set_maxValue((float) (int) data.LvCap);
            this.LvGauge.set_value((float) (int) data.Lv);
          }
          ((Component) this.LvGauge).get_gameObject().SetActive(true);
        }
        else
          ((Component) this.LvGauge).get_gameObject().SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpGauge, (UnityEngine.Object) null))
      {
        if (data != null)
        {
          if (data.Exp != this.mLastExpNum)
          {
            if ((int) data.Lv >= (int) data.LvCap)
            {
              this.ExpGauge.set_minValue(0.0f);
              Slider expGauge = this.ExpGauge;
              float num1 = 1f;
              this.ExpGauge.set_value(num1);
              double num2 = (double) num1;
              expGauge.set_maxValue((float) num2);
            }
            else
            {
              int showExp = data.GetShowExp();
              int nextExp = data.GetNextExp();
              this.ExpGauge.set_minValue(0.0f);
              this.ExpGauge.set_maxValue((float) (showExp + nextExp));
              this.ExpGauge.set_value((float) showExp);
            }
          }
          ((Component) this.ExpGauge).get_gameObject().SetActive(true);
        }
        else
          ((Component) this.ExpGauge).get_gameObject().SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PieceGauge, (UnityEngine.Object) null))
      {
        if (artifactParam != null)
        {
          this.PieceGauge.set_minValue(0.0f);
          ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(artifactParam.kakera);
          this.PieceGauge.set_maxValue((float) (int) MonoSingleton<GameManager>.Instance.GetRarityParam(artifactParam.rareini).ArtifactCreatePieceNum);
          this.PieceGauge.set_value(itemDataByItemId == null ? 0.0f : (float) itemDataByItemId.Num);
          ((Component) this.PieceGauge).get_gameObject().SetActive(true);
        }
        else
          ((Component) this.PieceGauge).get_gameObject().SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Icon, (UnityEngine.Object) null))
      {
        if (data != null || artifactParam != null)
        {
          string path = AssetPath.ArtifactIcon(data == null ? artifactParam : data.ArtifactParam);
          instance1.ApplyTextureAsync(this.Icon, path);
        }
        else
        {
          instance1.CancelTextureLoadRequest(this.Icon);
          this.Icon.set_texture((Texture) null);
        }
      }
      int index1 = 0;
      int index2 = 0;
      if (data != null)
      {
        index1 = (int) data.Rarity;
        index2 = (int) data.RarityCap;
      }
      else if (artifactParam != null)
      {
        index1 = artifactParam.rareini;
        index2 = artifactParam.raremax;
      }
      if (data != null || artifactParam != null)
      {
        bool flag1 = data != null && data.CheckEnableRarityUp() == ArtifactData.RarityUpResults.Success;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUp, (UnityEngine.Object) null))
          this.RarityUp.SetActive(flag1);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUpBack, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DefaultBack, (UnityEngine.Object) null))
        {
          ((Behaviour) this.RarityUpBack).set_enabled(flag1);
          ((Behaviour) this.DefaultBack).set_enabled(!flag1);
        }
        bool flag2 = false;
        if (artifactParam != null)
        {
          ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(artifactParam.kakera);
          if (itemDataByItemId != null)
          {
            RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam(artifactParam.rareini);
            flag2 = itemDataByItemId.Num >= (int) rarityParam.ArtifactCreatePieceNum;
          }
          else
            flag2 = false;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CanCreate, (UnityEngine.Object) null))
          this.CanCreate.SetActive(flag2);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CanCreateBack, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DefaultBack, (UnityEngine.Object) null))
        {
          ((Behaviour) this.CanCreateBack).set_enabled(flag2);
          ((Behaviour) this.DefaultBack).set_enabled(!flag2);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CanCreateGauge, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DefaultBack, (UnityEngine.Object) null))
        {
          ((Behaviour) this.CanCreateGauge).set_enabled(flag2);
          ((Behaviour) this.DefaultBack).set_enabled(!flag2);
        }
        if (this.NotCreateGrayIcon != null && this.NotCreateGrayIcon.Length > 0)
        {
          if (flag2)
          {
            for (int index3 = 0; index3 < this.NotCreateGrayIcon.Length; ++index3)
              ((Graphic) this.NotCreateGrayIcon[index3]).set_color(Color.get_white());
          }
          else
          {
            for (int index3 = 0; index3 < this.NotCreateGrayIcon.Length; ++index3)
              ((Graphic) this.NotCreateGrayIcon[index3]).set_color(Color.get_cyan());
          }
        }
        if (this.NotCreateGrayRawIcon != null && this.NotCreateGrayRawIcon.Length > 0)
        {
          if (flag2)
          {
            for (int index3 = 0; index3 < this.NotCreateGrayRawIcon.Length; ++index3)
              ((Graphic) this.NotCreateGrayRawIcon[index3]).set_color(Color.get_white());
          }
          else
          {
            for (int index3 = 0; index3 < this.NotCreateGrayRawIcon.Length; ++index3)
              ((Graphic) this.NotCreateGrayRawIcon[index3]).set_color(Color.get_cyan());
          }
        }
        if (data != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NotRarityUp, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CanRarityUp, (UnityEngine.Object) null))
        {
          bool flag3 = (int) data.Rarity == (int) data.RarityCap;
          this.NotRarityUp.SetActive(flag3);
          this.CanRarityUp.SetActive(!flag3);
        }
        if (data != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUpCost, (UnityEngine.Object) null))
          this.RarityUpCost.set_text(data.GetKakeraNeedNum().ToString());
        if (artifactParam != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TransmuteCost, (UnityEngine.Object) null))
          this.TransmuteCost.set_text((int) MonoSingleton<GameManager>.Instance.GetRarityParam(artifactParam.rareini).ArtifactCreatePieceNum.ToString());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PieceNum, (UnityEngine.Object) null))
        {
          ItemData itemData = data == null ? MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(artifactParam.kakera) : MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(data.Kakera);
          if (itemData != null)
          {
            this.PieceNum.set_text(itemData.Num.ToString());
            if (data != null && data.CheckEnableRarityUp() == ArtifactData.RarityUpResults.Success)
              ((Graphic) this.PieceNum).set_color(Color.get_yellow());
            else
              ((Graphic) this.PieceNum).set_color(Color.get_white());
          }
          else
          {
            this.PieceNum.set_text("0");
            ((Graphic) this.PieceNum).set_color(Color.get_white());
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Rarity, (UnityEngine.Object) null))
        {
          GameSettings instance2 = GameSettings.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance2, (UnityEngine.Object) null) && index1 < instance2.ArtifactIcon_Rarity.Length)
            this.Rarity.set_sprite(instance2.ArtifactIcon_Rarity[index1]);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityMax, (UnityEngine.Object) null))
        {
          GameSettings instance2 = GameSettings.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance2, (UnityEngine.Object) null) && index2 < instance2.ArtifactIcon_RarityBG.Length)
            this.RarityMax.set_sprite(instance2.ArtifactIcon_RarityBG[index2]);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityText, (UnityEngine.Object) null))
          this.RarityText.set_text((index1 + 1).ToString());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityMaxText, (UnityEngine.Object) null))
          this.RarityMaxText.set_text((index2 + 1).ToString());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Frame, (UnityEngine.Object) null))
        {
          GameSettings instance2 = GameSettings.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance2, (UnityEngine.Object) null) && index1 < instance2.ArtifactIcon_Frames.Length)
            this.Frame.set_sprite(instance2.ArtifactIcon_Frames[index1]);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Category, (UnityEngine.Object) null))
        {
          GameSettings instance2 = GameSettings.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance2, (UnityEngine.Object) null) && (data != null || artifactParam != null))
          {
            switch (data == null ? (int) artifactParam.type : (int) data.ArtifactParam.type)
            {
              case 1:
                this.Category.set_sprite(instance2.ArtifactIcon_Weapon);
                break;
              case 2:
                this.Category.set_sprite(instance2.ArtifactIcon_Armor);
                break;
              case 3:
                this.Category.set_sprite(instance2.ArtifactIcon_Misc);
                break;
            }
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecKakeraNum, (UnityEngine.Object) null))
          this.DecKakeraNum.set_text(data.GetKakeraChangeNum().ToString());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecCost, (UnityEngine.Object) null))
          this.DecCost.set_text(data.RarityParam.ArtifactChangeCost.ToString());
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Rarity, (UnityEngine.Object) null))
          this.Rarity.set_sprite((Sprite) null);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityMax, (UnityEngine.Object) null))
          this.RarityMax.set_sprite((Sprite) null);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Frame, (UnityEngine.Object) null))
          this.Frame.set_sprite((Sprite) null);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Category, (UnityEngine.Object) null))
          this.Category.set_sprite((Sprite) null);
      }
      bool flag = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Owner, (UnityEngine.Object) null))
      {
        if (data != null && this.SetOwnerIcon(instance1, data))
        {
          this.Owner.SetActive(true);
          flag = true;
        }
        else
          this.Owner.SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Favorite, (UnityEngine.Object) null))
      {
        if (data != null && data.IsFavorite)
        {
          this.Favorite.SetActive(true);
          flag = true;
        }
        else
          this.Favorite.SetActive(false);
      }
      if (this.ForceMask)
        flag = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LockMask, (UnityEngine.Object) null))
        this.LockMask.SetActive(flag);
      if (data == null)
        return;
      this.mLastExpNum = data.Exp;
    }

    private bool SetOwnerIcon(GameManager gm, ArtifactData data)
    {
      UnitData unit;
      JobData job;
      if (!gm.Player.FindOwner(data, out unit, out job))
        return false;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
      ItemParam itemParam = gm.GetItemParam(unit.UnitParam.piece);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OwnerIcon, (UnityEngine.Object) null))
        this.OwnerIcon.set_sprite(spriteSheet.GetSprite(itemParam.icon));
      return true;
    }

    public enum InstanceTypes
    {
      ArtifactData,
      ArtifactParam,
      SkillAbilityDeriveParam,
      ArtifactDataOrParam,
    }
  }
}
