using AttendanceCheck.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
namespace AttendanceCheck.Common
{
    public class BluetoothConnection
    {
        public static RfcommDeviceService service;
        public static StreamSocket _socket;
        public static DataWriter m_DataWriter;
        public static DataReader m_DataReader;

        public static async void ConnectToDevice()
        {
            var BTDevices = await DeviceInformation.FindAllAsync(
               RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));

            if (BTDevices.Count > 0)
            {
                foreach (var device in BTDevices)
                {
                    if (device.Name == gloablvalue.DeviceName)
                    {
                        service = await RfcommDeviceService.FromIdAsync(device.Id);
                        if (service != null)
                        {
                            try
                            {
                                _socket = new StreamSocket();
                                await _socket.ConnectAsync(
                                    service.ConnectionHostName,
                                    service.ConnectionServiceName);
                                m_DataReader = new DataReader(_socket.InputStream);
                                m_DataWriter = new DataWriter(_socket.OutputStream);
                                gloablvalue.DeviceState = "1";
                                Listen();
                            }
                            catch (Exception)
                            {
                                gloablvalue.DeviceState = "-2";
                            }
                        }
                        else if(service == null)
                        {
                            gloablvalue.DeviceState = "0";
                        }
                    }
                    else
                    {
                        gloablvalue.DeviceState = "2";
                    }
                }
            }
            else
            {
                gloablvalue.DeviceState = "3";
            }
        }

        /// <summary>
        /// receive strings
        /// </summary>
        public static async void Listen()
        {
            try
            {
                if (_socket != null)
                {
                    m_DataReader = new DataReader(_socket.InputStream);
                    CancellationTokenSource ReadCancellationTokenSource = new CancellationTokenSource();

                    // keep reading the serial input
                    while (true)
                    {
                        await ReadAsync(ReadCancellationTokenSource.Token);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
            }
        }

        public static async Task ReadAsync(CancellationToken cancellationToken)
        {
            Task<UInt32> loadAsyncTask;

            uint ReadBufferLength = 1024;

            // If task cancellation was requested, comply
            cancellationToken.ThrowIfCancellationRequested();

            // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
            m_DataReader.InputStreamOptions = InputStreamOptions.Partial;

            // Create a task object to wait for data on the serialPort.InputStream
            loadAsyncTask = m_DataReader.LoadAsync(ReadBufferLength).AsTask(cancellationToken);

            // Launch the task and wait
            UInt32 bytesRead = await loadAsyncTask;
            if (bytesRead > 0)
            {
                gloablvalue.RecivedText = m_DataReader.ReadString(bytesRead);
                gloablvalue.Status = "bytes read successfully!";
            }
            else
            {
                gloablvalue.RecivedText = "Not recived eny thing";
                gloablvalue.Status = "unsuccessfully bytes read";
            }
        }

        /// <summary>
        /// send commands
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        //public static async Task<uint> SendCommand(string command)
        //{
        //    uint sentCommandSize = 0;
        //    if (m_DataWriter != null)
        //    {
        //        uint commandSize = m_DataWriter.MeasureString(command);
        //        m_DataWriter.WriteByte((byte)commandSize);
        //        sentCommandSize = m_DataWriter.WriteString(command);
        //        await m_DataWriter.StoreAsync();
        //        Listen();
        //    }
        //    return sentCommandSize;
        //}

        public static Task<uint> SendCommand(string command)
        {
            uint sentCommandSize = 0;
            if (m_DataWriter != null)
            {
                uint commandSize = m_DataWriter.MeasureString(command);
                m_DataWriter.WriteByte((byte)commandSize);
                sentCommandSize = m_DataWriter.WriteString(command);
                return m_DataWriter.StoreAsync().AsTask<uint>();
            }

            throw new Exception("No connection found");
        }

        public static void CloseDevice()
        {
            if (_socket != null)
            {
                try
                {
                    _socket.Dispose();
                }
                catch (Exception)
                {
                    gloablvalue.DeviceState = "-2";
                }
            }
        }
    }
}
