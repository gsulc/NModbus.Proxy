namespace NModbus.Proxy
{
    public class ProxyHoldingRegisters : IPointSource<ushort>
    {
        private readonly IModbusMaster _master;
        private readonly byte _unitId;

        public ProxyHoldingRegisters(IModbusMaster master, byte unitId)
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
            _master.WriteMultipleRegisters(_unitId, startAddress, points);
        }
    }
}
