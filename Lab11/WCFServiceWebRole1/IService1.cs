using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1
{
	[ServiceContract]
	public interface IService1
	{

		[OperationContract]
		bool Create(string login, string password);
		[OperationContract]
		Guid Login(string login, string password);
		[OperationContract]
		bool Logout(string login);
		[OperationContract]
		bool Put(string name, string value, Guid sessionId);
		[OperationContract]
		string Get(string name, Guid sessionId);
	}

	// Use a data contract as illustrated in the sample below to add composite types to service operations.
	[DataContract]
	public class CompositeType
	{
		bool boolValue = true;
		string stringValue = "Hello ";

		[DataMember]
		public bool BoolValue
		{
			get { return boolValue; }
			set { boolValue = value; }
		}

		[DataMember]
		public string StringValue
		{
			get { return stringValue; }
			set { stringValue = value; }
		}
	}
}
