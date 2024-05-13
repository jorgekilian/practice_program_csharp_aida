using System.Collections.Generic;
using MarsRover.communicationProtocols.commandExtractor;
using static MarsRover.communicationProtocols.SequenceBasedCommunicationProtocol;

namespace MarsRover.communicationProtocols;

public class JointCommunicationProtocol : CommunicationProtocol {
    public virtual List<Command> CreateCommands(string commandsSequence, int displacement) {
        if (commandsSequence == string.Empty) {
            return new List<Command>();
        }
        var protocolIdentifier = commandsSequence.Substring(0, 1);
        var communicationProtocol = IdentifyCommunicationProtocol(protocolIdentifier);

        return communicationProtocol.CreateCommands(commandsSequence.Substring(1), displacement);

    }

    private CommunicationProtocol IdentifyCommunicationProtocol(string protocolIdentifier) {
        if (protocolIdentifier == "*") {
            return EsaCommunicationProtocol();
        }

        if (protocolIdentifier == "%") {
            return CnsaCommunicationProtocol();
        }

        if (protocolIdentifier == "+") {
            return JaxaCommunicationProtocol();
        }

        if (protocolIdentifier == "$") {
            return NasaCommunicationProtocol();
        }

        return new UnknownProtocol();
    }
}