using System.Collections.Generic;

namespace MarsRover.communicationProtocols;

internal class UnknownProtocol : CommunicationProtocol {
    public virtual List<Command> CreateCommands(string commandsSequence, int displacement) {
        return new List<Command>();
    }
}