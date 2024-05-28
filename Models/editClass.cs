using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Online_Recharge.Models
{
    public static class editClass
    {

        public static string payment { get; set; } = string.Empty;
        public static string id { get; set; } = string.Empty;
        public static string desc { get; set; } = string.Empty;
        public static string operators { get; set; } = string.Empty;
        public static string validity { get; set; } = string.Empty;
        public static string type { get; set; } = string.Empty;
    }
}
