namespace App.Application.Dtos
{
    public class TokenReadDto
    {
        public string Token { get; set; } = string.Empty;
        public int ExpresInMinutes {get; set;} = 0;
    }
}