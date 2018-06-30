namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    public class NeedEquipItemList
    {
        public Dictionary<byte, NeedEquipItemDictionary> CommonNeedNum;
        public SRPG.RecipeTree RecipeTree;
        public bool IsNotEnough;
        private ItemParam mLastAddParam;

        public NeedEquipItemList()
        {
            this.CommonNeedNum = new Dictionary<byte, NeedEquipItemDictionary>();
            base..ctor();
            this.SetRecipeTree(new SRPG.RecipeTree(null), 0);
            return;
        }

        public void Add(ItemParam _param, int _need_picec, bool is_soul)
        {
            if (this.CommonNeedNum.ContainsKey(_param.cmn_type) != null)
            {
                goto Label_002E;
            }
            this.CommonNeedNum[_param.cmn_type] = new NeedEquipItemDictionary(_param, is_soul);
        Label_002E:
            this.CommonNeedNum[_param.cmn_type].Add(_param, _need_picec);
            this.mLastAddParam = _param;
            return;
        }

        public unsafe string GetCommonItemListString()
        {
            object[] objArray1;
            StringBuilder builder;
            byte num;
            Dictionary<byte, NeedEquipItemDictionary>.KeyCollection.Enumerator enumerator;
            NeedEquipItemDictionary dictionary;
            builder = new StringBuilder();
            enumerator = this.CommonNeedNum.Keys.GetEnumerator();
        Label_0017:
            try
            {
                goto Label_007E;
            Label_001C:
                num = &enumerator.Current;
                dictionary = this.CommonNeedNum[num];
                objArray1 = new object[] { dictionary.CommonItemParam.name, (int) dictionary.NeedPicec, (int) dictionary.CommonEquipItemNum };
                builder.Append(LocalizedText.Get("sys.COMMON_EQUIP_CHECK_ITEM", objArray1));
                builder.Append(",");
            Label_007E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001C;
                }
                goto Label_009B;
            }
            finally
            {
            Label_008F:
                ((Dictionary<byte, NeedEquipItemDictionary>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_009B:
            builder.Remove(builder.Length - 1, 1);
            builder.Append("\n");
            return builder.ToString();
        }

        public List<SRPG.RecipeTree> GetCurrentRecipeTreeChildren()
        {
            if (this.RecipeTree == null)
            {
                goto Label_0031;
            }
            goto Label_0016;
        Label_0010:
            this.UpRecipeTree();
        Label_0016:
            if ((this.RecipeTree != null) && (this.RecipeTree.Parent != null))
            {
                goto Label_0010;
            }
        Label_0031:
            return ((this.RecipeTree == null) ? null : this.RecipeTree.Children);
        }

        public bool IsEnoughCommon()
        {
            List<byte> list;
            int num;
            if (this.IsNotEnough == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (this.CommonNeedNum.Keys.Count > 0)
            {
                goto Label_0025;
            }
            return 0;
        Label_0025:
            list = new List<byte>(this.CommonNeedNum.Keys);
            num = 0;
            goto Label_005F;
        Label_003D:
            if (this.CommonNeedNum[list[num]].IsEnough != null)
            {
                goto Label_005B;
            }
            return 0;
        Label_005B:
            num += 1;
        Label_005F:
            if (num < list.Count)
            {
                goto Label_003D;
            }
            return 1;
        }

        public void Remove()
        {
            if (this.mLastAddParam != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.CommonNeedNum.ContainsKey(this.mLastAddParam.cmn_type) != null)
            {
                goto Label_0028;
            }
            return;
        Label_0028:
            this.RecipeTree.RemoveLastAt();
            this.CommonNeedNum[this.mLastAddParam.cmn_type].Remove(this.mLastAddParam);
            return;
        }

        public void SetRecipeTree(SRPG.RecipeTree _recipe_tree, bool is_common)
        {
            if (this.RecipeTree == null)
            {
                goto Label_0017;
            }
            this.RecipeTree.SetChild(_recipe_tree);
        Label_0017:
            if (is_common == null)
            {
                goto Label_0023;
            }
            _recipe_tree.SetIsCommon();
        Label_0023:
            this.RecipeTree = _recipe_tree;
            return;
        }

        public void UpRecipeTree()
        {
            if (this.RecipeTree == null)
            {
                goto Label_002C;
            }
            if (this.RecipeTree.Parent == null)
            {
                goto Label_002C;
            }
            this.RecipeTree = this.RecipeTree.Parent;
        Label_002C:
            return;
        }
    }
}

