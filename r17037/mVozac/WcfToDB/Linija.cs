using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfToDB
{
    [DataContract]
    public class Linija
    {
        [DataMember]
        public string NazivLinije { get; set; }
    }
}
