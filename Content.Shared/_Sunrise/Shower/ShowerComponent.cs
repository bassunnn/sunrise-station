using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared._Sunrise.Shower;

[RegisterComponent]
public sealed partial class ShowerComponent : Component
{
    [DataField]
    public string SolutionName = "tank";

    [DataField]
    public float SpillAmount = 10f;
}
