using Flight.Aircrafts.Models;
using Mapster;

namespace Flight.Aircrafts.Features;

using CreatingAircraft.V1;
using MassTransit;
using ValueObjects;

public class AircraftMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateAircraftMongo, AircraftReadModel>()
        .Map(d => d.Id, s => NewId.NextGuid())
            .Map(d => d.AircraftId, s => AircraftId.Of(s.Id));

        config.NewConfig<Aircraft, AircraftReadModel>()
            .Map(d => d.Id, s => NewId.NextGuid())
            .Map(d => d.AircraftId, s => AircraftId.Of(s.Id.Value));

        config.NewConfig<CreateAircraftRequestDto, CreatingAircraft.V1.CreateAircraft>()
            .ConstructUsing(x => new CreatingAircraft.V1.CreateAircraft(x.Name, x.Model, x.ManufacturingYear));
    }
}