namespace LIBUtil
{
    public class ActivityLog
    {
        public static string editListStringFormat(string fieldName) { return fieldName + ": {0}"; }
        public static string editStringFormat(string fieldName) { return fieldName + ": '{0}' to '{1}'"; }
        public static string editStringFormat2(string fieldName) { return fieldName + ": '{0}'"; }
        public static string editStringFormat3(string fieldName) { return fieldName + ": '{1}'"; }
        public static string editIntFormat(string fieldName) { return fieldName + ": '{0:N0}' to '{1:N0}'"; }
        public static string editDateFormat(string fieldName) { return fieldName + ": '{0:dd/MM/yyyy}' to '{1:dd/MM/yyyy}'"; }
        public static string editTimeFormat(string fieldName) { return fieldName + ": '{0:HH:mm}' to '{1:HH:mm}'"; }
        public static string editDateTimeFormat(string fieldName) { return fieldName + ": '{0:dd/MM/yyyy HH:mm}' to '{1:dd/MM/yyyy HH:mm}'"; }
        public static string editDecimalFormat(string fieldName) { return fieldName + ": '{0:G29}' to '{1:G29}'"; }
        public static string editBooleanFormat(string fieldName) { return fieldName + ": {1}"; }


    }
}
