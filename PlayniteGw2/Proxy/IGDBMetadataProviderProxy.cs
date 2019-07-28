using System;
using System.Diagnostics.CodeAnalysis;
using Playnite.SDK;
using Playnite.SDK.Metadata;
using Playnite.SDK.Models;

namespace PlayniteGw2.Proxy
{
    [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Playnite specific")]
    internal class IGDBMetadataProviderProxy : LibraryMetadataProvider
    {
        private const string TYPE_NAME = "Playnite.Metadata.Providers.IGDBMetadataProvider, Playnite";
        private static readonly Type typeObject = Type.GetType(TYPE_NAME);

        private readonly LibraryMetadataProvider metadataProvider;
        private readonly string? gameName;

        public IGDBMetadataProviderProxy(string? gameName = null)
        {
            this.metadataProvider = (LibraryMetadataProvider)Activator.CreateInstance(typeObject);
            this.gameName = gameName;
        }

        public override GameMetadata GetMetadata(Game game) =>
            this.metadataProvider.GetMetadata(!string.IsNullOrWhiteSpace(this.gameName) ? new Game(this.gameName) : game);
    }
}
