using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Resources.Scripts
{
    public enum ApplicationStates
    {
        APP_STARTED,
        SCANNING,
        MODEL_TARGET_SCANNED,
        SPAWNING,
        MODEL_SPAWNED
    }

    public enum CoffeeMachineModelStates
    {
        NO_VIEW,
        EXTERNAL_VIEW,
        INNER_VIEW,
        EXPLODED_VIEW
    }

    public enum ModeStates
    {
        NO_ACTIVE_MODE,
        STANDARD_MODE,
        MAINTENANCE_MODE
    }
}
