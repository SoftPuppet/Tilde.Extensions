using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text.Json;

namespace Tilde.Extensions.Types
{
   public static partial class DateTimeExtensions
   {
      public static DateTime GetInternetTime(this DateTime @this, string timeServer = TIME_SERVER.WORLD_TIME_API_ORG)
      {
         @this = DateTime.MinValue;
         switch (timeServer)
         {
            default:
            case TIME_SERVER.WORLD_TIME_API_ORG:
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create(TIME_SERVER.WORLD_TIME_API_ORG);
               request.Method = "GET";
               request.Accept = "text/html, application/xhtml+xml, */*";
               request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";
               request.ContentType = "application/x-www-form-urlencoded";
               request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore); //No caching
               try
               {
                  HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                  if (response.StatusCode == HttpStatusCode.OK)
                  {
                     StreamReader stream = new StreamReader(response.GetResponseStream());
                     string jsonString = stream.ReadToEnd();
                     WorldTimeApiOrgObject worldTime = JsonSerializer.Deserialize<WorldTimeApiOrgObject>(jsonString) ?? new WorldTimeApiOrgObject();
                     @this = worldTime.utc_datetime.ToLocalTime();
                     response.Close();
                  }
               }
               catch (Exception)
               {
                  return @this;
               }
               break;

         }
         return @this;
      }
   }

   public static class TIME_SERVER
   {
      public const string WORLD_TIME_API_ORG = "http://worldtimeapi.org/api/timezone/Pacific/Auckland";
   }

   public class WorldTimeApiOrgObject
   {
      public string? abbreviation { get; set; }
      public string? client_ip { get; set; }
      public DateTime datetime { get; set; }
      public int day_of_week { get; set; }
      public int day_of_year { get; set; }
      public bool dst { get; set; }
      public object? dst_from { get; set; }
      public int dst_offset { get; set; }
      public object? dst_until { get; set; }
      public int raw_offset { get; set; }
      public string? timezone { get; set; }
      public int unixtime { get; set; }
      public DateTime utc_datetime { get; set; }
      public string? utc_offset { get; set; }
      public int week_number { get; set; }
   }
}
