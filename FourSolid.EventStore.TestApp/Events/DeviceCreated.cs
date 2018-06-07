using System;

namespace FourSolid.EventStore.TestApp.Events
{
    public class DeviceCreated
    {
        public readonly Guid DeviceId;
        public readonly string DeviceName;
        public readonly string SerialNumber;
        public readonly DateTime LastCommunication;

        public DeviceCreated(Guid deviceId, string deviceName, string serialNumber, DateTime lastCommunication)
        {
            this.DeviceId = deviceId;
            this.DeviceName = deviceName;
            this.SerialNumber = serialNumber;
            this.LastCommunication = lastCommunication;
        }
    }

    public class SerialNumberUpdated
    {
        public readonly Guid DeviceId;
        public readonly string SerialNumber;

        public SerialNumberUpdated(Guid deviceId, string serialNumber)
        {
            this.DeviceId = deviceId;
            this.SerialNumber = serialNumber;
        }
    }
}