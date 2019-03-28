using System;

namespace CounterCulture.Auth.Helpers {
    
    [System.Serializable]
    public class RepositoryIOException : Exception
    {
        public RepositoryIOException() { }
        public RepositoryIOException(string message) : base(message) { }
        public RepositoryIOException(string message, System.Exception inner) : base(message, inner) { }
        protected RepositoryIOException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}