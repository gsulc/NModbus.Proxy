namespace NModbus.Proxy
{
    public class ProxyCoilInputs : IPointSource<bool>
    {
        private readonly IModbusMaster _master;
        private readonly byte _unitId;

        public ProxyCoilInputs(IModbusMaster master, byte unitId)
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
            throw new InvalidModbusRequestException(
                "Coil Inputs are read-only.", 
                SlaveExceptionCodes.IllegalFunction);
        }
    }
}
