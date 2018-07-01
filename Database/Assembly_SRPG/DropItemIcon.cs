// Decompiled with JetBrains decompiler
// Type: SRPG.DropItemIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class DropItemIcon : ItemIcon
  {
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
      ItemParam itemParam;
      int itemNum;
      this.InstanceType.GetInstanceData(this.InstanceIndex, ((Component) this).get_gameObject(), out itemParam, out itemNum);
      DataSource.Bind<ItemParam>(((Component) tooltip2).get_gameObject(), itemParam);
      CanvasStack component = (CanvasStack) ((Component) tooltip2).GetComponent<CanvasStack>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SystemModal = true;
      component.Priority = 1;
    }
  }
}
