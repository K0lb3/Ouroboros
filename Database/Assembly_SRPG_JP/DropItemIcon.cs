// Decompiled with JetBrains decompiler
// Type: SRPG.DropItemIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class DropItemIcon : ItemIcon
  {
    private ConceptCardParam mSecretConceptCardParam;

    public override bool HasTooltip
    {
      get
      {
        if (!this.Tooltip)
          return false;
        ItemParam itemParam = (ItemParam) null;
        int itemNum = 0;
        this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).get_gameObject(), out itemParam, out itemNum);
        if (itemParam != null)
          return true;
        ConceptCardParam conceptCardParam = (ConceptCardParam) null;
        QuestResult.DropItemData dropItemData = (QuestResult.DropItemData) null;
        this.GetParam(ref conceptCardParam, ref dropItemData);
        return conceptCardParam != null || dropItemData != null;
      }
    }

    protected override void ShowTooltip(Vector2 screen)
    {
      RectTransform transform = ((Component) this).get_transform() as RectTransform;
      Vector2 vector2_1 = screen;
      Vector2 up = Vector2.get_up();
      Rect rect = transform.get_rect();
      // ISSUE: explicit reference operation
      double height = (double) ((Rect) @rect).get_height();
      Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Multiply(up, (float) height), 0.5f);
      Tooltip.TooltipPosition = Vector2.op_Addition(vector2_1, vector2_2);
      Tooltip tooltip1 = AssetManager.Load<Tooltip>("UI/ItemTooltip");
      if (!Object.op_Inequality((Object) tooltip1, (Object) null))
        return;
      Tooltip tooltip2 = (Tooltip) Object.Instantiate<Tooltip>((M0) tooltip1);
      ItemParam itemParam = (ItemParam) null;
      int itemNum = 0;
      this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).get_gameObject(), out itemParam, out itemNum);
      string str1 = string.Empty;
      string str2 = string.Empty;
      if (this.IsSecret)
      {
        str1 = "sys.ITEMTOOLTIP_SECRET_NAME";
        str2 = "sys.ITEMTOOLTIP_SECRET_DESC";
      }
      else if (itemParam != null)
      {
        str1 = itemParam.name;
        str2 = itemParam.Expr;
        DataSource.Bind<ItemParam>(((Component) tooltip2).get_gameObject(), itemParam);
      }
      else
      {
        ConceptCardParam conceptCardParam = (ConceptCardParam) null;
        QuestResult.DropItemData dropItemData = (QuestResult.DropItemData) null;
        this.GetParam(ref conceptCardParam, ref dropItemData);
        if (conceptCardParam != null)
        {
          str1 = conceptCardParam.name;
          str2 = conceptCardParam.expr;
        }
        else if (dropItemData != null)
        {
          if (dropItemData.IsItem)
          {
            str1 = dropItemData.itemParam.name;
            str2 = dropItemData.itemParam.Expr;
          }
          else if (dropItemData.IsConceptCard)
          {
            str1 = dropItemData.conceptCardParam.name;
            str2 = dropItemData.conceptCardParam.expr;
          }
          int num = dropItemData.Num;
        }
      }
      if (Object.op_Implicit((Object) tooltip2.TextName))
      {
        GameParameter component = (GameParameter) ((Component) tooltip2.TextName).GetComponent<GameParameter>();
        if (Object.op_Implicit((Object) component))
          ((Behaviour) component).set_enabled(false);
        tooltip2.TextName.set_text(str1);
      }
      if (Object.op_Implicit((Object) tooltip2.TextDesc))
      {
        GameParameter component = (GameParameter) ((Component) tooltip2.TextDesc).GetComponent<GameParameter>();
        if (Object.op_Implicit((Object) component))
          ((Behaviour) component).set_enabled(false);
        tooltip2.TextDesc.set_text(str2);
      }
      CanvasStack component1 = (CanvasStack) ((Component) tooltip2).GetComponent<CanvasStack>();
      if (!Object.op_Inequality((Object) component1, (Object) null))
        return;
      component1.SystemModal = true;
      component1.Priority = 1;
    }

    public override void UpdateValue()
    {
      ItemParam itemParam = (ItemParam) null;
      int itemNum = 0;
      this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).get_gameObject(), out itemParam, out itemNum);
      if (itemParam != null)
      {
        base.UpdateValue();
      }
      else
      {
        ConceptCardParam conceptCardParam = (ConceptCardParam) null;
        QuestResult.DropItemData dropItemData = (QuestResult.DropItemData) null;
        this.GetParam(ref conceptCardParam, ref dropItemData);
        if (conceptCardParam != null)
        {
          this.Refresh_ConceptCard(conceptCardParam);
        }
        else
        {
          if (dropItemData == null)
            return;
          this.Refresh_DropItem(dropItemData);
        }
      }
    }

    private void Refresh_Item(ItemParam param)
    {
      if (param == null)
        return;
      this.SetIconAsync(param, this.IsSecret);
      this.SetFrameSprite(param, this.IsSecret);
      this.SwapIconFramePriority(true);
      if (!this.IsSecret)
        return;
      if (Object.op_Implicit((Object) this.SecretAmount))
        this.SecretAmount.SetActive(false);
      if (!Object.op_Implicit((Object) this.SecretBadge))
        return;
      ((Behaviour) this.SecretBadge).set_enabled(false);
    }

    private void Refresh_ConceptCard(ConceptCardParam param)
    {
      if (param == null)
        return;
      this.mSecretConceptCardParam = param;
      this.SetIconAsync(param, this.IsSecret);
      this.SetFrameSprite(param, this.IsSecret);
      this.SwapIconFramePriority(this.IsSecret);
      if (!this.IsSecret)
        return;
      if (Object.op_Implicit((Object) this.SecretAmount))
        this.SecretAmount.SetActive(false);
      if (!Object.op_Implicit((Object) this.SecretBadge))
        return;
      ((Behaviour) this.SecretBadge).set_enabled(false);
    }

    private void Refresh_DropItem(QuestResult.DropItemData data)
    {
      if (data == null)
        return;
      if (data.IsItem)
        this.Refresh_Item(data.itemParam);
      else if (data.IsConceptCard)
        this.Refresh_ConceptCard(data.conceptCardParam);
      if (Object.op_Inequality((Object) this.Num, (Object) null))
        this.Num.set_text(data.Num.ToString());
      if (!Object.op_Inequality((Object) this.HaveNum, (Object) null))
        return;
      int num = -1;
      if (data.IsItem)
        num = this.GetHaveNum(data.itemParam, -1);
      else if (data.IsConceptCard)
        num = this.GetHaveNum(data.conceptCardParam, -1);
      if (num < 0)
        return;
      this.HaveNum.set_text(LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", new object[1]
      {
        (object) num
      }));
    }

    private void SetFrameSprite(ItemParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Frame, (Object) null))
        return;
      this.Frame.set_sprite(this.GetFrameSprite(param, isSecret));
    }

    private void SetFrameSprite(ConceptCardParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Frame, (Object) null))
        return;
      this.Frame.set_sprite(this.GetFrameSprite(param, isSecret));
    }

    private void SetIconAsync(ItemParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, this.GetIconPath(param, isSecret));
    }

    private void SetIconAsync(ConceptCardParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, this.GetIconPath(param, isSecret));
    }

    private void LoadIcon(ItemParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      this.Icon.set_texture((Texture) AssetManager.Load<Texture2D>(this.GetIconPath(param, isSecret)));
    }

    private void LoadIcon(ConceptCardParam param, bool isSecret)
    {
      if (Object.op_Equality((Object) this.Icon, (Object) null))
        return;
      this.Icon.set_texture((Texture) AssetManager.Load<Texture2D>(this.GetIconPath(param, isSecret)));
    }

    private void SwapIconFramePriority(bool iconIsTop)
    {
      if (!Object.op_Inequality((Object) this.Icon, (Object) null) || !Object.op_Inequality((Object) this.Frame, (Object) null))
        return;
      int siblingIndex1 = ((Component) this.Icon).get_transform().GetSiblingIndex();
      int siblingIndex2 = ((Component) this.Frame).get_transform().GetSiblingIndex();
      int num1 = Mathf.Min(siblingIndex1, siblingIndex2);
      int num2 = Mathf.Max(siblingIndex1, siblingIndex2);
      if (iconIsTop)
      {
        ((Component) this.Icon).get_transform().SetSiblingIndex(num2);
        ((Component) this.Frame).get_transform().SetSiblingIndex(num1);
      }
      else
      {
        ((Component) this.Icon).get_transform().SetSiblingIndex(num1);
        ((Component) this.Frame).get_transform().SetSiblingIndex(num2);
      }
    }

    private void GetParam(ref ConceptCardParam conceptCardParam, ref QuestResult.DropItemData dropItemData)
    {
      conceptCardParam = DataSource.FindDataOfClass<ConceptCardParam>(((Component) this).get_gameObject(), (ConceptCardParam) null);
      if (conceptCardParam != null)
        return;
      dropItemData = DataSource.FindDataOfClass<QuestResult.DropItemData>(((Component) this).get_gameObject(), (QuestResult.DropItemData) null);
      if (dropItemData != null)
        ;
    }

    private string GetSecretIconPath()
    {
      return AssetPath.ItemIcon("IT_UNKNOWN");
    }

    private string GetIconPath(ItemData data, bool isSecret)
    {
      return this.GetIconPath(data == null ? (ItemParam) null : data.Param, isSecret);
    }

    private string GetIconPath(ItemParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretIconPath();
      if (param == null)
        return string.Empty;
      return AssetPath.ItemIcon(param);
    }

    private string GetIconPath(ConceptCardParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretIconPath();
      if (param == null)
        return string.Empty;
      return AssetPath.ConceptCardIcon(param);
    }

    private Sprite GetSecretFrameSprite(Sprite defaultSprite)
    {
      if (GameSettings.Instance.ItemIcons.NormalFrames != null && GameSettings.Instance.ItemIcons.NormalFrames.Length != 0)
        return GameSettings.Instance.ItemIcons.NormalFrames[0];
      return defaultSprite;
    }

    private Sprite GetFrameSprite(ItemData data, bool isSecret)
    {
      return this.GetFrameSprite(data == null ? (ItemParam) null : data.Param, isSecret);
    }

    private Sprite GetFrameSprite(ItemParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretFrameSprite(!Object.op_Inequality((Object) this.Frame, (Object) null) ? (Sprite) null : this.Frame.get_sprite());
      if (param == null)
        return (Sprite) null;
      return GameSettings.Instance.GetItemFrame(param);
    }

    private Sprite GetFrameSprite(ConceptCardParam param, bool isSecret)
    {
      if (isSecret)
        return this.GetSecretFrameSprite(!Object.op_Inequality((Object) this.Frame, (Object) null) ? (Sprite) null : this.Frame.get_sprite());
      if (param == null)
        return (Sprite) null;
      return GameSettings.Instance.GetConceptCardFrame(param.rare);
    }

    private int GetHaveNum(ItemParam param, int default_value)
    {
      if (param.iname == "$COIN")
        return MonoSingleton<GameManager>.Instance.Player.Coin;
      ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(param);
      if (itemDataByItemParam != null)
        return itemDataByItemParam.Num;
      return default_value;
    }

    private int GetHaveNum(ConceptCardParam param, int default_value)
    {
      return default_value;
    }

    public override void ExchgSecretIcon()
    {
      if (!this.IsSecret || this.mReqExchgSecretIcon || this.mSecretItemParam == null && this.mSecretConceptCardParam == null)
        return;
      this.mReqExchgSecretIcon = true;
      this.StartCoroutine(this.exchgSecretIcon());
    }

    [DebuggerHidden]
    protected override IEnumerator exchgSecretIcon()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new DropItemIcon.\u003CexchgSecretIcon\u003Ec__Iterator106()
      {
        \u003C\u003Ef__this = this
      };
    }
  }
}
