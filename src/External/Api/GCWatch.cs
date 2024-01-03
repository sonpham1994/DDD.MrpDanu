namespace Api;

public class GCWatch
{
    // to know when the Full GC event be triggered. When the Full GC is triggered, it will clean up memory across Generations
    // from Generation 0 to 2. If may be there are a lot of LOH so this is why the full GC is triggered frequently.
    // that's why we need this class to monitor the frequent full GC event.
    public static void Start()
    {
        while (true)
        {
            // Check for a notification of an approaching collection.
            GCNotificationStatus s = GC.WaitForFullGCApproach();
            if (s == GCNotificationStatus.Succeeded)
            {
                Console.WriteLine("Full GC approaching!");
                // Log or take appropriate action
            }

            // Check for a notification of a completed collection.
            s = GC.WaitForFullGCComplete();
            if (s == GCNotificationStatus.Succeeded)
            {
                Console.WriteLine("Full GC completed!");
                // Log or take appropriate action
            }

            Thread.Sleep(500); // Sleep for a while before checking again.
        }
    }
}