﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;

namespace MyAppService
{
  public sealed class Inventory : IBackgroundTask
  {
    private BackgroundTaskDeferral backgroundTaskDeferral;
    private AppServiceConnection appServiceconnection;
    private String[] inventoryItems = new string[] { "Robot vacuum", "Chair" };
    private double[] inventoryPrices = new double[] { 129.99, 88.99 };

    public void Run(IBackgroundTaskInstance taskInstance)
    {
      // Get a deferral so that the service isn't terminated.
      this.backgroundTaskDeferral = taskInstance.GetDeferral();

      // Associate a cancellation handler with the background task.
      taskInstance.Canceled += OnTaskCanceled;

      // Retrieve the app service connection and set up a listener for incoming app service requests.
      var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
      appServiceconnection = details.AppServiceConnection;
      appServiceconnection.RequestReceived += OnRequestReceived;
    }

    private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
    {
      // This function is called when the app service receives a request.
    }

    private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
    {
      if (this.backgroundTaskDeferral != null)
      {
        // Complete the service deferral.
        this.backgroundTaskDeferral.Complete();
      }
    }
  }
}
