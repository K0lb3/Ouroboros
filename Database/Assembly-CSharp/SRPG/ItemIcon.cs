// Decompiled with JetBrains decompiler
// Type: SRPG.ItemIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ItemIcon : BaseIcon
  {
    [Description("「？」アイコン⇒正規アイコン変更アニメ開始までの時間")]
    public float SecretWaitSec = 1f;
    [Description("「？」アイコン⇒正規アイコン変更アニメトリガー名")]
    public string SecretAnimName = string.Empty;
    [Description("「？」アイコン⇒正規アイコン変更アニメ開始後、アイコンを差し替えるまでの時間")]
    public float SecretAnimWaitSec = 0.2f;
    protected const string TooltipPath = "UI/ItemTooltip";
    protected const string ICON_NAME_UNKNOWN = "IT_UNKNOWN";
    [Space(10f)]
    public GameParameter.ItemInstanceTypes InstanceType;
    public int InstanceIndex;
    [Space(10f)]
    public RawImage Icon;
    public Image Frame;
    public Text Num;
    public Slider NumSlider;
    public bool Tooltip;
    public Text HaveNum;
    public bool IsSecret;
    private ItemParam mSecretItemParam;
    [Description("個数表記GameObjectへの参照")]
    public GameObject SecretAmount;
    [Description("装備可能なユニットが存在する場合に表示状態を変更するバッジへの参照")]
    public Image SecretBadge;
    private bool mReqExchgSecretIcon;

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
      SRPG.Tooltip tooltip1 = AssetManager.Load<SRPG.Tooltip>("UI/ItemTooltip");
      if (!Object.op_Inequality((Object) tooltip1, (Object) null))
        return;
      SRPG.Tooltip tooltip2 = (SRPG.Tooltip) Object.Instantiate<SRPG.Tooltip>((M0) tooltip1);
      LayoutRebuilder.ForceRebuildLayoutImmediate(tooltip2.Body);
      Rect rect1 = tooltip2.Body.get_rect();
      // ISSUE: explicit reference operation
      float width = ((Rect) @rect1).get_width();
      Vector2 vector2_1 = screen;
      float num1 = (float) (screen.x - (double) width / 2.0);
      if ((double) num1 < 0.0)
        vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.get_right(), -num1));
      RectTransform component = (RectTransform) ((Component) ((Canvas) ((Component) ((Component) this).get_transform()).GetComponentInParent<Canvas>()).get_rootCanvas()).GetComponent<RectTransform>();
      LayoutRebuilder.ForceRebuildLayoutImmediate(component);
      Rect rect2 = component.get_rect();
      // ISSUE: explicit reference operation
      float num2 = ((Rect) @rect2).get_width() - (float) (screen.x + (double) width / 2.0);
      if ((double) num2 < 0.0)
        vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.get_left(), -num2));
      Vector2 vector2_2 = vector2_1;
      Vector2 up = Vector2.get_up();
      Rect rect3 = transform.get_rect();
      // ISSUE: explicit reference operation
      double height = (double) ((Rect) @rect3).get_height();
      Vector2 vector2_3 = Vector2.op_Multiply(Vector2.op_Multiply(up, (float) height), 0.5f);
      SRPG.Tooltip.TooltipPosition = Vector2.op_Addition(vector2_2, vector2_3);
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
      this.mSecretItemParam = itemParam;
      if (Object.op_Inequality((Object) this.Icon, (Object) null))
      {
        if (this.IsSecret)
        {
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon("IT_UNKNOWN"));
          if (Object.op_Implicit((Object) this.SecretAmount))
            this.SecretAmount.SetActive(false);
          if (Object.op_Implicit((Object) this.SecretBadge))
            ((Behaviour) this.SecretBadge).set_enabled(false);
        }
        else
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon(itemParam));
      }
      if (Object.op_Inequality((Object) this.Frame, (Object) null))
      {
        if (this.IsSecret)
        {
          if (GameSettings.Instance.ItemIcons.NormalFrames != null && GameSettings.Instance.ItemIcons.NormalFrames.Length != 0)
            this.Frame.set_sprite(GameSettings.Instance.ItemIcons.NormalFrames[0]);
        }
        else
          this.Frame.set_sprite(GameSettings.Instance.GetItemFrame(itemParam));
      }
      if (Object.op_Inequality((Object) this.Num, (Object) null))
        this.Num.set_text(itemNum.ToString());
      if (Object.op_Inequality((Object) this.NumSlider, (Object) null))
        this.NumSlider.set_value((float) itemNum / (float) (int) itemParam.cap);
      if (!Object.op_Inequality((Object) this.HaveNum, (Object) null))
        return;
      int num = -1;
      if (itemParam.iname == "$COIN")
      {
        num = MonoSingleton<GameManager>.Instance.Player.Coin;
      }
      else
      {
        ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(itemParam);
        if (itemDataByItemParam != null)
          num = itemDataByItemParam.Num;
      }
      if (num < 0)
        return;
      this.HaveNum.set_text(LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", new object[1]
      {
        (object) num
      }));
    }

    public void ExchgSecretIcon()
    {
      if (!this.IsSecret || this.mReqExchgSecretIcon || this.mSecretItemParam == null)
        return;
      this.mReqExchgSecretIcon = true;
      this.StartCoroutine(this.exchgSecretIcon());
    }

    [DebuggerHidden]
    private IEnumerator exchgSecretIcon()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ItemIcon.\u003CexchgSecretIcon\u003Ec__IteratorED() { \u003C\u003Ef__this = this };
    }
  }
}
