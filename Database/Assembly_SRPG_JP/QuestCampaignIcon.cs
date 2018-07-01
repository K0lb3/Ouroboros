// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class QuestCampaignIcon : MonoBehaviour
  {
    public Text Text;
    public Image UnitIcon;

    public QuestCampaignIcon()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      QuestCampaignData dataOfClass = DataSource.FindDataOfClass<QuestCampaignData>(((Component) this).get_gameObject(), (QuestCampaignData) null);
      if (dataOfClass == null)
      {
        ((Component) this).get_gameObject().SetActive(false);
      }
      else
      {
        if (Object.op_Inequality((Object) this.UnitIcon, (Object) null) && !this.SetUnitIcon(dataOfClass))
          ((Component) this.UnitIcon).get_gameObject().SetActive(false);
        if (!Object.op_Inequality((Object) this.Text, (Object) null))
          return;
        this.Text.set_text(this.ValueToString(dataOfClass.value));
      }
    }

    private bool SetUnitIcon(QuestCampaignData data)
    {
      if (string.IsNullOrEmpty(data.unit))
        return false;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      UnitParam unitParam = instanceDirect.GetUnitParam(data.unit);
      if (unitParam == null)
        return false;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
      ItemParam itemParam = instanceDirect.GetItemParam(unitParam.piece);
      if (Object.op_Inequality((Object) this.UnitIcon, (Object) null))
        this.UnitIcon.set_sprite(spriteSheet.GetSprite(itemParam.icon));
      return true;
    }

    private void ValueToFraction(int value, out int num, out int denom)
    {
      float num1 = (float) value / 100f;
      int num2 = 10;
      float num3 = 1E-06f;
      for (int index = 1; index <= num2; ++index)
      {
        int num4 = (int) ((double) num1 * (double) index);
        int num5 = num4 + 1;
        float num6 = (float) num4 / (float) index;
        float num7 = (float) num5 / (float) index;
        if ((double) Mathf.Abs(num6 - num1) < (double) num3)
        {
          num = num4;
          denom = index;
          return;
        }
        if ((double) Mathf.Abs(num7 - num1) < (double) num3)
        {
          num = num5;
          denom = index;
          return;
        }
      }
      num = 1;
      denom = 1;
    }

    private string ValueToString(int value)
    {
      if (value < 100)
      {
        int num;
        int denom;
        this.ValueToFraction(value, out num, out denom);
        return string.Format("{0}/{1}", (object) num, (object) denom);
      }
      if (value % 100 == 0)
        return string.Format("{0}", (object) Mathf.FloorToInt((float) value / 100f));
      return string.Format("{0}.{1}", (object) (float) Mathf.FloorToInt((float) value / 100f), (object) (float) Mathf.FloorToInt((float) (value % 100) / 10f));
    }
  }
}
