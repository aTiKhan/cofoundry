namespace Cofoundry.Domain;

public class GetCustomEntityVersionPageBlockEntityMicroSummariesByIdRangeQuery : IQuery<IReadOnlyDictionary<int, RootEntityMicroSummary>>
{
    public GetCustomEntityVersionPageBlockEntityMicroSummariesByIdRangeQuery()
    {
        CustomEntityVersionPageBlockIds = new List<int>();
    }

    public GetCustomEntityVersionPageBlockEntityMicroSummariesByIdRangeQuery(IEnumerable<int>? ids)
        : this(ids?.ToArray() ?? [])
    {
    }

    public GetCustomEntityVersionPageBlockEntityMicroSummariesByIdRangeQuery(IReadOnlyCollection<int> ids)
    {
        ArgumentNullException.ThrowIfNull(ids);

        CustomEntityVersionPageBlockIds = ids;
    }

    [Required]
    public IReadOnlyCollection<int> CustomEntityVersionPageBlockIds { get; set; }
}
