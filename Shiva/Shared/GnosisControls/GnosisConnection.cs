using System;
using System.Collections.Generic;
using System.Text;

namespace GnosisControls
{
    public class GnosisConnection : GnosisEntity
    {
        private string userNameField;

        private int userIDField;

        private bool isProtectedField;

        private bool showTooltipsField;

        private bool visibleField;

        [GnosisProperty]
        public string UserName
        {
            get
            {
                return this.userNameField;
            }
            set
            {
                this.userNameField = value;
            }
        }

        [GnosisProperty]
        public int UserID
        {
            get
            {
                return this.userIDField;
            }
            set
            {
                this.userIDField = value;
            }
        }

        [GnosisProperty]
        public bool IsProtected
        {
            get
            {
                return this.isProtectedField;
            }
            set
            {
                this.isProtectedField = value;
            }
        }

        [GnosisProperty]
        public bool ShowTooltips
        {
            get
            {
                return this.showTooltipsField;
            }
            set
            {
                this.showTooltipsField = value;
            }
        }

        [GnosisProperty]
        public bool Visible
        {
            get
            {
                return this.visibleField;
            }
            set
            {
                this.visibleField = value;
            }
        }
    }
}
