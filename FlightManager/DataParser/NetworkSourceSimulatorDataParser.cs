using FlightManager.Entity;

namespace FlightManager.DataParser;
internal class NetworkSourceSimulatorDataParser : IDataParser<byte[], byte[]>
{
    public (string, byte[]) ParseData(byte[] data)
    {
        const int entityCodeLength = 3;

        using MemoryStream memStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(memStream);

        string entityName = new string(reader.ReadChars(entityCodeLength));
        uint messageLength = reader.ReadUInt32();
        byte[] parameters = new byte[messageLength];
        Array.Copy(data, memStream.Position, parameters, 0, messageLength);
        return (entityName, parameters);
    }

    private static string MessageCodeToEntityIdentifier(string code) => code switch
    {
        "NCR" => EntitiesIdentifiers.CrewID,
        "NPA" => EntitiesIdentifiers.PassengerID,
        "NCA" => EntitiesIdentifiers.CargoID,
        "NCP" => EntitiesIdentifiers.CargoPlaneID,
        "NPP" => EntitiesIdentifiers.PassengerPlaneID,
        "NAI" => EntitiesIdentifiers.AirportID,
        "NFL" => EntitiesIdentifiers.FlightID,
        _ => throw new ArgumentException("Invalid code")
    };

}
