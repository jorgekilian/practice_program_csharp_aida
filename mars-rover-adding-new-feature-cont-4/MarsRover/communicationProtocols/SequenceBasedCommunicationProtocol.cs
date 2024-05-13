using System.Collections.Generic;
using System.Linq;
using MarsRover.communicationProtocols.commandExtractor;
using MarsRover.communicationProtocols.commandMappers;

namespace MarsRover.communicationProtocols;

public class SequenceBasedCommunicationProtocol : CommunicationProtocol
{
    private readonly CommandExtractor _commandExtractor;
    private readonly CommandMapper _commandMapper;

    public SequenceBasedCommunicationProtocol(CommandExtractor commandExtractor, CommandMapper commandMapper)
    {
        _commandExtractor = commandExtractor;
        _commandMapper = commandMapper;
    }

    public List<Command> CreateCommands(string commandsSequence, int displacement)
    {
        var commandRepresentations = _commandExtractor.Extract(commandsSequence);
        return commandRepresentations
            .Select(commandRepresentation => _commandMapper.CreateCommand(displacement, commandRepresentation))
            .ToList();
    }

    public static CommunicationProtocol NasaCommunicationProtocol()
    {
        return new SequenceBasedCommunicationProtocol(new FixedLengthCommandExtractor(1), new NasaCommandMapper());
    }

    public static CommunicationProtocol EsaCommunicationProtocol()
    {
        return new SequenceBasedCommunicationProtocol(new FixedLengthCommandExtractor(1), new EsaCommandMapper());
    }

    public static CommunicationProtocol CnsaCommunicationProtocol()
    {
        return new SequenceBasedCommunicationProtocol(new FixedLengthCommandExtractor(2), new CnsaCommandMapper());
    }

    public static CommunicationProtocol JaxaCommunicationProtocol()
    {
        return new SequenceBasedCommunicationProtocol(new JaxaCommandExtractor(), new JaxaCommandMapper());
    }
}