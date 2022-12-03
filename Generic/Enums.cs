namespace TWP_API_Auth.Generic
{
    public class Enums
    {
        public enum ClassName
        {

            UserMenu,
            UserMenuModule,
            UserMenuCategory,
            UserMenuSubCategory,
            GetMenu,
            RolePermission,
            AuthClaim,
            Role,
            UserClaim,
            User,
            UserRole,
            Company,
            Branch,
            FinancialYear,

        }
        public enum Operations
        {
            A,
            E,
            D,
            S,
            U,
            I,
            O,
            M

        }
        public enum Policies
        {
            View,
            ViewById,
            Insert,
            Update,
            Delete,
            Checked,
            Approved

        }
        public enum Roles
        {
            Role,
            SuperAdmin

        }

        public enum Misc
        {
            UserId,
            UserName,

            CompanyId,
            CompanyName,
            BranchId,
            BranchName,

            YearId,
            YearName,
            Email,
            EmployeeId,
            Key

        }
        public enum ErrorType
        {
            Error,
            Information,
            Warning,

        }
        public enum ColumnType
        {
            S,
            U,
            System,
            User,
        }


    }
}