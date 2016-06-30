using System.Reflection;
using Telligent.Evolution.CoreServices.Notifications.Model;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Version1;

namespace ArdourDigital.TelligentCommunity.Disablers.Notifications
{
    public class NotificationDisablerPlugin : IPlugin
    {
        public string Description
        {
            get
            {
                return "Allows notifications to be disabled in a certain code scope";
            }
        }

        public string Name
        {
            get
            {
                return "Ardour Digital - Notification Disabler";
            }
        }

        public void Initialize()
        {
            PublicApi.Notifications.Events.BeforeCreate += OnBeforeCreate;
        }

        private void OnBeforeCreate(NotificationBeforeCreateEventArgs e)
        {
            if (NotificationDisabler.IsDisabled)
            {
                // You can't edit the notification object from the event args
                // To change it have to use reflection - the UserId and Actors names should be upgrade safe, InternalEntity maybe not,
                // and there is a strong argument that using reflection for this kind of thing is not advisable, unsure of another way 
                // to achieve the desired result though.
                var notificationProperty = e.GetType().GetProperty("InternalEntity", BindingFlags.Instance | BindingFlags.NonPublic);

                if (notificationProperty != null)
                {
                    var notification = notificationProperty.GetValue(e);

                    if (notification != null)
                    {
                        var notificationType = notification.GetType();

                        var userIdProperty = notificationType.GetProperty("UserId");

                        if (userIdProperty != null)
                        {
                            userIdProperty.SetValue(notification, -1);
                        }

                        var actorsProperty = notificationType.GetProperty("Actors");

                        if (actorsProperty != null)
                        {
                            actorsProperty.SetValue(notification, new NotificationActor[0]);
                        }
                    }
                }
            }
        }
    }
}
