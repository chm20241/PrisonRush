﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Exeptions
{
    public class RepositoryCreateException : Exception
    {
        public RepositoryCreateException()
        {
            // Logging
        }

        public RepositoryCreateException(string message)
            : base(message)
        { }

        public RepositoryCreateException(string message, Exception innerException)
            : base(message, innerException)
        { }

        private void Log()
        {
            // TODO: Logging
        }
    }
}
