using System;
using System.Collections.Generic;
using System.Text;
using Preludio.Core.DefensiveCoding;

namespace Preludio.Application.Contracts.Exceptions
{
    public class CommandValidationException : Exception
    {
        public CommandValidationException(List<string> errors) : base(GenerateMessage(errors))
        {

        }

        private static string GenerateMessage(List<string> errors)
        {
            Enforce.That.CollectionHasBeenInitialized(ref errors);
            var output = new StringBuilder("following error(s) occured during validation of command :");
            foreach (var errorItem in errors)
            {
                output.AppendLine(errorItem);
            }
            return output.ToString();
        }
    }
}