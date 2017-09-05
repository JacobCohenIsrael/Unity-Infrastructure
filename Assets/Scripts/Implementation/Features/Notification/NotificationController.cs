using System.Collections;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Notification.Events;
using UnityEngine.UI;
using UnityEngine;

namespace Implementation.Features.Notification
{
	public class NotificationController : BaseFeature
	{
		[SerializeField]
		private Text _notificationTextField;
		protected override void SubscribeToEvents(SubscribeEvent e)
		{
			eventManager.AddListener<NotificationEvent>(OnNotificationReceived);
		}

		private void OnNotificationReceived(NotificationEvent e)
		{
			_notificationTextField.text = e.NotificationText;
			_notificationTextField.enabled = true;
			StartCoroutine(FadeTextToZeroAlpha(e.NotificationLength, _notificationTextField));
		}

		private IEnumerator FadeTextToZeroAlpha(float t, Text i)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
			while (i.color.a > 0.0f)
			{
				i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
				yield return null;
			}
			i.enabled = false;
		}
	}
}
