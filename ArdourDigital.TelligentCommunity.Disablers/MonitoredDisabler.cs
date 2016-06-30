using System;
using Telligent.Evolution.Components;

namespace ArdourDigital.TelligentCommunity.Disablers
{
    // Uses references to CSContext for storing state - this is not upgrade safe - should look at alternatives for context
    public abstract class MonitoredDisabler<T> : Disabler
    {
        private static readonly Guid _id;

        static MonitoredDisabler()
        {
            _id = new Guid();
        }

        private static string ItemKey
        {
            get
            {
                return string.Format("{0}_{1}_{2}", typeof(T).Name, _id, "State");
            }
        }

        public static bool IsDisabled
        {
            get
            {
                var isDisabled = CSContext.Current.Items[ItemKey];

                if (isDisabled == null)
                {
                    return false;
                }

                if (isDisabled is bool)
                {
                    return (bool)isDisabled;
                }

                return false;
            }
        }

        protected override void Enter()
        {
            CSContext.Current.Items[ItemKey] = true;
        }

        protected override void Exit()
        {
            CSContext.Current.Items[ItemKey] = false;
        }
    }
}
