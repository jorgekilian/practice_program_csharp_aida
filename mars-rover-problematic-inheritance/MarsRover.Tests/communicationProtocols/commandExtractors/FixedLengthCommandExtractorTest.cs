using System.Collections.Generic;
using MarsRover.communicationProtocols.commandExtractors;
using NUnit.Framework;

namespace MarsRover.Tests.communicationProtocols.commandExtractors;

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

    private static FixedLengthCommandExtractor CreateCommandExtractorForCommandsOfSize(uint size)
    {
        return new FixedLengthCommandExtractor(size);
    }
}