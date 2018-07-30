namespace NModbus.Proxy
{
    class ProxyCoilDiscretes : IPointSource<bool>
    {
        private readonly IModbusMaster _master;
        private readonly byte _unitId;

        public ProxyCoilDiscretes(IModbusMaster master, byte unitId)
        {
            _master = master;
            _unitId = unitId;
        }

        public bool[] ReadPoints(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadCoils(_unitId, startAddress, numberOfPoints);
        }

        public void WritePoints(ushort startAddress, bool[] points)
        {
            _master.WriteMultipleCoils(_unitId, startAddress, points);
        }
    }
}
