using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_proj.Validation
{
    public class IsNotEmptyString : Specification<string>
    {
        public override void Validate(string value)
        {
            if (value.Trim() == "")
            {
                throw new ValidationException(string.Format("String is empty."));
            }
        }
    }
}
