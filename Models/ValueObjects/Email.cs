using System.Text.RegularExpressions;

namespace SkillQuest.Api.Models.ValueObjects
{
    public record Email
    {
        public string Address { get; }

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("O email não pode estar vazio.", nameof(address));

            if (!Regex.IsMatch(address, @"^(.+)@(.+)$"))
                throw new ArgumentException("Formato de email inválido.", nameof(address));

            Address = address;
        }

        public static implicit operator string(Email email) => email.Address;

        public static explicit operator Email(string address) => new(address);
    }
}