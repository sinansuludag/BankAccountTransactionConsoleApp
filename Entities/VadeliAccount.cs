using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class VadeliAccount : Account, IEntity
    {
        public  DateTime VadeBaslangicTarihi { get; set; }
        public  DateTime VadeBitisTarihi { get; set; }
        public  decimal FaizOrani { get; set; }

    }
}
