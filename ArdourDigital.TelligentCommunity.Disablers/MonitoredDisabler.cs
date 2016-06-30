using Telligent.Evolution.Components;

namespace ArdourDigital.TelligentCommunity.Disablers
{
    // Uses references to CSContext for storing state - this is not upgrade safe - should look at alternatives for context
    public abstract class MonitoredDisabler<T> : Disabler
    {
        static MonitoredDisabler()
        {
        }

        private static string ItemKey
        {
            get
            {
                return string.Format("{0}_MonitorState", typeof(T).Name);
            }
        }

        public static bool IsDisabled
        {
            get
            {
                var isDisabled = CSContext.Current == null || CSContext.Current.Items == null ? false : CSContext.Current.Items[ItemKey];

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
            if (CSContext.Current == null || CSContext.Current.Items == null)
            {
                return;
            }

            CSContext.Current.Items[ItemKey] = true;
        }

        protected override void Exit()
        {
            if (CSContext.Current == null || CSContext.Current.Items == null)
            {
                return;
            }

            CSContext.Current.Items[ItemKey] = false;
        }
    }
}
