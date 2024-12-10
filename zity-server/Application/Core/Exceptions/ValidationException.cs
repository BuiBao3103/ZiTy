using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("One or more validation errors occurred")
        {
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        public ValidationException(string propertyName, string errorMessage)
            : this(new Dictionary<string, string[]>
            {
                { propertyName, new[] { errorMessage } }
            })
        {
        }
    }
}