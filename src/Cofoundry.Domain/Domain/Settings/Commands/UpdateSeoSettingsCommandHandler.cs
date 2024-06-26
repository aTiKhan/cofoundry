using Cofoundry.Core.Data;
using Cofoundry.Domain.Data;

namespace Cofoundry.Domain.Internal;

public class UpdateSeoSettingsCommandHandler
    : ICommandHandler<UpdateSeoSettingsCommand>
    , IPermissionRestrictedCommandHandler<UpdateSeoSettingsCommand>
{
    private readonly CofoundryDbContext _dbContext;
    private readonly SettingCommandHelper _settingCommandHelper;
    private readonly ISettingCache _settingCache;
    private readonly ITransactionScopeManager _transactionScopeManager;

    public UpdateSeoSettingsCommandHandler(
        CofoundryDbContext dbContext,
        SettingCommandHelper settingCommandHelper,
        ISettingCache settingCache,
        ITransactionScopeManager transactionScopeManager
        )
    {
        _settingCommandHelper = settingCommandHelper;
        _dbContext = dbContext;
        _settingCache = settingCache;
        _transactionScopeManager = transactionScopeManager;
    }

    public async Task ExecuteAsync(UpdateSeoSettingsCommand command, IExecutionContext executionContext)
    {
        var allSettings = await _dbContext
            .Settings
            .ToArrayAsync();

        _settingCommandHelper.SetSettingProperty(command, c => c.HumansTxt, allSettings, executionContext);
        _settingCommandHelper.SetSettingProperty(command, c => c.RobotsTxt, allSettings, executionContext);

        await _dbContext.SaveChangesAsync();

        _transactionScopeManager.QueueCompletionTask(_dbContext, _settingCache.Clear);
    }

    public IEnumerable<IPermissionApplication> GetPermissions(UpdateSeoSettingsCommand command)
    {
        yield return new SeoSettingsUpdatePermission();
    }
}
