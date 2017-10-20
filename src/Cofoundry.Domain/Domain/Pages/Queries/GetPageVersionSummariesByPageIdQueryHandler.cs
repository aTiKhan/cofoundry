﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain
{
    public class GetPageVersionSummariesByPageIdQueryHandler
        : IAsyncQueryHandler<GetPageVersionSummariesByPageIdQuery, IEnumerable<PageVersionSummary>>
        , IPermissionRestrictedQueryHandler<GetPageVersionSummariesByPageIdQuery, IEnumerable<PageVersionSummary>>
    {
        #region constructor

        private readonly CofoundryDbContext _dbContext;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IPageVersionSummaryMapper _pageVersionSummaryMapper;

        public GetPageVersionSummariesByPageIdQueryHandler(
            CofoundryDbContext dbContext,
            IQueryExecutor queryExecutor,
            IPageVersionSummaryMapper pageVersionSummaryMapper
            )
        {
            _dbContext = dbContext;
            _queryExecutor = queryExecutor;
            _pageVersionSummaryMapper = pageVersionSummaryMapper;
        }

        #endregion

        #region execution
        
        public async Task<IEnumerable<PageVersionSummary>> ExecuteAsync(GetPageVersionSummariesByPageIdQuery query, IExecutionContext executionContext)
        {
            var dbVersions = await Query(query.PageId).ToListAsync();
            var versions = dbVersions
                .Select(_pageVersionSummaryMapper.Map)
                .ToList();

            return versions;
        }

        private IQueryable<PageVersion> Query(int id)
        {
            return _dbContext
                .PageVersions
                .AsNoTracking()
                .Include(v => v.Creator)
                .Include(v => v.PageTemplate)
                .Include(v => v.OpenGraphImageAsset)
                .Where(v => v.PageId == id && !v.IsDeleted)
                .OrderByDescending(v => v.WorkFlowStatusId == (int)WorkFlowStatus.Draft)
                .ThenByDescending(v => v.WorkFlowStatusId == (int)WorkFlowStatus.Published)
                .ThenByDescending(v => v.CreateDate);
        }

        #endregion

        #region Permission

        public IEnumerable<IPermissionApplication> GetPermissions(GetPageVersionSummariesByPageIdQuery query)
        {
            yield return new PageReadPermission();
        }

        #endregion
    }
}
