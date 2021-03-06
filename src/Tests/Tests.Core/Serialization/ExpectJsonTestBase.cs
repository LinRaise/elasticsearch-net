﻿using Nest;
using Tests.Core.Extensions;

namespace Tests.Core.Serialization
{
	public abstract class ExpectJsonTestBase
	{
		protected ExpectJsonTestBase(IElasticClient client) => Tester = new SerializationTester(client);

		protected abstract object ExpectJson { get; }
		protected virtual bool IncludeNullInExpected => true;

		//TODO Validate all overrides for false whether they truly do not support deserialization
		protected virtual bool SupportsDeserialization => true;
		protected SerializationTester Tester { get; }

		protected void RoundTripsOrSerializes<T>(T @object)
		{
			if (@object == null) return;
			if (ExpectJson == null) return;

			if (SupportsDeserialization) Tester.AssertRoundTrip<T>(@object, ExpectJson, preserveNullInExpected: IncludeNullInExpected);
			else Tester.AssertSerialize(@object, ExpectJson, preserveNullInExpected: IncludeNullInExpected);
		}
	}
}
