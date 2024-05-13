using MarsRover.communicationProtocols;
using MarsRover.communicationProtocols.commandExtractor;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarsRover.Tests;

[TestFixture]
public class FixedLengthCommandExtractorTest
{
    [Test]
    public void extract_commands_with_length_one()
    {
        var commandExtractor = CreateCommandExtractorForCommandsOfSize(1);

        var result = commandExtractor.Extract("abc");

        Assert.That(result, Is.EqualTo(new List<string> { "a", "b", "c" }));
    }

    [Test]
    public void extract_commands_with_length_two()
    {
        var commandExtractor = CreateCommandExtractorForCommandsOfSize(2);

        var result = commandExtractor.Extract("abc");

        Assert.That(result, Is.EqualTo(new List<string> { "ab", "c" }));
    }

    private static CommandExtractor CreateCommandExtractorForCommandsOfSize(uint size)
    {
        return new FixedLengthCommandExtractor(size);
    }
}