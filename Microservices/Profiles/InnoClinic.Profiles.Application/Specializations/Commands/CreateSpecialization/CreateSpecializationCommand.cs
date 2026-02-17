using Wolverine;

namespace InnoClinic.Profiles.Application.Specializations.Commands.CreateSpecialization;

public record CreateSpecializationCommand(string Name) : ICommand;
