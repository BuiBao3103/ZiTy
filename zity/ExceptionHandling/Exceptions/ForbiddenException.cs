﻿namespace zity.ExceptionHandling.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message = "Access forbidden")
            : base(message)
        {
        }
    }
}