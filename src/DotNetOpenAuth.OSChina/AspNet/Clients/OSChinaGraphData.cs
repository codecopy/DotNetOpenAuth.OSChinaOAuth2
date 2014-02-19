using System;
using System.Runtime.Serialization;

namespace DotNetOpenAuth.AspNet.Clients
{
    [DataContract]
    public class OSChinaGraphData
    {
        [DataMember(Name = "id", IsRequired = true)]
        public string Id { get; set; }

        [DataMember(Name = "email", IsRequired = true)]
        public string Email { get; set; }

        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }

        [DataMember(Name = "gender", IsRequired = true)]
        public string Gender { get; set; }

        [DataMember(Name = "location")]
        public string Location { get; set; }

        [DataMember(Name = "avatar", IsRequired = true)]
        public Uri Avatar { get; set; }


        [DataMember(Name = "url")]
        public Uri Link { get; set; }
    }
}