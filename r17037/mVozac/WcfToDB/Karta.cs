﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfToDB
{
    [DataContract]
    public class Karta
    {
        [DataMember]
        public int KartaID { get; set; }
        [DataMember]
        public float Popust { get; set; }
        [DataMember]
        public int Vozac { get; set; }
        [DataMember]
        public int Voznja { get; set; }
        [DataMember]
        public bool Ponistena { get; set; }
    }
}
