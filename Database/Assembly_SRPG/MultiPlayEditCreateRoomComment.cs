namespace SRPG
{
    using System;
    using UnityEngine;

    public class MultiPlayEditCreateRoomComment : MonoBehaviour
    {
        public InputFieldCensorship Comment;

        public MultiPlayEditCreateRoomComment()
        {
            base..ctor();
            return;
        }

        public void OnClickEdit()
        {
            this.Comment.set_readOnly(0);
            this.Comment.ActivateInputField();
            return;
        }

        public void OnEndEdit()
        {
            this.Comment.set_readOnly(1);
            return;
        }

        private void Start()
        {
        }

        private void Update()
        {
        }
    }
}

