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
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibernate.Test.TransactionTest
{
	using System.Threading.Tasks;
	[TestFixture]
	public class TransactionFixtureAsync : TransactionFixtureBase
	{
		[Test]
		public async Task SecondTransactionShouldntBeCommittedAsync()
		{
			using (ISession session = OpenSession())
			{
				using (ITransaction t1 = session.BeginTransaction())
				{
					await (t1.CommitAsync());
				}

				using (ITransaction t2 = session.BeginTransaction())
				{
					Assert.IsFalse(t2.WasCommitted);
					Assert.IsFalse(t2.WasRolledBack);
				}
			}
		}

		[Test]
		public Task CommitAfterDisposeThrowsExceptionAsync()
		{
			try
			{
				using (ISession s = OpenSession())
				{
					ITransaction t = s.BeginTransaction();
					t.Dispose();
					Assert.ThrowsAsync<ObjectDisposedException>(() => t.CommitAsync());
				}
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		[Test]
		public Task RollbackAfterDisposeThrowsExceptionAsync()
		{
			try
			{
				using (ISession s = OpenSession())
				{
					ITransaction t = s.BeginTransaction();
					t.Dispose();
					Assert.ThrowsAsync<ObjectDisposedException>(() => t.RollbackAsync());
				}
				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		[Test]
		public async Task CommandAfterTransactionShouldWorkAsync()
		{
			using (ISession s = OpenSession())
			{
				using (s.BeginTransaction())
				{
				}

				await (s.CreateQuery("from Person").ListAsync());

				using (ITransaction t = s.BeginTransaction())
				{
					await (t.CommitAsync());
				}

				await (s.CreateQuery("from Person").ListAsync());

				using (ITransaction t = s.BeginTransaction())
				{
					await (t.RollbackAsync());
				}

				await (s.CreateQuery("from Person").ListAsync());
			}
		}

		[Test]
		public async Task WasCommittedOrRolledBackAsync()
		{
			using (ISession s = OpenSession())
			{
				using (ITransaction t = s.BeginTransaction())
				{
					Assert.AreSame(t, s.Transaction);
					Assert.IsFalse(s.Transaction.WasCommitted);
					Assert.IsFalse(s.Transaction.WasRolledBack);
					await (t.CommitAsync());

					// ISession.Transaction returns a new transaction
					// if the previous one completed!
					Assert.IsNotNull(s.Transaction);
					Assert.IsFalse(t == s.Transaction);

					Assert.IsTrue(t.WasCommitted);
					Assert.IsFalse(t.WasRolledBack);
					Assert.IsFalse(s.Transaction.WasCommitted);
					Assert.IsFalse(s.Transaction.WasRolledBack);
					Assert.IsFalse(t.IsActive);
					Assert.IsFalse(s.Transaction.IsActive);
				}

				using (ITransaction t = s.BeginTransaction())
				{
					await (t.RollbackAsync());

					// ISession.Transaction returns a new transaction
					// if the previous one completed!
					Assert.IsNotNull(s.Transaction);
					Assert.IsFalse(t == s.Transaction);

					Assert.IsTrue(t.WasRolledBack);
					Assert.IsFalse(t.WasCommitted);

					Assert.IsFalse(s.Transaction.WasCommitted);
					Assert.IsFalse(s.Transaction.WasRolledBack);

					Assert.IsFalse(t.IsActive);
					Assert.IsFalse(s.Transaction.IsActive);
				}
			}
		}

		[Test]
		public async Task FlushFromTransactionAppliesToSharingSessionAsync()
		{
			var flushOrder = new List<int>();
			using (var s = OpenSession(new TestInterceptor(0, flushOrder)))
			{
				var builder = s.SessionWithOptions().Connection();

				using (var s1 = builder.Interceptor(new TestInterceptor(1, flushOrder)).OpenSession())
				using (var s2 = builder.Interceptor(new TestInterceptor(2, flushOrder)).OpenSession())
				using (var s3 = s1.SessionWithOptions().Connection().Interceptor(new TestInterceptor(3, flushOrder)).OpenSession())
				using (var t = s.BeginTransaction())
				{
					var p1 = new Person();
					var p2 = new Person();
					var p3 = new Person();
					var p4 = new Person();
					await (s1.SaveAsync(p1));
					await (s2.SaveAsync(p2));
					await (s3.SaveAsync(p3));
					await (s.SaveAsync(p4));
					await (t.CommitAsync());
				}
			}

			Assert.That(flushOrder, Is.EqualTo(new[] { 0, 1, 2, 3 }));

			using (var s = OpenSession())
			using (var t = s.BeginTransaction())
			{
				Assert.That(await (s.Query<Person>().CountAsync()), Is.EqualTo(4));
				await (t.CommitAsync());
			}
		}
	}
}