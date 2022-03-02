using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BNS.Utilities
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
        public enum EIssueType
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
        public enum EIssueStatus
        {
            NEW = 1,
            IN_PROGRESS = 2,
            RESLOVED = 3,
            DONE = 4,
            FEEDBACK = 5,
            CANCELED = 6,
            CONFIRM = 7
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
        public enum EStatus
        {
            ACTIVE = 1,
            IN_ACTIVE = 2,
            WAILTING_CONFIRM_MAIL = 3,
            BLOCK = 4
        }
    }
}
