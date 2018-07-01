// Decompiled with JetBrains decompiler
// Type: SRPG.ChangeList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ChangeList : MonoBehaviour
  {
    public GameObject List;
    [FlexibleArray]
    public ChangeListItem[] ListItems;

    public ChangeList()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!Object.op_Equality((Object) this.List, (Object) null))
        return;
      this.List = ((Component) this).get_gameObject();
    }

    private void Start()
    {
      for (int index = 0; index < this.ListItems.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.ListItems[index], (Object) null) && ((Component) this.ListItems[index]).get_gameObject().get_activeInHierarchy())
          ((Component) this.ListItems[index]).get_gameObject().SetActive(false);
      }
    }

    public void SetData(ChangeListData[] src)
    {
      Transform transform = this.List.get_transform();
      for (int index1 = 0; index1 < src.Length; ++index1)
      {
        ChangeListData changeListData = src[index1];
        int index2 = -1;
        for (int index3 = 0; index3 < this.ListItems.Length; ++index3)
        {
          if (Object.op_Inequality((Object) this.ListItems[index3], (Object) null) && this.ListItems[index3].ID == changeListData.ItemID)
          {
            index2 = index3;
            break;
          }
        }
        if (index2 != -1)
        {
          ChangeListItem changeListItem = (ChangeListItem) Object.Instantiate<ChangeListItem>((M0) this.ListItems[index2]);
          if (changeListData.MetaData != null && (object) changeListData.MetaDataType != null)
            DataSource.Bind(((Component) changeListItem).get_gameObject(), changeListData.MetaDataType, changeListData.MetaData);
          if (Object.op_Inequality((Object) changeListItem.ValNew, (Object) null) && !string.IsNullOrEmpty(changeListData.ValNew))
            changeListItem.ValNew.set_text(changeListData.ValNew.ToString());
          if (Object.op_Inequality((Object) changeListItem.ValOld, (Object) null) && !string.IsNullOrEmpty(changeListData.ValOld))
            changeListItem.ValOld.set_text(changeListData.ValOld.ToString());
          long result1;
          long result2;
          if (Object.op_Inequality((Object) changeListItem.Diff, (Object) null) && !string.IsNullOrEmpty(changeListData.ValNew) && (!string.IsNullOrEmpty(changeListData.ValOld) && long.TryParse(changeListData.ValOld, out result1)) && long.TryParse(changeListData.ValNew, out result2))
            changeListItem.Diff.set_text((result2 - result1).ToString());
          if (Object.op_Inequality((Object) changeListItem.Label, (Object) null) && !string.IsNullOrEmpty(changeListData.Label))
            changeListItem.Label.set_text(changeListData.Label);
          ((Component) changeListItem).get_gameObject().SetActive(true);
          ((Component) changeListItem).get_transform().SetParent(transform, false);
        }
      }
    }
  }
}
