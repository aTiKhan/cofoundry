using Cofoundry.Domain.Data;

namespace Cofoundry.Domain.Internal;

/// <summary>
/// Mapper designed to be used internally to map <see cref="CustomEntityRenderSummary"/> objects.
/// </summary>
public interface ICustomEntityRenderSummaryMapper
{
    /// <summary>
    /// Maps an EF CustomEntityVersion record from the db into a <see cref="CustomEntityRenderSummary"/> 
    /// object. If the db record is null then null is returned.
    /// </summary>
    /// <param name="dbResult">CustomEntityVersion record from the database.</param>
    /// <param name="executionContext">Context to run any sub queries under.</param>
    Task<CustomEntityRenderSummary?> MapAsync(CustomEntityVersion? dbResult, IExecutionContext executionContext);

    /// <summary>
    /// Maps a collection of EF CustomEntityVersion record from the db into <see cref="CustomEntityRenderSummary"/> 
    /// objects.
    /// </summary>
    /// <param name="dbResults">CustomEntityVersion record from the database.</param>
    /// <param name="executionContext">Context to run any sub queries under.</param>
    Task<IReadOnlyCollection<CustomEntityRenderSummary>> MapAsync(IReadOnlyCollection<CustomEntityVersion> dbResults, IExecutionContext executionContext);
}
