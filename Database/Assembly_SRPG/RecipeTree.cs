namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class RecipeTree
    {
        private List<RecipeTree> children;
        private RecipeTree parent;
        private bool is_common;
        private ItemParam param;

        public RecipeTree(ItemParam param)
        {
            this.children = new List<RecipeTree>();
            base..ctor();
            this.param = param;
            return;
        }

        public void RemoveLastAt()
        {
            if (this.children == null)
            {
                goto Label_001C;
            }
            if (this.children.Count > 0)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            this.children.RemoveAt(this.children.Count - 1);
            return;
        }

        public void SetChild(RecipeTree child)
        {
            child.parent = this;
            this.children.Add(child);
            return;
        }

        public void SetIsCommon()
        {
            this.is_common = 1;
            if (this.parent == null)
            {
                goto Label_001D;
            }
            this.parent.SetIsCommon();
        Label_001D:
            return;
        }

        public List<RecipeTree> Children
        {
            get
            {
                return this.children;
            }
        }

        public RecipeTree Parent
        {
            get
            {
                return this.parent;
            }
        }

        public bool IsCommon
        {
            get
            {
                return this.is_common;
            }
        }

        public string iname
        {
            get
            {
                return ((this.param == null) ? null : this.param.iname);
            }
        }
    }
}

