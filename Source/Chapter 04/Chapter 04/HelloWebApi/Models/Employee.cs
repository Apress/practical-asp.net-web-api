using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HelloWebApi.Models
{
    // Employee model without DataContract
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public decimal Compensation { get; set; }

        public DateTime Doj { get; set; }
    }

    // Employee model class with Compensation
    //[DataContract]
    //public class Employee
    //{
    //    [DataMember]
    //    public int Id { get; set; }

    //    [DataMember]
    //    public string FirstName { get; set; }

    //    [DataMember]
    //    public string LastName { get; set; }

    //    public decimal Compensation { get; set; }

    //    [DataMember(Name = "Compensation")]
    //    private string CompensationSerialized { get; set; }

    //    [OnSerializing]
    //    void OnSerializing(StreamingContext context)
    //    {
    //        this.CompensationSerialized = this.Compensation.ToString();
    //    }
    //}


    // Employee model class with Compensation and Doj
    //[DataContract]
    //public class Employee
    //{
    //    [DataMember]
    //    public int Id { get; set; }

    //    [DataMember]
    //    public string FirstName { get; set; }

    //    [DataMember]
    //    public string LastName { get; set; }

    //    public DateTime Doj { get; set; }

    //    public decimal Compensation { get; set; }

    //    [DataMember(Name = "Compensation")]
    //    private string CompensationSerialized { get; set; }

    //    [DataMember(Name = "Doj")]
    //    private string DojSerialized { get; set; }

    //    [OnSerializing]
    //    void OnSerializing(StreamingContext context)
    //    {
    //        this.CompensationSerialized = this.Compensation.ToString();
    //        this.DojSerialized = this.Doj.ToString();
    //    }
    //}


}