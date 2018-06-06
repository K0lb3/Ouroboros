// Decompiled with JetBrains decompiler
// Type: SRPG.ItemIcon
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ItemIcon : BaseIcon
  {
    private const string TooltipPath = "UI/ItemTooltip";
    [Space(10f)]
    public GameParameter.ItemInstanceTypes InstanceType;
    public int InstanceIndex;
    [Space(10f)]
    public RawImage Icon;
    public Image Frame;
    public Text Num;
    public Slider NumSlider;
    public bool Tooltip;

    public override bool HasTooltip
    {
      get
      {
        if (!this.Tooltip)
          return false;
        ItemParam itemParam;
        int itemNum;
        this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).get_gameObject(), out itemParam, out itemNum);
        return itemParam != null;
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
      SRPG.Tooltip.TooltipPosition = Vector2.op_Addition(vector2_1, vector2_2);
      SRPG.Tooltip tooltip1 = AssetManager.Load<SRPG.Tooltip>("UI/ItemTooltip");
      if (!Object.op_Inequality((Object) tooltip1, (Object) null))
        return;
      SRPG.Tooltip tooltip2 = (SRPG.Tooltip) Object.Instantiate<SRPG.Tooltip>((M0) tooltip1);
      ItemParam itemParam;
      int itemNum;
      this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).get_gameObject(), out itemParam, out itemNum);
      DataSource.Bind<ItemParam>(((Component) tooltip2).get_gameObject(), itemParam);
    }

    public override void UpdateValue()
    {
      ItemParam itemParam;
      int itemNum;
      this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).get_gameObject(), out itemParam, out itemNum);
      if (itemParam == null)
        return;
      if (Object.op_Inequality((Object) this.Icon, (Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon(itemParam));
      if (Object.op_Inequality((Object) this.Frame, (Object) null))
        this.Frame.set_sprite(GameSettings.Instance.GetItemFrame(itemParam));
      if (Object.op_Inequality((Object) this.Num, (Object) null))
        this.Num.set_text(itemNum.ToString());
      if (!Object.op_Inequality((Object) this.NumSlider, (Object) null))
        return;
      this.NumSlider.set_value((float) itemNum / (float) (int) itemParam.cap);
    }
  }
}
