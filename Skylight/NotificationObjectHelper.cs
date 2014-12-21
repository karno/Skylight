using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Skylight
{
    public static class NotificationObjectHelper
    {
        public static IDisposable Subscribe(this INotifyPropertyChanged nobject, Action callback)
        {
            return Subscribe(nobject, null, callback);
        }

        public static IDisposable Subscribe<TNotifyPropertyChanged, TProperty>(
            this TNotifyPropertyChanged nobject,
            Expression<Func<TNotifyPropertyChanged, TProperty>> expr,
            Action callback)
            where TNotifyPropertyChanged : INotifyPropertyChanged
        {
            if (expr == null)
            {
                throw new ArgumentNullException("expr");
            }
            if (!(expr.Body is MemberExpression))
            {
                throw new ArgumentException("Argument should be a member expression.");
            }
            return Subscribe(nobject, ((MemberExpression)expr.Body).Member.Name, callback);
        }

        public static IDisposable Subscribe(this INotifyPropertyChanged nobject, string name, Action callback)
        {
            return new PropertyChangedEventListener(
                nobject, (obj, prop) =>
                {
                    if (name == null || prop.PropertyName == name)
                    {
                        callback();
                    }
                });
        }

        public static IDisposable Subscribe(this INotifyCollectionChanged collection, Action callback)
        {
            return collection.Subscribe(_ => callback());
        }

        public static IDisposable Subscribe(this INotifyCollectionChanged collection, Action<NotifyCollectionChangedEventArgs> callback)
        {
            return new CollectionChangedEventListener(collection, (o, e) => callback(e));
        }

        private class PropertyChangedEventListener : IDisposable
        {
            private readonly INotifyPropertyChanged _target;
            private readonly PropertyChangedEventHandler _handler;

            public PropertyChangedEventListener(INotifyPropertyChanged target, PropertyChangedEventHandler handler)
            {
                this._target = target;
                this._handler = handler;
                this._target.PropertyChanged += this._handler;
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~PropertyChangedEventListener()
            {
                this.Dispose(false);
            }

            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this._target.PropertyChanged -= this._handler;
                }
            }
        }

        private class CollectionChangedEventListener : IDisposable
        {
            private readonly INotifyCollectionChanged _target;
            private readonly NotifyCollectionChangedEventHandler _handler;

            public CollectionChangedEventListener(INotifyCollectionChanged target,
                NotifyCollectionChangedEventHandler handler)
            {
                this._target = target;
                this._handler = handler;
                this._target.CollectionChanged += this._handler;
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~CollectionChangedEventListener()
            {
                this.Dispose(false);
            }

            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this._target.CollectionChanged -= this._handler;
                }
            }
        }
    }
}
