using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfToDB
{
    [DataContract]
    public class Popust
    {
        [DataMember]
        public string NazivPopusta { get; set; }
        [DataMember]
        public float KolicinaPopusta { get; set; }
    }
}
