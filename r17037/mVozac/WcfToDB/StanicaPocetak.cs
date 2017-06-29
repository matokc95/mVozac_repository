using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfToDB
{
    [DataContract]
    public class StanicaPocetak
    {
        [DataMember]
        public string StanicaNaziv { get; set; }
        [DataMember]
        public bool Pocetak { get; set; }
        [DataMember]
        public bool Kraj { get; set; }
    }
}
