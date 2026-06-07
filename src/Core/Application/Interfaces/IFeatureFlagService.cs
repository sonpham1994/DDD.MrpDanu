namespace Application.Interfaces;

public interface IFeatureFlagService
{ 
    Task<bool> IsGcMeasurementEnabledAsync();
}