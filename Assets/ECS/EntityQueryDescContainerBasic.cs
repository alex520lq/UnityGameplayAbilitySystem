using Unity.Entities;

public struct EntityQueryDescContainerBasic<T0> : IEntityQueryDescContainer {
    public EntityQueryDesc BeginAbilityCastJobQueryDesc => new EntityQueryDesc
    {
        All = new[] { ComponentType.ReadOnly<AbilityStateComponent>(), ComponentType.ReadOnly<AbilitySourceTargetComponent>(), ComponentType.ReadOnly<T0>() },
    };

    public EntityQueryDesc UpdateCooldownsJobQueryDesc => new EntityQueryDesc
    {
        All = new[] { ComponentType.ReadOnly<GrantedAbilityComponent>(), ComponentType.ReadWrite<GrantedAbilityCooldownComponent>(), ComponentType.ReadOnly<T0>() },
    };

    public EntityQueryDesc CheckAbilityAvailableJobQueryDesc_UpdateAvailability => new EntityQueryDesc
    {
        All = new[] { ComponentType.ReadWrite<AbilityStateComponent>(), ComponentType.ReadOnly<AbilitySourceTargetComponent>(), ComponentType.ReadOnly<T0>() },
    };

    public EntityQueryDesc CheckAbilityAvailableJobQueryDesc_CheckResources => new EntityQueryDesc
    {
        All = new[] { ComponentType.ReadWrite<AbilityStateComponent>(), ComponentType.ReadOnly<AbilitySourceTargetComponent>(), ComponentType.ReadOnly<T0>() },
    };

    public EntityQueryDesc CheckAbilityGrantedJobQueryDesc => new EntityQueryDesc
    {
        All = new[] { ComponentType.ReadOnly<T0>() },
    };

}
