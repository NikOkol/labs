using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_proj.Validation
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract void Validate(T value);

        public Specification<T> And(ISpecification<T> second)
        {
            return new AndSpecification<T>(this, second);
        }

    }
}
