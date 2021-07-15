using System;
using System.Collections.Generic;
using IpInfo;
using Newtonsoft.Json;
 
namespace BlazorApp.Shared
{
    public class GeoInfo
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        public string ip { get; set; }

        public string hostname { get; set; }

        public string city { get; set; }

        public string region { get; set; }

        public string country { get; set; }

        public string loc { get; set; }

        public string postal { get; set; }

        public string timezone { get; set; }

        public string org { get; set; }

        public string date { get; set; }

        public DateTime timestamp { get; set; }

        public GeoInfo(FullResponse response)
        {
            if (response != null) 
            {
                 this.id = Guid.NewGuid().ToString();
                this.ip = response.Ip;
                this.hostname = response.Hostname;
                this.city = response.City;
                this.region = response.Region;
                this.country = response.Country;
                this.loc = response.Loc;
                this.postal = response.Postal;
                this.timezone = response.Timezone;
                this.org = response.Org;
                this.timestamp = DateTime.Now;
                this.date = this.timestamp.ToString("yyyy-MM-dd");
            }
        }
    }
}