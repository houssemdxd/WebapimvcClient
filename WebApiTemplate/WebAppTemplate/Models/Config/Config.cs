
namespace WebAppTemplate.Models
{
    public class Config
    {
        public const int MAX_BDMS_PARAMS_NUM = 100;
        public static string ServerIP { get; set; }
        private static bool DebugMode = true;

        public static string GetConnectionString()
        {
            // Add appSettings entry key="ConnectionString", value="Server=localhost;Port=3306;UId=root;Pwd=******;Connection Timeout=30;Database=test;"
            return System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        }

        public static bool GetDebugMode()
        {
            return DebugMode;
        }

    }
}
