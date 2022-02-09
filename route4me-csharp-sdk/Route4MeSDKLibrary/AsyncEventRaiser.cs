using System;
using System.Threading.Tasks;

namespace Route4MeSDKLibrary
{
    public static class AsyncEventRaiser
    {
        public static async Task SafeRaiseSequentiallyAsync<T>(Func<T, Task> @event, T arg)
        {
            var localEvent = @event;
            if (localEvent != null)
            {
                Delegate[] invocationList = localEvent.GetInvocationList();
                foreach (var @delegate in invocationList)
                {
                    var task = ((Func<T, Task>)@delegate)(arg);
                    await task.ConfigureAwait(false);
                }
            }
        }
    }
}