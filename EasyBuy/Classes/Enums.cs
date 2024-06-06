namespace EasyBuy.Classes
{
    public class Enums
    {
        public enum Action
        {
            NONE = -1,
            ALL = 0,
            CREATE = 1,
            UPDATE = 2,
            DELETE = 3,
            PRINT = 4,
            EXPORT = 5,
            SELECT = 6,
            CHECKLOGIN = 7,
            LIST = 8,
        }

        public enum ResponseMessage
        {
            SESSIONOUT = -1,
            SUCCESS = 0,
            ERROR = 1,
            INVALID = 2,
            WARNING = 3,
            VALIDATION_ERROR = 4,
            INFORMATIONAL = 5,
        }

        public enum Transactions
        {
            Student,
            Order,
            Branch,
            User,
            State,
            City
        }
    }
}