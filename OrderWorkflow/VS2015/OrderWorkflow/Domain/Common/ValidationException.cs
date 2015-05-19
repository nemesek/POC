using System;

namespace OrderWorkflow.Domain.Common
{
	public class ValidationException : System.Exception
	{
		public ValidationException() { }
		public ValidationException( string message ) : base( message ) { }
		public ValidationException( string message, System.Exception inner ) : base( message, inner ) { }
		protected ValidationException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context ) : base( info, context ) { }
	}
}