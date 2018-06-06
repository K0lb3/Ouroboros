// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
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
    [Space(10f)]
    public Text TextConsumeAp;
    public Color TextConsumeApColor;

    public QuestCampaignList()
    {
      base.\u002Ector();
    }

    public void RefreshIcons()
    {
      QuestParam dataOfClass1 = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (dataOfClass1 != null && dataOfClass1.type == QuestTypes.Tower)
      {
        ((Component) this).get_gameObject().SetActive(false);
      }
      else
      {
        QuestCampaignData[] dataOfClass2 = DataSource.FindDataOfClass<QuestCampaignData[]>(((Component) this).get_gameObject(), (QuestCampaignData[]) null);
        if (dataOfClass2 == null || dataOfClass2.Length == 0)
        {
          ((Component) this).get_gameObject().SetActive(false);
        }
        else
        {
          List<GameObject> gameObjectList = new List<GameObject>();
          for (int index = 0; index < ((Component) this).get_transform().get_childCount(); ++index)
          {
            Transform child = ((Component) this).get_transform().GetChild(index);
            if (!Object.op_Equality((Object) this.TemplateExpPlayer, (Object) ((Component) child).get_gameObject()) && !Object.op_Equality((Object) this.TemplateExpUnit, (Object) ((Component) child).get_gameObject()) && (!Object.op_Equality((Object) this.TemplateExpUnitAll, (Object) ((Component) child).get_gameObject()) && !Object.op_Equality((Object) this.TemplateDrapItem, (Object) ((Component) child).get_gameObject())) && !Object.op_Equality((Object) this.TemplateAp, (Object) ((Component) child).get_gameObject()))
              gameObjectList.Add(((Component) child).get_gameObject());
          }
          while (gameObjectList.Count != 0)
          {
            GameObject gameObject = gameObjectList[0];
            gameObjectList.Remove(gameObject);
            Object.DestroyImmediate((Object) gameObject);
          }
          bool flag = false;
          for (int index = 0; index < dataOfClass2.Length && index != 2; ++index)
          {
            GameObject gameObject1 = (GameObject) null;
            QuestCampaignData data = dataOfClass2[index];
            switch (data.type)
            {
              case QuestCampaignValueTypes.ExpPlayer:
                gameObject1 = this.TemplateExpPlayer;
                break;
              case QuestCampaignValueTypes.ExpUnit:
                gameObject1 = !string.IsNullOrEmpty(data.unit) ? this.TemplateExpUnit : this.TemplateExpUnitAll;
                break;
              case QuestCampaignValueTypes.DropRate:
              case QuestCampaignValueTypes.DropNum:
                if (!flag)
                {
                  gameObject1 = this.TemplateDrapItem;
                  flag = true;
                  break;
                }
                break;
              case QuestCampaignValueTypes.Ap:
                gameObject1 = this.TemplateAp;
                if (Object.op_Inequality((Object) this.TextConsumeAp, (Object) null))
                {
                  ((Graphic) this.TextConsumeAp).set_color(this.TextConsumeApColor);
                  break;
                }
                break;
            }
            if (Object.op_Inequality((Object) gameObject1, (Object) null))
            {
              GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) gameObject1);
              Vector3 localScale = gameObject2.get_transform().get_localScale();
              gameObject2.get_transform().SetParent(((Component) this).get_transform());
              gameObject2.get_transform().set_localScale(localScale);
              DataSource.Bind<QuestCampaignData>(gameObject2, data);
              gameObject2.SetActive(true);
            }
          }
          if (((Component) this).get_gameObject().get_activeSelf())
            return;
          ((Component) this).get_gameObject().SetActive(true);
        }
      }
    }

    private void Start()
    {
      this.RefreshIcons();
    }
  }
}
