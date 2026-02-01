using Cattobot.Db.Models;

namespace Cattobot.Services.Abstractions;

public interface ITrackRepository
{
    Task<Guid> Add(TrackDb trackDb, CancellationToken ct = default);
    
    Task<List<Guid>> AddRange(List<TrackDb> trackDbs, CancellationToken ct = default);
}