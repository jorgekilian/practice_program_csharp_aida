namespace MarsRover.communicationProtocols;

public interface CommandMapper
{
    public Command CreateCommand(int displacement, string commandRepresentation);
}