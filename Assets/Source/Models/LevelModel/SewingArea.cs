using System.Collections.Generic;

public class SewingArea : ObjectModel
{
    public List<SewingMachine> sewingMachines;

    public void SetMachines(LevelSewingMachineData data)
    {
        for (int i = 0; i < sewingMachines.Count; i++)
        {
            sewingMachines[i].Initialize(data.sewingMachineData[i]);
        }
    }
}