using System;
using System.Runtime.Serialization;

namespace SCv20_Tools.Core.Data {

    [Serializable]
    public class CoreException : Exception {

        public CoreException() {
        }

        public CoreException(string message)
            : base(message) {
        }

        public CoreException(string message, Exception inner)
            : base(message, inner) {
        }

        protected CoreException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}