// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class QuestCampaignList : MonoBehaviour
  {
    public GameObject TemplateExpPlayer;
    public GameObject TemplateExpUnit;
    public GameObject TemplateExpUnitAll;
    public GameObject TemplateDrapItem;
    public GameObject TemplateAp;
    public GameObject TemplateTrustUp;
    public GameObject TemplateTrustIncidence;
    public GameObject TemplateTrustSpecific;
    [Space(10f)]
    public Text TextConsumeAp;
    public Color TextConsumeApColor;

    public QuestCampaignList()
    {
      base.\u002Ector();
    }

    public void RefreshIcons()
    {
      this.ResetTemplateActive();
      QuestParam dataOfClass1 = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (dataOfClass1 != null && dataOfClass1.type == QuestTypes.Tower)
        return;
      QuestCampaignData[] dataOfClass2 = DataSource.FindDataOfClass<QuestCampaignData[]>(((Component) this).get_gameObject(), (QuestCampaignData[]) null);
      if (dataOfClass2 == null || dataOfClass2.Length == 0)
        return;
      bool flag = false;
      for (int index = 0; index < dataOfClass2.Length && index != 2; ++index)
      {
        GameObject gameObject = (GameObject) null;
        QuestCampaignData data = dataOfClass2[index];
        switch (data.type)
        {
          case QuestCampaignValueTypes.ExpPlayer:
            gameObject = this.TemplateExpPlayer;
            break;
          case QuestCampaignValueTypes.ExpUnit:
            gameObject = !string.IsNullOrEmpty(data.unit) ? this.TemplateExpUnit : this.TemplateExpUnitAll;
            break;
          case QuestCampaignValueTypes.DropRate:
          case QuestCampaignValueTypes.DropNum:
            if (!flag)
            {
              gameObject = this.TemplateDrapItem;
              flag = true;
              break;
            }
            break;
          case QuestCampaignValueTypes.Ap:
            gameObject = this.TemplateAp;
            if (Object.op_Inequality((Object) this.TextConsumeAp, (Object) null))
            {
              ((Graphic) this.TextConsumeAp).set_color(this.TextConsumeApColor);
              break;
            }
            break;
          case QuestCampaignValueTypes.TrustUp:
            gameObject = this.TemplateTrustUp;
            break;
          case QuestCampaignValueTypes.TrustIncidence:
            gameObject = this.TemplateTrustIncidence;
            break;
          case QuestCampaignValueTypes.TrustSpecific:
            gameObject = this.TemplateTrustSpecific;
            break;
        }
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          DataSource.Bind<QuestCampaignData>(gameObject, data);
          gameObject.SetActive(true);
        }
      }
      if (((Component) this).get_gameObject().get_activeSelf())
        return;
      ((Component) this).get_gameObject().SetActive(true);
    }

    private void Start()
    {
      this.RefreshIcons();
    }

    private void ResetTemplateActive()
    {
      if (Object.op_Inequality((Object) this.TemplateExpPlayer, (Object) null))
        this.TemplateExpPlayer.SetActive(false);
      if (Object.op_Inequality((Object) this.TemplateExpUnit, (Object) null))
        this.TemplateExpUnit.SetActive(false);
      if (Object.op_Inequality((Object) this.TemplateExpUnitAll, (Object) null))
        this.TemplateExpUnitAll.SetActive(false);
      if (Object.op_Inequality((Object) this.TemplateDrapItem, (Object) null))
        this.TemplateDrapItem.SetActive(false);
      if (Object.op_Inequality((Object) this.TemplateAp, (Object) null))
        this.TemplateAp.SetActive(false);
      if (Object.op_Inequality((Object) this.TemplateTrustUp, (Object) null))
        this.TemplateTrustUp.SetActive(false);
      if (Object.op_Inequality((Object) this.TemplateTrustIncidence, (Object) null))
        this.TemplateTrustIncidence.SetActive(false);
      if (!Object.op_Inequality((Object) this.TemplateTrustSpecific, (Object) null))
        return;
      this.TemplateTrustSpecific.SetActive(false);
    }
  }
}
