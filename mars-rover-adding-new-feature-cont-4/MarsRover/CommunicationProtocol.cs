using System.Collections.Generic;
using System.Linq;
using MarsRover.communicationProtocols;

namespace MarsRover;

public abstract class CommunicationProtocol
{
    private readonly CommandExtractor _commandExtractor;
    private readonly CommandMapper _commandMapper;
    
    protected CommunicationProtocol(CommandExtractor commandExtractor, CommandMapper commandMapper)
    {
        _commandExtractor = commandExtractor;
        _commandMapper = commandMapper;
    }

    public virtual List<Command> CreateCommands(string commandsSequence, int displacement)
    {
        var commandRepresentations = _commandExtractor.Extract(commandsSequence);
        return commandRepresentations
            .Select(commandRepresentation => _commandMapper.CreateCommand(displacement, commandRepresentation))
            .ToList();
    }
}