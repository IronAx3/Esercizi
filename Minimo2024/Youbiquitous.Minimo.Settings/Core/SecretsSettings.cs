///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Minimo.Settings.Storage;

namespace Youbiquitous.Minimo.Settings.Core;

public class SecretsSettings
{

    public SecretsSettings()
    {
        MinimoDatabase = new StorageDescriptor();
        MinimoBlob = new StorageDescriptor();
    }
    public string BlobStorageConnectionString { get; set; }
    /// <summary>
    /// Selector of the application database
    /// </summary>
    public StorageDescriptor MinimoDatabase { get; set; }

    /// <summary>
    /// Application blob storage (if any)
    /// </summary>
    public StorageDescriptor MinimoBlob { get; set; }

    /// <summary>
    /// Whether connected to the live DB
    /// </summary>
    /// <returns></returns>
    public bool IsConnectedToLiveData()
    {
        return MinimoDatabase.IsLive();
    }
}