using System;

namespace ArdourDigital.TelligentCommunity.Disablers
{
    public abstract class Disabler : IDisposable
    {
        private bool _isDisposed = false;

        public Disabler()
        {
            Enter();
        }

        public virtual void Dispose()
        {
            if (!_isDisposed)
            {
                Exit();
                _isDisposed = true;
            }
        }

        protected abstract void Enter();

        protected abstract void Exit();
    }
}
