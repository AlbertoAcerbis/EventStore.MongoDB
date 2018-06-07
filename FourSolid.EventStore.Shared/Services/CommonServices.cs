using System;
using System.Linq;

namespace FourSolid.EventStore.Shared.Services
{
    public class CommonServices
    {
        private static readonly Random Random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!._";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string GetErrorMessage(Exception ex)
        {
            return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        }

        public static string GetEventDate()
        {
            return $"{DateTime.UtcNow}";
        }

        public static string ChkStringNull(string stringToChk, string stringToReplace = "")
        {
            return !string.IsNullOrEmpty(stringToChk) ? stringToChk :
                !string.IsNullOrEmpty(stringToReplace) ? stringToReplace : string.Empty;
        }
    }
}