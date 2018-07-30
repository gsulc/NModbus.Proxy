namespace NModbus.Proxy
{
    public class ProxyInputRegisters : IPointSource<ushort>
    {
        private readonly IModbusMaster _master;
        private readonly byte _unitId;

        public ProxyInputRegisters(IModbusMaster master, byte unitId)
        {
            _master = master;
            _unitId = unitId;
        }

        public ushort[] ReadPoints(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadHoldingRegisters(_unitId, startAddress, numberOfPoints);
        }

        public void WritePoints(ushort startAddress, ushort[] points)
        {
            throw new InvalidModbusRequestException("Input Registers are read-only.", SlaveExceptionCodes.IllegalFunction);
        }
    }
}
