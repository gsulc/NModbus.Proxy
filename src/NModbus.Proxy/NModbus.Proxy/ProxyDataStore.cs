namespace NModbus.Proxy
{
    public class ProxyDataStore : ISlaveDataStore
    {
        private readonly byte _unitId;
        private readonly IModbusMaster _master;
        private IPointSource<bool> _coilDiscretes;
        private IPointSource<bool> _coilInputs;
        private IPointSource<ushort> _holdingRegisters;
        private IPointSource<ushort> _inputRegisters;

        public ProxyDataStore(byte unitId, IModbusMaster master)
        {
            _unitId = unitId;
            _master = master;
        }

        public IPointSource<bool> CoilDiscretes => 
            _coilDiscretes ?? (_coilDiscretes = new ProxyCoilDiscretes(_master, _unitId));

        public IPointSource<bool> CoilInputs => 
            _coilInputs ?? (_coilInputs = new ProxyCoilInputs(_master, _unitId));

        public IPointSource<ushort> HoldingRegisters => 
            _holdingRegisters ?? (_holdingRegisters = new ProxyHoldingRegisters(_master, _unitId));

        public IPointSource<ushort> InputRegisters => 
            _inputRegisters ?? (_inputRegisters = new ProxyInputRegisters(_master, _unitId));
    }
}
