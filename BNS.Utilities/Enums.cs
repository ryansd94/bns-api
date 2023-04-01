﻿namespace BNS.Utilities
{
    public class Enums
    {
        public enum EErrorCode
        {
            Success,
            Failed,
            USERNAME_PASSWORD_NOTCORRECT,
            USERNAME_ISEXISTS,
            NotExistsData,
            IsExistsData,
            SystemError,
            UserHasJoinTeam,
            TokenNotValid,
            UserNotRegister
        }

        public enum EAccountType
        {
            SupperAdmin = 1,
            User = 2
        }
        public enum ETaskType
        {
            STORY = 1,
            EPIC = 2,
            BUG = 3,
            TASK = 4
        }

        public enum EType
        {
            Add,
            Edit
        }
        public enum ELang
        {
            vi,
            en
        }

        public enum ERoleType
        {
            Position = 0,
            Department = 1
        }
        public enum ESortEnum
        {
            asc, desc
        }
        public enum EDataType
        {
            Product,
            TableOrderReport,
            Order,
            Branch,
            Discount,
            FieldInfo,
            ColumnInfo,
            PriceArea,
            Room,
            ShopIPSetting,
            Employee,
            Menu
        }
        public enum EUploadType
        {
            Product,
            Employee
        }
        public enum EVersionType
        {
            Trial = 1,
            Basic = 2,
            Standard = 3,
            Pro = 4
        }
        public enum Sort
        {
            asc,
            desc
        }
        public enum EUserStatus
        {
            ACTIVE = 1,
            IN_ACTIVE = 2,
            WAILTING_CONFIRM_MAIL = 3,
            BLOCK = 4
        }
        public enum EUserValidate
        {
            OK = 1,
            IS_HAS_ACCOUNT = 2,
        }

        public enum EWhereCondition
        {
            Equal = 0,
            NotEqual = 1,
            Contains = 2,
            Greater = 3,
            GreaterOrEqual = 4,
            Less = 5,
            LessEqual = 6,
            Dynamic = 7,
            LessThanOrEqual = 8,
            NotContains = 9,
        }

        public enum EAndOr
        {
            And,
            Or
        }

        public enum EControlType
        {
            Typography,
            TextField,
            Editor,
            Select,
            Group,
            DatePicker,
            Number,
            DateTimePicker,
        }

        public enum EColumnPosition
        {
            Column1,
            Column2,
            Column3
        }

        public enum EViewMode
        {
            List = 0,
            Board = 1
        }
    }
}
