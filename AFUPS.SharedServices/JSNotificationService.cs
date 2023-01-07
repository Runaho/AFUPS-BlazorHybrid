using System;
using System.Drawing;
using Microsoft.JSInterop;

namespace AFUPS.SharedServices
{
    // Must be added bootstrap notify .js
	public class JSNotificationService
	{
		public JSNotificationService(IJSRuntime jSRuntime)
		{
            JSRuntime = jSRuntime;
        }

        public IJSRuntime JSRuntime { get; }

        //SendNotification(from, align, icon, color, message,timer)
        /// <summary>
        /// Javascript Notification Sender blazor-custom-events : window.SendNotification
        /// </summary>
        /// <param name="from">top/bottom</param>
        /// <param name="align">left,right,center</param>
        /// <param name="icon">Nucleo Icon</param>
        /// <param name="color">bootstrap color : primary, info, success, warning, danger</param>
        /// <param name="message">html Message</param>
        /// <param name="timer">How long appeare ms </param>
        public async void SendNotification(string from, string align, string icon, string color, string message, int timer)
        {
            await JSRuntime.InvokeVoidAsync("SendNotification", from, align, icon, color, message, timer);
        }

        public async void ClearNotifications()
        {
            await JSRuntime.InvokeVoidAsync("ClearNotifications");
        }
    }
}

