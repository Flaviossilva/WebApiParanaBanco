namespace WebApiParanaBanco.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public List<Telefone> Telefones { get; set; }

       
    }

    public class Telefone
    {
        public int  DDD { get; set; }
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Tipo { get; set; } // fixo ou celular
    }


}

