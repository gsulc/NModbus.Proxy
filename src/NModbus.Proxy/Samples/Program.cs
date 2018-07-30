using NModbus;
using NModbus.Proxy;
using NModbus.Serial;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 502;
            var address = new IPAddress(new byte[] { 127, 0, 0, 1 });

            var factory = new ModbusFactory();
            using (var serialPort = new SerialPort("COM1"))
            {
                var adapter = new SerialPortAdapter(serialPort);
                var master = factory.CreateRtuMaster(adapter);
                var slaveTcpListener = new TcpListener(address, port);
                slaveTcpListener.Start();

                var tcpNetwork = factory.CreateSlaveNetwork(slaveTcpListener);
                var proxy1 = factory.CreateSlaveProxy(1, master);
                var proxy2 = factory.CreateSlaveProxy(2, master);
                tcpNetwork.AddSlave(proxy1);
                tcpNetwork.AddSlave(proxy2);

                var tcpClient = new TcpClient(address.ToString(), port);
                var secondMaster = factory.CreateMaster(tcpClient);

                ushort[] holdingRegs = secondMaster.ReadHoldingRegisters(1, 1, 2);
            }
        }
    }
}
