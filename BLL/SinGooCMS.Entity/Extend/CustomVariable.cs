using System;

namespace SinGooCMS.Entity
{
    [Serializable]
    public class CustomVariable
    {
        private string varName;
        private int varType;
        private string varValue;

        public string VarName
        {
            get
            {
                return this.varName;
            }
            set
            {
                this.varName = value;
            }
        }

        public int VarType
        {
            get
            {
                return this.varType;
            }
            set
            {
                this.varType = value;
            }
        }

        public string VarValue
        {
            get
            {
                return this.varValue;
            }
            set
            {
                this.varValue = value;
            }
        }
    }
}

