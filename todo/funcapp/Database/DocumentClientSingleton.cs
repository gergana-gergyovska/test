using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;

namespace funcapp.Database
{
    class DocumentClientSingleton
	{
		public DocumentClient Client { get; set; }

		public DocumentClientSingleton(CosmosDBSettings optionsInjection)
		{
			Client = new DocumentClient(new Uri(optionsInjection.Account),
				optionsInjection.Key,
				new ConnectionPolicy
				{
					ConnectionMode = ConnectionMode.Gateway,
					ConnectionProtocol = Protocol.Https,
					MaxConnectionLimit = 50000,
					RequestTimeout = TimeSpan.FromSeconds(10),
					RetryOptions = new RetryOptions() { MaxRetryAttemptsOnThrottledRequests = 9, MaxRetryWaitTimeInSeconds = 10 }
				});
		}
	}
}