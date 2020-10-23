namespace StarWars.Characters.Domain.Responses
{
    public enum CharacterModuleResponseStatus
    {
        SUCCESS, NOTFOUND, DUPLICATE, SELFFRIEND, ERROR
    }

    public class CharacterModuleResponse
    {
        public CharacterModuleResponseStatus Status { get; }

        protected CharacterModuleResponse(CharacterModuleResponseStatus status)
        {
            Status = status;
        }

        public static CharacterModuleResponse Success => new CharacterModuleResponse(CharacterModuleResponseStatus.SUCCESS); 
        public static CharacterModuleResponse NotFound => new CharacterModuleResponse(CharacterModuleResponseStatus.NOTFOUND);
        public static CharacterModuleResponse Duplicate => new CharacterModuleResponse(CharacterModuleResponseStatus.DUPLICATE);
        public static CharacterModuleResponse SelfFriend => new CharacterModuleResponse(CharacterModuleResponseStatus.SELFFRIEND);
        public static CharacterModuleResponse Error => new CharacterModuleResponse(CharacterModuleResponseStatus.ERROR);
    }

    public class CharacterModuleResponse<T> : CharacterModuleResponse
    {
        public T Data { get; }

        protected CharacterModuleResponse(CharacterModuleResponseStatus status, T data) : base(status)
        {   
            Data = data;
        }

        public static new CharacterModuleResponse<T> Success(T data) => new CharacterModuleResponse<T>(CharacterModuleResponseStatus.SUCCESS, data);
    }
}