using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class RequestModel
    {
        public string UserName { get; set; }
        public int RequestNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<CarDto> Cars { get; set; }
    }
}
