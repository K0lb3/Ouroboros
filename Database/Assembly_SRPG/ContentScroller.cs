namespace SRPG
{
    using System;

    public class ContentScroller : SRPG_ScrollRect
    {
        private ContentController mContentController;

        public ContentScroller()
        {
            base..ctor();
            return;
        }

        protected override void LateUpdate()
        {
            ContentController controller;
            base.LateUpdate();
            if ((this.contentController == null) == null)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            return;
        }

        public ContentController contentController
        {
            get
            {
                if ((this.mContentController == null) == null)
                {
                    goto Label_0033;
                }
                if ((base.get_content() != null) == null)
                {
                    goto Label_0033;
                }
                this.mContentController = base.get_content().GetComponent<ContentController>();
            Label_0033:
                return this.mContentController;
            }
        }
    }
}

