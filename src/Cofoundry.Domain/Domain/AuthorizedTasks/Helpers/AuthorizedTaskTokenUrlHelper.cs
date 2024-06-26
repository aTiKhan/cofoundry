﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Cofoundry.Domain.Internal;

/// <inheritdoc/>
public class AuthorizedTaskTokenUrlHelper : IAuthorizedTaskTokenUrlHelper
{
    private const string TOKEN_PARAM_NAME = "t";

    public string MakeUrl(
        string baseUrl,
        string token
        )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(token);

        if (!Uri.IsWellFormedUriString(baseUrl, UriKind.RelativeOrAbsolute))
        {
            throw new ArgumentException("Url is not a well formatted url: " + baseUrl, nameof(baseUrl));
        }

        var queryParams = new Dictionary<string, string?>()
        {
            { TOKEN_PARAM_NAME, token }
        };

        var formattedUri = baseUrl.ToString().TrimEnd('/');

        var url = QueryHelpers.AddQueryString(formattedUri, queryParams);

        return url;
    }

    public string? ParseTokenFromQuery(IQueryCollection queryCollection)
    {
        if (queryCollection.TryGetValue(TOKEN_PARAM_NAME, out var value))
        {
            var result = Uri.UnescapeDataString(value.ToString());

            if (!string.IsNullOrWhiteSpace(result))
            {
                return result;
            }
        }

        return null;
    }
}
