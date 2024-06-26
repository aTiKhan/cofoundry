﻿namespace Cofoundry.Core;

public static class IEnumerableExtensions
{
    /// <summary>
    /// Removes nullable entries from the sequence
    /// </summary>
    [Obsolete("Use WhereNotNull instead.")]
    public static IEnumerable<T> FilterNotNull<T>(this IEnumerable<Nullable<T>> source)
        where T : struct
    {
        return source
            .Where(s => s.HasValue)
            .Select(s => s!.Value);
    }

    /// <summary>
    /// Removes nullable entries from the sequence.
    /// </summary>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<Nullable<T>> source)
        where T : struct
    {
        return source
            .Where(s => s != null)
            .Cast<T>();
    }

    /// <summary>
    /// Removes nullable entries from the sequence.
    /// </summary>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
    {
        return source
            .Where(s => s != null)
            .Cast<T>();
    }

    /// <summary>
    /// Removes nullable null or empty strings from the sequence.
    /// </summary>
    public static IEnumerable<string> WhereNotNullOrEmpty(this IEnumerable<string?> source)
    {
        return source
            .Where(s => !string.IsNullOrEmpty(s))
            .Cast<string>();
    }

    /// <summary>
    /// Removes nullable null, empty or strings that contain only whitespace 
    /// from the sequence.
    /// </summary>
    public static IEnumerable<string> WhereNotNullOrWhitespace(this IEnumerable<string?> source)
    {
        return source
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Cast<string>();
    }
}
