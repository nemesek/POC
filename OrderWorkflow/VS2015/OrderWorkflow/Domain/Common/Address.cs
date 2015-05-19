using System;

namespace OrderWorkflow.Domain.Common
{
	public class Address
	{
		private readonly string _city;
		private readonly string _state;
		private readonly string _zip;
		private readonly string _street;
		private readonly string _streetNumber;
		public Address(string city,	string state,string zip,string street,string streetNumber)
		{
			ValidateString(city, 30, "city");
			ValidateString(state, 2, "state");
			ValidateString(zip, 10, "zip");
			ValidateString(street, 30, "street");
			ValidateString(streetNumber, 30, "streetNumber");
		
			_city = city;
			_state = state;
			_zip = zip;
			_street = street;
			_streetNumber = streetNumber;	
		}
		
		public void Save()
		{
			Console.WriteLine("Saving update");
		}
		
		private void ValidateString(string value, int length, string output)
		{
			if(string.IsNullOrWhiteSpace(value)) throw new ValidationException(string.Format("{0} cannot be null or empty.", 0));
			if(value.Length > length) throw new ValidationException(string.Format("{0} cannot exceed {1} characters", output,length));
		}
	}
}