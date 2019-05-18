﻿using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Extendable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Domain
{
    /// <summary>
    /// IAdvancedContentRespository extension root for the ImageAsset entity.
    /// </summary>
    public interface IAdvancedContentRepositoryImageAssetRepository
    {
        #region queries

        /// <summary>
        /// Retieve an image asset by a unique database id.
        /// </summary>
        IAdvancedContentRepositoryImageAssetByIdQueryBuilder GetById(int imageAssetId);

        /// <summary>
        /// Retieve a set of image assets using a batch of database ids.
        /// The Cofoundry.Core dictionary extensions can be useful for 
        /// ordering the results e.g. results.FilterAndOrderByKeys(ids).
        /// </summary>
        IContentRepositoryImageAssetByIdRangeQueryBuilder GetByIdRange(IEnumerable<int> imageAssetIds);

        /// <summary>
        /// Search for image assets, returning paged lists of data.
        /// </summary>
        IContentRepositoryImageAssetSearchQueryBuilder Search();

        #endregion

        #region commands

        /// <summary>
        /// Adds a new image asset.
        /// </summary>
        Task AddAsync(AddImageAssetCommand command);

        /// <summary>
        /// Updates the properties of an existing image asset. Updating
        /// the file is optional, but if you do then existing links to the
        /// asset file will redirect to the new asset file.
        /// </summary>
        Task UpdateAsync(UpdateImageAssetCommand command);

        /// <summary>
        /// Removes an image asset from the system and
        /// queues any related files or caches to be removed
        /// as a separate process.
        /// </summary>
        /// <param name="imageAssetId">Id of the image asset to delete.</param>
        Task DeleteAsync(int imageAssetId);

        #endregion
    }
}
