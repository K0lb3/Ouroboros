// Decompiled with JetBrains decompiler
// Type: SRPG.NeedEquipItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class NeedEquipItemList
  {
    public Dictionary<byte, NeedEquipItemDictionary> CommonNeedNum = new Dictionary<byte, NeedEquipItemDictionary>();
    public RecipeTree RecipeTree;
    public bool IsNotEnough;
    private ItemParam mLastAddParam;

    public NeedEquipItemList()
    {
      this.SetRecipeTree(new RecipeTree((ItemParam) null), false);
    }

    public void Add(ItemParam _param, int _need_picec, bool is_soul = false)
    {
      if (!this.CommonNeedNum.ContainsKey(_param.cmn_type))
        this.CommonNeedNum[_param.cmn_type] = new NeedEquipItemDictionary(_param, is_soul);
      this.CommonNeedNum[_param.cmn_type].Add(_param, _need_picec);
      this.mLastAddParam = _param;
    }

    public bool IsEnoughCommon()
    {
      if (this.IsNotEnough || this.CommonNeedNum.Keys.Count <= 0)
        return false;
      List<byte> byteList = new List<byte>((IEnumerable<byte>) this.CommonNeedNum.Keys);
      for (int index = 0; index < byteList.Count; ++index)
      {
        if (!this.CommonNeedNum[byteList[index]].IsEnough)
          return false;
      }
      return true;
    }

    public void SetRecipeTree(RecipeTree _recipe_tree, bool is_common)
    {
      if (this.RecipeTree != null)
        this.RecipeTree.SetChild(_recipe_tree);
      if (is_common)
        _recipe_tree.SetIsCommon();
      this.RecipeTree = _recipe_tree;
    }

    public void UpRecipeTree()
    {
      if (this.RecipeTree == null || this.RecipeTree.Parent == null)
        return;
      this.RecipeTree = this.RecipeTree.Parent;
    }

    public List<RecipeTree> GetCurrentRecipeTreeChildren()
    {
      if (this.RecipeTree != null)
      {
        while (this.RecipeTree != null && this.RecipeTree.Parent != null)
          this.UpRecipeTree();
      }
      if (this.RecipeTree != null)
        return this.RecipeTree.Children;
      return (List<RecipeTree>) null;
    }

    public string GetCommonItemListString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      using (Dictionary<byte, NeedEquipItemDictionary>.KeyCollection.Enumerator enumerator = this.CommonNeedNum.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          NeedEquipItemDictionary equipItemDictionary = this.CommonNeedNum[enumerator.Current];
          stringBuilder.Append(LocalizedText.Get("sys.COMMON_EQUIP_CHECK_ITEM", (object) equipItemDictionary.CommonItemParam.name, (object) equipItemDictionary.NeedPicec, (object) equipItemDictionary.CommonEquipItemNum));
          stringBuilder.Append(",");
        }
      }
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
      stringBuilder.Append("\n");
      return stringBuilder.ToString();
    }

    public void Remove()
    {
      if (this.mLastAddParam == null || !this.CommonNeedNum.ContainsKey(this.mLastAddParam.cmn_type))
        return;
      this.RecipeTree.RemoveLastAt();
      this.CommonNeedNum[this.mLastAddParam.cmn_type].Remove(this.mLastAddParam);
    }
  }
}
