using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace DentalCenterAPI.Utility
{
    public class Utility
    {
        /// <summary>
        /// Get Database Connectionstring
        /// </summary>
        /// <returns>Return Connectionstring as string</returns>
        public static string GetDatabaseConnectionstring()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var ConnectionString = configuration["ConnectionStrings:DentalCenterDB"];
            return ConnectionString;
        }

        /// <summary>
        /// Get TimeZone
        /// </summary>
        /// <returns>Return TimeZone String</returns>
        public static string GetTimeZone()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var timeZone = configuration["TimeZone"];
            return timeZone;
        }

        /// <summary>
        /// Get DateTime With TimeZone
        /// </summary>
        /// <returns>Return DateTime</returns>
        public static DateTime GetDateTimeByTimeZone()
        {
            // var dateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(GetTimeZone()));
            var dateTime = DateTime.UtcNow;
            return dateTime;
        }

        /// <summary>
        /// Get JWT SecretKey
        /// </summary>
        /// <returns>Return JWT SecretKey String</returns>
        public static string GetJWTTokenSecretKey()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var SecretKey = configuration["JwtToken:SecretKey"];
            return SecretKey;
        }

        /// <summary>
        ///  Get JWT Token Issuer
        /// </summary>
        /// <returns>Return JWT TokenIssuer String</returns>
        public static string GetJWTTokenIssuer()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var Issuer = configuration["JwtToken:Issuer"];
            return Issuer;
        }

        /// <summary>
        /// Get JWT ExpireTime
        /// </summary>
        /// <returns>Return JWT ExpireTime String</returns>
        public static string GetJWTTokenExpireTime()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var expire = configuration["JwtToken:expire"];
            return expire;
        }

        /// <summary>
        /// Get Site URL
        /// </summary>
        /// <returns>Return Site URL String</returns>
        public static string GetSiteURL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var expire = configuration["SiteURL"];
            return expire;
        }
    }
}