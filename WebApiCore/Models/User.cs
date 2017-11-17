using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApiCore.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public Address address { get; set; }
        public virtual string email { get; set; }
    }
}

