using Unity.Entities;

struct GlobalCooldownEffect : ICooldown, IComponentData {
    const float Duration = 2;
    public Entity Caster { get; set; }

    public EGameplayEffect GameplayEffect => EGameplayEffect.GlobalCooldown;
    public void ApplyCooldownEffect(int index, EntityCommandBuffer.Concurrent Ecb, float WorldTime) {
        var attributeModData = new AttributeModificationComponent()
        {
            Add = 0,
            Multiply = 0,
            Divide = 0,
            Change = 0,
            Source = Caster,
            Target = Caster
        };

        var attributeModEntity = Ecb.CreateEntity(index);
        var gameplayEffectData = new GameplayEffectDurationComponent()
        {
            WorldStartTime = WorldTime,
            Duration = Duration,
            Effect = EGameplayEffect.GlobalCooldown
        };
        var cooldownEffectComponent = new CooldownEffectComponent()
        {
            Caster = Caster
        };

        Ecb.AddComponent(index, attributeModEntity, new NullAttributeModifier());
        Ecb.AddComponent(index, attributeModEntity, new TemporaryAttributeModification());
        Ecb.AddComponent(index, attributeModEntity, gameplayEffectData);
        Ecb.AddComponent(index, attributeModEntity, attributeModData);
        Ecb.AddComponent(index, attributeModEntity, cooldownEffectComponent);
        Ecb.AddComponent(index, attributeModEntity, new GlobalCooldownTagComponent());
    }
}

public struct GlobalCooldownTagComponent : IComponentData { }
