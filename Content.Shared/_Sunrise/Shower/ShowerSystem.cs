using Content.Shared.Interaction;
using Content.Shared.Chemistry.EntitySystems;
using Content.Shared.Fluids;
using Content.Shared.FixedPoint;

namespace Content.Shared._Sunrise.Shower;

public sealed class ShowerSystem : EntitySystem
{
    [Dependency] private readonly SharedSolutionContainerSystem _solutionContainerSystem = default!;
    [Dependency] private readonly SharedPuddleSystem _puddleSystem = default!;
    [Dependency] private readonly SharedAppearanceSystem _appearanceSystem = default!;
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<ShowerComponent, ActivateInWorldEvent>(OnActivate);
    }

    private void OnActivate(EntityUid uid, ShowerComponent component, ref ActivateInWorldEvent args)
    {
        if (!_solutionContainerSystem.TryGetSolution(uid, component.SolutionName, out var tankSolutionEntity, out var tankSolution))
            return;

        FixedPoint2 drainAmount = component.SpillAmount;
        if (tankSolution.Volume <= 0) return;

        var splitSolution = _solutionContainerSystem.SplitSolution(tankSolutionEntity.Value, drainAmount);

        var coordinates = Transform(uid).Coordinates;

        _puddleSystem.TrySpillAt(coordinates, splitSolution, out _);

        _appearanceSystem.SetData(uid, ShowerVisuals.Working, true);

        args.Handled = true;
    }
}
