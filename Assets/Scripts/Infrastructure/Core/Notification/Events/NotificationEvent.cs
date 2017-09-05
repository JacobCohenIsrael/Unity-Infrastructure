
using Newtonsoft.Json;

namespace Infrastructure.Core.Notification.Events
{
    public class NotificationEvent
    {
        [JsonProperty("notificationText")]
        public string NotificationText;

        public float NotificationLength = 2f;
    }
}
