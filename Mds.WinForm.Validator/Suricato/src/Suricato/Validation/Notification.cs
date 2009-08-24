using System.Collections.Generic;

namespace Suricato.Validation
{
    public class Notification
    {
        private IList<Error> errors = new List<Error>();

        public bool HasErrors {
            get { return errors.Count != 0; }
        }
    }

    public class Error
    {
        private string code;
        private string message;

        public string Message {
            get { return message; }
            set { message = value; }
        }

        public string Code {
            get { return code; }
            set { code = value; }
        }
    }
}