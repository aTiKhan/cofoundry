namespace Cofoundry.Domain.Internal;

/// <summary>
///  This repository is used for looking up INestedDataModel types. An instance
///  of each is registered with the DI container which is intended to beused for looking
///  up definitions of model properties. This repository checks for duplicate type definitions
///  and will throw an exception on startup if duplicates are defined.
/// </summary>
public class NestedDataModelTypeRepository : INestedDataModelTypeRepository
{
    const string MODEL_SUFFIX = "DataModel";

    private readonly Dictionary<string, INestedDataModel> _nestedDataModels;

    public NestedDataModelTypeRepository(
        IEnumerable<INestedDataModel> allNestedDataModels
        )
    {
        DetectDuplicates(allNestedDataModels);
        _nestedDataModels = allNestedDataModels.ToDictionary(GetDataModelName, StringComparer.OrdinalIgnoreCase);
    }

    private void DetectDuplicates(IEnumerable<INestedDataModel> allNestedDataModels)
    {
        var dulpicateCodes = allNestedDataModels
            .GroupBy(GetDataModelName)
            .Where(g => g.Count() > 1);

        if (dulpicateCodes.Any())
        {
            throw new Exception($"Duplicate INestedDataModel type name detected. {dulpicateCodes.First().Key}. Model names must be unique.");
        }
    }

    private string GetDataModelName(INestedDataModel model)
    {
        var typeName = model.GetType().Name;

        return NormalizeName(typeName);
    }

    /// <summary>
    /// Gets a specific INestedDataModel type by it's name. The "DataModel"
    /// suffix is options e.g. "CarouselItemDataModel" and "CarouselItem"
    /// both match the same type.
    /// </summary>
    /// <param name="name">
    /// The name of the model to get. The "DataModel" suffix is options e.g. 
    /// "CarouselItemDataModel" and "CarouselItem" both match the same type.
    /// </param>
    public Type? GetByName(string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }

        var normalizedName = NormalizeName(name);
        var model = _nestedDataModels.GetValueOrDefault(normalizedName);

        return model?.GetType();
    }

    private static string NormalizeName(string typeName)
    {
        return StringHelper.RemoveSuffix(typeName, MODEL_SUFFIX, StringComparison.OrdinalIgnoreCase);
    }
}
