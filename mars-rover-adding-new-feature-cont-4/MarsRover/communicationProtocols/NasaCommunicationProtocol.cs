using MarsRover.commands;
using MarsRover.communicationProtocols.commandExtractor;

namespace MarsRover.communicationProtocols;

public class NasaCommunicationProtocol : CommunicationProtocol
{
    public NasaCommunicationProtocol() : base(new FixedLengthCommandExtractor(1), new NasaCommandMapper())
    {
    }

    protected override Command CreateCommand(int displacement, string commandRepresentation)
    {
        return commandRepresentation switch
        {
            "l" => new RotationLeft(),
            "r" => new RotationRight(),
            "f" => new MovementForward(displacement),
            _ => new MovementBackward(displacement)
        };
    }
}

public class NasaCommandMapper : CommandMapper
{
    public Command CreateCommand(int displacement, string commandRepresentation)
    {
        return commandRepresentation switch
        {
            "l" => new RotationLeft(),
            "r" => new RotationRight(),
            "f" => new MovementForward(displacement),
            _ => new MovementBackward(displacement)
        };
    }
}