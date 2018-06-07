using System;
using FourSolid.EventStore.TestApp.Abstracts;
using FourSolid.EventStore.TestApp.Events;

namespace FourSolid.EventStore.TestApp.Entities
{
    public class Device : AggregateRoot
    {
        private string _deviceName;
        private string _serialNumber;
        private DateTime _lastCommunicationDate;

        protected Device()
        { }

        internal static Device CreateDevice(Guid deviceId, string deviceName, string serialNumber,
            DateTime lastCommunication)
        {
            return new Device(deviceId, deviceName, serialNumber, lastCommunication);
        }

        private Device(Guid deviceId, string deviceName, string serialNumber, DateTime lastCommunication)
        {
            this.RaiseEvent(new DeviceCreated(deviceId, deviceName, serialNumber, lastCommunication));
        }

        private void Apply(DeviceCreated @event)
        {
            this.Id = @event.DeviceId;
            this._deviceName = @event.DeviceName;
            this._serialNumber = @event.SerialNumber;
            this._lastCommunicationDate = @event.LastCommunication;
        }

        internal void UpdateSerialNumber(string serialNumber)
        {
            this.RaiseEvent(new SerialNumberUpdated(this.Id, serialNumber));
        }

        private void Apply(SerialNumberUpdated @event)
        {
            this._serialNumber = @event.SerialNumber;
        }

        internal string GetSerialNumber()
        {
            return this._serialNumber;
        }
    }
}