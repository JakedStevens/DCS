using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DynamicCoffeeSellers.Models
{
    public class RequestForm
    {
        public string firstname { set; get; }
        public string lastname { set; get; }
        public string companyname { set; get; }
        [JsonProperty("jej_Industry@odata.bind")]
        public string industry { set; get; }
        public string subject { set; get; }
    }
}
