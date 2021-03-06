﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Data.Common;
using System.Threading;
using System.Transactions;
using NUnit.Framework;

namespace NHibernate.Test.SystemTransactions
{
	using System.Threading.Tasks;
	[TestFixture]
	public class TransactionNotificationFixtureAsync : TestCase
	{
		protected override IList Mappings
		{
			get { return new string[] {}; }
		}

		[Test]
		public async Task TwoTransactionScopesInsideOneSessionAsync()
		{
			var interceptor = new RecordingInterceptor();
			using (var session = Sfi.WithOptions().Interceptor(interceptor).OpenSession())
			{
				using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					await (session.CreateCriteria<object>().ListAsync());
					scope.Complete();
				}

				using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					await (session.CreateCriteria<object>().ListAsync());
					scope.Complete();
				}
			}
			Assert.AreEqual(2, interceptor.afterTransactionBeginCalled);
			Assert.AreEqual(2, interceptor.beforeTransactionCompletionCalled);
			Assert.AreEqual(2, interceptor.afterTransactionCompletionCalled);
		}

		[Test]
		public async Task OneTransactionScopesInsideOneSessionAsync()
		{
			var interceptor = new RecordingInterceptor();
			using (var session = Sfi.WithOptions().Interceptor(interceptor).OpenSession())
			{
				using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					await (session.CreateCriteria<object>().ListAsync());
					scope.Complete();
				}
			}
			Assert.AreEqual(1, interceptor.afterTransactionBeginCalled);
			Assert.AreEqual(1, interceptor.beforeTransactionCompletionCalled);
			Assert.AreEqual(1, interceptor.afterTransactionCompletionCalled);
		}


		[Description("NH2128, NH3572")]
		[Theory]
		public async Task ShouldNotifyAfterDistributedTransactionAsync(bool doCommit)
		{
			// Note: For distributed transaction, calling Close() on the session isn't
			// supported, so we don't need to test that scenario.

			var interceptor = new RecordingInterceptor();
			ISession s1 = null;
			ISession s2 = null;

			using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				try
				{
					s1 = OpenSession(interceptor);
					s2 = OpenSession(interceptor);

					await (s1.CreateCriteria<object>().ListAsync());
					await (s2.CreateCriteria<object>().ListAsync());
				}
				finally
				{
					if (s1 != null)
						s1.Dispose();
					if (s2 != null)
						s2.Dispose();
				}

				if (doCommit)
					tx.Complete();
			}

			Assert.That(s1.IsOpen, Is.False);
			Assert.That(s2.IsOpen, Is.False);
			Assert.That(interceptor.afterTransactionCompletionCalled, Is.EqualTo(2));
		}


		[Description("NH2128")]
		[Theory]
		public async Task ShouldNotifyAfterDistributedTransactionWithOwnConnectionAsync(bool doCommit)
		{
			// Note: For distributed transaction, calling Close() on the session isn't
			// supported, so we don't need to test that scenario.

			var interceptor = new RecordingInterceptor();
			ISession s1 = null;

			using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				var ownConnection1 = await (Sfi.ConnectionProvider.GetConnectionAsync(CancellationToken.None));

				try
				{
					using (s1 = Sfi.WithOptions().Connection(ownConnection1).Interceptor(interceptor).OpenSession())
					{
						await (s1.CreateCriteria<object>().ListAsync());
					}

					if (doCommit)
						tx.Complete();
				}
				finally
				{
					Sfi.ConnectionProvider.CloseConnection(ownConnection1);
				}
			}

			// Transaction completion may happen asynchronously, so allow some delay.
			Assert.That(() => s1.IsOpen, Is.False.After(500, 100));

			Assert.That(interceptor.afterTransactionCompletionCalled, Is.EqualTo(1));
		}

	}
}