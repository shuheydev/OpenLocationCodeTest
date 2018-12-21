using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OpenLocationCodeTest
{
    //Json貼り付け時にクラスを生成

    [DataContract]
    public class PlusCodeResponse
    {
        [DataMember(Name = "plus_code")]
        public Plus_Code PlusCode { get; set; }
        [DataMember(Name = "status")]
        public string status { get; set; }
    }

    [DataContract]
    public class Plus_Code
    {
        [DataMember(Name = "global_code")]
        public string GlobalCode { get; set; }
        [DataMember(Name = "geometry")]
        public Geometry Geometry { get; set; }
        [DataMember(Name = "local_code")]
        public string LocalCode { get; set; }
        [DataMember(Name = "locality")]
        public Locality Locality { get; set; }
        [DataMember(Name = "best_street_address")]
        public string BestStreetAddress { get; set; }
    }

    [DataContract]
    public class Geometry
    {
        [DataMember(Name = "bounds")]
        public Bounds Bounds { get; set; }
        [DataMember(Name = "location")]
        public Location Location { get; set; }
    }

    [DataContract]
    public class Bounds
    {
        [DataMember(Name = "northeast")]
        public Northeast northeast { get; set; }
        [DataMember(Name = "southwest")]
        public Southwest southwest { get; set; }
    }

    [DataContract]
    public class Northeast
    {
        [DataMember(Name = "lat")]
        public float Lat { get; set; }
        [DataMember(Name = "lng")]
        public float Lng { get; set; }
    }

    [DataContract]
    public class Southwest
    {
        [DataMember(Name = "lat")]
        public double Lat { get; set; }
        [DataMember(Name = "lng")]
        public double Lng { get; set; }
    }

    [DataContract]
    public class Location
    {
        [DataMember(Name = "lat")]
        public double Lat { get; set; }
        [DataMember(Name = "lng")]
        public double Lng { get; set; }
    }

    [DataContract]
    public class Locality
    {
        [DataMember(Name = "local_address")]
        public string local_address { get; set; }
    }


}
