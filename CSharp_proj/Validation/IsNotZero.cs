using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_proj.Validation
{
    public class IsNotZero : Specification<double>
    {
        public override void Validate(double value)
        {
            if (value == 0)
            {
                throw new ValidationException(string.Format("Value {0} must not be zero.", value));
            }
        }
    }
}
