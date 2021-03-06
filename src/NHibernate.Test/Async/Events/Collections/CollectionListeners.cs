﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;
using NHibernate.Event.Default;

namespace NHibernate.Test.Events.Collections
{
	/// <content>
	/// Contains generated async methods
	/// </content>
	public partial class CollectionListeners
	{

		#region Nested type: AbstractListener

		#endregion
		#region Nested type: IListener

		#endregion
		#region Nested type: InitializeCollectionListener

		#endregion

		#region Nested type: PostCollectionRecreateListener

		/// <content>
		/// Contains generated async methods
		/// </content>
		public partial class PostCollectionRecreateListener : AbstractListener, IPostCollectionRecreateEventListener
		{

			#region IPostCollectionRecreateEventListener Members

			public Task OnPostRecreateCollectionAsync(PostCollectionRecreateEvent @event, CancellationToken cancellationToken)
			{
				try
				{
					AddEvent(@event, this);
					return Task.CompletedTask;
				}
				catch (System.Exception ex)
				{
					return Task.FromException<object>(ex);
				}
			}

			#endregion
		}

		#endregion

		#region Nested type: PostCollectionRemoveListener

		/// <content>
		/// Contains generated async methods
		/// </content>
		public partial class PostCollectionRemoveListener : AbstractListener, IPostCollectionRemoveEventListener
		{

			#region IPostCollectionRemoveEventListener Members

			public Task OnPostRemoveCollectionAsync(PostCollectionRemoveEvent @event, CancellationToken cancellationToken)
			{
				try
				{
					AddEvent(@event, this);
					return Task.CompletedTask;
				}
				catch (System.Exception ex)
				{
					return Task.FromException<object>(ex);
				}
			}

			#endregion
		}

		#endregion

		#region Nested type: PostCollectionUpdateListener

		/// <content>
		/// Contains generated async methods
		/// </content>
		public partial class PostCollectionUpdateListener : AbstractListener, IPostCollectionUpdateEventListener
		{

			#region IPostCollectionUpdateEventListener Members

			public Task OnPostUpdateCollectionAsync(PostCollectionUpdateEvent @event, CancellationToken cancellationToken)
			{
				try
				{
					AddEvent(@event, this);
					return Task.CompletedTask;
				}
				catch (System.Exception ex)
				{
					return Task.FromException<object>(ex);
				}
			}

			#endregion
		}

		#endregion

		#region Nested type: PreCollectionRecreateListener

		/// <content>
		/// Contains generated async methods
		/// </content>
		public partial class PreCollectionRecreateListener : AbstractListener, IPreCollectionRecreateEventListener
		{

			#region IPreCollectionRecreateEventListener Members

			public Task OnPreRecreateCollectionAsync(PreCollectionRecreateEvent @event, CancellationToken cancellationToken)
			{
				try
				{
					AddEvent(@event, this);
					return Task.CompletedTask;
				}
				catch (System.Exception ex)
				{
					return Task.FromException<object>(ex);
				}
			}

			#endregion
		}

		#endregion

		#region Nested type: PreCollectionRemoveListener

		/// <content>
		/// Contains generated async methods
		/// </content>
		public partial class PreCollectionRemoveListener : AbstractListener, IPreCollectionRemoveEventListener
		{

			#region IPreCollectionRemoveEventListener Members

			public Task OnPreRemoveCollectionAsync(PreCollectionRemoveEvent @event, CancellationToken cancellationToken)
			{
				try
				{
					AddEvent(@event, this);
					return Task.CompletedTask;
				}
				catch (System.Exception ex)
				{
					return Task.FromException<object>(ex);
				}
			}

			#endregion
		}

		#endregion

		#region Nested type: PreCollectionUpdateListener

		/// <content>
		/// Contains generated async methods
		/// </content>
		public partial class PreCollectionUpdateListener : AbstractListener, IPreCollectionUpdateEventListener
		{

			#region IPreCollectionUpdateEventListener Members

			public Task OnPreUpdateCollectionAsync(PreCollectionUpdateEvent @event, CancellationToken cancellationToken)
			{
				try
				{
					AddEvent(@event, this);
					return Task.CompletedTask;
				}
				catch (System.Exception ex)
				{
					return Task.FromException<object>(ex);
				}
			}

			#endregion
		}

		#endregion
	}
}
