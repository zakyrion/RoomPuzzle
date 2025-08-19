using RoomPuzzle.Features.Input.Model;
using Zenject;

public class InputUpdater : ITickable
{
    private readonly InputModel _inputModel;

    public InputUpdater(InputModel inputModel)
    {
        _inputModel = inputModel;
    }

    public void Tick()
    {
        _inputModel.Update();
    }
}
