using Cadmus.Core.Config;
using Cadmus.Seed;
using Cadmus.Seed.Codicology.Parts;
using Cadmus.Seed.General.Parts;
using Cadmus.Seed.Ndp.Parts;
using Cadmus.Seed.NdpBooks.Parts;
using Cadmus.Seed.Philology.Parts;
using Fusi.Microsoft.Extensions.Configuration.InMemoryJson;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace CadmusVpiApi.Services;

/// <summary>
/// Application's part seeders factory provider. Usually, this is implemented
/// in your project's Services library. Here we have no specific project, so we
/// just provide an API app service here.
/// </summary>
public sealed class AppPartSeederFactoryProvider : IPartSeederFactoryProvider
{
    private static IHost GetHost(string config)
    {
        // build the tags to types map for parts/fragments
        Assembly[] seedAssemblies =
        [
            // Cadmus.General.Seed.Parts
            typeof(NotePartSeeder).Assembly,
            // Cadmus.Seed.Philology.Parts
            typeof(ApparatusLayerFragmentSeeder).Assembly,
            // Cadmus.Seed.Codicology.Parts
            typeof(CodShelfmarksPartSeeder).GetTypeInfo().Assembly,
            // Cadmus.Seed.Ndp.Parts
            typeof(TextPassagesPartSeeder).GetTypeInfo().Assembly,
            // Cadmus.Seed.NdpBooks.Parts
            typeof(PrintFontsPartSeeder).GetTypeInfo().Assembly,
        ];
        TagAttributeToTypeMap map = new();
        map.Add(seedAssemblies);

        return new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                PartSeederFactory.ConfigureServices(services,
                    new StandardPartTypeProvider(map),
                    seedAssemblies);
            })
            // extension method from Fusi library
            .AddInMemoryJson(config)
            .Build();
    }

    /// <summary>
    /// Gets the part/fragment seeders factory.
    /// </summary>
    /// <param name="profile">The profile.</param>
    /// <returns>Factory.</returns>
    /// <exception cref="ArgumentNullException">profile</exception>
    public PartSeederFactory GetFactory(string profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

        return new PartSeederFactory(GetHost(profile));
    }
}
