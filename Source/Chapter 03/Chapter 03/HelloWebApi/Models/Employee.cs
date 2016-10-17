using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace HelloWebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Department { get; set; }
    }


    // 3.10.1 Black-listing Members
    //public class Employee
    //{
    //    public int Id { get; set; }

    //    public string FirstName { get; set; }

    //    public string LastName { get; set; }

    //    public decimal Compensation
    //    {
    //        get
    //        {
    //            return 5000.00M;
    //        }
    //    }

    //    [JsonIgnore] // Ignored only by Json.NET
    //    public string Title { get; set; }

    //    [IgnoreDataMember] // Ignored by both Json.NET and DCS
    //    public string Department { get; set; }
    //}


    // 3.10.2 White-listing Members
    //[DataContract]
    //public class Employee
    //{
    //    [DataMember]
    //    public int Id { get; set; }

    //    public string FirstName { get; set; } // Does not get serialized

    //    [DataMember]
    //    public string LastName { get; set; }

    //    [DataMember]
    //    public decimal Compensation
    //    {
    //        // Serialized with json.NET but fails with an exception in case of
    //        // DataContractSerializer, since set method is absent
    //        get
    //        {
    //            return 5000.00M;
    //        }
    //    }
    //}


}