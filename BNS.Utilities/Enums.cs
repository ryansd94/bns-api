﻿using System;
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

        public enum EWhereOperation
        {
            Equal = 0,
            NotEqual = 1,
            Contains = 2,
            Greater = 3,
            GreaterOrEqual = 4,
            Less = 5,
            LessEqual = 6
        }
    }
}
