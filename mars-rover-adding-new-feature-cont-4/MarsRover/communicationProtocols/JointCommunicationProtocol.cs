using System.Collections.Generic;
using static MarsRover.communicationProtocols.SequenceBasedCommunicationProtocol;

namespace MarsRover.communicationProtocols;

public class JointCommunicationProtocol : CommunicationProtocol
{
    private Dictionary<string, CommunicationProtocol> _protocols;

    public JointCommunicationProtocol()
    {
        _protocols = new Dictionary<string, CommunicationProtocol>()
        {
            {"*", EsaCommunicationProtocol() },
            {"%", CnsaCommunicationProtocol() },
            {"+", JaxaCommunicationProtocol() },
            {"$", NasaCommunicationProtocol() }
        };
    }

    public virtual List<Command> CreateCommands(string commandsSequence, int displacement)
    {
        if (commandsSequence == string.Empty)
        {
            return new List<Command>();
        }
        var protocolIdentifier = commandsSequence.Substring(0, 1);
        var communicationProtocol = IdentifyCommunicationProtocol(protocolIdentifier);

        return communicationProtocol.CreateCommands(commandsSequence.Substring(1), displacement);

    }

    private CommunicationProtocol IdentifyCommunicationProtocol(string protocolIdentifier)
    {
        if (_protocols.TryGetValue(protocolIdentifier, out var protocol))
        {
            return protocol;
        }

        return new UnknownProtocol();
    }
}