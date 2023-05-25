namespace ApiVecttot.DTOs
{
    public class ResponseDTO<TEntity>
    {
        
        public string Error { get; set; } = "";
        public bool Success { get; set; }
        public TEntity Content { get; set; } = default!;
        public static ResponseDTO<TEntity> Ok(TEntity content)
        {
            return new ResponseDTO<TEntity>
            {
                Success = true,
                Content = content
            };
        }
        public static ResponseDTO<TEntity> Failed(string mensaje)
        {
            return new ResponseDTO<TEntity>
            {
                Error = mensaje,
                Success = false
            };
        }
    }
}
