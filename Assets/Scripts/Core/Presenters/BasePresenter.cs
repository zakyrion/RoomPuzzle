using System;
using Zenject;

namespace Core.Presenters
{
    /// <summary>
    /// Base presenter class implementing common MVP pattern.
    /// Provides standard lifecycle management (Initialize/Dispose) for presenters.
    /// </summary>
    /// <typeparam name="TModel">Model interface type</typeparam>
    /// <typeparam name="TProvider">Provider interface type</typeparam>
    /// <typeparam name="TView">View interface type</typeparam>
    public abstract class BasePresenter<TModel, TProvider, TView> : IInitializable, IDisposable
        where TModel : class
        where TProvider : class
        where TView : class
    {
        protected readonly TModel Model;
        protected readonly TProvider Provider;
        protected TView View;

        protected BasePresenter(TModel model, TProvider provider)
        {
            Model = model;
            Provider = provider;
        }

        public virtual void Initialize()
        {
            View = GetViewFromProvider();
            OnViewCreated();
        }

        public virtual void Dispose()
        {
            OnBeforeDispose();
            View = null;
        }

        /// <summary>
        /// Override this method to retrieve the view from the provider.
        /// </summary>
        protected abstract TView GetViewFromProvider();

        /// <summary>
        /// Called after view is created. Override to initialize view and subscribe to events.
        /// </summary>
        protected virtual void OnViewCreated()
        {
        }

        /// <summary>
        /// Called before disposal. Override to unsubscribe from events and cleanup.
        /// </summary>
        protected virtual void OnBeforeDispose()
        {
        }
    }
}
