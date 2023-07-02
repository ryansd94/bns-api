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
            UserNotRegister,
            NotPermission
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
            TransferList,
            ListObject
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

        public enum EActionType
        {
            View,
            Add,
            ColumnConfig,
            Edit,
            Delete,
            All
        }

        public enum EPermissionObject
        {
            User,
            Team
        }

        public enum EControllerKey
        {
            User,
            Summary,
            Dashboard,
            Project,
            Team,
            Priority,
            Template,
            Status,
            Tasktype,
            Task,
            Permission,
            Overview,
            Category,
            TaskGroup,
            PermissionGroup
        }

        public enum EClaimType
        {
            UserId,
            AccountCompanyId,
            DefaultOrganization,
            CompanyId,
            Role,
            IsMainAccount,
            TeamId,
            Organization
        }

        public enum ERestMethod
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public enum ENotifyObjectType
        {
            TaskCommentMention,
            TaskAssigned,
            TaskCommentReply
        }

        public enum EUserType
        {
            Individual = 1,
            Team = 2,
            Company = 3,
            Organization = 4
        }

        public enum EScale
        {
            From1To50 = 1,
            From50To200 = 2,
            From200To1k = 3,
            Over1k = 4
        }

        public enum EProjectType
        {
            Basic,
            Phase
        }

        public enum ERowStatus
        {
            AddNew = 0,
            Update = 1,
            NoChange = 2,
            Delete = 3
        }

        public enum EStatus
        {
            InActive = 0,
            Active = 1
        }
    }
}
