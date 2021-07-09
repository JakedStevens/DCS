using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicCoffeeSellers
{
    public class DynamicsResponse<T>
    {
        public List<T> Value { get; set; }
    }
}
