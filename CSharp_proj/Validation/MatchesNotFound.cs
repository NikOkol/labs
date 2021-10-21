using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_proj.Validation
{
    public class MatchesNotFound : ISpecification<bool>
    {
        public void Validate(bool value)
        {
            if (value == false)
            {
                throw new ValidationException(string.Format("(-)No emails/phone numbers/IP adresses in "));
            }
        }
    }
}
