# NModbus.Proxy
Use a modbus master as a proxy for modbus slaves.

# Use-Case
Once an application opens a SerialPort, no-other application can use it. Existing Modbus applications would not be able to use a remoting, as they don't know about your handle. One way to get around this is to open up a TCP proxy for each slave device on the network. Each slave device interfaces with the serial modbus master.

```csharp
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
```
