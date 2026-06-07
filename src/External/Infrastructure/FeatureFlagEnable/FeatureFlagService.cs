using Application.Interfaces;
using Microsoft.FeatureManagement;

namespace Infrastructure.FeatureFlagEnable;

internal sealed class FeatureFlagService(IFeatureManager featureManager) : IFeatureFlagService
{
    public Task<bool> IsGcMeasureEnabledAsync()
    {
        return featureManager.IsEnabledAsync("GcEventListener");
    }
}