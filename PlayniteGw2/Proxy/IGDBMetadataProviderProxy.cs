using System;
using Playnite.SDK;
using Playnite.SDK.Metadata;
using Playnite.SDK.Models;

namespace PlayniteGw2.Proxy
{
    internal class IGDBMetadataProviderProxy : ILibraryMetadataProvider
    {
        private const string TypeName = "Playnite.Metadata.Providers.IGDBMetadataProvider, Playnite";
        private static readonly Type TypeObject;

        private readonly ILibraryMetadataProvider metadataProvider;

        static IGDBMetadataProviderProxy()
        {
            TypeObject = Type.GetType(TypeName);
            if (TypeObject == null)
                throw new InvalidOperationException($"Type {TypeName} could not be found, cannot proceed.");
        }

        public IGDBMetadataProviderProxy()
        {
            this.metadataProvider = (ILibraryMetadataProvider)Activator.CreateInstance(TypeObject);
        }

        public virtual GameMetadata GetMetadata(Game game) =>
            this.metadataProvider.GetMetadata(game);
    }
}
