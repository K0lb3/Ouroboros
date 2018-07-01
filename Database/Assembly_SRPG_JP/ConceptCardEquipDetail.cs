// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEquipDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "更新終了", FlowNode.PinTypes.Output, 10)]
  public class ConceptCardEquipDetail : MonoBehaviour
  {
    public const int PIN_REFRESH = 0;
    public const int PIN_REFRESH_END = 10;
    [HeaderBar("▼ConceptCardDescriptionの参照方式")]
    [SerializeField]
    private ConceptCardEquipDetail.DescriptionInstanceType m_DescriptionInstanceType;
    [SerializeField]
    private ConceptCardDescription mConceptCardDescription;
    [SerializeField]
    [HeaderBar("▼複製したConceptCardDescriptionを入れる親")]
    private RectTransform mConceptCardDescriptionRoot;
    [SerializeField]
    private GameObject mConceptCardIconRoot;
    [SerializeField]
    private Text mCardNameText;
    [SerializeField]
    private ConceptCardIcon mConceptCardIcon;
    [SerializeField]
    private Text mConceptCardNum;
    private ConceptCardData mConceptCardData;
    private UnitData mUnitData;
    private static UnitData s_UnitData;

    public ConceptCardEquipDetail()
    {
      base.\u002Ector();
    }

    public static void SetSelectedUnitData(UnitData mUnitData)
    {
      ConceptCardEquipDetail.s_UnitData = mUnitData;
    }

    private void Start()
    {
      if (this.m_DescriptionInstanceType == ConceptCardEquipDetail.DescriptionInstanceType.PrefabInstantiate)
      {
        this.mConceptCardDescription = (ConceptCardDescription) Object.Instantiate<ConceptCardDescription>((M0) this.mConceptCardDescription);
        ((Component) this.mConceptCardDescription).get_transform().SetParent((Transform) this.mConceptCardDescriptionRoot, false);
      }
      this.SetParam();
    }

    public void SetParam()
    {
      this.mConceptCardData = (ConceptCardData) GlobalVars.SelectedConceptCardData;
      this.mUnitData = ConceptCardEquipDetail.s_UnitData;
      if (this.mConceptCardData == null)
        return;
      this.Refresh();
      this.mConceptCardDescription.SetConceptCardData(this.mConceptCardData, ((Component) this).get_gameObject(), false, this.CheckGetUnitFrame(), this.mUnitData);
    }

    private bool CheckGetUnitFrame()
    {
      bool flag = false;
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue != null)
        flag = currentValue.GetBool("is_first_get_unit");
      return flag;
    }

    private void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      this.SetText(this.mCardNameText, this.mConceptCardData.Param.name);
      if (Object.op_Inequality((Object) this.mConceptCardIconRoot, (Object) null))
        this.mConceptCardIconRoot.SetActive(true);
      if (Object.op_Inequality((Object) this.mConceptCardIcon, (Object) null))
        this.mConceptCardIcon.Setup(this.mConceptCardData);
      this.SetText(this.mConceptCardNum, MonoSingleton<GameManager>.Instance.Player.GetConceptCardNum(this.mConceptCardData.Param.iname));
    }

    public void SetText(Text text, string str)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.set_text(str);
    }

    public void SetText(Text text, int value)
    {
      this.SetText(text, value.ToString());
    }

    private void OnDestroy()
    {
      ConceptCardEquipDetail.s_UnitData = (UnitData) null;
    }

    private enum DescriptionInstanceType
    {
      DirectUse,
      PrefabInstantiate,
    }
  }
}
