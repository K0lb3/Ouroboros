// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPickerButtonChanger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitPickerButtonChanger : MonoBehaviour
  {
    [CustomGroup("ウィンド")]
    [CustomField("ウィンド", CustomFieldAttribute.Type.GameObject)]
    public GameObject m_Root;
    [CustomGroup("差し替えボタン画像")]
    [CustomField("OFF", CustomFieldAttribute.Type.UISprite)]
    public Sprite m_ImageDefault;
    [CustomField("ON", CustomFieldAttribute.Type.UISprite)]
    [CustomGroup("差し替えボタン画像")]
    public Sprite m_ImageOn;
    [CustomGroup("オブジェクト")]
    [CustomField("イメージ", CustomFieldAttribute.Type.UIImage)]
    public Image m_Image;
    [CustomField("テキスト", CustomFieldAttribute.Type.UIText)]
    [CustomGroup("オブジェクト")]
    public Text m_Text;
    private UnitListWindow m_Window;
    private UnitListSortWindow.SelectType m_Sort;
    private UnitListFilterWindow.SelectType m_Filter;

    public UnitPickerButtonChanger()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.m_Root, (Object) null))
        return;
      this.m_Window = (UnitListWindow) this.m_Root.GetComponent<UnitListWindow>();
    }

    private void Start()
    {
    }

    private void Update()
    {
      if (Object.op_Equality((Object) this.m_Window, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.m_Text, (Object) null) && this.m_Window.sortWindow != null && this.m_Sort != this.m_Window.sortWindow.GetSectionReg())
      {
        this.m_Sort = this.m_Window.sortWindow.GetSectionReg();
        string text = UnitListSortWindow.GetText(this.m_Sort);
        if (!string.IsNullOrEmpty(text) && this.m_Text.get_text() != text)
          this.m_Text.set_text(text);
      }
      if (!Object.op_Inequality((Object) this.m_Image, (Object) null) || this.m_Window.filterWindow == null)
        return;
      UnitListFilterWindow.SelectType selectReg = this.m_Window.filterWindow.GetSelectReg(UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5 | UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE | UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER);
      if (this.m_Filter == selectReg)
        return;
      this.m_Filter = selectReg;
      if ((this.m_Filter ^ (UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5 | UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE | UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER)) == UnitListFilterWindow.SelectType.NONE)
        this.m_Image.set_sprite(this.m_ImageDefault);
      else
        this.m_Image.set_sprite(this.m_ImageOn);
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }
  }
}
