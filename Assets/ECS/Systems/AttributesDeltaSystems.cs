﻿using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

public class ResetAttributesDeltaSystem : JobComponentSystem {

    [BurstCompile]
    public struct Job : IJobForEach<AttributesComponent> {
        public void Execute(ref AttributesComponent attributesComponent) {
            attributesComponent.MaxHealth.TempDelta = 0;
            attributesComponent.Health.TempDelta = 0;
            attributesComponent.Mana.TempDelta = 0;
            attributesComponent.MaxMana.TempDelta = 0;
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDependencies) {
        var job = new Job();
        var jobHandle = job.Schedule(this, inputDependencies);
        return jobHandle;
    }
}


[UpdateAfter(typeof(ResetAttributesDeltaSystem))]
public class ApplyAttributesDeltaSystem : JobComponentSystem {

    [BurstCompile]
    public struct Job : IJobForEach<AttributesComponent> {
        public void Execute(ref AttributesComponent attributesComponent) {
            attributesComponent.MaxHealth.CurrentValue = attributesComponent.MaxHealth.BaseValue + attributesComponent.MaxHealth.TempDelta;
            attributesComponent.Health.CurrentValue = attributesComponent.Health.BaseValue + attributesComponent.Health.TempDelta;
            attributesComponent.Mana.CurrentValue = attributesComponent.Mana.BaseValue + attributesComponent.Mana.TempDelta;
            attributesComponent.MaxMana.CurrentValue = attributesComponent.MaxMana.BaseValue + attributesComponent.MaxMana.TempDelta;
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDependencies) {
        var job = new Job();
        var jobHandle = job.Schedule(this, inputDependencies);
        return jobHandle;
    }
}

public class RemovePermanentAttributeModificationTag : JobComponentSystem {

    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    protected override void OnCreate() {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        base.OnCreate();
    }

    [BurstCompile]
    public struct Job : IJobForEachWithEntity<PermanentAttributeModification> {
        public EntityCommandBuffer.Concurrent Ecb { get; set; }

        public void Execute(Entity entity, int index, [ReadOnly] ref PermanentAttributeModification _) {
            Ecb.DestroyEntity(index, entity);
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDependencies) {
        var job = new Job()
        {
            Ecb = m_EntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
        };

        var jobHandle = job.Schedule(this, inputDependencies);
        m_EntityCommandBufferSystem.AddJobHandleForProducer(jobHandle);
        return jobHandle;
    }
}