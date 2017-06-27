using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfToDB
{
    [DataContract]
    public class Bus
    {
        [DataMember]
        public float Duzina { get; set; }
        [DataMember]
        public float Sirina { get; set; }
        [DataMember]
        public int BrojSjedala { get; set; }
        [DataMember]
        public float MaxBrzina { get; set; }

    }
}
