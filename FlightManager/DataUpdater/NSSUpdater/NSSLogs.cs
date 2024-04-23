namespace FlightManager.DataUpdater.NSSUpdater;

using NSS = NetworkSourceSimulator;
public static class NSSLogs
{
    public static string ObjectNotFound(ulong ID) => $"Couldn't find object with ID: {ID}";

    public static string InvalidOperation(ulong ID, string operation) => $"Operation {operation} is not valid for object with ID {ID}";

    public static string SuccessfulPositionUpdate(ulong ID, NSS.PositionUpdateArgs args) =>
        $"Successfully updated position of object with ID {ID} to ({args.Longitude}, {args.Latitude}, {args.AMSL})";

    public static string SuccessfulContactInfoUpdate(ulong ID, NSS.ContactInfoUpdateArgs args) =>
        $"Successfully updated contact info of object with ID {ID} to ({args.EmailAddress}, {args.PhoneNumber})";

    public static string SuccesfulIDUpdate(ulong ID, NSS.IDUpdateArgs args) =>
        $"Successfully updated ID of object with ID {ID} to ({args.NewObjectID})";

    public static string IDAlreadyUsed(ulong ID) => $"ID {ID} is already used";
}