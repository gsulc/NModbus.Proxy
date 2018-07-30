namespace NModbus.Proxy
{
    public static class FactoryExtensions
    {
        public static IModbusSlave CreateSlaveProxy(
            this IModbusFactory factory, 
            byte unitId, 
            IModbusMaster master)
        {
            var dataStore = new ProxyDataStore(unitId, master);
            return factory.CreateSlave(unitId, dataStore);
        }
    }
}
