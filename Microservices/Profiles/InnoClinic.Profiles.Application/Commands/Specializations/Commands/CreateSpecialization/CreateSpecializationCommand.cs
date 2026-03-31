using Wolverine;

namespace InnoClinic.Profiles.Application.Commands.Specializations.Commands.CreateSpecialization;

public record CreateSpecializationCommand(string Name) : ICommand;
