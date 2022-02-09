using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.EngineIoClientDotNet.Client;
using Quobject.SocketIoClientDotNet.Client;
using System.Collections.Immutable;

namespace Route4MeSDK.FastProcessing
{
    public class Options : Transport.Options
    {
        public bool AutoConnect = true;
        public bool ForceNew = true;

        public string Host;
        public bool Multiplex = true;
        public string QueryString;

        public bool Reconnection = true;
        public int ReconnectionAttempts;
        public long ReconnectionDelay;
        public long ReconnectionDelayMax;
        public bool RememberUpgrade;
        public long Timeout = -1;
        public ImmutableList<string> Transports;
        public bool Upgrade;
    }
}