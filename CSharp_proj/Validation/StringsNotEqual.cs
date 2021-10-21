using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_proj.Validation
{
    public class StringsNotEqual : ISpecification<int>
    {
        public void Validate(int value)
        {
            if (value != 0)
            {
                throw new ValidationException(string.Format("(-)Strings are not equal."));
            }
        }
    }
}
